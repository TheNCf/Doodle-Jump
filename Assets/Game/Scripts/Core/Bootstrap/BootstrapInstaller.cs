using Game.Scripts.Core.SceneLoader;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Core.Bootstrap
{
    public class BootstrapInstaller :MonoInstaller
    {
        [SerializeField] private SceneLoaderSettings _sceneLoaderSettings;
        
        public override void InstallBindings()
        {
            BindBootstrapper();
        }

        private void BindBootstrapper()
        {
            Container
                .BindInterfacesAndSelfTo<Bootstrapper>()
                .AsSingle()
                .WithArguments(_sceneLoaderSettings)
                .NonLazy();
        }
    }
}