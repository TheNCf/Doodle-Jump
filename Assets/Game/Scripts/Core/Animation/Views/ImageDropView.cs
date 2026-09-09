using System;
using Game.Scripts.Core.Animation.Effectors;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Core.Animation.Views
{
    [Serializable]
    public class ImageDropView : MonoBehaviour, IAnimatable<ImageDropEffector>
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _image;
        [SerializeField] private ObjectAnimationData _data;
    
        public RectTransform RectTransform => _rectTransform;
        public Image Image => _image;
        public ObjectAnimationData AnimationData => _data;
        public ImageDropEffector AnimationStarter { get; private set; } 
        
        [Inject]
        public void Construct(ImageDropEffector imageDropEffector)
        {
            AnimationStarter = imageDropEffector;
        }
    }
}
