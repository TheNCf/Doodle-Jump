using System;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
    public class PlatformSpawner
    {
        private ShiftRegistry _shiftRegistry;
        private PlatformDisposer _disposer;
        
        private ObjectPool<BounceView> _pool;

        public PlatformSpawner(ObjectPool<BounceView> pool, ShiftRegistry shiftRegistry, PlatformDisposer disposer)
        {
            _shiftRegistry = shiftRegistry;
            _disposer = disposer;
            
            _pool = pool;
            
            _disposer.MarkedForDisposal += Release;
        }

        public BounceView SpawnSingle(BounceConfig config, Vector2 position, float spawnHeight, float distanceFromCenter)
        {
            BounceView view = _pool.Get();
            view.Initialize(config, spawnHeight, distanceFromCenter);
            view.transform.position = position;
            _shiftRegistry.Register(view);
            _disposer.AddForTracking(view);
            return view;
        }

        public void SpawnStructure(PlatformStructure structure, Vector2 position, float spawnHeight, float distanceFromCenter)
        {
            foreach (PlatformSpawnData data in structure.Data)
                SpawnSingle(
                    data.Config,
                    position + data.RelativePosition, 
                    spawnHeight + data.RelativePosition.y,
                    distanceFromCenter + data.RelativePosition.y);
        }

        private void Release(BounceView platform)
        {
            _disposer.RemoveFromTracking(platform);
            _shiftRegistry.Unregister(platform);
            _pool.Release(platform);
        }
    }
} 
