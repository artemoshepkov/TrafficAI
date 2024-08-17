using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure
{
    public enum CellType
    {
        Empty,
        Road,
        House,
        None
    }
    public class Cell
    {
        public int X;
        public int Y;
        public CellType CellType;
        public CellType[] Neighbours;
        /*
         *      1
         *  0   c   2
         *      3
         */
        public Cell(int x, int y, CellType cellType, CellType[] neighbours)
        {
            X = x;
            Y = y;
            CellType = cellType;
            Neighbours = neighbours;
        }

        public Cell(int x, int y, CellType cellType)
        {
            X = x;
            Y = y;
            CellType = cellType;
        }
    }

    public class CityGrid
    {
        private CellType[,] _grid;

        public event Action<Cell> OnGridChanged;

        public List<Cell> Roads = new List<Cell>();
        // private Graph _graph = new Graph();

        public int SizeX => _grid.GetLength(1);
        public int SizeY => _grid.GetLength(0);
        
        public CityGrid(int sizeX, int sizeY)
        {
            _grid = new CellType[sizeY, sizeX];
        }
        public CityGrid(CellType[,] cellTypes)
        {
            _grid = cellTypes;
            
            for (int i = 0; i < SizeX; i++)
            for (int j = 0; j < SizeY; j++)
                if (_grid[i, j] == CellType.Road)
                {
                    Roads.Add(new Cell(i, j, _grid[i, j]));
                }
        }

        public CellType[] GetNeighbours(int i, int j)
        {
            return new CellType[]
            {
                LeftNeighbour(i, j), 
                UpNeighbour(i, j), 
                RightNeighbour(i, j), 
                DownNeighbour(i, j), 
            };
        }
        public CellType this[int i, int j]
        {
            get => _grid[i, j];
            set
            {
                _grid[i, j] = value;
                OnGridChanged?.Invoke(new Cell(i, j, value, GetNeighbours(i, j) ));
                
                if (value == CellType.Road)
                    Roads.Add(new Cell(i, j, value));
            }
        }

        public CellType LeftNeighbour(int i, int j) => j > 0 ? _grid[i, j - 1] : CellType.None;
        public CellType UpNeighbour(int i, int j) => i > 0 ? _grid[i - 1, j] : CellType.None;
        public CellType RightNeighbour(int i, int j) => j < SizeX - 1 ? _grid[i, j + 1] : CellType.None;
        public CellType DownNeighbour(int i, int j) => i < SizeY - 1 ? _grid[i + 1, j] : CellType.None;
    }
}