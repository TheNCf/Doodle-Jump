using Game.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class MovingBehaviour : ITickable
    {
        private IMovable _movable;
        private CameraView _cameraView;
        
        private bool _isInitialized = false;
        private bool _isFalling = false;

        private float _platformHalfWidth;
        private float _cameraHalfWidth;
        private float _horizontalDirection = 1.0f;
        
        public MovingBehaviour(CameraView cameraView)
        {
            _cameraView = cameraView;
            
            _cameraHalfWidth = _cameraView.Size.x / 2.0f;
        }
        
        public bool IsEnabled { get; set; }

        public void Initialize(IMovable movable, float width)
        {
            _movable = movable;
            _platformHalfWidth = width;
            
            _isFalling = false;

            movable.EnteredTrigger -= EnableFall;
            movable.EnteredTrigger += EnableFall;
            
            _isInitialized = true;
        }

        public void Tick()
        {
            MoveHorizontally();
            Fall();
        }

        private void EnableFall(Collider2D _)
        {
            _isFalling = true;
            Debug.Log("Falling");
        }

        private void MoveHorizontally()
        {
            if (IsEnabled == false || _isInitialized == false || _movable is null || _isFalling == true)
                return;
            
            if (_movable.Transform.position.x * _horizontalDirection > _cameraHalfWidth - _platformHalfWidth)
                _horizontalDirection *= -1;

            _movable.Transform.Translate(_movable.HorizontalSpeed * _horizontalDirection * Time.deltaTime, 0, 0);
        }

        private void Fall()
        {
            if (_isFalling == false || _isInitialized == false || _movable is null)
                return;
            
            _movable.Transform.Translate(0, _movable.FallSpeed * Time.deltaTime, 0);
        }
    }
}