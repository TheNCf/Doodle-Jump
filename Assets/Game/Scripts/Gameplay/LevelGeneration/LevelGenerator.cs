using Game.Scripts.Core;
using Game.Scripts.Gameplay.LevelGeneration;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Scripts.Gameplay
{
    public class LevelGenerator : IInitializable
    {
        const float HundredPercent = 100.0f;
        
        private GameBalance _gameBalance;
        private CameraView _cameraView;
        private ObjectShifter _objectShifter;
        private PlatformSpawner _platformSpawner;
        private PlayerCharacterView _playerCharacterView;

        private float _spawnTrigger = -15.0f;
        private float _spawnAdditionalHeight;
        private float _elevationLowering = 0.0f;

        private DifficultyTier _currentDifficulty;

        public LevelGenerator(GameBalance gameBalance, CameraView cameraView, ObjectShifter objectShifter,
            PlatformSpawner platformSpawner, PlayerCharacterView playerCharacterView)
        {
            _gameBalance = gameBalance;
            _cameraView = cameraView;
            _objectShifter = objectShifter;
            _platformSpawner = platformSpawner;
            _playerCharacterView = playerCharacterView;
        }

        public float SpawnAdditionalHeight => _spawnAdditionalHeight;

        public void Initialize()
        {
            _spawnAdditionalHeight = _cameraView.Size.y;

            OnHeightChanged(_objectShifter.RelativeHeight);
            _objectShifter.RelativeHeightChanged += OnHeightChanged;
            _objectShifter.ReturnedBackByValue += OnReturn;
        }

        public void Dispose()
        {
            _objectShifter.RelativeHeightChanged -= OnHeightChanged;
            _objectShifter.ReturnedBackByValue -= OnReturn;
        }

        private void OnHeightChanged(float height)
        {
            while (height > _spawnTrigger)
            {
                _currentDifficulty = _gameBalance.GetTier(_objectShifter.TotalHeight);
                
                float jumpHeight = _playerCharacterView.GetJumpHeight();
                float elevationPercent = Random.Range(_currentDifficulty.NextSpawnMinElevationPercent,
                    _currentDifficulty.NextSpawnMaxElevationPercent);
                float elevation = (jumpHeight - _elevationLowering) * elevationPercent / HundredPercent;
                float spawnHeight = _spawnAdditionalHeight + _spawnTrigger;
                
                bool isStructure = Random.Range(0, 100) <= _currentDifficulty.StructureChancePercent;
                bool structureListNotEmpty = _currentDifficulty.AvailableStructures.Count > 0;
                
                if (isStructure && structureListNotEmpty)
                {
                    int randomIndex = Random.Range(0, _currentDifficulty.AvailableStructures.Count);
                    Vector2 position = new Vector2(0, spawnHeight);
                    
                    elevation += _platformSpawner.SpawnStructure(
                        _currentDifficulty.AvailableStructures[randomIndex],
                        position,
                        _spawnTrigger,
                        _spawnAdditionalHeight);
                }
                else
                {
                    float cameraHalfWidth = _cameraView.Size.x / 2.0f;
                    float randomX = Random.Range(-cameraHalfWidth, cameraHalfWidth);
                    Vector2 position = new Vector2(randomX, spawnHeight);
                    BounceConfig config = GetRandomBounceConfig(_objectShifter.TotalHeight);
                    
                    _platformSpawner.SpawnSingle(
                        config, 
                        position, 
                        _spawnTrigger, 
                        _spawnAdditionalHeight);

                    if (elevation <= 0f)
                    {
                        Debug.LogError(
                            $"[{nameof(LevelGenerator)}] Elevation is {elevation}. Check your DifficultyTier settings for percentages! Breaking loop to prevent freeze.");
                        break;
                    }

                    _elevationLowering = 0;

                    if (config.Type == BounceType.Broken)
                        _elevationLowering = elevation;
                }
                
                _spawnTrigger += elevation;
            }
        }

        private void OnReturn(float shiftValue)
        {
            _spawnTrigger -= shiftValue;
        }

        private BounceConfig GetRandomBounceConfig(float totalHeight)
        {
            float totalWeight = 0;

            foreach (var item in _currentDifficulty.PlatformChances)
                totalWeight += item.Weight;

            float randomValue = Random.Range(0, totalWeight);
            float currentWeightSum = 0;

            foreach (var item in _currentDifficulty.PlatformChances)
            {
                currentWeightSum += item.Weight;

                if (randomValue <= currentWeightSum)
                    return item.Config;
            }

            return _currentDifficulty.PlatformChances[0].Config;
        }
    }
}