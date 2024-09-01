using CodeBase.RoadGraph;
using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
    public class RoadFactory : IService
    {
        private readonly AssetProvider _assetProvider;

        public RoadFactory(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public GameObject InstantiateRoad(RoadType type, Vector3 onPos, Quaternion quaternion)
        {
            switch (type)
            {
                // case RoadType.Road:
                //     return _assetProvider.Instantiate(GameConfig.RoadPath, onPos, quaternion);
                // case RoadType.ShortRoad:
                //     return _assetProvider.Instantiate(GameConfig.ShortRoadPath, onPos, quaternion);
                // case RoadType.Crossroad:
                //     return _assetProvider.Instantiate(GameConfig.CrossRoadPath, onPos, quaternion);
                // case RoadType.TurningRoad:
                //     return _assetProvider.Instantiate(GameConfig.TurningRoadPath, onPos, quaternion);
            }

            return new GameObject();
        }
    }
}
