namespace Scripts.StateMachineSystem
{
    public class IdleState : State
    {

        protected override void Initialize() { }

        protected override void EnterActions() { }

        protected override void TickActions() { }

        protected override void FixedTickActions() { }

        protected override void LateTickActions() { }

        protected override void ExitActions() { }

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