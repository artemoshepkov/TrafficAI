namespace CodeBase.Infrastructure
{
    public class CrossRoad : CellComponent
    {
        public override void ConnectCell(CellComponent nodes, Dir dir)
        {
            nodes.GetNodesToConnect(dir)?.ConnectNode(GetNodeToConnect1(dir));
        }
        public Node GetNodeToConnect1(Dir from)
        {
            if (from == Dir.Left)
                return Nodes[6];
            if (from == Dir.Down)
                return Nodes[4];
            if (from == Dir.Right)
                return Nodes[2];
            if (from == Dir.Up)
                return Nodes[0];
            

            return null;
        }
        
        public override Node GetNodesToConnect(Dir from)
        {
            if (from == Dir.Left)
                return Nodes[3];
            if (from == Dir.Down)
                return Nodes[1];
            if (from == Dir.Right)
                return Nodes[7];
            if (from == Dir.Up)
                return Nodes[5];
            

            return null;
        }
    }
}