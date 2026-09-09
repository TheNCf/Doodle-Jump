using System;
using Game.Scripts.Core.Animation;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Core.SceneLoader
{
    public class SceneLoader
    {
        private SceneLoaderView _sceneLoaderView;
        
        private AsyncOperation _loadingOperation;
        
        public event Action AnimationsFinished;
        
        public SceneLoader(SceneLoaderView sceneLoaderView)
        {
            _sceneLoaderView = sceneLoaderView;

        }

        public void StartSceneLoading(SceneLoaderSettings sceneLoaderSettings)
        {
            _loadingOperation = SceneManager.LoadSceneAsync(sceneLoaderSettings.SceneToLoad, LoadSceneMode.Single);

            if (_loadingOperation != null)
            {
                _loadingOperation.allowSceneActivation = false;
                AnimationsFinished += () => _loadingOperation.allowSceneActivation = true;
            }
            
            StartAnimation();
        }

        private void StartAnimation()
        {
            float maxSpentTime = -1.0f;
            IAnimatable<IAnimationStarter> longestAnimation = null;

            if (_sceneLoaderView.Animatables.Count == 0)
            {
                AnimationsFinished?.Invoke();
                return;
            }

            foreach (AnimatableWrapper animatableWrapper in _sceneLoaderView.Animatables)
            {
                IAnimatable<IAnimationStarter> animatable = animatableWrapper.Interface;
                
                float spentTime = animatable.AnimationData.Delay + animatable.AnimationData.Duration;

                if (spentTime > maxSpentTime)
                {
                    maxSpentTime = spentTime;
                    longestAnimation = animatable;
                }
            }
            
            if (longestAnimation != null)
            {
                Action handler = null;
                
                handler = () =>
                {
                    longestAnimation.AnimationStarter.EffectFinished -= handler;
                    AnimationsFinished?.Invoke();
                };
                
                longestAnimation.AnimationStarter.EffectFinished += handler;
            }

            foreach (AnimatableWrapper animatableWrapper in _sceneLoaderView.Animatables)
            {
                IAnimatable<IAnimationStarter> animatable = animatableWrapper.Interface;
                animatable.AnimationStarter.Activate(animatable);
            }
        }
    }
}