using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Core.Animation
{
    public interface IAnimatable<out T> where T : IAnimationStarter
    {
        public ObjectAnimationData AnimationData { get; }
        public RectTransform RectTransform { get; }
        public Image Image { get; }
        public T AnimationStarter { get; }
    }
}