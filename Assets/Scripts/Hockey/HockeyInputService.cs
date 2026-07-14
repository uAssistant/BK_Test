using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Hockey
{
    public class HockeyInputService : IInitializable, IDisposable
    {
        private readonly InputAction _pressAction;
        private readonly InputAction _pointerPositionAction;

        public event Action<Vector2> PointerPressed;
        public event Action<Vector2> PointerMoved;
        public event Action<Vector2> PointerReleased;

        public Vector2 CurrentPointerPosition =>
            _pointerPositionAction.ReadValue<Vector2>();

        public HockeyInputService(
            [Inject(Id = "Press")] InputActionReference pressActionReference,
            [Inject(Id = "PointerPosition")] InputActionReference pointerPositionActionReference)
        {
            _pressAction = pressActionReference.action;
            _pointerPositionAction = pointerPositionActionReference.action;
        }

        public void Initialize()
        {
            _pressAction.started += OnPressStarted;
            _pressAction.canceled += OnPressCanceled;
            _pointerPositionAction.performed += OnPointerPositionPerformed;

            _pressAction.Enable();
            _pointerPositionAction.Enable();
        }

        private void OnPressStarted(InputAction.CallbackContext context)
        {
            PointerPressed?.Invoke(CurrentPointerPosition);
        }

        private void OnPressCanceled(InputAction.CallbackContext context)
        {
            PointerReleased?.Invoke(CurrentPointerPosition);
        }

        private void OnPointerPositionPerformed(InputAction.CallbackContext context)
        {
            var screenPosition = context.ReadValue<Vector2>();
            PointerMoved?.Invoke(screenPosition);
        }
        
        public void Dispose()
        {
            _pressAction.started -= OnPressStarted;
            _pressAction.canceled -= OnPressCanceled;
            _pointerPositionAction.performed -= OnPointerPositionPerformed;

            _pressAction.Disable();
            _pointerPositionAction.Disable();
        }
    }
}