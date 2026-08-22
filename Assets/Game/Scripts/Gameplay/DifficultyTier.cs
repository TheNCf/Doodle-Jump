using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
    [Serializable]
    public struct DifficultyTier
    {
        [field: SerializeField] public float MinHeight { get; private set; }
        [SerializeField] [Range(0, 100)] private float _nextSpawnElevationPercent;
        [SerializeField] private List<PlatformChance> _platformChances;
        [SerializeField] private List<PlatformStructure> _availableStructures;
        
        public float NextSpawnElevationPercent => _nextSpawnElevationPercent;
        public IReadOnlyList<PlatformChance> PlatformChances => _platformChances;
        public IReadOnlyList<PlatformStructure> AvailableStructures => _availableStructures;
    }
}