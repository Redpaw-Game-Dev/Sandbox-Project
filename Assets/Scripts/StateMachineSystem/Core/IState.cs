using System;
using Sandbox.Abstraction;

namespace Sandbox.StateMachineSystem
{
    public interface IState
    {
        public event Action OnEntered;
        public event Action OnExited;
        
        public void Enter();
        public void Tick();
        public void FixedTick();
        public void LateTick();
        public void Exit();
    }
}