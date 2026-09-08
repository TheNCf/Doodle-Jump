using UnityEngine;
using Zenject;

namespace Game.Scripts.Bootstrap
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private BootstrapView _view;
        [SerializeField] private BootstrapSettings _settings;

        public override void InstallBindings()
        {
            BindImageDropEffector();
            BindBootstrapSettings();
            BindBootstrapView();
            BindBootstrapper();
        }

        private void BindBootstrapSettings()
        {
            Container
                .Bind<BootstrapSettings>()
                .FromInstance(_settings)
                .NonLazy();
        }

        private void BindBootstrapper()
        {
            Container
                .BindInterfacesAndSelfTo<Bootstrapper>()
                .AsSingle()
                .NonLazy();
        }

        private void BindImageDropEffector()
        {
            Container
                .BindInterfacesAndSelfTo<ImageDropEffector>()
                .AsSingle()
                .NonLazy();
        }

        private void BindBootstrapView()
        {
            Container.Bind<BootstrapView>()
                .FromInstance(_view)
                .AsSingle()
                .NonLazy();
        }
    }
}