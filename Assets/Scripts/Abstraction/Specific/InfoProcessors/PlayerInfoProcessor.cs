using Scripts.Input;
using UnityEngine;
using Zenject;

namespace Scripts.Abstraction
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