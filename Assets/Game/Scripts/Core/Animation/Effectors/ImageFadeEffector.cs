using System;
using DG.Tweening;
using UniRx;

namespace Game.Scripts.Core.Animation.Effectors
{
    public class ImageFadeEffector : IAnimationStarter
    {
        public event Action EffectFinished;
        public void Activate(IAnimatable<IAnimationStarter> animatable)
        {
            Observable
                .Timer(TimeSpan.FromSeconds(animatable.AnimationData.Delay))
                .Subscribe(_ => StartEffect(animatable))
                .AddTo(animatable.RectTransform.gameObject);
        }
        
        private void StartEffect(IAnimatable<IAnimationStarter> animatable)
        {
            ObjectAnimationData data = animatable.AnimationData;
            DOTween.Sequence()
                .Append(animatable.Image.DOFade(data.EndValue, data.Duration).SetEase(Ease.Linear))
                .OnComplete(() => EffectFinished?.Invoke())
                .SetLink(animatable.RectTransform.gameObject)
                .Play();
        }
    }
}