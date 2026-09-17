using System;
using Game.Scripts.Core.Animation.Effectors;
using UnityEngine;

namespace Game.Scripts.Core.Animation.Views
{
    [Serializable]
    public class RectTransformDropView : MonoBehaviour, IAnimatable
    {
        [field: SerializeField] public RectTransform RectTransform { get; private set; }
        [field: SerializeField] public float From { get; private set; }
        [field: SerializeField] public float Min { get; private set; }
        [field: SerializeField] public float To { get; private set; }
        [field: SerializeField] public float ToMinDurationFraction { get; private set; }

        private IAnimation _animation;
        [field: SerializeField] public ObjectAnimationData AnimationData { get; private set; }

        public IAnimation Animation
        {
            get
            {
                if (_animation == null)
                    _animation = new RectTransformDropAnimation(this);

                return _animation;
            }
        }
    }
}