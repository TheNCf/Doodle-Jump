using Game.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class MovingBehaviour : ITickable
    {
        private readonly float _cameraHalfWidth;
        private readonly CameraView _cameraView;
        private float _horizontalDirection = 1.0f;
        private bool _isFalling;

        private bool _isInitialized;
        private IMovable _movable;

        private float _platformHalfWidth;

        public MovingBehaviour(CameraView cameraView)
        {
            _cameraView = cameraView;

            _cameraHalfWidth = _cameraView.Size.x / 2.0f;
        }

        public bool IsEnabled { get; set; }

        public void Tick()
        {
            MoveHorizontally();
            Fall();
        }

        public void Initialize(IMovable movable, float width)
        {
            _movable = movable;
            _platformHalfWidth = width;

            _isFalling = false;

            movable.EnteredTrigger -= EnableFall;
            movable.EnteredTrigger += EnableFall;

            _isInitialized = true;
        }

        private void EnableFall(Collider2D _)
        {
            _isFalling = true;
        }

        private void MoveHorizontally()
        {
            if (!IsEnabled || !_isInitialized || _movable is null || _isFalling)
                return;

            if (_movable.Transform.position.x * _horizontalDirection > _cameraHalfWidth - _platformHalfWidth)
                _horizontalDirection *= -1;

            _movable.Transform.Translate(_movable.HorizontalSpeed * _horizontalDirection * Time.deltaTime, 0, 0);
        }

        private void Fall()
        {
            if (!_isFalling || !_isInitialized || _movable is null)
                return;

            _movable.Transform.Translate(0, _movable.FallSpeed * Time.deltaTime, 0);
        }
    }
}