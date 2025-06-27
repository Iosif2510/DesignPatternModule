namespace _2510.DesignPatternModule.StateMachine
{
    public abstract class StateMachine
    {
        protected IState currentState;

        public IState CurrentState => currentState;

        protected abstract IState InitialState { get; }
        
        public void EnterMachine()
        {
            ChangeState(InitialState);
        }

        /// <summary>
        /// Executes transition to a new state.
        /// </summary>
        /// <param name="newState">Next <see cref="IState"/> to start</param>
        public virtual void ChangeState(IState newState)
        {
            if (currentState == newState) return;
            currentState?.Exit();

            currentState = newState;

            currentState?.Enter();
        }

        public void Update()
        {
            currentState?.Update();
        }

        public void PhysicsUpdate()
        {
            currentState?.PhysicsUpdate();
        }
    }
}