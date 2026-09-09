using System;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace Game.Scripts.Core.Animation.Effectors
{
    public class ImageDropEffector : IAnimationStarter
    {
        private float _shrinkFraction = 0.3f; 
        private float _inflateFraction = 0.7f; 
        
        public event Action EffectFinished;

        public void Activate(IAnimatable<IAnimationStarter> animatable)
        {
            animatable.RectTransform.localScale = Vector3.one * animatable.AnimationData.StartValue;
            
            Observable
                .Timer(TimeSpan.FromSeconds(animatable.AnimationData.Delay))
                .Subscribe(_ => StartEffect(animatable))
                .AddTo(animatable.RectTransform.gameObject);
        }

        private void StartEffect(IAnimatable<IAnimationStarter> animatable)
        {
            ObjectAnimationData data = animatable.AnimationData;
            DOTween.Sequence()
                .Append(animatable.RectTransform.DOScale(data.MinValue, data.Duration * _shrinkFraction).SetEase(Ease.Linear))
                .Append(animatable.RectTransform.DOScale(data.EndValue, data.Duration * _inflateFraction).SetEase(Ease.Linear))
                .OnComplete(() => EffectFinished?.Invoke())
                .SetLink(animatable.RectTransform.gameObject)
                .Play();
        }
    }
}