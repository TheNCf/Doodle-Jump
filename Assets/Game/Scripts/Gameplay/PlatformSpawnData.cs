using System;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
    [Serializable]
    public struct PlatformSpawnData
    {
        [field: SerializeField] public BounceConfig Config {get; private set;}
        [field: SerializeField] public Vector2 RelativePosition {get; private set;}
    }
}