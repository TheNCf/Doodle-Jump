using Game.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class PlayerCharacterMover : IFixedTickable
    {
        private IInputService _inputService;
        private PlayerCharacterView _playerCharacterView;
    
        public PlayerCharacterMover(IInputService inputService, PlayerCharacterView playerCharacterView)
        {
            _inputService = inputService;
            _playerCharacterView = playerCharacterView;
        }

        public void FixedTick()
        {
            Move();
        }

        private void Move()
        {
            Vector3 newVelocity = _playerCharacterView.Rigidbody.velocity;
            newVelocity.x = Mathf.MoveTowards(
                newVelocity.x, 
                _playerCharacterView.MaxSpeed * _inputService.HorizontalInput,
                Time.fixedDeltaTime * _playerCharacterView.SpeedInterpolation);
            _playerCharacterView.Rigidbody.velocity = newVelocity;

            if (newVelocity.x > 0)
                _playerCharacterView.SpriteRenderer.flipX = false;
            
            if (newVelocity.x < 0)
                _playerCharacterView.SpriteRenderer.flipX = true;
        }
    }
}