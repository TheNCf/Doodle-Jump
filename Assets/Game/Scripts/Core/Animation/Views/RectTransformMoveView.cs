using Game.Scripts.Core.Animation.Effectors;
using UnityEngine;

namespace Game.Scripts.Core.Animation.Views
{
    public class RectTransformMoveView : MonoBehaviour, IAnimatable
    {
        [field: SerializeField] public RectTransform RectTransform { get; private set; }
        [field: SerializeField] public ObjectAnimationData AnimationData { get; private set; }
        [field: SerializeField] public Vector3 To { get; private set; }

        public IAnimation Animation
        {
            get
            {
                if (_animation == null)
                    _animation = new RectTransformMoveAnimation(this);
                
                return _animation;
            }
        }

        private IAnimation _animation;
    }
}