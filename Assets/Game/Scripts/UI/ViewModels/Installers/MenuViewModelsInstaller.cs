using Game.Scripts.Core.SceneLoader;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI.ViewModels
{
    public class MenuViewModelsInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoaderSettings _gameSceneLoaderSettings;
        
        public override void InstallBindings()
        {
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