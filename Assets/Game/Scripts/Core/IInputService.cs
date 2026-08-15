using System;

namespace Game.Scripts.Core
{
    public interface IInputService
    {
        public event Action ShootPressed;
        public float HorizontalInput { get; }
    }
}