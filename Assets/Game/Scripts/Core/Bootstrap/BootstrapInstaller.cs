using Game.Scripts.Core.SceneLoader;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Core.Bootstrap
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoaderSettings _sceneLoaderSettings;
        [SerializeField] private BootstrapView _bootstrapView;

        public override void InstallBindings()
        {
            BindBootstrapView();
            BindBootstrapper();
        }

        private void BindBootstrapView()
        {
            Container
                .Bind<BootstrapView>()
                .FromInstance(_bootstrapView)
                .AsSingle()
                .NonLazy();
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