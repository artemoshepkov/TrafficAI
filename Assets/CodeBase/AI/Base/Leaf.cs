namespace CodeBase.AI.Base
{
    public class Leaf : Node
    {
        protected IStrategy Strategy;

        public Leaf(IStrategy strategy, string name = "Node") : base(name) => Strategy = strategy;

        public override Status Process() => Strategy.Process();
        public override void Reset() => Strategy.Reset();
    }
}