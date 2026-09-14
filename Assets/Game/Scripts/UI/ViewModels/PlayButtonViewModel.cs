using Game.Scripts.Core.SceneLoader;
using MVVM;
using Zenject;

namespace Game.Scripts.UI.ViewModels
{
    public class PlayButtonViewModel
    {
        private SceneLoader _sceneLoader;
        private SceneLoaderSettings _sceneLoaderSettings;
        
        public PlayButtonViewModel(SceneLoader sceneLoader, SceneLoaderSettings sceneLoaderSettings)
        {
            _sceneLoader = sceneLoader;
            _sceneLoaderSettings = sceneLoaderSettings;
        }

        [Method("OnToPlayClick")]
        public void OnStartClicked()
        {
            _sceneLoader.StartSceneLoading(_sceneLoaderSettings.SceneToLoad);
        }
    }
}