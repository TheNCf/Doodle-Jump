using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Gameplay.LevelGeneration
{
    [CreateAssetMenu(fileName = "Platform Structure", menuName = "Doodle Jump/Platform Structure")]
    public class PlatformStructure : ScriptableObject
    {
        [SerializeField] private List<PlatformSpawnData> _data;

        public IReadOnlyList<PlatformSpawnData> Data => _data;
    }
}