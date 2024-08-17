using CodeBase.Car;
using CodeBase.Tools.CityEditor;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        private CityGrid _grid;
        private GridPlacer _gridPlacer;
        [SerializeField] private MapEditor _mapEditor;
        [SerializeField] private CarSpawner _carSpawner;
        [SerializeField] private int _sizeX;
        [SerializeField] private int _sizeY;
        [SerializeField] private bool IsDefaultMap;

        private void Start()
        {
            _grid = new CityGrid(MakeGrid());
            _gridPlacer = new GridPlacer(_grid);
            
            _gridPlacer.PlaceGrid();

            _mapEditor.Init(_grid);
            _carSpawner.Init(_grid, _gridPlacer);
        }

        private CellType[,] MakeGrid()
        {
            if (IsDefaultMap)
            {
                return new CellType[_sizeY, _sizeX];
            }
            else
            {
                var cells = new CellType[3, 3];

                
                cells[0, 0] = CellType.Road;
                cells[0, 1] = CellType.Road;
                cells[0, 2] = CellType.Road;
                //
                // cells[0, 0] = CellType.Road;
                // cells[1, 0] = CellType.Road;
                // cells[2, 0] = CellType.Road;
                //
                // cells[0, 1] = CellType.Road;
                // // cells[1, 1] = CellType.Road;
                // cells[2, 1] = CellType.Road;
                //
                // cells[0, 2] = CellType.Road;
                // cells[1, 2] = CellType.Road;
                // cells[2, 2] = CellType.Road;
                //
                return cells;
            }
        }
    }
}
