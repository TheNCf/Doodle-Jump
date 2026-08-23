using Game.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class PlayerCharacterMover : IFixedTickable
    {
        private readonly CameraView _cameraView;
        private readonly IInputService _inputService;
        private readonly PlayerCharacterView _playerCharacterView;
        
        private readonly Rigidbody2D _rigidbody;
        private readonly Transform _transform;
        private readonly SpriteRenderer _spriteRenderer;
    
        public PlayerCharacterMover(IInputService inputService, PlayerCharacterView playerCharacterView, CameraView cameraView)
        {
            _inputService = inputService;
            _playerCharacterView = playerCharacterView;
            _cameraView = cameraView;

            _rigidbody = _playerCharacterView.Rigidbody;
            _transform = _playerCharacterView.Transform;
            _spriteRenderer = _playerCharacterView.SpriteRenderer;

        }

        public void FixedTick()
        {
            Move();
        }

        private void Move()
        {
            Vector3 newVelocity = _rigidbody.velocity;
            newVelocity.x = Mathf.MoveTowards(
                newVelocity.x, 
                _playerCharacterView.MaxSpeed * _inputService.HorizontalInput,
                Time.fixedDeltaTime * _playerCharacterView.SpeedInterpolation);
            _rigidbody.velocity = newVelocity;

            if (newVelocity.x > 0)
                _spriteRenderer.flipX = false;
            
            if (newVelocity.x < 0)
                _spriteRenderer.flipX = true;

            if (_transform.position.x > _spriteRenderer.size.x / 2 + _cameraView.Size.x)
                _transform.Translate(-(_spriteRenderer.size.x / 2 + _cameraView.Size.x) * 2, 0, 0);

            if (_transform.position.x < -_spriteRenderer.size.x / 2 - _cameraView.Size.x)
                _transform.Translate((_spriteRenderer.size.x / 2 + _cameraView.Size.x) * 2, 0, 0);
        }
    }
}
