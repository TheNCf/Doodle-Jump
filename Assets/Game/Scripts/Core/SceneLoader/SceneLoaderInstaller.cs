using UnityEngine;
using Zenject;

namespace Game.Scripts.Core.SceneLoader
{
    public class SceneLoaderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<SceneStartedLoading>().OptionalSubscriber();

            BindSceneLoader();
        }

        private void BindSceneLoader()
        {
            Container
                .BindInterfacesAndSelfTo<SceneLoader>()
                .AsSingle()
                .NonLazy();
        }
    }
}