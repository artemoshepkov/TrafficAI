using System.Collections.Generic;
using System.Linq;

namespace CodeBase.Tools
{
    public class RoadMap
    {
        private List<List<RoadType>> _map = new List<List<RoadType>>();

        public int RowCount => _map.Count;
        public int ColumnCount => _map.First().Count;

        public void Reset() => _map = new List<List<RoadType>>();
    }
    
    public static class TrafficGraph
    {
        private static RoadMap _roadMap = new RoadMap();
        public static void Reset()
        {
            _roadMap.Reset();
        }
    }
}