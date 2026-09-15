using System;
using Game.Scripts.Core.Animation.Effectors;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Core.Animation.Views
{
    [Serializable]
    public class RectTransformDropView : MonoBehaviour, IAnimatable
    {
        [field: SerializeField] public RectTransform RectTransform { get; private set; }
        [field: SerializeField] public ObjectAnimationData AnimationData { get; private set; }
        [field: SerializeField] public float From { get; private set; }
        [field: SerializeField] public float Min { get; private set; }
        [field: SerializeField] public float To { get; private set; }
        [field: SerializeField] public float ToMinDurationFraction { get; private set; }
        public IAnimation Animation => new RectTransformDropAnimation(this);
    }
}