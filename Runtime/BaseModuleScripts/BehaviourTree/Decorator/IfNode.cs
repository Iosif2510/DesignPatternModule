using System;
using System.Collections.Generic;

namespace _2510.DesignPatternModule.BehaviourTree
{
    public sealed class IfNode<TData> : DecoratorNode
    {
        private TData _data;
        private Func<TData, bool> _condition;
        
        public IfNode(BehaviourNode child, TData data, Func<TData, bool> condition) : base(child)
        {
            _condition = condition;
            _data = data;
        }

        protected override NodeState OnExecute(HashSet<BehaviourNode> previousNodes,
            HashSet<BehaviourNode> executedNodes)
        {
            if (Child == null) 
            {
                throw new InvalidOperationException("Child node is not set.");
            }
            return _condition(_data) ? Child.Execute(previousNodes, executedNodes) : NodeState.Failure;
        }
    }
}