using System;

namespace Game.Scripts.Core
{
    public interface IInputService
    {
        public float HorizontalInput { get; }
        public event Action ShootPressed;
    }
}