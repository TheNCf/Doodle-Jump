using UnityEngine;
using Zenject;

namespace Game.Scripts.Bootstrap
{
    public class ImageDropView : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private ImageDropData _data;
    
        public RectTransform RectTransform => _rectTransform;
        public ImageDropData Data => _data;
        public ImageDropEffector ImageDropEffector { get; private set; }
        
        [Inject]
        public void Construct(ImageDropEffector imageDropEffector)
        {
            ImageDropEffector = imageDropEffector;
        }
    }
}
