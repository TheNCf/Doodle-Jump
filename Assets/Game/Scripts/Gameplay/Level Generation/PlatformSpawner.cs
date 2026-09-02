using System;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
    public class PlatformSpawner
    {
        private ShiftRegistry _shiftRegistry;
        private PlatformDisposer _disposer;
        
        private ObjectPool<PlatformView> _pool;

        public PlatformSpawner(ObjectPool<PlatformView> pool, ShiftRegistry shiftRegistry, PlatformDisposer disposer)
        {
            _shiftRegistry = shiftRegistry;
            _disposer = disposer;
            
            _pool = pool;
            
            _disposer.MarkedForDisposal += Release;
        }

        public PlatformView SpawnSingle(BounceConfig config, Vector2 position, float spawnHeight, float distanceFromCenter)
        {
            PlatformView view = _pool.Get();
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

        private void Release(IDisposable obj)
        {
            if (obj is PlatformView == false)
                return;
            
            PlatformView platform = (PlatformView)obj;
            _disposer.RemoveFromTracking(platform);
            _shiftRegistry.Unregister(platform);
            _pool.Release(platform);
        }
    }
} 
