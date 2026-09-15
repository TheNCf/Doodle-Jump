using System;
using DG.Tweening;
using Game.Scripts.Core.Animation.Views;
using UniRx;
using UnityEngine;

namespace Game.Scripts.Core.Animation.Effectors
{
    public class RectTransformDropAnimation : IAnimation, IDisposable
    {
        private readonly RectTransform _target;
        private readonly float _from;
        private readonly float _min;
        private readonly float _to;
        private readonly float _duration;
        private readonly float _toMinDurationFraction;
        private readonly float _delay;
        private readonly Ease _ease;

        private Tween _tween;

        public RectTransformDropAnimation(RectTransformDropView view)
        {
            _target = view.RectTransform;
            _from = view.From;
            _min = view.Min;
            _to = view.To;
            _toMinDurationFraction = view.ToMinDurationFraction;
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
                _target.localScale = Vector3.one * _from;

                _tween = DOTween.Sequence()
                    .AppendInterval(_delay)
                    .Append(
                        _target
                            .DOScale(_min, _duration * _toMinDurationFraction)
                            .SetEase(_ease)
                    )
                    .Append(
                        _target
                            .DOScale(_to, _duration * (1 - _toMinDurationFraction))
                            .SetEase(_ease)
                    )
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