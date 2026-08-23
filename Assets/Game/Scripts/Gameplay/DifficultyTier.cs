using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
    [Serializable]
    public struct DifficultyTier
    {
        [field: SerializeField] public float MinHeight { get; private set; }
        [SerializeField] [Range(0, 100)] private float _nextSpawnMinElevationPercent;
        [SerializeField] [Range(0, 100)] private float _nextSpawnMaxElevationPercent;
        [SerializeField] private List<PlatformChance> _platformChances;
        [SerializeField] private List<PlatformStructure> _availableStructures;
        
        public float NextSpawnMinElevationPercent => _nextSpawnMinElevationPercent;
        public float NextSpawnMaxElevationPercent => _nextSpawnMaxElevationPercent;
        public IReadOnlyList<PlatformChance> PlatformChances => _platformChances;
        public IReadOnlyList<PlatformStructure> AvailableStructures => _availableStructures;
    }
}