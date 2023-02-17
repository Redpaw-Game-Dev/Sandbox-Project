using Sandbox.StateMachineSystem;
using UnityEngine;

namespace Sandbox.Abstraction
{
    public class TryChangeStateTask : Task
    {
        [SerializeField] private StateMachine _stateMachine;
        
        public override void Do(IInfo info = null)
        {
            _stateMachine.TryChangeState();
        }
    }
}