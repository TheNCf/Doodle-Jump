using Game.Scripts.Core.SceneLoader;
using Game.Scripts.Gameplay.Signals;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoaderSettings _gameSceneLoaderSettings;
        [SerializeField] private PlayerCharacterView _playerCharacterViewInstance;
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            
            Container.DeclareSignal<OpenGameSceneSignal>();
            
            Container
                .BindInterfacesAndSelfTo<GameSceneOpener>()
                .AsSingle()
                .WithArguments(_gameSceneLoaderSettings)
                .NonLazy();
            
            BindPlayerCharacterView();
            BindPlayerCharacterBouncer();
        }
        
        private void BindPlayerCharacterBouncer()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerCharacterBouncer>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindPlayerCharacterView()
        {
            Container
                .Bind<PlayerCharacterView>()
                .FromInstance(_playerCharacterViewInstance)
                .AsSingle()
                .NonLazy();
        }
    }
}