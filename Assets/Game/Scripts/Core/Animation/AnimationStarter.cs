using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Game.Scripts.Core.Animation
{
    public class AnimationStarter
    {
        public void Play(IReadOnlyList<AnimatableWrapper> animatables, Action onComplete, MonoBehaviour addTo)
        {
            if (animatables.Count == 0)
                return;

            var animationStreams =
                animatables.Select(a => a.Interface.Animation.Play());

            animationStreams.WhenAll()
                .Subscribe(_ => { onComplete?.Invoke(); })
                .AddTo(addTo);
        }
    }
}