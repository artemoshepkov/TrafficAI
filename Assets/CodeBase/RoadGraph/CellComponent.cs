using System.Collections.Generic;
using System.Linq;
using CodeBase.Roads;
using UnityEngine;

namespace CodeBase.RoadGraph
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
        public List<RoadNode> Nodes;
        public Vector3 RoadRotation => gameObject.transform.rotation.eulerAngles;
        
        private void Awake() => Nodes = Nodes.OrderBy(n => n.Id).ToList();

        public virtual RoadNode GetNodesToConnect(Dir dir) => null;

        private void ResetNodes() => Nodes = GetComponentsInChildren<RoadNode>().OrderBy(n => n.Id).ToList();

        public virtual void ConnectCell(CellComponent nodes, Dir dir)
        {
            
        }
    }
}