using Game.Scripts.Core.SceneLoader;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private PlayerCharacterView _playerCharacterViewInstance;
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            
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