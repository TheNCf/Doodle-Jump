using System;
using DG.Tweening;
using Game.Scripts.Core.Animation.Views;
using UniRx;
using UnityEngine.UI;

namespace Game.Scripts.Core.Animation.Effectors
{
    public class ImageFadeAnimation : IAnimation, IDisposable
    {
        private readonly Image _target;
        private readonly float _to;
        private readonly float _duration;
        private readonly float _delay;
        private readonly Ease _ease;

        private Tween _tween;

        public ImageFadeAnimation(ImageFadeView view)
        {
            _target = view.Image;
            _to = view.To;
            _duration = view.AnimationData.Duration;
            _delay = view.AnimationData.Delay;
            _ease = view.AnimationData.Ease;
        }

        public bool IsPlaying =>
            _tween != null &&
            _tween.IsActive() &&
            _tween.IsPlaying();

        public IObservable<Unit> Play()
        {
            Stop();

            return Observable.Create<Unit>(observer =>
            {
                _tween = _target
                    .DOFade(_to, _duration)
                    .SetDelay(_delay)
                    .SetEase(_ease)
                    .OnComplete(() =>
                    {
                        observer.OnNext(Unit.Default);
                        observer.OnCompleted();
                    });

                return Disposable.Create(Stop);
            });
        }

        public void Stop()
        {
            _tween?.Kill();
            _tween = null;
        }

        public void Dispose()
        {
            Stop();
        }
    }
}