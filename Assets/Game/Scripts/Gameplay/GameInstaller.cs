using System;
using Game.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private CameraView _cameraView;
        [SerializeField] private PlayerCharacterView _playerCharacterViewPrefab;
        [SerializeField] private GameObject _platformPrefab;
        [SerializeField] private int _initialPoolSize = 20;
        [SerializeField] private GameBalance _gameBalance;

        public override void InstallBindings()
        {
            BindInputService();
            BindCameraView();
            BindPlayerCharacterMover();
            BindPlayerCharacterBouncer();
            BindPlatfromObjectPool();
            BindGameBalance();
            BindObjectShifter();
            BindPlatformDisposer();
            BindPlatformSpawner();
            BindShiftRegistry();
            BindLevelGenerator();
            BindMovingBehaviour();

            BindCharacterView();
        }

        private void BindPlatformDisposer()
        {
            Container.BindInterfacesAndSelfTo<PlatformDisposer>()
                .AsSingle()
                .NonLazy();
        }

        private void BindMovingBehaviour()
        {
            Container
                .Bind<MovingBehaviour>()
                .AsTransient();
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

        private void BindGameBalance()
        {
            Container.BindInterfacesAndSelfTo<GameBalance>()
                .FromInstance(_gameBalance)
                .AsSingle()
                .NonLazy();
        }

        private void BindPlatfromObjectPool()
        {
            Container.Bind<ObjectPool<BounceView>>()
                .FromMethod(CreatePlatformPool)
                .AsSingle();
        }

        private void BindCharacterView()
        {
            Container
                .Bind<PlayerCharacterView>()
                .FromComponentInNewPrefab(_playerCharacterViewPrefab)
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerCharacterBouncer()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerCharacterBouncer>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerCharacterMover()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerCharacterMover>()
                .AsSingle()
                .NonLazy();
        }

        private void BindCameraView()
        {
            Container
                .Bind<CameraView>()
                .FromInstance(_cameraView)
                .AsSingle();
        }

        private void BindInputService()
        {
            Container
                .BindInterfacesAndSelfTo<InputService>()
                .AsSingle()
                .NonLazy();
        }
        
        private ObjectPool<BounceView> CreatePlatformPool(InjectContext context)
        {
            Func<BounceView> createFunc = () =>
            {
                BounceView spawnedPlatform = Container.InstantiatePrefabForComponent<BounceView>(_platformPrefab);
                spawnedPlatform.gameObject.SetActive(false);
                return spawnedPlatform;
            };
            Action<BounceView> onGet = (platform) => platform.Activate();
            Action<BounceView> onRelease = (platform) => platform.ResetObject();
            Action<BounceView> onClear = (platform) => 
            {
                if (platform != null && platform.gameObject != null)
                    Destroy(platform.gameObject);
            };

            return new ObjectPool<BounceView>(createFunc, onGet, onRelease, onClear, _initialPoolSize);
        }
    }
}