using System.Collections.Generic;
using Game.Scripts.Core.Animation;
using UnityEngine;

namespace Game.Scripts.Core.SceneLoader
{
    public class SceneLoaderView : MonoBehaviour
    {
        [SerializeField] private List<AnimatableWrapper> _animatables;
        
        public IReadOnlyList<AnimatableWrapper> Animatables => _animatables;
    }
}