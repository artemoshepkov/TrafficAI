using System.Collections.Generic;

namespace CodeBase.AI.Base
{
    public abstract class Node
    {
        public enum Status { Success, Running, Failure }

        // protected int _currentChildIndex = 0;
        protected readonly List<Node> Children = new();

        // protected Node CurrentChild => Children[_currentChildIndex];
        
        public readonly string Name;

        public Node(string name = "Node") => Name = name;
        public Node(List<Node> children, string name = "Node") : this(name) => Children = children;

        public void AddChild(Node node) => Children.Add(node);
        // public virtual Status Process() => Children[_currentChildIndex].Process();
        public abstract Status Process();
        public virtual void Reset()
        {
            // _currentChildIndex = 0;
            Children.ForEach(n => n.Reset());
        }
    }
}