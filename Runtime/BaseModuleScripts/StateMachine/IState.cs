namespace _2510.DesignPatternModule.StateMachine
{
    public interface IState
    {
        public bool IsComplete { get; }
        public void Enter();
        public void Exit();
        public void Update();
        public void PhysicsUpdate();
    }
}

