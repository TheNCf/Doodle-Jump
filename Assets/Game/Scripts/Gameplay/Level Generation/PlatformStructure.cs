using UnityEngine;
using System.Collections.Generic;

namespace Game.Scripts.Gameplay
{
    [CreateAssetMenu(fileName = "Platform Structure", menuName = "Doodle Jump/Platform Structure")]
    public class PlatformStructure : ScriptableObject
    {
        [SerializeField] private List<PlatformSpawnData> _data;

        public IReadOnlyList<PlatformSpawnData> Data => _data;
    }
}