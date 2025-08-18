using System.Collections.Generic;

namespace _2510.DesignPatternModule.BehaviourTree
{
    public class StateMachineUpdateNode<TStateEnum> : BehaviourNode where TStateEnum : System.Enum
    {
        private readonly StateMachine.StateMachine<TStateEnum> _stateMachine;
        
        public StateMachineUpdateNode(StateMachine.StateMachine<TStateEnum> stateMachine)
        {
            _stateMachine = stateMachine;
        }

        protected override void Enter()
        {
            base.Enter();
            _stateMachine.EnterMachine();
        }

        protected override NodeState OnExecute(HashSet<BehaviourNode> previousNodes,
            HashSet<BehaviourNode> executedNodes)
        {
            _stateMachine.Update();
            return _stateMachine.CurrentState.IsComplete ? NodeState.Success : NodeState.Running;
        }
        
        public override void FixedTimeExecute() 
        {
            _stateMachine.PhysicsUpdate();
        }
    }
}