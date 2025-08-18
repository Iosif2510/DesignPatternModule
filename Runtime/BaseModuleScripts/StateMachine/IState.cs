namespace _2510.DesignPatternModule.StateMachine
{
    public interface IState<TStateEnum> where TStateEnum : System.Enum
    {
        public TStateEnum StateKey { get; }
        public bool IsComplete { get; }
        public void Enter();
        public void Exit();
        public void Update();
        public void PhysicsUpdate();
    }
}

