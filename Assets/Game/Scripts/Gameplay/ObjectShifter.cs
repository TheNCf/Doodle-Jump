using System;
using Game.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class ObjectShifter : ILateTickable
    {
        private PlayerCharacterView _playerCharacterView;
        private ShiftRegistry _shiftRegistry;
        private CameraView _cameraView;

        private float _heightThreshold = 20.0f;
        
        public event Action<float> ShiftedByValue;
        public event Action<float> RelativeHeightChanged;

        public ObjectShifter(PlayerCharacterView playerCharacterView, ShiftRegistry shiftRegistry, CameraView cameraView)
        {
            _playerCharacterView = playerCharacterView;
            _shiftRegistry = shiftRegistry;
            _cameraView = cameraView;
        }
        
        public float RelativeHeight { get; private set; } = 0.0f;
        public float TotalHeight { get; private set; } = 0.0f;
        
        public void LateTick()
        {
            TryToShift();
            TryReturnToCenter();
        }

        private void TryToShift()
        {
            if (_playerCharacterView.Transform.position.y <= _playerCharacterView.HeightToShift + _cameraView.Transform.position.y)
                return;

            float shift = _playerCharacterView.Transform.position.y - (_playerCharacterView.HeightToShift + _cameraView.Transform.position.y);
            RelativeHeight += shift;
            TotalHeight += shift;
            _cameraView.Transform.Translate(0, shift, 0);
            
            ShiftedByValue?.Invoke(shift);
            RelativeHeightChanged?.Invoke(RelativeHeight);
        }

        private void TryReturnToCenter()
        {
            if (_cameraView.Transform.position.y < _heightThreshold || _playerCharacterView.Rigidbody.velocity.y > 0)
                return;
            
            Vector3 shiftVector = new Vector3(0, _heightThreshold);

            _playerCharacterView.Rigidbody.position -= (Vector2)shiftVector;
            _playerCharacterView.Transform.position -= shiftVector;
            Physics2D.SyncTransforms();
    
            _cameraView.Transform.position -= shiftVector;

            foreach (IShiftable shiftable in _shiftRegistry.Shiftables)
                shiftable.ShiftDown(_heightThreshold);
            
            ShiftedByValue?.Invoke(_heightThreshold);
            
            RelativeHeight -= _heightThreshold;
            RelativeHeightChanged?.Invoke(RelativeHeight);
        }
    }
}