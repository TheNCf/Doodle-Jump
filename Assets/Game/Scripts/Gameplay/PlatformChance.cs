using UnityEngine;

namespace Game.Scripts.Gameplay
{
    [System.Serializable]
    public struct PlatformChance
    {
        [field: SerializeField] private BounceConfig _config;
        [field: SerializeField] [Range(0, 100)] private float _weight;
        
        public BounceConfig Config => _config;
        public float Weight => _weight;
    }
}