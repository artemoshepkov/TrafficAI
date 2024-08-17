using UnityEngine;

namespace CodeBase
{
    public enum RoadType
    {
        ShortRoad,
        Crossroad,
        TurningRoad,
        Road,
    }

    public static class GameConfig
    {
        public static string RoadPath = "Prefabs/City/Road";
        public static string CrossRoadPath = "Prefabs/City/Сrossroad";
        public static string ShortRoadPath = "Prefabs/City/ShortRoad";
        public static string TurningRoadPath = "Prefabs/City/TurningRoad";
        
        public static string[] HousePath =
        {
            "Prefabs/City/House1",
        };
    }
    
    public class Road : MonoBehaviour
    {
    }
}
