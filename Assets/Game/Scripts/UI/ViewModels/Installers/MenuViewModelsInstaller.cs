using Game.Scripts.Core.SceneLoader;
using Game.Scripts.UI.ViewModels.Signals;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI.ViewModels
{
    public class MenuViewModelsInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoaderSettings _gameSceneLoaderSettings;

        public override void InstallBindings()
        {
            Container.DeclareSignal<PlayButtonPressedSignal>();

            BindPlayButtonViewModel();
        }

        private void BindPlayButtonViewModel()
        {
            Container.Bind<PlayButtonViewModel>()
                .AsSingle()
                .WithArguments(_gameSceneLoaderSettings)
                .NonLazy();
        }
    }
}