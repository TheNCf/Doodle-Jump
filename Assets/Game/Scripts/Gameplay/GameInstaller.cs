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
        [SerializeField] private Transform _startPoint;
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

            BindCharacterView();
        }

        private void BindGameBalance()
        {
            Container.Bind<GameBalance>().FromInstance(_gameBalance);
        }

        private void BindPlatfromObjectPool()
        {
            Container.Bind<ObjectPool<BounceView>>()
                .FromMethod(CreatePlatformPool)
                .AsSingle();
        }

        private void BindCharacterView()
        {
            PlayerCharacterView playerCharacterView = Container
                .InstantiatePrefabForComponent<PlayerCharacterView>(
                    _playerCharacterViewPrefab, 
                    _startPoint.position,
                    Quaternion.identity, 
                    null);

            Container
                .Bind<PlayerCharacterView>()
                .FromInstance(playerCharacterView)
                .AsSingle();
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
            Func<BounceView> createFunc = () => Container.InstantiatePrefabForComponent<BounceView>(_platformPrefab);
            Action<BounceView> onGet = (platform) => platform.gameObject.SetActive(true);
            Action<BounceView> onRelease = (platform) => platform.gameObject.SetActive(false);
            Action<BounceView> onClear = (platform) => 
            {
                if (platform != null && platform.gameObject != null)
                    Destroy(platform.gameObject);
            };

            return new ObjectPool<BounceView>(createFunc, onGet, onRelease, onClear, _initialPoolSize);
        }
    }
}