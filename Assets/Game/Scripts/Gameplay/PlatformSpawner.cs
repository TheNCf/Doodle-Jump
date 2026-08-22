using System;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
    public class PlatformSpawner
    {
        private ObjectPool<BounceView> _pool;

        public PlatformSpawner(ObjectPool<BounceView> pool)
        {
            _pool = pool;
        }

        public BounceView SpawnSingle(BounceConfig config, Vector2 position)
        {
            BounceView view = _pool.Get();
            view.Initialize(config);
            view.transform.position = position;
            return view;
        }

        public void SpawnStructure(PlatformStructure structure, Vector2 position)
        {
            foreach (PlatformSpawnData data in structure.Data)
                SpawnSingle(data.Config, position + data.RelativePosition);
        }
    }
} 
