using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Gameplay.LevelGeneration
{
    [CreateAssetMenu(fileName = "Game Balance", menuName = "Doodle Jump/Game Balance")]
    public class GameBalance : ScriptableObject
    {
        [SerializeField] private List<DifficultyTier> _tiers;

        public DifficultyTier GetTier(float height)
        {
            var currentTier = _tiers[0];

            foreach (var tier in _tiers)
                if (height >= tier.MinHeight)
                    currentTier = tier;

            return currentTier;
        }
    }
}