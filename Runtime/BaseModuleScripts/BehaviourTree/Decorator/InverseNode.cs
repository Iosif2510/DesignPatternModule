using System;
using System.Collections.Generic;

namespace _2510.DesignPatternModule.BehaviourTree
{
    public class InverseNode : DecoratorNode
    {
        public InverseNode(BehaviourNode child) : base(child)
        {
        }

        protected override NodeState OnExecute(HashSet<BehaviourNode> previousNodes,
            HashSet<BehaviourNode> executedNodes)
        {
            if (Child == null) 
            {
                throw new InvalidOperationException("Child node is not set.");
            }
            var state = Child.Execute(previousNodes, executedNodes);
            return state switch
            {
                NodeState.Success => NodeState.Failure,
                NodeState.Failure => NodeState.Success,
                NodeState.Running => NodeState.Running,
                _ => throw new InvalidOperationException($"Unexpected node state: {state}")
            };
        }
    }
}