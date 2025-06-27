using System.Collections.Generic;
using UnityEngine;

namespace _2510.DesignPatternModule.BehaviourTree
{
    /// <summary>
    /// Executes all child nodes in parallel and returns success or failure based on the configured policies.
    /// </summary>
    public sealed class ParallelNode : CompositeNode
    {
        public enum ParallelPolicy
        {
            All,
            Any
        }
        
        private readonly ParallelPolicy _successPolicy;
        private readonly ParallelPolicy _failurePolicy;
        
        private bool _anyChildSuccess;
        private bool _anyChildFailure;
        private bool _allChildrenSuccess;
        private bool _allChildrenFailure;

        /// <param name="successPolicy">
        /// Policy for returning <see cref="BehaviourNode.NodeState.Success"/> as result state.
        /// <para> <see cref="ParallelPolicy.All"/>: All children must succeed for the node to succeed.</para>
        /// <see cref="ParallelPolicy.Any"/>: The node succeeds if any children succeeds.
        /// </param>
        /// <param name="failurePolicy">
        /// Policy for returning <see cref="BehaviourNode.NodeState.Failure"/> as result state.
        /// <para> <see cref="ParallelPolicy.All"/>: All children must fail for the node to fail.</para>
        /// <see cref="ParallelPolicy.Any"/>: The node fails if any children fails.
        /// </param>
        public ParallelNode(ParallelPolicy successPolicy, ParallelPolicy failurePolicy)
        {
            this._successPolicy = successPolicy;
            this._failurePolicy = failurePolicy;
        }

        protected override NodeState OnExecute(HashSet<BehaviourNode> previousNodes,
            HashSet<BehaviourNode> executedNodes)
        {
            if (Children.Count == 0) return NodeState.Failure;
            
            _anyChildSuccess = false;
            _anyChildFailure = false;
            _allChildrenSuccess = true;
            _allChildrenFailure = true;
            
            ExecutingChildCount = 0;
            foreach (var child in Children)
            {
                ExecutingChildCount++;
                var state = child.Execute(previousNodes, executedNodes);
                switch (state)
                {
                    case NodeState.Success:
                        _anyChildSuccess = true;
                        break;
                    case NodeState.Failure:
                        _anyChildFailure = true;
                        break;
                }
                _allChildrenSuccess &= state == NodeState.Success;
                _allChildrenFailure &= state == NodeState.Failure;
            }

            switch (_successPolicy)
            {
                case ParallelPolicy.All when _allChildrenSuccess:
                case ParallelPolicy.Any when _anyChildSuccess:
                    return NodeState.Success;
                default:
                    switch (_failurePolicy)
                    {
                        case ParallelPolicy.Any when _anyChildFailure:
                        case ParallelPolicy.All when _allChildrenFailure:
                            return NodeState.Failure;
                        default:
                            return NodeState.Running;
                    }
            }
        }
    }
}