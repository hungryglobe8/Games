using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    struct Coordinate
    {
        public int x, y;

        public Coordinate(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public static Coordinate operator +(Coordinate a, Coordinate b)
        {
            return new Coordinate(a.x + b.x, a.y + b.y);
        }
    }

    public class Field
    {
        // 2-dimensional field of tiles (x,y)
        private Tile[,] tiles;
        public int NumMines { private set; get; }

        /// <summary>
        /// Create starting parameters for a field with a certain number of mines.
        /// </summary>
        /// <param name="x">num of tiles in the x direction</param>
        /// <param name="y">num of tiles in the y direction</param>
        /// <param name="_numMines"></param>
        public Field(int x, int  y, int _numMines)
        {
            tiles = new Tile[x, y];
            NumMines = _numMines;
        }

        /// <summary>
        /// Populate the field with NumMines.
        /// </summary>
        /// <param name="seed">allows user to fix the random generator, if desired</param>
        public void PopulateField(int? seed = null)
        {
            // If seed has a value, rnd uses it. Else use time-dependent generator.
            Random rnd = seed.HasValue ? new Random(seed.Value) : new Random();

            // Store mine coordinates.
            IList<Coordinate> mineCoordinates = new List<Coordinate>();
            int count = 0;
            while (count < NumMines)
            {
                int row = rnd.Next(tiles.GetLength(0));
                int col = rnd.Next(tiles.GetLength(1));
                Coordinate coor = new Coordinate(row, col);
                // Only add mine if there isn't a mine already at the chosen location.
                if (tiles[row, col] == null)
                {
                    tiles[row, col] = new Tile(true);
                    // Add to mine list.
                    mineCoordinates.Add(coor);
                    // Increase counter.
                    count++;
                }
                // Choose a new location.       
            }
            
            // Add normal tiles.
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (tiles[i, j] == null)
                        tiles[i, j] = new Tile();
                }
            }

            // Increase danger of tiles next to mines.
            foreach (Coordinate mineCoor in mineCoordinates)
            {
                IList<Tile> neighbors = GetNeighbors(mineCoor.x, mineCoor.y);
                foreach (Tile tile in neighbors)
                    tile.DangerUp();
            }
        }

        /// <summary>
        /// Get a tile at a specific coordinate.
        /// </summary>
        public Tile GetTile(int x, int y)
        {
            return tiles[x, y];
        }

        /// <summary>
        /// Get a list of neighbors for a given coordinate.
        /// </summary>
        public IList<Tile> GetNeighbors(int x, int y)
        {
            IList<Tile> neighbors = new List<Tile>();
            int lowX = x - 1;
            int lowY =  y - 1;
            int highX = x + 1;
            int highY = y + 1;
            //bottom left
            if (lowX >= 0 && lowY >= 0)
                neighbors.Add(tiles[lowX, lowY]);
            //bottom middle
            if (lowY >= 0)
                neighbors.Add(tiles[x, lowY]);
            //bottom right
            if (highX < tiles.GetLength(0) && lowY >= 0)
                neighbors.Add(tiles[highX, lowY]);
            //middle left
            if (lowX >= 0)
                neighbors.Add(tiles[lowX, y]);
            //middle right
            if (highX < tiles.GetLength(0))
                neighbors.Add(tiles[highX, y]);
            //top left
            if (lowX >= 0 && highY < tiles.GetLength(1))
                neighbors.Add(tiles[lowX, highY]);
            //top middle
            if (highY < tiles.GetLength(1))
                neighbors.Add(tiles[x, highY]);
            //top right
            if (highX < tiles.GetLength(0) && highY < tiles.GetLength(1))
                neighbors.Add(tiles[highX, highY]);
            return neighbors;
        }
    }
}
