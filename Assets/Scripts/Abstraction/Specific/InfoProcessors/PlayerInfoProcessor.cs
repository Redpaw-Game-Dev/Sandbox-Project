using Scripts.Input;
using Zenject;

namespace Scripts.Abstraction
{
    public class PlayerInfoProcessor : InfoProcessor
    {
        [Inject] private InputManager _inputManager;
        
        private void HandleMove(Vector2Info info)
        {
            Process(info);
        }

        private void OnEnable()
        {
            _inputManager.OnMove += HandleMove;
        }

        private void OnDisable()
        {
            _inputManager.OnMove -= HandleMove;
        }
    }
}