using System;
using System.Collections.Generic;
using System.Linq;

namespace _2510.DesignPatternModule.BehaviourTree
{
    public class RandomSelectorNode : CompositeNode
    {
        private readonly Random _random = new();

        protected override NodeState OnExecute(HashSet<BehaviourNode> previousNodes, HashSet<BehaviourNode> executedNodes)
        {
            ExecutingChildCount = 0;
            var randomChildren = Children.OrderBy(_ => _random.Next());
            foreach (var child in randomChildren)
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