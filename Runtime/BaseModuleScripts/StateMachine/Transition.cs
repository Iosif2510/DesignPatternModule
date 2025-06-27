namespace _2510.DesignPatternModule.StateMachine
{
    public class Transition
    {
        private IState _previousState;
        private IState _nextState;
        private StateMachine _machine;

        public Transition(StateMachine machine, IState previousState, IState nextState)
        {
            _machine = machine;
            _previousState = previousState;
            _nextState = nextState;
        }
        
        public void DoTransition()
        {
            if (_machine.CurrentState == _nextState) return;
            _previousState?.Exit();
            _machine.ChangeState(_nextState);
            _nextState?.Enter();
        }
    }
}