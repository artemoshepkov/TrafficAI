using UnityEngine;

namespace CodeBase.AI.Base
{
    public abstract class TreeBehaviour : MonoBehaviour
    {
        private Node _root = null;

        private void Start() => _root = SetupTree();
        private void FixedUpdate() => _root?.Process();

        protected abstract Node SetupTree();

        // public override Status Process()
        // {
        //     foreach (Node child in Children)
        //     {
        //         Status status = child.Process();
        //         if (status != Status.Success)
        //             return status;   
        //     }
        //
        //     return Status.Success;
        //     
        //     // while (_currentChildIndex < Children.Count)
        //     // {
        //     //     Status status = CurrentChild.Process();
        //     //     if (status != Status.Success)
        //     //         return status;
        //     //
        //     //     _currentChildIndex++;
        //     // }
        //     //
        //     // // _currentChildIndex = (_currentChildIndex + 1) % Children.Count;
        //     // _currentChildIndex = 0;
        // }
    }
}