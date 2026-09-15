using System.Collections.Generic;
using Game.Scripts.Core.Animation;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
    public class LoseMenuView : MonoBehaviour
    {
        [SerializeField] private List<AnimatableWrapper> _animatables;
        public IReadOnlyList<AnimatableWrapper> Animatables => _animatables;
    }
}