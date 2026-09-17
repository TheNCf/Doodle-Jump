using System;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay.LevelGeneration
{
    [RequireComponent(typeof(BounceView))]
    [RequireComponent(typeof(Collider2D))]
    public class PlatformView : MonoBehaviour, IShiftable, IDisposableObject, IPoolableObject, IMovable
    {
        [SerializeField] private BounceView _springBounceView;
        [SerializeField] private BounceConfig _springConfig;

        private Collider2D _collider2D;

        private MovingBehaviour _movingBehaviour;
        private TickableManager _tickableManager;

        public BounceView BounceView { get; private set; }
        public BounceView SpringBounceView => _springBounceView;
        public BounceConfig SpringConfig => _springConfig;

        private void OnTriggerEnter2D(Collider2D other)
        {
            EnteredTrigger?.Invoke(other);
            BounceView.SpriteRenderer.sprite = BounceView.Config.ActivatedSprite;
        }

        public float SpawnHeight { get; private set; }
        public float DistanceFromCenter { get; private set; }

        public event Action<Collider2D> EnteredTrigger;
        public Transform Transform => transform;
        public float HorizontalSpeed { get; private set; }
        public float FallSpeed { get; private set; }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void ResetObject()
        {
            gameObject.SetActive(false);
            _springBounceView.gameObject.SetActive(false);
        }

        public void ShiftDown(float distance)
        {
            SpawnHeight -= distance;
            transform.Translate(0, -distance, 0);
        }

        [Inject]
        public void Construct(MovingBehaviour movingBehaviour, TickableManager tickableManager)
        {
            BounceView = GetComponent<BounceView>();
            _collider2D = GetComponent<Collider2D>();

            _movingBehaviour = movingBehaviour;
            _tickableManager = tickableManager;

            _tickableManager.Add(_movingBehaviour);
        }

        public void Initialize(BounceConfig config, float spawnHeight, float distanceFromCenter)
        {
            SpawnHeight = spawnHeight;
            DistanceFromCenter = distanceFromCenter;
            HorizontalSpeed = config.Speed;
            FallSpeed = config.FallSpeed;

            BounceView.Initialize(config);
            var width = _collider2D.bounds.extents.x;
            _movingBehaviour.Initialize(this, width);

            _movingBehaviour.IsEnabled = config.Type == BounceType.Moving;
            _collider2D.isTrigger = config.Type == BounceType.Broken;
        }
    }
}