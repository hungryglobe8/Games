using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class SudokuGrid
    {
        public SudokuCell[,] cells;
        public SudokuCell activeCell;
        public int size;
        public int cellsLeft;

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
                    // Create size*size cells.
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
        public bool ModifyCell(SudokuCell cell, int value)
        {
            // Do nothing if the cell is locked.
            if (cell.IsLocked)
                return false;

            // Clear the cell if value is 0.
            if (value == 0)
            {
                // Increase num of cellsLeft if old Value was not 0.
                if (cell.Value != 0)
                    cellsLeft++;
                cell.Clear();
                return true;
            }

            // Modify and warn invalid moves.
            if (GetConflicts(cell, value).Count == 0)
            {
                cell.SetValue(value, true);
                cell.ForeColor = SystemColors.ControlDarkDark;
                cellsLeft--;
                ShiftOpen();
            }
            else
            {
                cell.SetValue(value, false);
                cell.ForeColor = Color.Red;
            }
            cell.Text = value.ToString();
            return true;
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
        /// Shift to the next available open cell, including starting cell.
        /// If the end of the board is reached, perform no shift.
        /// </summary>
        public bool ShiftOpen()
        {
            // All cells are filled.
            if (cellsLeft == 0)
                return false;

            int count = 0;
            // Loop until an empty or invalid cell is reached.
            while (activeCell.Value != 0 || !activeCell.IsValid)
            {
                // Edge of board.
                if (activeCell.X == size - 1)
                    ShiftDown();
                // Always shift right.
                ShiftRight();
                count++;
            }
            return true;
        }
        #endregion

        #region Solve

        Random random = new Random();
        private bool SolveCell()
        {
            if (activeCell.IsLocked)
            {
                if (!ShiftOpen())
                    return true;
                return SolveCell();
            }

            // Remember this cell if we have to return to it later on.
            SudokuCell currentCell = activeCell;
            int value = 0;
            // Only consider the possibilities currently available.
            List<int> possNums = GetPossibleNums(currentCell);

            do
            {
                // No answers left, return false and go back to an earlier cell.
                if (possNums.Count < 1)
                {
                    currentCell.Clear();
                    cellsLeft++;
                    return false;
                }
                activeCell = currentCell;

                value = possNums[random.Next(0, possNums.Count)];
                // Remove value from list of possibilities.
                possNums.Remove(value);
                if (!ModifyCell(activeCell, value))
                    continue;

                // Active cell moves on.
                //ShiftOpen();
                if (cellsLeft == 0)
                {
                    currentCell.Text = value.ToString();
                    return true;
                }
            } while (GetConflicts(currentCell, value).Count != 0 || !SolveCell());

            currentCell.Text = value.ToString();
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
            // Move to first empty square.
            ShiftOpen();
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
        ///     unlocked, valid, values set to 0
        /// </summary>
        public void ClearBoard()
        {
            foreach (SudokuCell cell in cells)
            {
                cell.Clear();
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
