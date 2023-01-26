using Scripts.Input;
using UnityEngine;
using Zenject;

namespace Scripts.StateMachineSystem
{
    public class InputAxisZeroCondition : Condition
    {
        private InputManager _inputManager;

        [Inject]
        private void Construct(InputManager inputManager)
        {
            _inputManager = inputManager;
        }

        public override bool IsFulfilled()
        {
            return _logicalNot 
                ? _inputManager.MoveAxis != Vector2.zero 
                : _inputManager.MoveAxis == Vector2.zero;
        }
    }
}