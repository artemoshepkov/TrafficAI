using System;

namespace CodeBase.AI.Base
{
    public class Condition : IStrategy
    {
        private Func<bool> _predicate;

        public Condition(Func<bool> predicate) => _predicate = predicate;

        public Node.Status Process() => _predicate() ? Node.Status.Success : Node.Status.Failure;
    }
}