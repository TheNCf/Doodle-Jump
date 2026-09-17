using System;
using Game.Scripts.Core;
using Game.Scripts.Gameplay.LevelGeneration;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Scripts.Gameplay
{
    public class LevelGenerator : IInitializable, IDisposable
    {
        private const float HundredPercent = 100.0f;
        private readonly CameraView _cameraView;

        private readonly PlatformConfigSelector _configSelector = new();

        private readonly GameBalance _gameBalance;
        private readonly ObjectShifter _objectShifter;
        private readonly PlatformSpawner _platformSpawner;
        private readonly PlayerCharacterView _playerCharacterView;
        private DifficultyTier _currentDifficulty;
        private float _elevationLowering;
        private float _spawnAdditionalHeight;

        private float _spawnTrigger = -15.0f;

        public LevelGenerator(GameBalance gameBalance, CameraView cameraView, ObjectShifter objectShifter,
            PlatformSpawner platformSpawner, PlayerCharacterView playerCharacterView)
        {
            _gameBalance = gameBalance;
            _cameraView = cameraView;
            _objectShifter = objectShifter;
            _platformSpawner = platformSpawner;
            _playerCharacterView = playerCharacterView;
        }

        public void Dispose()
        {
            _objectShifter.RelativeHeightChanged -= OnHeightChanged;
            _objectShifter.ReturnedBackByValue -= OnReturn;
        }

        public void Initialize()
        {
            _spawnAdditionalHeight = _cameraView.Size.y;

            OnHeightChanged(_objectShifter.RelativeHeight);
            _objectShifter.RelativeHeightChanged += OnHeightChanged;
            _objectShifter.ReturnedBackByValue += OnReturn;
        }

        private void OnHeightChanged(float height)
        {
            while (height > _spawnTrigger)
            {
                _currentDifficulty = _gameBalance.GetTier(_objectShifter.TotalHeight);

                var jumpHeight = PhysicsUtils.GetJumpHeight(
                    _playerCharacterView.Rigidbody.gravityScale,
                    _playerCharacterView.JumpStrength);

                var elevationPercent = Random.Range(_currentDifficulty.NextSpawnMinElevationPercent,
                    _currentDifficulty.NextSpawnMaxElevationPercent);

                var elevation = (jumpHeight - _elevationLowering) * elevationPercent / HundredPercent;
                var spawnHeight = _spawnAdditionalHeight + _spawnTrigger;

                var isStructure = Random.Range(0, HundredPercent) <= _currentDifficulty.StructureChancePercent;
                var structureListNotEmpty = _currentDifficulty.AvailableStructures.Count > 0;

                if (isStructure && structureListNotEmpty)
                {
                    var randomIndex = Random.Range(0, _currentDifficulty.AvailableStructures.Count);
                    var position = new Vector2(0, spawnHeight);

                    elevation += _platformSpawner.SpawnStructure(
                        _currentDifficulty.AvailableStructures[randomIndex],
                        position,
                        _spawnTrigger,
                        _spawnAdditionalHeight);
                }
                else
                {
                    var cameraHalfWidth = _cameraView.Size.x / 2.0f;
                    var randomX = Random.Range(-cameraHalfWidth, cameraHalfWidth);
                    var position = new Vector2(randomX, spawnHeight);
                    var config = _configSelector.GetRandomBounceConfig(_currentDifficulty);

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
    }
}