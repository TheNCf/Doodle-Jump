using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class BounceView : MonoBehaviour, IBounceable, IPoolableObject, IShiftable
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        [SerializeField] private BounceType _type;
        [SerializeField] private float _bounceMultiplier = 1.0f;
        [SerializeField] private BounceConfig _config;
        [SerializeField] private float _horizontalSpeed = 1.0f;
        
        private MovingBehaviour _movingBehaviour;
        private TickableManager _tickableManager;
        
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public float BounceMultiplier => _bounceMultiplier;
        public float HorizontalSpeed => _horizontalSpeed;
        
        [Inject]
        public void Construct(MovingBehaviour movingBehaviour, TickableManager tickableManager)
        {
            _movingBehaviour = movingBehaviour;
            _tickableManager = tickableManager;
            
            _movingBehaviour.Initialize(this);
        }
        
        public void Initialize(BounceConfig config)
        {
            _config = config;

            ApplyConfig();
            
            gameObject.SetActive(true);
            
            if (_type == BounceType.Moving)
                _tickableManager.Add(_movingBehaviour);
        }

        private void ApplyConfig()
        {
            _type = _config.Type;
            _spriteRenderer.sprite = _config.Sprite;
        }

        public void Activate()
        {
            gameObject.SetActive(false);
        }

        public void ResetObject()
        {
            _tickableManager.Remove(_movingBehaviour);
            gameObject.SetActive(false);
        }

        public void ShiftDown(float distance)
        {
            transform.Translate(0, -distance, 0);
        }
    }
}