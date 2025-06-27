using System.Collections.Generic;

namespace _2510.DesignPatternModule.BehaviourTree
{
    public class SequenceNode : CompositeNode
    {
        protected override NodeState OnExecute(HashSet<BehaviourNode> previousNodes,
            HashSet<BehaviourNode> executedNodes)
        {
            ExecutingChildCount = 0;
            foreach (var child in Children)
            {
                ExecutingChildCount++;
                var state = child.Execute(previousNodes, executedNodes);
                if (state == NodeState.Success) continue;
                return state;
            }

            return NodeState.Success;
        }
    }
}