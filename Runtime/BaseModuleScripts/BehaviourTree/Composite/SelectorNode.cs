using System.Collections.Generic;

namespace _2510.DesignPatternModule.BehaviourTree
{
    public sealed class SelectorNode : CompositeNode
    {
        protected override NodeState OnExecute(HashSet<BehaviourNode> previousNodes, HashSet<BehaviourNode> executedNodes)
        {
            ExecutingChildCount = 0;
            foreach (var child in Children)
            {
                ExecutingChildCount++;
                var state = child.Execute(previousNodes, executedNodes);
                if (state == NodeState.Failure) continue;
                return state;
            }
            return NodeState.Failure;
        }
    }
}