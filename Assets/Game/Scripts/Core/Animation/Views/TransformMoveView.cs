using Game.Scripts.Core.Animation.Effectors;
using UnityEngine;

namespace Game.Scripts.Core.Animation.Views
{
    public class TransformMoveView : MonoBehaviour, IAnimatable
    {
        [field: SerializeField] public Vector3 Delta { get; private set; }

        private IAnimation _animation;
        public Transform Transform => transform;
        [field: SerializeField] public ObjectAnimationData AnimationData { get; private set; }

        public IAnimation Animation
        {
            get
            {
                if (_animation == null)
                    _animation = new TransformMoveAnimation(this);

                return _animation;
            }
        }
    }
}