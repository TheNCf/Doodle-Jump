using System;
using Game.Scripts.Core.Animation.Effectors;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Core.Animation.Views
{
    [Serializable]
    public class RectTransformDropView : MonoBehaviour, IAnimatable<RectTransformDropEffector>
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _image;
        [SerializeField] private ObjectAnimationData _data;
    
        public RectTransform RectTransform => _rectTransform;
        public Image Image => _image;
        public ObjectAnimationData AnimationData => _data;
        public RectTransformDropEffector AnimationStarter { get; private set; } 
        
        [Inject]
        public void Construct(RectTransformDropEffector rectTransformDropEffector)
        {
            AnimationStarter = rectTransformDropEffector;
        }
    }
}
