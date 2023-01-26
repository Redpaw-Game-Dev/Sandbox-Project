using System;
using Scripts.Abstraction;

namespace Scripts.StateMachineSystem
{
    public interface IState
    {
        public event Action OnEntered;
        public event Action OnExited;
        
        public void Enter();
        public void Tick();
        public void Exit();
    }
}