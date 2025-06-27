using System.Collections.Generic;
using UnityEngine;

namespace _2510.DesignPatternModule.BehaviourTree
{
    public abstract class BehaviourNode
    {
        public enum NodeState
        {
            Running,
            Success,
            Failure
        }
        
        public class BehaviourNodeCaller
        {
            protected static void SetParent(BehaviourNode node, BehaviourNode parent)
            {
                node.SetParent(parent);
            }
            
            protected static void Exit(BehaviourNode node)
            {
                node.Exit();
            }
        }
    
        /// <summary>
        /// Parent node in the behaviour tree.
        /// </summary>
        public BehaviourNode Parent { get; protected set; }
        
        /// <summary>
        /// Current running state of the node.
        /// Use to determine if the node is still running, has succeeded, or has failed.
        /// </summary>
        public NodeState CurrentState { get; private set; }

        
        protected internal void SetParent(BehaviourNode parent)
        {
            Parent = parent;
        }

        /// <summary>
        /// Called if the node is executed on this tick and was not executed on the previous tick.
        /// </summary>
        protected virtual void Enter()
        {
// #if UNITY_EDITOR
//             Debug.Log($"Entering {GetType().Name} at {Time.time}");
// #endif
        }

        /// <summary>
        /// Called if the node stopped being executed on this tick and was executed on the previous tick.
        /// </summary>
        protected virtual void Exit()
        {
// #if UNITY_EDITOR
//             Debug.Log($"Exiting {GetType().Name} at {Time.time}");
// #endif
        }
        
        /// <summary>
        /// Node Update Wrapper. Adds itself to the executed nodes' parameter.
        /// Do not override or reimplement.
        /// </summary>
        /// <param name="previousNodes">
        /// Set of nodes that were executed in the previous tick.
        /// Used to determine if the node is entering for the first time.
        /// </param>
        /// <param name="executedNodes">
        /// Set of nodes that were executed in the current tick.
        /// Used with <paramref name="previousNodes"/> to determine if any node stopped running and exited.
        /// </param>
        /// <returns>
        /// Running state of the node.
        /// </returns>
        public NodeState Execute(HashSet<BehaviourNode> previousNodes, HashSet<BehaviourNode> executedNodes)
        {
            executedNodes.Add(this);
            if (!previousNodes.Contains(this)) Enter();
            CurrentState = OnExecute(previousNodes, executedNodes);
            return CurrentState;
        }

        /// <summary>
        /// Called by <see cref="BehaviourTreeRunner"/> once per tick.
        /// <para>
        /// Call child nodes' <see cref="Execute"/> methods and use the returned values for execution logic if needed.
        /// </para>
        /// <remarks>
        /// Do not call <see cref="OnExecute"/> itself on parent nodes, as it can result in tracking loss from <see cref="BehaviourTreeRunner"/> and omit enter/exit logic.
        /// </remarks>
        /// </summary>
        /// <param name="previousNodes">
        /// Set of nodes that were executed in the previous tick.
        /// Do not modify; Just pass it to called <see cref="Execute"/> methods of child nodes.
        /// </param>
        /// <param name="executedNodes">
        /// Set of nodes that were executed in the current tick.
        /// Do not modify; Just pass it to called <see cref="Execute"/> methods of child nodes.
        /// </param>
        /// <returns>
        /// Running state of the node.
        /// </returns>
        protected abstract NodeState OnExecute(HashSet<BehaviourNode> previousNodes,
            HashSet<BehaviourNode> executedNodes);

        /// <summary>
        /// Called by <see cref="BehaviourTreeRunner"/> once per fixed time step event(e.g. MonoBehaviour FixedUpdate).
        /// Override if the node needs to execute physics-related logic.
        /// <remarks>Must be overridden and call every executed child's <see cref="FixedTimeExecute"/> if derived node is designed to have children. </remarks>
        /// </summary>
        public virtual void FixedTimeExecute()
        {
            
        }
    }
}