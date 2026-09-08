using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Bootstrap
{
    public class BootstrapView : MonoBehaviour
    {
        [SerializeField] private List<ImageDropView> _imageDropViews;
        
        public IReadOnlyList<ImageDropView> ImageDropViews => _imageDropViews;
    }
}