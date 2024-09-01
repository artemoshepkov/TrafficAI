using System;
using System.Collections.Generic;

namespace CodeBase.AI.Base
{
    public class Selector : Node
    {
        public Selector(string name = "Node") : base(name) { }
        public Selector(List<Node> children, string name = "Node") : base(children, name) { }

        public override Status Process()
        {
            foreach (Node child in Children)
            {
                Status status = child.Process();

                switch (status)
                {
                    case Status.Success:
                        Reset();
                        return Status.Success;
                    case Status.Running:
                        return Status.Running;
                    case Status.Failure:
                        continue;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            Reset();
            return Status.Failure;
            
            // while (_currentChildIndex < Children.Count)
            // {
            //     Status status = CurrentChild.Process();
            //
            //     switch (status)
            //     {
            //         case Status.Success:
            //             Reset();
            //             return Status.Success;
            //         case Status.Running:
            //             return Status.Running;
            //         case Status.Failure:
            //             _currentChildIndex++;
            //             return Status.Running;
            //         default:
            //             throw new ArgumentOutOfRangeException();
            //     }
            // }
            //
            // Reset();
            // return Status.Failure;
        }
    }
}