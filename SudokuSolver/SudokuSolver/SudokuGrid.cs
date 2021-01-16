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
        public int size;
        public int cellsLeft;
        // For picking possible solution paths.
        private readonly Random random = new Random();


        /// <summary>
        /// Initializes a new instance of the SudokuSolver.SudokuGrid class. 
        /// </summary>
        public SudokuGrid(int _size)
        {
            cells = CreateCells(_size);
            SelectCell(cells[0, 0]);
            size = _size;
            cellsLeft = size * size;
        }

        /// <summary>
        /// Create empty cells to fill a Sudoku board of size by size.
        /// </summary>
        private SudokuCell[,] CreateCells(int size)
        {
            SudokuCell[,] cells = new SudokuCell[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    cells[i, j] = new SudokuCell(i, j);
                }
            }
            return cells;
        }

        /// <summary>
        /// Select a cell, making it the activeCell of the grid.
        /// </summary>
        public void SelectCell(SudokuCell cell) => activeCell = cell;

        #region ModifyCells
        public bool ModifyCell(int value)
        {
            // Set activeCell value.
            if (activeCell.SetValue(value))
            {
                // Remove old conflicts.
                activeCell.RemoveConflicts();
                // Highlight any new invalid moves.
                ISet<SudokuCell> conflicts = GetConflicts(activeCell, value);
                foreach (var cell in conflicts)
                {
                    cell.AddConflict(activeCell);
                    cell.Notify();
                }
                activeCell.Notify();

                if (activeCell.IsValid)
                    JumpForward();

                cellsLeft--;
                return true;
            }
            else
            {
                activeCell.Notify();
                return false;
            }
        }

        /// <summary>
        /// Clear the activeCell, if it is not locked or already clear.
        /// Increase the number of cells available.
        /// </summary>
        public void Delete()
        {
            if (activeCell.SetValue(0))
                cellsLeft++;
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
            if (cellsLeft == 0)
                return false;

            // Loop until a different empty or invalid cell is reached.
            do
            {
                // Edge of board.
                if (activeCell.X == edge)
                    verticalShift();
                // Shift left or right.
                horizontalShift();
            } while (activeCell.Value != 0 || !activeCell.IsValid);
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
                Delete();
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
                
                // Active cell moves on.
                if (cellsLeft == 0)
                {
                    return true;
                }
            } while (!SolveCell());

            return true;
        }

        /// <summary>
        /// Return a list of numbers without conflicts at a given cell.
        /// </summary>
        /// <returns>list of numbers 1-9 which could be valid in a cell</returns>
        private List<int> GetPossibleNums(SudokuCell cell)
        {
            List<int> nums = Enumerable.Range(1, 9).ToList();
            for (int i = 1; i < 10; i++)
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
                if (i != x && cells[i, y].Value == value)
                    conflictingCells.Add(cells[i, y]);

                if (i != y && cells[x, i].Value == value)
                    conflictingCells.Add(cells[x, i]);

                // Check diagonals.
                //if (i != x && x == y && cells[i, i].Value == value)
                //    conflictingCells.Add(cells[i, i]);
            }
            // Check boxes don't have duplicates.
            // Ex: go from 5 - (2) to 5 - (2) + 3
            for (int i = x - (x % 3); i < x - (x % 3) + 3; i++)
            {
                for (int j = y - (y % 3); j < y - (y % 3) + 3; j++)
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
            cellsLeft = size * size;
        }

        /// <summary>
        /// Lock all cells on the grid.
        /// </summary>
        public void LockAll()
        {
            foreach (SudokuCell cell in cells)
            {
                cell.Lock();
            }
        }
    }
}
