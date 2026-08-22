using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class BounceView : MonoBehaviour, IBounceable, IPoolableObject
    {
        [SerializeField] private BounceType _type;
        [SerializeField] private float _bounceMultiplier = 1.0f;
        [SerializeField] private BounceConfig _config;
        
        private MovingBehaviour _movingBehaviour;
        
        public float BounceMultiplier => _bounceMultiplier;
        
        [Inject]
        public void Construct(MovingBehaviour movingBehaviour)
        {
            _movingBehaviour = movingBehaviour;
        }
        
        public void Initialize(BounceConfig config)
        {
            _config = config;

            ApplyConfig();
        }

        private void ApplyConfig()
        {
            
        }

        public void Activate()
        {
            
        }

        public void ResetObject()
        {
            
        }
    }
}