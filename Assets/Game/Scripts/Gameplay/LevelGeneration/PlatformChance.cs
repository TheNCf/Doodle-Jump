using System;
using UnityEngine;

namespace Game.Scripts.Gameplay.LevelGeneration
{
    [Serializable]
    public struct PlatformChance
    {
        [field: SerializeField] public BounceConfig Config { get; private set; }
        [field: SerializeField] public float Weight { get; private set; }
    }
}