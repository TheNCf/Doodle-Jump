using DG.Tweening;
using Game.Scripts.Core.Animation.Effectors;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Core.Animation.Views
{
    public class RectTransformMoveView : MonoBehaviour, IAnimatable
    {
        [field: SerializeField] public RectTransform RectTransform { get; private set; }
        [field: SerializeField] public ObjectAnimationData AnimationData { get; private set; }
        [field: SerializeField] public Vector3 To { get; private set; }
        public IAnimation Animation => new RectTransformMoveAnimation(this);
    }
}