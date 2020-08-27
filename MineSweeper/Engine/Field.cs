using System;
using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// This class contains all of the logic for a MineSweeper field and 
    /// interactions between armed and nonarmed pieces.
    /// </summary>
    public class Field
    {
        #region Properties
        public int Width { get; }
        public int Height { get; }

        // 2-dimensional field of tiles (x,y).
        private readonly Tile[,] tiles;
        // Store mines.
        private readonly IList<Tile> mines;
        // Store neighbors.
        private readonly IDictionary<Tile, IList<Tile>> neighbors;
        public bool firstClick = false;

        public int NumMines { private set; get; }
        public int NumFlags { private set; get; }
        public int NumRevealed { private set; get; }
        // Returns true when all normal tiles have been revealed.
        public bool FoundAllNormalTiles => NumRevealed == (Width * Height) - NumMines;
        #endregion

        #region Constructor
        /// <summary>
        /// Create starting parameters for a field with a certain number of mines.
        /// </summary>
        /// <param name="width">num of tiles in the x direction</param>
        /// <param name="height">num of tiles in the y direction</param>
        /// <param name="_numMines"></param>
        public Field(int width, int height, int _numMines)
        {
            Width = width;
            Height = height;
            tiles = new Tile[width, height];
            // Populate tiles.
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    Tile tile = new Tile(x, y);
                    tiles[x, y] = tile;
                    //neighbors[tile]
                }
            }
            mines = new List<Tile>();
            NumMines = _numMines;
            NumFlags = NumMines;
            NumRevealed = 0;
        }
        #endregion

        #region Generate Field
        /// <summary>
        /// Populate the field with a certain number of mines (NumMines).
        /// Protect a chosen tile and the area around it from being armed, so that tile is guaranteed to be 0.
        /// If the tile is unchosen, pick the tile at (0, 0).
        /// </summary>
        /// <param name="seed">allows user to fix the random generator, if desired</param>
        public void PopulateField(Tile initialClick = null, int? seed = null)
        {
            // If no initial tile is chosen, pick one.
            if (initialClick == null)
                initialClick = GetTile(0, 0);
            // Guarantees first click to be a zero (more playable).
            IList<Tile> protectedSpace = GetNeighbors(initialClick);
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
                // Do not add if chosen space has been selected before or is within protected space.
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
                IList<Tile> neighbors = GetNeighbors(mine);
                foreach (Tile tile in neighbors)
                    tile.DangerUp();
            }

            // Field has been generated.
            firstClick = true;
        }
        #endregion

        #region Reveal Tile
        /// <summary>
        /// Driver for reveal tile functionality.
        /// Attempting to reveal an unpopulated field will result in population first.
        /// Returns a set of all tiles revealed by the algorithm.
        /// </summary>
        /// <param name="tile">initial tile to reveal</param>
        public ISet<Tile> Reveal(Tile tile)
        {
            // On first click, populate minefield.
            if (!firstClick)
                PopulateField(tile);

            ISet<Tile> revealedTiles = new HashSet<Tile>();
            Reveal(tile, revealedTiles);
            return revealedTiles;
        }

        /// <summary>
        /// Reveal a specific tile, changing its state, and revealing its neighbors
        /// if there are no bombs nearby.
        /// Doesn't do anything if tile is flagged or revealed.
        /// </summary>
        /// <param name="tile">tile to be revealed</param>
        /// <param name="revealedTiles">collection of revealed tiles</param>
        private void Reveal(Tile tile, ISet<Tile> revealedTiles)
        {
            // Only reveal an unopened tile.
            if (tile.state != State.Unopened)
                return;

            tile.LeftClick();
            revealedTiles.Add(tile);
            // Add to numRevealed if normal tile.
            if (!tile.IsArmed)
                NumRevealed++;

            // Recursively find all neighbors of 0 danger tiles.
            if (tile.GetDanger() == 0)
            {
                IList<Tile> neighbors = GetNeighbors(tile);
                foreach (Tile neighbor in neighbors)
                {
                    // Only reveal tiles that haven't been opened yet (to prevent infinite loop).
                    if (neighbor.state == State.Unopened)
                        Reveal(neighbor, revealedTiles);
                }
            }
        }
        #endregion

        #region Flag Tile
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
        #endregion
        
        #region Neighbors
        /// <summary>
        /// Get a list of neighbors for a given tile.
        /// </summary>
        public IList<Tile> GetNeighbors(Tile tile)
        {
            int x = tile.X;
            int y = tile.Y;

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
        #endregion

        #region Getters
        /// <summary>
        /// Get the mines of the field.
        /// </summary>
        public IList<Tile> GetMines() => mines;

        /// <summary>
        /// Get a tile at a specific coordinate.
        /// </summary>
        public Tile GetTile(int x, int y) => tiles[x, y];

        /// <summary>
        /// Get all tiles in the field.
        /// </summary>
        public IEnumerable<Tile> GetTiles()
        {
            IList<Tile> res = new List<Tile>();
            foreach (Tile tile in tiles)
                res.Add(tile);
            return res;
        }
        #endregion
    }
}
