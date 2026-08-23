using System;
using Game.Scripts.Core;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Scripts.Gameplay
{
    public class LevelGenerator : IInitializable, IDisposable
    {
        private GameBalance _gameBalance;
        private CameraView _cameraView;
        private ObjectShifter _objectShifter;
        private PlatformSpawner _platformSpawner;
        private PlayerCharacterView _playerCharacterView;

        private float _spawnTrigger = -15.0f;
        private float _spawnHeight;
        
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
        
        public void Initialize()
        {
            _spawnHeight = _cameraView.Size.y * 2f;

            OnHeightChanged(_objectShifter.RelativeHeight);
            _objectShifter.RelativeHeightChanged += OnHeightChanged;
        }

        public void Dispose()
        {
            _objectShifter.RelativeHeightChanged -= OnHeightChanged;
        }

        private void OnHeightChanged(float height)
        {
            while (height > _spawnTrigger)
            {
                float randomX = Random.Range(-_cameraView.Size.x, _cameraView.Size.x);
                Vector2 position = new Vector2(randomX, _spawnHeight + _spawnTrigger);
                
                _platformSpawner.SpawnSingle(GetRandomBounceConfig(), position);

                float jumpHeight = _playerCharacterView.GetJumpHeight();
                float elevationPercent = Random.Range(_currentDifficulty.NextSpawnMinElevationPercent, 
                    _currentDifficulty.NextSpawnMaxElevationPercent);
                
                float elevation = jumpHeight * elevationPercent / 100.0f;
                
                if (elevation <= 0f)
                {
                    Debug.LogError($"[{nameof(LevelGenerator)}] Elevation is {elevation}. Check your DifficultyTier settings for percentages! Breaking loop to prevent freeze.");
                    break; 
                }
                
                _spawnTrigger += elevation;
            }
        }

        private BounceConfig GetRandomBounceConfig()
        {
            _currentDifficulty = _gameBalance.GetTier(_spawnTrigger);

            float totalWeight = 0;

            foreach (var item in _currentDifficulty.PlatformChances)
                totalWeight += item.Weight;

            float randomValue = UnityEngine.Random.Range(0, totalWeight);
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