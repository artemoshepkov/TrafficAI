using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Roads
{
    public class RoadNode : MonoBehaviour
    {
        private event Action<RoadNode> OnDestroyEvent;

        private Transform _transform;
        private float _radius = 0.06f;

        [field: SerializeField] public int Id { get; private set; } = 0;

        public List<RoadNode> ConnectedNodes = new List<RoadNode>();
        
        public Vector3 Position => _transform.position;
        public Vector3 Forward => _transform.forward;
        public Vector3 Right => _transform.right;

        private void Awake() => _transform = transform;

        private void OnDestroy() => OnDestroyEvent?.Invoke(this);
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            
            // Gizmos.DrawWireSphere(transform.position, _radius);
            Gizmos.DrawSphere(transform.position, _radius);

            foreach (RoadNode node in ConnectedNodes)
            {
                Gizmos.DrawLine(transform.position, node.transform.position);
            }
        }

        public RoadNode GetRandomConnectedNode() => ConnectedNodes[Random.Range(0, ConnectedNodes.Count)];
        public void ConnectNode(RoadNode roadNode)
        {
            if (roadNode == null)
                return;
            
            ConnectNodes(roadNode);
            // node.ConnectNodes(this);
        }
        public void RemoveConnectedNode(RoadNode roadNodeToRemove) => ConnectedNodes.Remove(roadNodeToRemove);
        private void ConnectNodes(RoadNode roadNode)
        {
            if (ConnectedNodes.Contains(roadNode))
                return;
            
            ConnectedNodes.Add(roadNode);
            roadNode.OnDestroyEvent += RemoveConnectedNode;
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