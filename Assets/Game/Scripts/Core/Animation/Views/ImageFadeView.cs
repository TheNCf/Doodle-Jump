using Game.Scripts.Core.Animation.Effectors;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Core.Animation.Views
{
    public class ImageFadeView : MonoBehaviour, IAnimatable
    {
        [field: SerializeField] public Image Image { get; private set; }
        [field: SerializeField] public ObjectAnimationData AnimationData { get; private set; }
        [field: SerializeField] public float To { get; private set; }

        public IAnimation Animation => new ImageFadeAnimation(this);
    }
}