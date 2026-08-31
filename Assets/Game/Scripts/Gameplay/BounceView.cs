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
        
        private float _distanceFromCenter;
        
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public float BounceMultiplier => _bounceMultiplier;
        public float HorizontalSpeed => _horizontalSpeed;
        public float SpawnHeight { get; private set; }
        public float DistanceFromCenter => _distanceFromCenter;
        
        [Inject]
        public void Construct(MovingBehaviour movingBehaviour, TickableManager tickableManager)
        {
            _movingBehaviour = movingBehaviour;
            _tickableManager = tickableManager;
            
            _movingBehaviour.Initialize(this);
            _tickableManager.Add(_movingBehaviour);
        }
        
        public void Initialize(BounceConfig config, float spawnHeight, float distanceFromCenter)
        {
            _config = config;
            SpawnHeight = spawnHeight;
            _distanceFromCenter = distanceFromCenter;

            ApplyConfig();
            
            gameObject.SetActive(true);

            _movingBehaviour.IsEnabled = _type == BounceType.Moving;
        }

        private void ApplyConfig()
        {
            _type = _config.Type;
            _spriteRenderer.sprite = _config.Sprite;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void ResetObject()
        {
            _movingBehaviour.IsEnabled = false;
            gameObject.SetActive(false);
        }

        public void ShiftDown(float distance)
        {
            SpawnHeight -= distance;
            transform.Translate(0, -distance, 0);
        }
    }
}