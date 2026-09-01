using Game.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    [RequireComponent(typeof(BounceView))]
    public class PlatformView : MonoBehaviour, IShiftable, IDisposable, IPoolableObject
    {
        private BounceView _bounceView;
        
        private MovingBehaviour _movingBehaviour;
        private TickableManager _tickableManager;
        
        public float SpawnHeight { get; private set; }
        public float DistanceFromCenter { get; private set; }
        
        [Inject]
        public void Construct(MovingBehaviour movingBehaviour, TickableManager tickableManager)
        {
            _bounceView = GetComponent<BounceView>();
            
            _movingBehaviour = movingBehaviour;
            _tickableManager = tickableManager;
            
            _movingBehaviour.Initialize(_bounceView);
            _tickableManager.Add(_movingBehaviour);
        }
        
        public void Initialize(BounceConfig config, float spawnHeight, float distanceFromCenter)
        {
            SpawnHeight = spawnHeight;
            DistanceFromCenter = distanceFromCenter;
            
            _bounceView.Initialize(config);

            _movingBehaviour.IsEnabled = config.Type == BounceType.Moving;
        }
        
        public void ShiftDown(float distance)
        {
            SpawnHeight -= distance;
            transform.Translate(0, -distance, 0);
        }
        
        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void ResetObject()
        {
            gameObject.SetActive(false);
        }
    }
}