using UnityEngine;

namespace Game.Scripts.Gameplay
{
    public class BounceView : MonoBehaviour, IBounceable
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public BounceConfig Config { get; private set; }

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public float BounceMultiplier { get; private set; } = 1.0f;

        public void Initialize(BounceConfig config)
        {
            Config = config;
            ApplyConfig();
            gameObject.SetActive(true);
        }

        private void ApplyConfig()
        {
            BounceMultiplier = Config.BounceMultiplier;
            _spriteRenderer.sprite = Config.Sprite;
        }
    }
}