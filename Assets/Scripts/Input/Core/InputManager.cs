using System;
using Sandbox.Abstraction;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Sandbox.Input
{
    public class InputManager : IInitializable, ITickable
    {
        private InputActionsConfig _inputActionsConfig;
        private bool _isMoving;
        private Vector2 _moveAxis;

        public InputActionsConfig InputActionsConfig => _inputActionsConfig;
        public Vector2 MoveAxis => _moveAxis;
        
        public event Action<InputAxisInfo> OnMoveTick;
        public event Action<InputAxisInfo> OnMoveAxisChanged;

        public void Initialize()
        {
            _inputActionsConfig = new InputActionsConfig();
            _inputActionsConfig.Base.Move.started += HandleMoveStarted;
            _inputActionsConfig.Base.Move.performed += HandleMovePerformed;
            _inputActionsConfig.Base.Move.canceled += HandleMoveCanceled;
            _inputActionsConfig.Base.Enable();
        }
        
        public void Tick()
        {
            if (_isMoving)
            {
                var moveInput = _inputActionsConfig.Base.Move.ReadValue<Vector2>();
                if (moveInput != Vector2.zero)
                { 
                    OnMoveTick?.Invoke(new InputAxisInfo(moveInput));
                }
               
            }
        }
        
        private void HandleMoveStarted(InputAction.CallbackContext context)
        {
            _isMoving = true;
        }
        
        private void HandleMovePerformed(InputAction.CallbackContext context)
        {
            _moveAxis = context.ReadValue<Vector2>();
            OnMoveAxisChanged?.Invoke(new InputAxisInfo(_moveAxis));
        }
        
        private void HandleMoveCanceled(InputAction.CallbackContext context)
        {
            _isMoving = false;
            _moveAxis = Vector2.zero;
            OnMoveAxisChanged?.Invoke(new InputAxisInfo(_moveAxis));
            
        }

    }
}