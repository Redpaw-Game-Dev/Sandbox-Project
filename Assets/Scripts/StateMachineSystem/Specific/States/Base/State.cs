using System;

namespace Sandbox.StateMachineSystem
{
    public abstract class State : IState
    {
        private bool _isInitialized;
        
        public event Action OnEntered;
        public event Action OnExited;
        
        protected abstract void Initialize();
        protected abstract void EnterActions();
        protected abstract void TickActions();
        protected abstract void FixedTickActions();
        protected abstract void LateTickActions();
        protected abstract void ExitActions();
        
        public void Enter()
        {
            if (!_isInitialized)
            {
                Initialize();
                _isInitialized = true;
            }
            EnterActions();
            OnEntered?.Invoke();
        }

        public void Tick()
        {
            TickActions();
        }

        public void FixedTick()
        {
            FixedTickActions();
        }

        public void LateTick()
        {
            LateTickActions();
        }

        public void Exit()
        {
            ExitActions();
            OnExited?.Invoke();
        }
    }
}