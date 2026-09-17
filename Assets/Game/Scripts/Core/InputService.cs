using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Scripts.Core
{
    public class InputService : IInputService, IInitializable, IDisposable
    {
        private readonly PlayerInputActions _inputActions;

        public InputService()
        {
            _inputActions = new PlayerInputActions();
        }

        public void Dispose()
        {
            _inputActions.Disable();

            UnsubscribeFromInputEvents();

            _inputActions.Dispose();
        }

        public void Initialize()
        {
            _inputActions.Enable();

            if (Accelerometer.current != null)
                InputSystem.EnableDevice(Accelerometer.current);

            SubscribeToInputEvents();
        }

        public event Action ShootPressed;

        public float HorizontalInput { get; private set; }

        private void SubscribeToInputEvents()
        {
            _inputActions.Player.Movement.started += OnMoveInput;
            _inputActions.Player.Movement.performed += OnMoveInput;
            _inputActions.Player.Movement.canceled += OnMoveInput;

            _inputActions.Player.Shoot.started += OnShootInput;
        }

        private void UnsubscribeFromInputEvents()
        {
            _inputActions.Player.Movement.started -= OnMoveInput;
            _inputActions.Player.Movement.performed -= OnMoveInput;
            _inputActions.Player.Movement.canceled -= OnMoveInput;

            _inputActions.Player.Shoot.started -= OnShootInput;
        }

        private void OnMoveInput(InputAction.CallbackContext obj)
        {
            HorizontalInput = obj.ReadValue<Vector3>().x;
        }

        private void OnShootInput(InputAction.CallbackContext obj)
        {
            ShootPressed?.Invoke();
        }
    }
}