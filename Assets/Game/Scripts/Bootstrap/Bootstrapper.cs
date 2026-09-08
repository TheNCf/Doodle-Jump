using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Scripts.Bootstrap
{
    public class Bootstrapper : IInitializable
    {
        private BootstrapView _bootstrapView;
        private BootstrapSettings _bootstrapSettings;
        
        private AsyncOperation _loadingOperation;
        
        public event Action AnimationsFinished;
        
        public Bootstrapper(BootstrapView bootstrapView, BootstrapSettings bootstrapSettings)
        {
            _bootstrapView = bootstrapView;
            _bootstrapSettings = bootstrapSettings;
        }

        public void Initialize()
        {
            StartSceneLoading();
            StartAnimation();
        }

        private void StartSceneLoading()
        {
            _loadingOperation = SceneManager.LoadSceneAsync(_bootstrapSettings.SceneToLoad, LoadSceneMode.Single);

            if (_loadingOperation != null)
            {
                _loadingOperation.allowSceneActivation = false;
                AnimationsFinished += () => _loadingOperation.allowSceneActivation = true;
            }
        }

        private void StartAnimation()
        {
            float maxSpentTime = -1.0f;
            ImageDropView longestAnimationView = null;

            foreach (ImageDropView imageDropView in _bootstrapView.ImageDropViews)
            {
                float spentTime = imageDropView.Data.Delay + imageDropView.Data.ShrinkDuration;

                if (spentTime > maxSpentTime)
                {
                    maxSpentTime = spentTime;
                    longestAnimationView = imageDropView;
                }
            }
            
            if (longestAnimationView)
            {
                Action handler = null;
                
                handler = () =>
                {
                    longestAnimationView.ImageDropEffector.EffectFinished -= handler;
                    AnimationsFinished?.Invoke();
                };
                
                longestAnimationView.ImageDropEffector.EffectFinished += handler;
            }

            foreach (ImageDropView imageDropView in _bootstrapView.ImageDropViews)
                imageDropView.ImageDropEffector.Activate(imageDropView);
        }
    }
}