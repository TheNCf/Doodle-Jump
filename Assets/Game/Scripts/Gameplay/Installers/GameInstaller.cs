using Game.Scripts.Core;
using Game.Scripts.Gameplay.LevelGeneration;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private CameraView _cameraView;
        [SerializeField] private PlayerCharacterView _playerCharacterViewPrefab;
        [SerializeField] private LoseCheckerView _loseCheckerView;
        [SerializeField] private LoseMenuView _loseMenuView;
        [SerializeField] private PlatformView _platformPrefab;
        [SerializeField] private int _initialPoolSize = 20;
        [SerializeField] private GameBalance _gameBalance;

        public override void InstallBindings()
        {
            InfrastructureInstaller.Install(Container, _gameBalance);
            LevelGenerationInstaller.Install(Container, _platformPrefab, _initialPoolSize);
            GameplayInstaller.Install(Container, _playerCharacterViewPrefab);
            
            Container.DeclareSignal<LoseSignal>();
            
            BindCameraView();
            BindLoseCheckerView();
            BindLoseChecker();
            
            BindLoseMenuView();
            BindLoseMenu();
        }

        private void BindLoseMenu()
        {
            Container
                .BindInterfacesAndSelfTo<LoseMenu>()
                .AsSingle()
                .NonLazy();
        }

        private void BindLoseMenuView()
        {
            Container
                .Bind<LoseMenuView>()
                .FromInstance(_loseMenuView)
                .AsSingle();
        }

        private void BindLoseCheckerView()
        {
            Container
                .Bind<LoseCheckerView>()
                .FromInstance(_loseCheckerView)
                .AsSingle()
                .NonLazy();
        }

        private void BindLoseChecker()
        {
            Container
                .BindInterfacesAndSelfTo<LoseChecker>()
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
    }
}