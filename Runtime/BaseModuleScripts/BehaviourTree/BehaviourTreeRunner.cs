using System.Collections.Generic;
using System.Linq;

namespace _2510.DesignPatternModule.BehaviourTree
{
    public class BehaviourTreeRunner : BehaviourNode.BehaviourNodeCaller
    {
        private readonly BehaviourNode _root;
        
        private HashSet<BehaviourNode> _previous = new();
        private HashSet<BehaviourNode> _current = new();

        public BehaviourTreeRunner(BehaviourNode root)
        {
            _root = root;
            SetParent(_root, null); // Ensure the root has no parent
        }

        /// <summary>
        /// Call this method at every frame or certain tick(e.g. MonoBehaviour Update) to update the behaviour tree.
        /// </summary>
        public void Update()
        {
            _current.Clear();
            _root.Execute(_previous, _current);

            foreach (var node in _previous.Where(node => !_current.Contains(node)))
            {
                Exit(node);
            }
            
            (_previous, _current) = (_current, _previous);
        }

        /// <summary>
        /// Call this method at every fixed time tick(e.g. MonoBehaviour FixedUpdate) to execute physic-related logic of the behaviour tree nodes.
        /// </summary>
        public void PhysicsUpdate()
        {
            foreach (var node in _current)
            {
                node.FixedTimeExecute();
            }
        }


    }
}