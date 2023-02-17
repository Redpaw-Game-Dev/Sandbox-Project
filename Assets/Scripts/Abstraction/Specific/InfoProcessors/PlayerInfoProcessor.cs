using Sandbox.Input;
using UnityEngine;
using Zenject;

namespace Sandbox.Abstraction
{
    public class PlayerInfoProcessor : InfoProcessor
    {
        [Inject] private InputManager _inputManager;
        
        private void HandleMoveTick(InputAxisInfo inputAxis)
        {
            Process(inputAxis);
        }

        private void OnEnable()
        {
            _inputManager.OnMoveAxisChanged += HandleMoveTick;
        }

        private void OnDisable()
        {
            _inputManager.OnMoveAxisChanged -= HandleMoveTick;
        }
    }
}