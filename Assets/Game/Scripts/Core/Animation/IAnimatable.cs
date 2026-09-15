using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Core.Animation
{
    public interface IAnimatable
    {
        public ObjectAnimationData AnimationData { get; }
        public IAnimation Animation { get; }
    }
}