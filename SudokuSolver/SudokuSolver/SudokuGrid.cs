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
        private object groups;
        public SudokuCell activeCell;
        public readonly int width, height, size;
        // For picking possible solution paths.
        private readonly Random random = new Random();

        /// <summary>
        /// Initializes a new instance of the SudokuSolver.SudokuGrid class. 
        /// </summary>
        public SudokuGrid(int width, int height, int size)
        {
            this.width = width;
            this.height = height;
            this.size = size;

            this.cells = CreateCells();
            this.groups = CreateGroups();
            SelectCell(cells[0, 0]);
        }

        private IList<IEnumerable<SudokuCell>> CreateGroups()
        {
            var list = new List<SudokuCell>();
            foreach (var cell in cells)
            {
                list.Add(cell);
            }

            IList<IEnumerable<SudokuCell>> collection = new List<IEnumerable<SudokuCell>>();
            for (int i = 0; i < size; i++)
            {
                var col = from cell in list where cell.X == i select cell;
                var row = from cell in list where cell.Y == i select cell;
                var box = from cell in list where CoorsInBox(cell.X, cell.Y, i) select cell;

                collection.Add(row);
                collection.Add(col);
                collection.Add(box);
            }

            return collection;
        }

        private bool CoorsInBox(int x, int y, int i)
        {
            int x_range = (x - (x % width)) / width;
            int y_range = y - (y % height);
            return x_range + y_range == i;
        }

        /// <summary>
        /// Create empty cells to fill a Sudoku board of size by size.
        /// Regions of width by height are differentiated by shading.
        /// </summary>
        private SudokuCell[,] CreateCells()
        {
            SudokuCell[,] cells = new SudokuCell[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    cells[i, j] = new SudokuCell(i, j)
                    {
                        // Choose one of two backColors based on location.
                        BackColor = ((i / width) + (j / height)) % 2 == 0 ? SystemColors.Control : Color.DarkGray
                    };
                }
            }
            return cells;
        }

        private void AddNeighbors(SudokuCell cell)
        {
            bool horizontal, vertical, diagonal, box = true;
            // Add columns and rows.
            AddRowsAndCols(cell);
            // Add diagonals.
            AddDiagonals(cell);
            // Add boxes.
            AddBoxes(cell);
            // Remove self.
            cell.Neighbors.Remove(cell);
        }

        private void AddDiagonals(SudokuCell cell)
        {
            for (int i = 0; i < size; i++)
            {
                SudokuCell leftDiag = cells[i, i];
                SudokuCell rightDiag = cells[size - 1 - i, i];

                if (cell.X == cell.Y)
                    cell.AddNeighbor(leftDiag);
                if (cell.X + cell.Y == size - 1)
                    cell.AddNeighbor(rightDiag);
            }
        }

        private void AddRowsAndCols(SudokuCell cell)
        {
            for (int i = 0; i < size; i++)
            {
                SudokuCell horizontal = cells[i, cell.Y];
                SudokuCell vertical = cells[cell.X, i];

                cell.AddNeighbor(horizontal);
                cell.AddNeighbor(vertical);
            }
        }

        private void AddBoxes(SudokuCell cell)
        {
            int x = cell.X;
            int y = cell.Y;
            // Ex: go from 5 - (2) to 5 - (2) + 3
            for (int i = x - (x % width); i < x - (x % width) + width; i++)
            {
                for (int j = y - (y % height); j < y - (y % height) + height; j++)
                {
                    cell.AddNeighbor(cells[i, j]);
                }
            }
        }


        /// <summary>
        /// Select a cell, making it the activeCell of the grid.
        /// </summary>
        public void SelectCell(SudokuCell cell) => activeCell = cell;

        #region ModifyCells
        /// <summary>
        /// Modify a cell, returning true if the cell's value was changed.
        /// Updates known conflicts and jump forward if the cell had none.
        /// </summary>
        /// <param name="value">new value for the cell</param>
        /// <returns>true if the cell's value was changed, false otherwise</returns>
        public bool ModifyCell(int value)
        {
            int oldValue = activeCell.Value;
            // Set activeCell value.
            if (activeCell.SetValue(value))
            {
                // Conflicts must be updated with change.
                CheckConflicts();
                //UpdateConflicts(value);
                // Check validity to jump forward.
                if (activeCell.IsValid)
                    JumpForward();
                return true;
            }
            return false;
        }

        private void CheckConflicts()
        {
            foreach (var neighbor in activeCell.Neighbors)
            {
                if (activeCell.Value == neighbor.Value)
                {
                    activeCell.IsValid = false;
                    neighbor.IsValid = false;
                }
                else
                {
                    neighbor.IsValid = true;
                }
                activeCell.Notify();
                neighbor.Notify();
            }
        }

        /// <summary>
        /// Remove old conflicts, look for new ones, and notify all involved cells.
        /// </summary>
        private void UpdateConflicts(int value)
        {
            // Remove old conflicts.
            IList<SudokuCell> oldConflicts = activeCell.Conflicts.ToList<SudokuCell>();
            UpdateStatus(oldConflicts, activeCell.RemoveConflict);

            // Show new conflicts.
            ISet<SudokuCell> newConflicts = GetConflicts(activeCell, value);
            UpdateStatus(newConflicts, activeCell.AddConflict);

            activeCell.Notify();
        }

        private void UpdateStatus(IEnumerable<SudokuCell> cells, Action<SudokuCell> action)
        {
            foreach (var cell in cells)
            {
                action(cell);
                cell.Notify();
            }
        }
        #endregion

        #region ShiftFocus
        /// <summary>
        /// Attempt to select a new square, which also changes the activeCell.
        /// If out of bounds, focus should wrap around the board on the same row or column.
        /// </summary>
        /// <param name="x">new x value to try</param>
        /// <param name="y">new y value to try</param>
        private void Shift(int x, int y, int? newX = null, int? newY = null)
        {
            try
            {
                SelectCell(cells[x, y]);
            }
            catch (Exception)
            {
                // Focus wraps around.
                if (newX.HasValue && newY.HasValue)
                    SelectCell(cells[newX.Value, newY.Value]);
            }
        }

        public void ShiftUp() => Shift(activeCell.X, activeCell.Y - 1, activeCell.X, size - 1);
        public void ShiftDown() => Shift(activeCell.X, activeCell.Y + 1, activeCell.X, 0);
        public void ShiftLeft() => Shift(activeCell.X - 1, activeCell.Y, size - 1, activeCell.Y);
        public void ShiftRight() => Shift(activeCell.X + 1, activeCell.Y, 0, activeCell.Y);

        /// <summary>
        /// Jump to the next available open or invalid cell.
        /// Can jump forward or backward, and past grid's borders.
        /// </summary>
        private bool Jump(int edge, Action verticalShift, Action horizontalShift)
        {
            // All cells are filled.
            if (AllCellsFilled())
                return false;

            // Loop until a different empty or invalid cell is reached.
            do
            {
                // Edge of board.
                if (activeCell.X == edge)
                    verticalShift();
                // Shift left or right.
                horizontalShift();
                // || !activeCell.IsValid
            } while (activeCell.Value != 0);
            return true;
        }

        public bool AllCellsFilled()
        {
            foreach (var cell in cells)
            {
                if (cell.Value == 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Jump to the next available open or invalid cell to the right and down.
        /// </summary>
        /// <returns></returns>
        public bool JumpForward()
        {
            return Jump(size - 1, ShiftDown, ShiftRight);
        }
        /// <summary>
        /// Jump to the next available open or invalid cell to the left and up.
        /// </summary>
        public bool JumpBackward()
        {
            return Jump(0, ShiftUp, ShiftLeft);
        }
        #endregion

        #region Solve

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

            return ModifyCell(value);
        }
        private bool SolveCell()
        {
            // Remember this cell if we have to return to it later on.
            SudokuCell currentCell = activeCell;
            // Only consider the possibilities currently available.
            List<int> possNums = GetPossibleNums(currentCell);

            do
            {
                activeCell = currentCell;
                // Try another random possibility for the activeCell.
                if (!TryRandomFromList(possNums))
                    return false;

                // Break from loop if no more cells are left.
                if (AllCellsFilled())
                {
                    return true;
                }
            } while (!SolveCell());

            return true;
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
                if (GetConflicts(cell, i).Count != 0)
                    nums.Remove(i);
            }
            return nums;
        }

        /// <summary>
        /// Solve the game.
        /// </summary>
        public void Solve()
        {
            // Start in the top left corner.
            Shift(0, 0);
            // Move to first empty square (possibly first).
            JumpBackward();
            JumpForward();
            SolveCell();
        }

        /// <summary>
        /// Checks for conflicts when changing the value of a cell, based on its neighbors.
        /// </summary>
        /// <returns>list of conflicting cells</returns>
        private ISet<SudokuCell> GetConflicts(SudokuCell cell, int value)
        {
            ISet<SudokuCell> conflictingCells = new HashSet<SudokuCell>();
            int x = cell.X;
            int y = cell.Y;
            // Check columns and rows don't have duplicates.
            for (int i = 0; i < size; i++)
            {
                SudokuCell xCell = cells[i, y];
                SudokuCell yCell = cells[x, i];
                SudokuCell leftDiagCell = cells[i, i];
                SudokuCell rightDiagCell = cells[i, size - 1 - i];
                if (cell != xCell && xCell.Value == value)
                    conflictingCells.Add(xCell);

                if (cell != yCell && yCell.Value == value)
                    conflictingCells.Add(yCell);

                // Check diagonals.
                //if (x == y && leftDiagCell != cell && leftDiagCell.Value == value)
                //    conflictingCells.Add(leftDiagCell);
                //if (x + y == size - 1 && rightDiagCell != cell && rightDiagCell.Value == value)
                //    conflictingCells.Add(rightDiagCell);
            }
            // Check boxes don't have duplicates.
            // Ex: go from 5 - (2) to 5 - (2) + 3
            for (int i = x - (x % width); i < x - (x % width) + width; i++)
            {
                for (int j = y - (y % height); j < y - (y % height) + height; j++)
                {
                    if (i != x && j != y && cells[i, j].Value == value)
                        conflictingCells.Add(cells[i, j]);
                }
            }
            return conflictingCells;
        }
        #endregion

        /// <summary>
        /// Resets all cells to their default state:
        ///     unlocked, values set to 0
        /// cellsLeft gets reset.
        /// </summary>
        public void ClearBoard()
        {
            foreach (SudokuCell cell in cells)
            {
                cell.Unlock();
                cell.SetValue(0);
            }
        }

        /// <summary>
        /// Lock all cells on the grid.
        /// </summary>
        public void LockAll()
        {
            foreach (SudokuCell cell in cells)
            {
                cell.Lock();
                cell.Notify();
            }
        }
    }
}
