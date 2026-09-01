using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class BounceView : MonoBehaviour, IBounceable
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private BounceConfig _config;
        private BounceType _type;
        private float _bounceMultiplier = 1.0f;
        private float _horizontalSpeed = 1.0f;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public float BounceMultiplier => _bounceMultiplier;
        public float HorizontalSpeed => _horizontalSpeed;
        
        public void Initialize(BounceConfig config)
        {
            _config = config;
            ApplyConfig();
            gameObject.SetActive(true);
        }

        private void ApplyConfig()
        {
            _bounceMultiplier = _config.BounceMultiplier;
            _horizontalSpeed = _config.Speed;
            _type = _config.Type;
            _spriteRenderer.sprite = _config.Sprite;
        }
    }
}