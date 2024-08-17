namespace CodeBase.Infrastructure
{
    public class TurningRoad : CellComponent
    {
        public override void ConnectCell(CellComponent nodes, Dir dir)
        {
            nodes.GetNodesToConnect(dir)?.ConnectNode(GetNodesToConnect(dir));

        }

        public override Node GetNodesToConnect(Dir from)
        {
            if (RoadRotation.y.EqualTo(180f))
            {
                if (from == Dir.Left)
                    return Nodes[3];
                if (from == Dir.Down)
                    return Nodes[1];
                if (from == Dir.Right)
                    return Nodes[0];
                if (from == Dir.Up)
                    return Nodes[2];
            }

            if (RoadRotation.y.EqualTo(0))
            {
                if (from == Dir.Left)
                    return Nodes[0];
                if (from == Dir.Down)
                    return Nodes[2];
                if (from == Dir.Right)
                    return Nodes[3];
                if (from == Dir.Up)
                    return Nodes[1];
            }
            
            if (RoadRotation.y.EqualTo(270))
            {
                if (from == Dir.Left)
                    return Nodes[1];
                if (from == Dir.Down)
                    return Nodes[0];
                if (from == Dir.Right)
                    return Nodes[2];
                if (from == Dir.Up)
                    return Nodes[3];
            }
            
            if (RoadRotation.y.EqualTo(90))
            {
                if (from == Dir.Left)
                    return Nodes[2];
                if (from == Dir.Down)
                    return Nodes[3];
                if (from == Dir.Right)
                    return Nodes[1];
                if (from == Dir.Up)
                    return Nodes[0];
            }
            
            return null;
        }
    }
}