using UnityEngine;
using Zenject;

namespace Game.Scripts.Core.SceneLoader
{
    public class SceneLoaderInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoaderView _view;

        public override void InstallBindings()
        {
            BindSceneLoaderView();
            BindSceneLoader();
        }

        private void BindSceneLoader()
        {
            Container
                .BindInterfacesAndSelfTo<SceneLoader>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSceneLoaderView()
        {
            Container.Bind<SceneLoaderView>()
                .FromInstance(_view)
                .AsSingle()
                .NonLazy();
        }
    }
}