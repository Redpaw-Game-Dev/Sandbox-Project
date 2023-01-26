using System;

namespace Scripts.StateMachineSystem
{
    public class IdleState : IState
    {
        public event Action OnEntered;
        public event Action OnExited;

        public void Enter()
        {
            OnEntered?.Invoke();;
        }

        public void Tick()
        {
            
        }

        public void Exit()
        {
            OnExited?.Invoke();
        }
        
        public override bool Equals(object obj)
        {
            return obj is IdleState;
        }

        public override int GetHashCode()
        {
            return nameof(IdleState).GetHashCode();
        }
    }
}