using Game.Scripts.Core.SceneLoader;
using MVVM;

namespace Game.Scripts.UI.ViewModels
{
    public class MenuButtonViewModel
    {
        private SceneLoader _sceneLoader;
        private SceneLoaderSettings _sceneLoaderSettings;
        
        public MenuButtonViewModel(SceneLoader sceneLoader, SceneLoaderSettings sceneLoaderSettings)
        {
            _sceneLoader = sceneLoader;
            _sceneLoaderSettings = sceneLoaderSettings;
        }

        [Method("OnToMenuClick")]
        public void OnStartClicked()
        {
            _sceneLoader.StartSceneLoading(_sceneLoaderSettings.SceneToLoad);
        }
    }
}