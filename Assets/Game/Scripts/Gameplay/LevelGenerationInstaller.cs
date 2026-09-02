using Game.Scripts.Core;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class LevelGenerationInstaller : Installer<PlatformView, int, LevelGenerationInstaller>
    {
        private readonly PlatformView _platformPrefab;
        private readonly int _initialPoolSize;
        
        public LevelGenerationInstaller(PlatformView platformPrefab, int initialPoolSize)
        {
            _platformPrefab = platformPrefab;
            _initialPoolSize = initialPoolSize;
        }
        
        public override void InstallBindings()
        {
            BindPlatformObjectPool();
            BindPlatformDisposer();
            BindShiftRegistry();
            BindObjectShifter();
            BindPlatformSpawner();
            BindLevelGenerator();
        }

        private void BindPlatformDisposer()
        {
            Container.BindInterfacesAndSelfTo<PlatformDisposer>()
                .AsSingle()
                .NonLazy();
        }

        private void BindLevelGenerator()
        {
            Container.BindInterfacesAndSelfTo<LevelGenerator>()
                .AsSingle();
        }

        private void BindShiftRegistry()
        {
            Container
                .BindInterfacesAndSelfTo<ShiftRegistry>()
                .AsSingle()
                .NonLazy();
        }

        private void BindObjectShifter()
        {
            Container
                .BindInterfacesAndSelfTo<ObjectShifter>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlatformSpawner()
        {
            Container
                .BindInterfacesAndSelfTo<PlatformSpawner>()
                .AsSingle();
        }

        private void BindPlatformObjectPool()
        {
            Container.Bind<ObjectPool<PlatformView>>()
                .FromMethod(ctx => ObjectPoolFactory.CreateMonoPool(
                    Container, 
                    _platformPrefab, 
                    _initialPoolSize,
                    onGet: platform => platform.Activate(),
                    onRelease: platform => platform.ResetObject()
                ))
                .AsSingle();
        }
    }
}