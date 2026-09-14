using Game.Scripts.Core.SceneLoader;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI.ViewModels
{
    public class GameViewModelsInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoaderSettings _gameSceneLoaderSettings;
        [SerializeField] private SceneLoaderSettings _menuSceneLoaderSettings;

        public override void InstallBindings()
        {
            BindScoreViewModel();
            BindPlayButtonViewModel();
            BindMenuButtonViewModel();
        }

        private void BindScoreViewModel()
        {
            Container
                .BindInterfacesAndSelfTo<ScoreViewModel>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindPlayButtonViewModel()
        {
            Container.Bind<PlayButtonViewModel>()
                .AsSingle()
                .WithArguments(_gameSceneLoaderSettings)
                .NonLazy();
        }
        
        private void BindMenuButtonViewModel()
        {
            Container.Bind<MenuButtonViewModel>()
                .AsSingle()
                .WithArguments(_menuSceneLoaderSettings)
                .NonLazy();
        }
    }
}