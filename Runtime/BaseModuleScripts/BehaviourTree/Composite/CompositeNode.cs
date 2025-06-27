using System.Collections.Generic;

namespace _2510.DesignPatternModule.BehaviourTree
{
    /// <summary>
    /// Behaviour tree node with multiple children.
    /// Call <see cref="BehaviourNode.Execute"/> on some or all children and return a state based on the children's states.
    /// </summary>
    public abstract class CompositeNode : BehaviourNode
    {
        protected List<BehaviourNode> Children = new();
        
        protected int ChildCount => Children.Count;
        protected int ExecutingChildCount;
        
        public void AddChild(BehaviourNode child)
        {
            Children.Add(child);
            child.SetParent(this);
        }
        
        public void ClearChildren()
        {
            Children.Clear();
        }

        public override void FixedTimeExecute()
        {
            base.FixedTimeExecute();
            for (var i = 0; i < ExecutingChildCount; i++)
            {
                Children[i].FixedTimeExecute();
            }
        }
    }
}