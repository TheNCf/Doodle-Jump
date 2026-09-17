using Game.Scripts.Core;
using UnityEngine;

namespace Game.Scripts.Gameplay.LevelGeneration
{
    public class PlatformSpawner
    {
        private const float HundredPercent = 100.0f;
        private readonly PlatformDisposer _disposer;

        private readonly ObjectPool<PlatformView> _pool;

        private readonly ShiftRegistry _shiftRegistry;

        public PlatformSpawner(ObjectPool<PlatformView> pool, ShiftRegistry shiftRegistry, PlatformDisposer disposer)
        {
            _shiftRegistry = shiftRegistry;
            _disposer = disposer;

            _pool = pool;

            _disposer.MarkedForDisposal += Release;
        }

        public PlatformView SpawnSingle(BounceConfig config, Vector2 position, float spawnHeight,
            float distanceFromCenter)
        {
            var view = _pool.Get();
            view.Initialize(config, spawnHeight, distanceFromCenter);
            view.transform.position = position;

            if (Random.value * HundredPercent < config.SpringChance)
            {
                var localX = Random.Range(-view.BounceView.SpriteRenderer.bounds.extents.x,
                    view.BounceView.SpriteRenderer.bounds.extents.x);
                var localPosition = view.SpringBounceView.transform.localPosition;
                localPosition.x = localX;
                view.SpringBounceView.transform.localPosition = localPosition;
                view.SpringBounceView.Initialize(view.SpringConfig);
            }

            _shiftRegistry.Register(view);
            _disposer.AddForTracking(view);
            return view;
        }

        public float SpawnStructure(PlatformStructure structure, Vector2 position, float spawnHeight,
            float distanceFromCenter)
        {
            foreach (var data in structure.Data)
                SpawnSingle(
                    data.Config,
                    position + data.RelativePosition,
                    spawnHeight + data.RelativePosition.y,
                    distanceFromCenter);

            return structure.Data[structure.Data.Count - 1].RelativePosition.y;
        }

        private void Release(IDisposableObject obj)
        {
            if (obj is not PlatformView platform)
                return;

            _disposer.RemoveFromTracking(platform);
            _shiftRegistry.Unregister(platform);
            _pool.Release(platform);
        }
    }
}