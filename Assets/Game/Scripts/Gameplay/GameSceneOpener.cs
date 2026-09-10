using System;
using Game.Scripts.Core.SceneLoader;
using Game.Scripts.Gameplay.Signals;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class GameSceneOpener : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private SceneLoaderSettings _sceneLoaderSettings;
        private SceneLoader _sceneLoader;
        
        public GameSceneOpener(SignalBus signalBus, SceneLoaderSettings sceneLoaderSettings, SceneLoader sceneLoader)
        {
            _signalBus = signalBus;
            _sceneLoaderSettings = sceneLoaderSettings;
            _sceneLoader = sceneLoader;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<OpenGameSceneSignal>(LoadScene);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<OpenGameSceneSignal>(LoadScene);
        }

        private void LoadScene()
        {
            _sceneLoader.StartSceneLoading(_sceneLoaderSettings.SceneToLoad);
        }
    }
}