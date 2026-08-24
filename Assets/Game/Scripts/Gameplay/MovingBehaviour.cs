using Game.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class MovingBehaviour : ITickable
    {
        private BounceView _bounceView;
        private CameraView _cameraView;
        
        private bool _isInitialized = false;

        private float _platformHalfWidth;
        private float _cameraHalfWidth;
        private float _horizontalDirection = 1.0f;
        
        public MovingBehaviour(CameraView cameraView)
        {
            _cameraView = cameraView;
            
            _cameraHalfWidth = _cameraView.Size.x / 2.0f;
        }

        public void Initialize(BounceView bounceView)
        {
            _bounceView = bounceView;
            
            var spriteRenderer = bounceView.SpriteRenderer;
            _platformHalfWidth = spriteRenderer.bounds.extents.x;

            _isInitialized = true;
        }

        public void Tick()
        {
            if (_isInitialized == false || _bounceView is null)
                return;
            
            if (_bounceView.transform.position.x * _horizontalDirection > _cameraHalfWidth - _platformHalfWidth)
                _horizontalDirection *= -1;

            _bounceView.transform.Translate(_bounceView.HorizontalSpeed * _horizontalDirection * Time.deltaTime, 0, 0);
        }
    }
}