using System;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
    public interface IMovable
    {
        public event Action<Collider2D> EnteredTrigger;
        public Transform Transform { get; }
        
        public float HorizontalSpeed { get; }
        public float FallSpeed { get; }
    }
}