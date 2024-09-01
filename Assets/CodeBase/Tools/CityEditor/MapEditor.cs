using CodeBase.Infrastructure;
using CodeBase.RoadGraph;
using UnityEngine;

namespace CodeBase.Tools.CityEditor
{
    public class MapEditor : MonoBehaviour
    {
        private CityGrid _cityGrid;
        private CellType _cellToPlace;
        
        [SerializeField] private MouseDetector _mouseDetector;

        private void Start()
        {
            _mouseDetector.OnMouseDown += PlaceCell;
            _mouseDetector.OnMouseHold += PlaceCell;
            SetRoadToPlace();
        }

        public void Init(CityGrid cityGrid) => _cityGrid = cityGrid;

        public void SetEarthToPlace() => _cellToPlace = CellType.Empty;
        public void SetRoadToPlace() => _cellToPlace = CellType.Road;

        private void PlaceCell(Vector3Int pos)
        {
            if (_cityGrid[pos.x, pos.z] == _cellToPlace)
                return;
            
            _cityGrid[pos.x, pos.z] = _cellToPlace;
        }
    }
}
