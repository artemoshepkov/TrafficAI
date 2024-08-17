using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor.Windows
{
    public static class RoadGpraph
    {
        
    }

    public class Node
    {
        public GameObject Item;
        
        public List<Edge> Edges;
    }

    public class Edge
    {
        public List<Node> Nodes;
    }

    [CustomEditor(typeof(Road))]
    public class RoadEditorWindow : BaseRoadEditor
    {
        protected override float GetShift()
        {
            switch (RoadType)
            {
                case RoadType.Road:
                    return 2f;
                case RoadType.ShortRoad:
                    return 1.36f;
                case RoadType.Crossroad:
                    return 1.42f;
                case RoadType.TurningRoad:
                    return 1.36f;
            }

            return 0f;
        }
    }
}
