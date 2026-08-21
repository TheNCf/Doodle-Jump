using UnityEngine;

namespace Game.Scripts.Gameplay
{
    public class BounceView : MonoBehaviour, IBounceable, IPoolableObject
    {
        [SerializeField] private float _bounceMultiplier = 1.0f;
        [SerializeField] private BounceType _type;
        
        public float BounceMultiplier => _bounceMultiplier;
        
        public void Activate()
        {
            
        }

        public void ResetObject()
        {
            
        }
    }
}