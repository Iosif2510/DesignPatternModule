using System.Collections.Generic;

namespace _2510.DesignPatternModule.BehaviourTree
{
    public class StateMachineUpdateNode : BehaviourNode
    {
        private readonly StateMachine.StateMachine _stateMachine;
        
        public StateMachineUpdateNode(StateMachine.StateMachine stateMachine)
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