using System;
using DG.Tweening;
using Game.Scripts.Core.Animation.Views;
using UniRx;
using UnityEngine;

namespace Game.Scripts.Core.Animation.Effectors
{
    public class TransformMoveAnimation : IAnimation, IDisposable
    {
        private readonly Transform _target;
        private readonly Vector2 _delta;
        private readonly float _duration;
        private readonly float _delay;
        private readonly Ease _ease;

        private Tween _tween;

        public TransformMoveAnimation(TransformMoveView view)
        {
            _target = view.Transform;
            _delta = view.Delta;
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
                    .DOMove(_delta + (Vector2)_target.position, _duration)
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