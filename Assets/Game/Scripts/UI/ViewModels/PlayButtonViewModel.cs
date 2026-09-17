using Game.Scripts.Core.SceneLoader;
using MVVM;

namespace Game.Scripts.UI.ViewModels
{
    public class PlayButtonViewModel
    {
        private readonly SceneLoader _sceneLoader;
        private readonly SceneLoaderSettings _sceneLoaderSettings;

        public PlayButtonViewModel(SceneLoader sceneLoader, SceneLoaderSettings sceneLoaderSettings)
        {
            _sceneLoader = sceneLoader;
            _sceneLoaderSettings = sceneLoaderSettings;
        }

        [Method("OnToPlayClick")]
        public void OnStartClicked()
        {
            _sceneLoader.StartSceneLoading(_sceneLoaderSettings.SceneToLoad);
            _sceneLoader.AllowSceneActivation();
        }
    }
}