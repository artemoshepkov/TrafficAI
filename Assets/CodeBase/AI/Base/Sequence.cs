using System;
using System.Collections.Generic;

namespace CodeBase.AI.Base
{
    public class Sequence : Node
    {
        public Sequence(string name = "Node") : base(name) { }
        public Sequence(List<Node> children, string name = "Node") : base(children, name) { }

        public override Status Process()
        {
            foreach (Node child in Children)
            {
                Status status = child.Process();

                switch (status)
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Failure:
                        Reset();
                        return Status.Failure;
                    case Status.Success:
                        continue;
                    default:
                        throw new NotImplementedException();
                }
            }
            
            Reset();
            return Status.Success;
            
            // while (_currentChildIndex < Children.Count)
            // {
            //     Status status = CurrentChild.Process();
            //
            //     switch (status)
            //     {
            //         case Status.Running:
            //             return Status.Running;
            //         case Status.Failure:
            //             Reset();
            //             return Status.Failure;
            //         case Status.Success:
            //             _currentChildIndex++;
            //             return _currentChildIndex == Children.Count ? Status.Success : Status.Running;
            //         default:
            //             throw new NotImplementedException();
            //     }
            // }
            //
            // Reset();
            // return Status.Success;
        }
    }
}