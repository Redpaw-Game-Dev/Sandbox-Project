using Scripts.Input;
using Zenject;

namespace Scripts.Abstraction
{
    public class PlayerInfoProcessor : InfoProcessor
    {
        [Inject] private InputManager _inputManager;
        
        private void HandleMoveTick(Vector2Info info)
        {
            Process(info);
        }

        private void OnEnable()
        {
            _inputManager.OnMoveTick += HandleMoveTick;
        }

        private void OnDisable()
        {
            _inputManager.OnMoveTick -= HandleMoveTick;
        }
    }
}