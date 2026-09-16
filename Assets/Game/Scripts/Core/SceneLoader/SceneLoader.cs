using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Scripts.Core.SceneLoader
{
    public class SceneLoader
    {
        private SignalBus _signalBus;

        private AsyncOperation _loadingOperation;

        public SceneLoader(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void StartSceneLoading(SceneName sceneName)
        {
            _loadingOperation = SceneManager.LoadSceneAsync((int)sceneName, LoadSceneMode.Single);

            if (_loadingOperation != null)
            {
                _loadingOperation.allowSceneActivation = false;
                _signalBus.Fire(new SceneStartedLoading(sceneName));
            }
            else
            {
                Debug.LogError("[SceneLoader] Could not load scene " + sceneName);
            }
        }

        public void AllowSceneActivation()
        {
            if (_loadingOperation != null)
                _loadingOperation.allowSceneActivation = true;
        }
    }
}