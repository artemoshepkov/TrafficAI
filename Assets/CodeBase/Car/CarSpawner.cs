using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Car
{
    public class CarSpawner : MonoBehaviour
    {
        private AssetProvider _assetProvider = ServiceLocator.GetService<AssetProvider>();
        private GridPlacer _gridPlacer;
        private CityGrid _cityGrid;
        private Vector3 _stepYToRoad = new Vector3(0f, -0.05f, 0f);

        [SerializeField] private GameObject _carPrefab;

        public void Spawn()
        {
            var randomRoadCell = _cityGrid.Roads[Random.Range(0, _cityGrid.Roads.Count)];
            var randomRoadNode = _gridPlacer.GameObjectsGrid[randomRoadCell.X, randomRoadCell.Y].Nodes[0];
            
            var car = Instantiate(_carPrefab, randomRoadNode.transform.position + _stepYToRoad, Quaternion.identity);

            car.GetComponent<OnGraphMover>().Init(randomRoadNode);
        }

        public void Init(CityGrid cityGrid, GridPlacer gridPlacer)
        {
            _cityGrid = cityGrid;
            _gridPlacer = gridPlacer;
        }
    }
}
