using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Infrastructure
{
    public class Node : MonoBehaviour
    {
        private event Action<Node> OnDestroyEvent;

        private Transform _transform;
        private float _radius = 0.06f;

        [field: SerializeField] public int Id { get; private set; } = 0;

        public List<Node> ConnectedNodes = new List<Node>();
        
        public Vector3 Position => _transform.position;
        [field: SerializeField] public Vector3 Direction1 { get; private set; }
        
        public Vector3 Forward => _transform.forward;
        public Vector3 Right => _transform.right;
        public Vector3 Right1;

        private void Awake()
        {
            _transform = transform;
            Right1 = _transform.right;
        }

        private void Update()
        {
            Direction1 = _transform.forward;
        }

        private void OnDestroy() => OnDestroyEvent?.Invoke(this);
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            
            // Gizmos.DrawWireSphere(transform.position, _radius);
            Gizmos.DrawSphere(transform.position, _radius);

            foreach (Node node in ConnectedNodes)
            {
                Gizmos.DrawLine(transform.position, node.transform.position);
            }
        }

        public Node GetRandomConnectedNode() => ConnectedNodes[Random.Range(0, ConnectedNodes.Count)];
        public void ConnectNode(Node node)
        {
            if (node == null)
                return;
            
            ConnectNodes(node);
            // node.ConnectNodes(this);
        }
        public void RemoveConnectedNode(Node nodeToRemove) => ConnectedNodes.Remove(nodeToRemove);
        private void ConnectNodes(Node node)
        {
            if (ConnectedNodes.Contains(node))
                return;
            
            ConnectedNodes.Add(node);
            node.OnDestroyEvent += RemoveConnectedNode;
        }
    }

    public enum Dir
    {
        Left,
        Up,
        Right,
        Down,
        None
    }
}