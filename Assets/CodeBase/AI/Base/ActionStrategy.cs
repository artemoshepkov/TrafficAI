using System;

namespace CodeBase.AI.Base
{
    public class ActionStrategy : IStrategy
    {
        private Action _doAction;

        public ActionStrategy(Action doAction) => _doAction = doAction;

        public Node.Status Process()
        {
            _doAction();
            return Node.Status.Success;
        }
    }
}