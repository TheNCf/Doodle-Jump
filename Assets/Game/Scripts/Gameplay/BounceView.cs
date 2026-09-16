using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class BounceView : MonoBehaviour, IBounceable
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private BounceConfig _config;
        private float _bounceMultiplier = 1.0f;

        public BounceConfig Config => _config;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public float BounceMultiplier => _bounceMultiplier;

        public void Initialize(BounceConfig config)
        {
            _config = config;
            ApplyConfig();
            gameObject.SetActive(true);
        }

        private void ApplyConfig()
        {
            _bounceMultiplier = _config.BounceMultiplier;
            _spriteRenderer.sprite = _config.Sprite;
        }
    }
}