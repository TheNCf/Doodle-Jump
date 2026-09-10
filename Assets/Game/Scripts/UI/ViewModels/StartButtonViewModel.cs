using Game.Scripts.Core.SceneLoader;
using MVVM;
using Zenject;

namespace Game.Scripts.UI.ViewModels
{
    public class StartButtonViewModel
    {
        private SceneLoader _sceneLoader;
        private SceneLoaderSettings _sceneLoaderSettings;
        
        public StartButtonViewModel(SceneLoader sceneLoader, SceneLoaderSettings sceneLoaderSettings)
        {
            _sceneLoader = sceneLoader;
            _sceneLoaderSettings = sceneLoaderSettings;
        }

        [Method("OnStartClick")]
        public void OnStartClicked()
        {
            _sceneLoader.StartSceneLoading(_sceneLoaderSettings.SceneToLoad);
        }
    }
}