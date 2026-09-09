using Game.Scripts.Core.SceneLoader;
using Zenject;

namespace Game.Scripts.Core.Bootstrap
{
    public class Bootstrapper : IInitializable
    {
        private SceneLoader.SceneLoader _sceneLoader;
        private SceneLoaderSettings _settings;
        
        public Bootstrapper(SceneLoaderSettings settings, SceneLoader.SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _settings = settings;
        }
        
        public void Initialize()
        {
            _sceneLoader.StartSceneLoading(_settings);
        }
    }
}