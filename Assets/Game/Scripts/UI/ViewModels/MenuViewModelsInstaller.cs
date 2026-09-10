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
            BindStartButtonViewModel();
        }

        private void BindStartButtonViewModel()
        {
            Container.Bind<StartButtonViewModel>()
                .AsSingle()
                .WithArguments(_gameSceneLoaderSettings)
                .NonLazy();
        }
    }
}