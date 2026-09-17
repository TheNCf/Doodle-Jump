using UnityEngine;

namespace Game.Scripts.Gameplay.LevelGeneration
{
    public class PlatformConfigSelector
    {
        public BounceConfig GetRandomBounceConfig(DifficultyTier difficultyTier)
        {
            float totalWeight = 0;

            foreach (var item in difficultyTier.PlatformChances)
                totalWeight += item.Weight;

            var randomValue = Random.Range(0, totalWeight);
            float currentWeightSum = 0;

            foreach (var item in difficultyTier.PlatformChances)
            {
                currentWeightSum += item.Weight;

                if (randomValue <= currentWeightSum)
                    return item.Config;
            }

            return difficultyTier.PlatformChances[0].Config;
        }
    }
}