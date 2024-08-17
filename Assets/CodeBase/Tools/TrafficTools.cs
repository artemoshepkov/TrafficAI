using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Tools
{
    public static class TrafficTools
    {
        private static readonly AssetProvider AssetProvider = ServiceLocator.GetService<AssetProvider>();
        
        public static void ResetRoadsPrefabs()
        {
            GameObject[] roads = GameObject.FindGameObjectsWithTag("Road");

            foreach (var road in roads)
            {
                var transform = road.transform;
                
                AssetProvider.Instantiate(GameConfig.RoadPath, transform.position,
                    transform.rotation);
                
                Object.DestroyImmediate(road);
            }
            
            ShortRoad[] shortRoads = Object.FindObjectsOfType<ShortRoad>();

            foreach (var road in shortRoads)
            {
                var transform = road.transform;
                
                AssetProvider.Instantiate(GameConfig.ShortRoadPath, transform.position,
                    transform.rotation);
                
                Object.DestroyImmediate(road.gameObject);
            }
            
            CrossRoad[] crossRoads = Object.FindObjectsOfType<CrossRoad>();

            foreach (var road in crossRoads)
            {
                var transform = road.transform;
                
                AssetProvider.Instantiate(GameConfig.RoadPath, transform.position,
                    transform.rotation);
                
                Object.DestroyImmediate(road.gameObject);
            }
        }
    }
}