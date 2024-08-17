using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public enum RoadType
    {
        Straight,
        Turning,
        Cross,
        ThreeWays,
    }
    
    public class CellComponent : MonoBehaviour
    {
        public RoadType RoadType;
        public List<Node> Nodes;
        public Vector3 RoadRotation => gameObject.transform.rotation.eulerAngles;
        
        private void Awake() => Nodes = Nodes.OrderBy(n => n.Id).ToList();

        public virtual Node GetNodesToConnect(Dir dir) => null;

        private void ResetNodes() => Nodes = GetComponentsInChildren<Node>().OrderBy(n => n.Id).ToList();

        public virtual void ConnectCell(CellComponent nodes, Dir dir)
        {
            
        }
    }
}