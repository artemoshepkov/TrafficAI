using System;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.RoadGraph
{
    public class GridPlacer
    {
        private readonly string _prefabPathEarth = "Prefabs/City/Earth";
        private readonly string _prefabPathStraightRoad = "Prefabs/City/Roads/StraightRoad";
        private readonly string _prefabPathTurningRoad = "Prefabs/City/Roads/TurningRoad";
        private readonly string _prefabPath3WaysRoad = "Prefabs/City/Roads/3WaysRoad";
        private readonly string _prefabPathCrossRoad = "Prefabs/City/Roads/CrossRoad";

        public event Action<int, int> OnRoadChanged;
        
        private Vector3 _beginPosition = Vector3.zero;
        private Vector3 _roadYStep =  new Vector3(0f, 0.02f, 0f);
        private Vector3 _cellSize = new Vector3(1f, 0f, 1f);

        private CityGrid _grid;
        private AssetProvider _assetProvider = ServiceLocator.GetService<AssetProvider>();

        public CellComponent[,] GameObjectsGrid;

        public GridPlacer(CityGrid grid)
        {
            _grid = grid;
            GameObjectsGrid = new CellComponent[_grid.SizeY, _grid.SizeX];
            _grid.OnGridChanged += ReplaceCell;
            OnRoadChanged += Graph.OnRoadChanged;
            Graph.Grid = GameObjectsGrid;
        }

        public void PlaceGrid()
        {
            Camera.main.transform.position = GetCellPosition(_grid.SizeX / 2, _grid.SizeY / 2) + new Vector3(0f, 10f, 0f);
            Camera.main.transform.Rotate(new Vector3(90f, -90f, 0f));
                
            for (int i = 0; i < _grid.SizeX; i++)
                for (int j = 0; j < _grid.SizeY; j++)
                    PlaceCell(i, j, _grid[i, j]);

            foreach (Cell cell in _grid.Roads) 
                TryConnectBesideRoads(cell.X, cell.Y);
        }

        public void ReplaceCell(int i, int j, CellType type)
        {
            PlaceCell(i, j, type);
            
            if (type == CellType.Road || type == CellType.Empty)
            {
                TryConnectBesideRoads(i, j);
                if (j > 0)
                    TryConnectBesideRoads(i, j - 1);
                if (i > 0)
                    TryConnectBesideRoads(i - 1, j);
                if (j < _grid.SizeX - 1)
                    TryConnectBesideRoads(i, j + 1);
                if (i < _grid.SizeY - 1)
                    TryConnectBesideRoads(i + 1, j);
            }
        }

        private void PlaceCell(int i, int j, CellType cellType)
        {
            GameObject insertedGameObject = null;
            var position = GetCellPosition(i, j);
            if (cellType == CellType.Empty)
            {
                insertedGameObject = _assetProvider.Instantiate(_prefabPathEarth, position);
            }
            else if (cellType == CellType.Road)
            {
                insertedGameObject = _assetProvider.Instantiate(_prefabPathStraightRoad, position + _roadYStep);
            }

            ReplaceGameObject(insertedGameObject, i, j);
        }

        private void TryConnectBesideRoads(int i, int j)
        {
            if (_grid[i, j] != CellType.Road)
                return;
            
            int neighboursCount = 0;
            if (_grid.LeftNeighbour(i, j) == CellType.Road)
                neighboursCount++;
            if (_grid.UpNeighbour(i, j) == CellType.Road)
                neighboursCount++;
            if (_grid.RightNeighbour(i, j) == CellType.Road)
                neighboursCount++;
            if (_grid.DownNeighbour(i, j) == CellType.Road)
                neighboursCount++;

            if (neighboursCount == 1)
                Make1WaysRoad(i, j);
            
            if (neighboursCount == 2)
                Make2WaysRoad(i, j);
            
            if (neighboursCount == 3)
                Make3WaysRoad(i, j);
            
            if (neighboursCount == 4)
                Make4WaysRoad(i, j);
        }

        private void Make1WaysRoad(int i, int j)
        {
            // --
             if (_grid.LeftNeighbour(i, j) == CellType.Road || _grid.RightNeighbour(i, j) == CellType.Road)
            {
                var quaternion = Quaternion.identity;
                quaternion.eulerAngles = new Vector3(0f, 90f, 0f);

                TryReplaceCell(i, j, quaternion, RoadType.Straight, _prefabPathStraightRoad);
                
                if (_grid.LeftNeighbour(i, j) == CellType.Road)
                {
                    // GameObjectsGrid[i, j].Nodes[0].ConnectNode(GameObjectsGrid[i, j - 1].Nodes[0]);
                    // Graph.ConnectRoads(GameObjectsGrid[i, j - 1], GameObjectsGrid[i, j]);
                }
                else
                {
                    // GameObjectsGrid[i, j].Nodes[1].ConnectNode(GameObjectsGrid[i, j + 1].Nodes[1]);

                    // Graph.ConnectRoads(GameObjectsGrid[i, j], GameObjectsGrid[i, j + 1]);
                }

                return;
            }
            
            /*      
             *    |
             *    |
             */   
            if (_grid.UpNeighbour(i, j) == CellType.Road || _grid.DownNeighbour(i, j) == CellType.Road)
            {
                TryReplaceCell(i, j, Quaternion.identity, RoadType.Straight, _prefabPathStraightRoad);

                if (_grid.UpNeighbour(i, j) == CellType.Road)
                {
                    
                    // GameObjectsGrid[i - 1, j].Nodes[0].ConnectNode(GameObjectsGrid[i, j].Nodes[0]);

                    // Graph.ConnectRoads(GameObjectsGrid[i - 1, j], GameObjectsGrid[i, j]);
                }
                else
                {
                    // GameObjectsGrid[i + 1, j].Nodes[1].ConnectNode(GameObjectsGrid[i, j].Nodes[1]);

                    // Graph.ConnectRoads(GameObjectsGrid[i, j], GameObjectsGrid[i + 1, j]);
                }
                
                return;
            }
        }
        private void Make2WaysRoad(int i, int j)
        {
            Make1WaysRoad(i, j);

            var quaternion = Quaternion.identity;

            /*      
             *  - -
             *    |
             */     
            if (_grid.LeftNeighbour(i, j) == CellType.Road && _grid.DownNeighbour(i, j) == CellType.Road)
            {
                quaternion.eulerAngles = new Vector3(0f, 180f, 0f);

                TryReplaceCell(i, j, quaternion, RoadType.Turning, _prefabPathTurningRoad);

                // GameObjectsGrid[i, j].Nodes[0].ConnectNode(GameObjectsGrid[i, j - 1].Nodes[0]);
                // GameObjectsGrid[i + 1, j].Nodes[1].ConnectNode(GameObjectsGrid[i, j].Nodes[0]);

                // Graph.ConnectRoads(GameObjectsGrid[i, j], GameObjectsGrid[i, j - 1]);
                // Graph.ConnectRoads(GameObjectsGrid[i, j], GameObjectsGrid[i + 1, j]);
                
                return;
            }
            
            /*
             *  - -
             *  |
             */
            if (_grid.RightNeighbour(i, j) == CellType.Road && _grid.DownNeighbour(i, j) == CellType.Road)
            {
                quaternion.eulerAngles = new Vector3(0f, 90f, 0f);
                
                TryReplaceCell(i, j, quaternion, RoadType.Turning, _prefabPathTurningRoad);

                // Graph.ConnectRoads(GameObjectsGrid[i, j], GameObjectsGrid[i, j + 1]);
                // Graph.ConnectRoads(GameObjectsGrid[i, j], GameObjectsGrid[i + 1, j]);
                    
                return;
            }
            
            /*      
             *      |
             *    - -
             */
            if (_grid.LeftNeighbour(i, j) == CellType.Road && _grid.UpNeighbour(i, j) == CellType.Road)
            {
                quaternion.eulerAngles = new Vector3(0f, 270f, 0f);

                TryReplaceCell(i, j, quaternion, RoadType.Turning, _prefabPathTurningRoad);

                // Graph.ConnectRoads(GameObjectsGrid[i, j], GameObjectsGrid[i, j - 1]);
                // Graph.ConnectRoads(GameObjectsGrid[i, j], GameObjectsGrid[i - 1, j]);
                    
                return;
            }
            
            // /*      
            //  *   |
            //  *   - -
            //  */
            if (_grid.RightNeighbour(i, j) == CellType.Road && _grid.UpNeighbour(i, j) == CellType.Road)
            {
                TryReplaceCell(i, j, Quaternion.identity, RoadType.Turning, _prefabPathTurningRoad);
                
                // Graph.ConnectRoads(GameObjectsGrid[i, j], GameObjectsGrid[i, j + 1]);
                // Graph.ConnectRoads(GameObjectsGrid[i, j], GameObjectsGrid[i - 1, j]);
                
                return;
            }
        }
        private void Make3WaysRoad(int i, int j)
        {
            var quaternion = Quaternion.identity;

            /*      
            *    |
            *  - |
            *    |
            */
            if (_grid.LeftNeighbour(i, j) == CellType.Road && _grid.UpNeighbour(i, j) == CellType.Road &&
                _grid.DownNeighbour(i, j) == CellType.Road)
            {
                quaternion.eulerAngles = new Vector3(0f, 90f, 0f);
                TryReplaceCell(i, j, quaternion, RoadType.ThreeWays, _prefabPath3WaysRoad);
                
                // ConnectRoads(GameObjectsGrid[i, j - 1], GameObjectsGrid[i, j]);
                // ConnectRoads(GameObjectsGrid[i - 1, j], GameObjectsGrid[i, j]);
                // ConnectRoads(GameObjectsGrid[i + 1, j], GameObjectsGrid[i, j]);
                
                return;
            }

            /*      
            *    |
            *    | -
            *    |
            */
            if (_grid.RightNeighbour(i, j) == CellType.Road && _grid.UpNeighbour(i, j) == CellType.Road &&
                _grid.DownNeighbour(i, j) == CellType.Road)
            {
                quaternion.eulerAngles = new Vector3(0f, 270f, 0f);
                TryReplaceCell(i, j, quaternion, RoadType.ThreeWays, _prefabPath3WaysRoad);

                // ConnectRoads(GameObjectsGrid[i, j + 1], GameObjectsGrid[i, j]);
                // ConnectRoads(GameObjectsGrid[i - 1, j], GameObjectsGrid[i, j]);
                // ConnectRoads(GameObjectsGrid[i + 1, j], GameObjectsGrid[i, j]);
                
                return;
            }
            
            /*
              *    |
              *   ---
              */
            if (_grid.UpNeighbour(i, j) == CellType.Road && _grid.LeftNeighbour(i, j) == CellType.Road &&
                _grid.RightNeighbour(i, j) == CellType.Road)
            {
                quaternion.eulerAngles = new Vector3(0f, 180f, 0f);                
                TryReplaceCell(i, j, quaternion, RoadType.ThreeWays, _prefabPath3WaysRoad);
                //
                // ConnectRoads(GameObjectsGrid[i, j - 1], GameObjectsGrid[i, j]);
                // ConnectRoads(GameObjectsGrid[i - 1, j], GameObjectsGrid[i, j]);
                // ConnectRoads(GameObjectsGrid[i, j + 1], GameObjectsGrid[i, j]);
                
                return;
            }
            
            /*
              *  ---
              *   |
              */
            if (_grid.DownNeighbour(i, j) == CellType.Road && _grid.LeftNeighbour(i, j) == CellType.Road &&
                _grid.RightNeighbour(i, j) == CellType.Road)
            {
                TryReplaceCell(i, j, Quaternion.identity, RoadType.ThreeWays, _prefabPath3WaysRoad);

                // ConnectRoads(GameObjectsGrid[i, j - 1], GameObjectsGrid[i, j]);
                // ConnectRoads(GameObjectsGrid[i + 1, j], GameObjectsGrid[i, j]);
                // ConnectRoads(GameObjectsGrid[i, j + 1], GameObjectsGrid[i, j]);
                
                return;
            }
        }
        private void Make4WaysRoad(int i, int j)
        {
            TryReplaceCell(i, j, Quaternion.identity, RoadType.Cross, _prefabPathCrossRoad);

            // ConnectClosestNodesOnRoads(GameObjectsGrid[i - 1, j], GameObjectsGrid[i, j]);
            // ConnectClosestNodesOnRoads(GameObjectsGrid[i, j + 1], GameObjectsGrid[i, j]);
            // ConnectClosestNodesOnRoads(GameObjectsGrid[i, j - 1], GameObjectsGrid[i, j]);
            // ConnectClosestNodesOnRoads(GameObjectsGrid[i + 1, j], GameObjectsGrid[i, j]);
            // ConnectRoads(GameObjectsGrid[i - 1, j], GameObjectsGrid[i, j]);
            // ConnectRoads(GameObjectsGrid[i, j + 1], GameObjectsGrid[i, j]);
            // ConnectRoads(GameObjectsGrid[i, j - 1], GameObjectsGrid[i, j]);
            // ConnectRoads(GameObjectsGrid[i + 1, j], GameObjectsGrid[i, j]);
        }
        private void TryReplaceCell(int i, int j, Quaternion quaternion, RoadType roadType, string prefabPath)
        {
            if (GameObjectsGrid[i, j].RoadType == roadType)
            {
                GameObjectsGrid[i, j].transform.rotation = quaternion;
            }
            else
                ReplaceGameObject(
                    _assetProvider.Instantiate(prefabPath, GetCellPosition(i, j) + _roadYStep, quaternion), i, j);
            OnRoadChanged?.Invoke(i, j);
        }
        private void ReplaceCell(Cell cell) => ReplaceCell(cell.X, cell.Y, cell.CellType);
        private void ReplaceGameObject(GameObject insertedGameObject, int i, int j)
        {
            if (GameObjectsGrid[i, j] != null) 
                Object.Destroy(GameObjectsGrid[i, j].gameObject);
            GameObjectsGrid[i, j] = insertedGameObject.GetComponent<CellComponent>();

        }
        private Vector3 GetCellPosition(int i, int j) => _beginPosition + new Vector3(i * _cellSize.x, 0f, j * _cellSize.z);
    }
}
