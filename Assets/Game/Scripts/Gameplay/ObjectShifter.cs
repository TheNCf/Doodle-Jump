using System;
using Game.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class ObjectShifter : IInitializable, IDisposable, ILateTickable
    {
        private readonly CameraView _cameraView;

        private readonly float _heightThreshold = 10.0f;
        private readonly PlayerCharacterView _playerCharacterView;
        private readonly ShiftRegistry _shiftRegistry;
        private readonly SignalBus _signalBus;
        private bool _isGameEnded;

        public ObjectShifter(PlayerCharacterView playerCharacterView, ShiftRegistry shiftRegistry,
            CameraView cameraView, SignalBus signalBus)
        {
            _playerCharacterView = playerCharacterView;
            _shiftRegistry = shiftRegistry;
            _cameraView = cameraView;
            _signalBus = signalBus;
        }

        public float RelativeHeight { get; private set; }
        public float TotalHeight { get; private set; }

        public void Dispose()
        {
            _signalBus.Unsubscribe<LoseSignal>(OnLose);
        }

        public void Initialize()
        {
            _signalBus.Subscribe<LoseSignal>(OnLose);
        }

        public void LateTick()
        {
            if (_isGameEnded)
                return;

            TryToShift();
            TryReturnToCenter();
        }

        public event Action<float> ReturnedBackByValue;
        public event Action<float> RelativeHeightChanged;
        public event Action<float> TotalHeightChanged;

        private void TryToShift()
        {
            if (_playerCharacterView.Transform.position.y <=
                _playerCharacterView.HeightToShift + _cameraView.Transform.position.y)
                return;

            var shift = _playerCharacterView.Transform.position.y -
                        (_playerCharacterView.HeightToShift + _cameraView.Transform.position.y);
            RelativeHeight += shift;
            TotalHeight += shift;
            _cameraView.Transform.Translate(0, shift, 0);

            RelativeHeightChanged?.Invoke(RelativeHeight);
            TotalHeightChanged?.Invoke(TotalHeight);
        }

        private void TryReturnToCenter()
        {
            if (_cameraView.Transform.position.y < _heightThreshold || _playerCharacterView.Rigidbody.velocity.y > 0)
                return;

            var shiftVector = new Vector3(0, _heightThreshold);

            _playerCharacterView.Rigidbody.position -= (Vector2)shiftVector;
            _playerCharacterView.Transform.position -= shiftVector;
            Physics2D.SyncTransforms();

            _cameraView.Transform.position -= shiftVector;

            foreach (var shiftable in _shiftRegistry.Shiftables)
                shiftable.ShiftDown(_heightThreshold);

            ReturnedBackByValue?.Invoke(_heightThreshold);

            RelativeHeight -= _heightThreshold;
            RelativeHeightChanged?.Invoke(RelativeHeight);
        }

        private void OnLose()
        {
            _isGameEnded = true;
        }
    }
}