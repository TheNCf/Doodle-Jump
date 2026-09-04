using System;
using Game.Scripts.Core;
using Game.Scripts.Gameplay.LevelGeneration;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private CameraView _cameraView;
        [SerializeField] private PlayerCharacterView _playerCharacterViewPrefab;
        [SerializeField] private PlatformView _platformPrefab;
        [SerializeField] private int _initialPoolSize = 20;
        [SerializeField] private GameBalance _gameBalance;

        public override void InstallBindings()
        {
            InfrastructureInstaller.Install(Container, _gameBalance);
            LevelGenerationInstaller.Install(Container, _platformPrefab, _initialPoolSize);
            GameplayInstaller.Install(Container);

            BindCameraView();
            BindCharacterView();
        }

        private void BindCharacterView()
        {
            Container
                .Bind<PlayerCharacterView>()
                .FromComponentInNewPrefab(_playerCharacterViewPrefab)
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