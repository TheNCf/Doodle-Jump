using Game.Scripts.Core.Animation.Effectors;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Core.Animation.Views
{
    public class ImageFadeView : MonoBehaviour, IAnimatable<ImageFadeEffector>
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _image;
        [SerializeField] private ObjectAnimationData _data;

        public ObjectAnimationData AnimationData => _data;
        public RectTransform RectTransform => _rectTransform;
        public Image Image => _image;
        public ImageFadeEffector AnimationStarter { get; private set; }
        
        [Inject]
        public void Construct(ImageFadeEffector imageFadeEffector)
        {
            AnimationStarter = imageFadeEffector;
        }
    }
}