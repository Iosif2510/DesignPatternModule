namespace _2510.DesignPatternModule.BehaviourTree
{
    /// <summary>
    /// Behaviour node with only child; Use to conditionally execute child or modify child's result
    /// </summary>
    public abstract class DecoratorNode : BehaviourNode
    {
        protected BehaviourNode Child;

        /// <param name="child">Only required child node</param>
        protected DecoratorNode(BehaviourNode child)
        {
            SetChild(child);
        }

        public void SetChild(BehaviourNode child)
        {
            Child?.SetParent(null); // Remove the old child's parent reference
            Child = child;
            child.SetParent(this);
        }
        
        public override void FixedTimeExecute()
        {
            base.FixedTimeExecute();
            Child?.FixedTimeExecute();
        }
    }
}