using System;
using System.Collections.Generic;

namespace Engine
{
    public class Field
    {
        public int Width { get; }
        public int Height { get; }

        // 2-dimensional field of tiles (x,y).
        private readonly Tile[,] tiles;
        // Store mines.
        private readonly IList<Tile> mines;
        private readonly bool firstClick = false;

        public int NumMines { private set; get; }
        public int NumFlags { set; get; }
        public int NumRevealed { get; private set; }

        /// <summary>
        /// Create starting parameters for a field with a certain number of mines.
        /// </summary>
        /// <param name="x">num of tiles in the x direction</param>
        /// <param name="y">num of tiles in the y direction</param>
        /// <param name="_numMines"></param>
        public Field(int x, int y, int _numMines)
        {
            Width = x;
            Height = y;
            tiles = new Tile[x, y];
            // Populate tiles.
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (tiles[i, j] == null)
                        tiles[i, j] = new Tile(i, j);
                }
            }
            mines = new List<Tile>();
            NumMines = _numMines;
            NumFlags = NumMines;
        }

        /// <summary>
        /// Populate the field with NumMines.
        /// </summary>
        /// <param name="seed">allows user to fix the random generator, if desired</param>
        public void PopulateField(Tile initialClick, int? seed = null)
        {
            // Guarantees first click to be a zero (more playable).
            IList<Tile> protectedSpace = GetNeighbors(initialClick.X, initialClick.Y);
            protectedSpace.Add(initialClick);

            // If seed has a value, rnd uses it. Else use time-dependent generator.
            Random rnd = seed.HasValue ? new Random(seed.Value) : new Random();

            // Store mine coordinates.
            int count = 0;
            while (count < NumMines)
            {
                int row = rnd.Next(tiles.GetLength(0));
                int col = rnd.Next(tiles.GetLength(1));
                Tile potentialMine = tiles[row, col];
                // Only add mine if there isn't a mine already at the chosen location.
                if (!potentialMine.IsArmed && !protectedSpace.Contains(potentialMine))
                {
                    potentialMine.AddMine();
                    // Add to mine list.
                    mines.Add(tiles[row, col]);
                    // Increase counter.
                    count++;
                }
                // Choose a new location.       
            }

            // Increase danger of tiles next to mines.
            foreach (Tile mine in mines)
            {
                IList<Tile> neighbors = GetNeighbors(mine.X, mine.Y);
                foreach (Tile tile in neighbors)
                    tile.DangerUp();
            }
        }

        /// <summary>
        /// Flag a given tile. If there are no flags left undo the right click.
        /// </summary>
        /// <param name="tile"></param>
        public void Flag(Tile tile)
        {
            // Update number of flags.
            tile.RightClick();
            if (tile.state == State.Flagged)
                NumFlags--;
            else if (tile.state == State.Unopened)
                NumFlags++;
            // If no more flags available, undo right click.
            if (NumFlags < 0)
            {
                NumFlags++;
                tile.RightClick();
            }
        }

        /// <summary>
        /// Get the mines of the field.
        /// </summary>
        public IList<Tile> GetMines()
        {
            return mines;
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
            int lowY = y - 1;
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

        public IEnumerable<Tile> GetTiles()
        {
            IList<Tile> res = new List<Tile>();
            foreach (Tile tile in tiles)
                res.Add(tile);
            return res;
        }

        public bool WonGame() => NumRevealed == (Width * Height) - NumMines;
    }
}
