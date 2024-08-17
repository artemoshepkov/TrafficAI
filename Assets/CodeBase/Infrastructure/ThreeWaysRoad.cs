namespace CodeBase.Infrastructure
{
    public class ThreeWaysRoad : CellComponent
    {
        public override void ConnectCell(CellComponent nodes, Dir dir)
        {
            nodes.GetNodesToConnect(dir)?.ConnectNode(GetNodeToConnect1(dir));
        }
        public Node GetNodeToConnect1(Dir from)
        {
            if (RoadRotation.y.EqualTo(90f))
            {
                if (from == Dir.Left)
                    return Nodes[4];
                if (from == Dir.Down)
                    return Nodes[2];
                if (from == Dir.Right)
                    return null;
                if (from == Dir.Up)
                    return Nodes[0];
            }
            if (RoadRotation.y.EqualTo(270f))
            {
                if (from == Dir.Left)
                    return null;
                if (from == Dir.Down)
                    return Nodes[0];
                if (from == Dir.Right)
                    return Nodes[4];
                if (from == Dir.Up)
                    return Nodes[2];
            }
            if (RoadRotation.y.EqualTo(0f))
            {
                if (from == Dir.Left)
                    return Nodes[0];
                if (from == Dir.Down)
                    return Nodes[4];
                if (from == Dir.Right)
                    return Nodes[2];
                if (from == Dir.Up)
                    return null;
            }
            if (RoadRotation.y.EqualTo(180f))
            {
                if (from == Dir.Left)
                    return Nodes[2];
                if (from == Dir.Down)
                    return null;
                if (from == Dir.Right)
                    return Nodes[0];
                if (from == Dir.Up)
                    return Nodes[4];
            }

            return null;
        }

        public override Node GetNodesToConnect(Dir from)
        {
            if (RoadRotation.y.EqualTo(90f))
            {
                if (from == Dir.Left)
                    return null;
                if (from == Dir.Down)
                    return Nodes[1];
                if (from == Dir.Right)
                    return Nodes[5];
                if (from == Dir.Up)
                    return Nodes[3];
            }
            if (RoadRotation.y.EqualTo(270f))
            {
                if (from == Dir.Left)
                    return Nodes[5];
                if (from == Dir.Down)   
                    return Nodes[3];
                if (from == Dir.Right)
                    return null;
                if (from == Dir.Up)
                    return Nodes[1];
            }
            if (RoadRotation.y.EqualTo(0f))
            {
                if (from == Dir.Left)
                    return Nodes[3];
                if (from == Dir.Down)
                    return null;
                if (from == Dir.Right)
                    return Nodes[1];
                if (from == Dir.Up)
                    return Nodes[5];
            }
            if (RoadRotation.y.EqualTo(180f))
            {
                if (from == Dir.Left)
                    return Nodes[1];
                if (from == Dir.Down)
                    return Nodes[5];
                if (from == Dir.Right)
                    return Nodes[3];
                if (from == Dir.Up)
                    return null;
            }
            
            return null;
        }
    }
}