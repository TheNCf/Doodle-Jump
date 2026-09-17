using System.Collections.Generic;
using Game.Scripts.Core.Animation;
using UnityEngine;

namespace Game.Scripts.Core.Bootstrap
{
    public class BootstrapView : MonoBehaviour
    {
        [SerializeField] private List<AnimatableWrapper> _animatables = new();

        public IReadOnlyList<AnimatableWrapper> Animatables => _animatables;
    }
}