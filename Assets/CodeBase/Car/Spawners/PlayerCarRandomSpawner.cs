using CodeBase.Player;
using CodeBase.RoadGraph;
using UnityEngine;

namespace CodeBase.Car.Spawners
{
    public class PlayerCarRandomSpawner : CarSpawner
    {
        private GridPlacer _gridPlacer;
        private CityGrid _cityGrid;
        private Vector3 _stepYToRoad = new Vector3(0f, -0.05f, 0f);

        [SerializeField] private CameraMover _cameraMover;
        [SerializeField] private GameObject _carPrefab;

        public override void Spawn()
        {
            var randomRoadCell = _cityGrid.Roads[Random.Range(0, _cityGrid.Roads.Count)];
            var randomRoadNode = _gridPlacer.GameObjectsGrid[randomRoadCell.X, randomRoadCell.Y].Nodes[0];

            var nodeTransform = randomRoadNode.transform;
            var car = Instantiate(_carPrefab, nodeTransform.position + _stepYToRoad, Quaternion.identity);
            
            var carTransform = car.GetComponent<Transform>(); 
            // carTransform.forward = nodeTransform.right;
            _cameraMover.Init(carTransform);
        }

        public void Init(CityGrid cityGrid, GridPlacer gridPlacer)
        {
            _cityGrid = cityGrid;
            _gridPlacer = gridPlacer;
        }
    }
}