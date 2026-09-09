using System;

namespace Game.Scripts.Core.Animation
{
    public interface IAnimationStarter
    {
        public event Action EffectFinished;
        
        public void Activate(IAnimatable<IAnimationStarter> animatable);
    }
}