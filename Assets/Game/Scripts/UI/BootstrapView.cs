using System.Collections.Generic;
using Game.Scripts.UI.Views;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class BootstrapView : MonoBehaviour
    {
        [SerializeField] private List<ImageDropView> _imageDropViews;
        
        public IReadOnlyList<ImageDropView> ImageDropViews => _imageDropViews;
    }
}