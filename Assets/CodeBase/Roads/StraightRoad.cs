using CodeBase.RoadGraph;

namespace CodeBase.Roads
{
    public class StraightRoad : CellComponent
    {
        public override void ConnectCell(CellComponent nodes, Dir dir) => nodes.GetNodesToConnect(dir)?.ConnectNode(GetNodesToConnect(dir));

        public override RoadNode GetNodesToConnect(Dir from)
        {
            if (RoadRotation.y.EqualTo(0f))
            {
                if (from == Dir.Up)
                    return Nodes[0];
                if (from == Dir.Down)
                    return Nodes[1];
            }
            if (RoadRotation.y.EqualTo(90f))
            {
                if (from == Dir.Left)
                    return Nodes[1];
                if (from == Dir.Right)
                    return Nodes[0];
            }
    
            return null;
        }
    }
}