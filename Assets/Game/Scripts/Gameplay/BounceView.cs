using UnityEngine;

namespace Game.Scripts.Gameplay
{
    public class BounceView : MonoBehaviour, IBounceable
    {
        [SerializeField] private float _bounceMultiplier = 1.0f;
        
        public float BounceMultiplier => _bounceMultiplier;
    }
}