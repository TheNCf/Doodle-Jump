using System;
using Game.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class ObjectShifter : IFixedTickable
    {
        private PlayerCharacterView _playerCharacterView;
        private ShiftRegistry _shiftRegistry;
        
        public event Action<float> ShiftedByValue;
        public event Action<float> RelativeHeightChanged;

        public ObjectShifter(PlayerCharacterView playerCharacterView, ShiftRegistry shiftRegistry)
        {
            _playerCharacterView = playerCharacterView;
            _shiftRegistry = shiftRegistry;
        }
        
        public float RelativeHeight { get; private set; } = 0.0f;
        
        public void FixedTick()
        {
            TryToShift();
        }

        private void TryToShift()
        {
            if (_playerCharacterView.Transform.position.y <= _playerCharacterView.HeightToShift)
                return;

            float shift = _playerCharacterView.Transform.position.y - _playerCharacterView.HeightToShift;
            RelativeHeight += shift;
            
            _playerCharacterView.Rigidbody.position -= new Vector2(0, shift);

            foreach (IShiftable shiftable in _shiftRegistry.Shiftables)
                shiftable.ShiftDown(shift);
            
            ShiftedByValue?.Invoke(shift);
            RelativeHeightChanged?.Invoke(RelativeHeight);
        }
    }
}