using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    /// <summary>
    /// SudokuGrid contains the business logic for creating, modifying, and solving a Sudoku game.
    /// </summary>
    public class SudokuGrid
    {
        public SudokuCell[,] cells;
        public SudokuCell activeCell;
        public readonly int width, height, size;
        private IDictionary<Direction, Func<SudokuCell, SudokuCell>> mapping;
        // For picking possible solution paths.
        private readonly Random random = new Random();
        // Store cells relevant to a given cells validity.
        private IDictionary<SudokuCell, IEnumerable<SudokuCell>> neighbors;

        public bool AllCellsFilled => cells.All(cell => cell.Value != 0);

        /// <summary>
        /// Initializes a new instance of the SudokuSolver.SudokuGrid class. 
        /// </summary>
        public SudokuGrid(int width, int height, int size)
        {
            this.width = width;
            this.height = height;
            this.size = size;

            this.cells = GridOperations.Create(size);
            var movement = new Movement(cells);
            this.mapping = new Dictionary<Direction, Func<SudokuCell, SudokuCell>>
            {
                { Direction.Up, cell => movement.Up(cell) },
                { Direction.Down, cell => movement.Down(cell) },
                { Direction.Left, cell => movement.Left(cell) },
                { Direction.Right, cell => movement.Right(cell) },
                { Direction.JumpForward, cell => movement.JumpForward(cell) },
                { Direction.JumpBackward, cell => movement.JumpBackward(cell) }
            };

            this.neighbors = FindNeighbors();
            this.activeCell = cells[0, 0];
        }

        private IDictionary<SudokuCell, IEnumerable<SudokuCell>> FindNeighbors()
        {
            Dictionary<SudokuCell, IEnumerable<SudokuCell>> keyValuePairs = new Dictionary<SudokuCell, IEnumerable<SudokuCell>>();
            foreach (var cell in cells)
            {
                IEnumerable<SudokuCell> region = new List<SudokuCell>();
                activeCell = cell;
                region = Block.GetBox(cell, this).Union(Block.GetHorizontal(this)).Union(Block.GetVertical(this));
                if (activeCell.X + activeCell.Y == size - 1)
                    region = region.Union(Block.BottomLeftToTopRight(this));
                if (activeCell.X == activeCell.Y)
                    region = region.Union(Block.TopLeftToBottomRight(this));
                keyValuePairs[cell] = region;
            }
            return keyValuePairs;
        }

        public void Select(int x, int y) => activeCell = cells[x, y];

        /// <summary>
        /// Return the cell returned by moving according to a specific direction.
        /// </summary>
        public SudokuCell Shift(Direction dir, SudokuCell cell) => mapping[dir](activeCell);

        /// <summary>
        /// Change the active cell of the grid according to a specific direction.
        /// </summary>
        /// <param name="dir"></param>
        public void Shift(Direction dir) => activeCell = Shift(dir, activeCell);

        #region ModifyCells
        /// <summary>
        /// Modify a cell, returning true if the cell's value was changed.
        /// Updates known conflicts and jump forward if the cell had none.
        /// </summary>
        /// <param name="value">new value for the cell</param>
        /// <returns>true if the cell's value was changed, false otherwise</returns>
        public void ModifyCell(int value)
        {
            // Value changed, don't check conflicts when value=0.
            if (activeCell.SetValue(value))
            {
                // Find and add new conflicts.
                CheckConflicts();

                // Check validity to jump forward.
                if (activeCell.IsValid && activeCell.Value != 0)
                    Shift(Direction.JumpForward);
            }
        }

        /// <summary>
        /// Mark cells as invalid, if there are more than 2 that share the same value.
        /// </summary>
        private void CheckConflicts()
        {
            foreach (var cell in neighbors[activeCell])
            {
                if (cell != activeCell && cell.Value == activeCell.Value)
                {
                    activeCell.AddConflict(cell);
                }
            }
        }
        #endregion

        #region Solve
        /// <summary>
        /// Solve what remains of the sudoku game.
        /// </summary>
        public void Solve()
        {
            // Start in the top left corner.
            activeCell = cells[0, 0];
            // Move to first empty square (possibly first).
            Shift(Direction.JumpBackward);
            Shift(Direction.JumpForward);
            SolveCell();
        }
        private bool TryRandomFromList(IList<int> possNums)
        {
            if (possNums.Count < 1)
            {
                ModifyCell(0);
                return false;
            }

            int value = possNums[random.Next(0, possNums.Count)];
            // Remove value from list of possibilities.
            possNums.Remove(value);

            ModifyCell(value);
            return true;
        }

        private bool SolveCell()
        {
            // Break from method if no more cells are left.
            if (AllCellsFilled)
                return true;

            SudokuCell cell = activeCell;
            List<int> possNums = GetPossibleNums(cell);
            do
            {
                activeCell = cell;
                if (!TryRandomFromList(possNums))
                    return false;
            }
            while (!cell.IsValid || !SolveCell());

            return true;
        }

        public IList<SudokuCell> GetNeighbors(SudokuCell key)
        {
            return neighbors[key].ToList();
        }

        /// <summary>
        /// Return a list of numbers without conflicts at a given cell.
        /// </summary>
        /// <returns>list of numbers 1-size which could be valid in a cell</returns>
        private List<int> GetPossibleNums(SudokuCell cell)
        {
            List<int> nums = Enumerable.Range(1, size).ToList();
            for (int i = 1; i < size + 1; i++)
            {
                if (GetConflicts(cell, i))
                    nums.Remove(i);
            }
            return nums;
        }

        private bool GetConflicts(SudokuCell activeCell, int i)
        {
            foreach (SudokuCell cell in neighbors[activeCell])
            {
                if (i == cell.Value)
                    return true;
            }
            return false;
        }
        #endregion
    }
}