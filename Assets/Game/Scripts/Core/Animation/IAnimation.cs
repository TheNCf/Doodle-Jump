using System;
using UniRx;

namespace Game.Scripts.Core.Animation
{
    public interface IAnimation
    {
        bool IsPlaying { get; }

        IObservable<Unit> Play();

        void Stop();
    }
}