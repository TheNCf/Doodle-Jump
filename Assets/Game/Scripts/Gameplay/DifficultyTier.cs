using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
    [Serializable]
    public struct DifficultyTier
    {
        [SerializeField] private float _minHeight;
        [SerializeField] [Range(0, 100)] private float _nextSpawnElevationPercent;
        [SerializeField] private List<PlatformChance> _platformChances;
        [SerializeField] private List<PlatformStructure> _availableStructures;
        
        public float MinHeight => _minHeight;
        public IReadOnlyList<PlatformChance> PlatformChances => _platformChances;
        public IReadOnlyList<PlatformStructure> AvailableStructures => _availableStructures;
    }
}