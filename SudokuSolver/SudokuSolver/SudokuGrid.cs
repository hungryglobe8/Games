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
                    // Create size*size cells with locations.
                    cells[i, j] = new SudokuCell
                    {
                        X = i,
                        Y = j
                    };
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
            // Do nothing if the cell is locked or already empty.
            if (cell.IsLocked || (cell.Value == 0 && value == 0))
                return false;
            // Clear the cell if value is 0.
            if (value == 0)
            {
                cell.Clear();
                cellsLeft++;
                return true;
            }

            // Modify and warn invalid moves.
            if (IsValidMove(cell, value))
            {
                cell.Value = value;
                cell.Text = value.ToString();
                cell.ForeColor = SystemColors.ControlDarkDark;
                ShiftOpen();
                cellsLeft--;
            }
            else
            {
                cell.Value = value;
                cell.Text = value.ToString();
                cell.ForeColor = Color.Red;
            }
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
                // Focus wraps around
                if (newX.HasValue && newY.HasValue)
                    SelectCell(cells[newX.Value, newY.Value]);
            }
        }

        public void ShiftUp() => Shift(activeCell.X, activeCell.Y - 1, activeCell.X, size - 1);
        public void ShiftDown() => Shift(activeCell.X, activeCell.Y + 1, activeCell.X, 0);
        public void ShiftLeft() => Shift(activeCell.X - 1, activeCell.Y, size - 1, activeCell.Y);
        public void ShiftRight() => Shift(activeCell.X + 1, activeCell.Y, 0, activeCell.Y);
        
        /// <summary>
        /// Shift to the next available open cell.
        /// If the end of the board is reached, perform no shift.
        /// </summary>
        public void ShiftOpen()
        {
            SudokuCell oldActive = activeCell;
            // Loop until an empty cell or the end of the board is reached.
            while (activeCell.Value != 0)
            {
                // End of board.
                if (activeCell.X == 8 && activeCell.Y == 8)
                {
                    Shift(oldActive.X, oldActive.Y);
                    return;
                }
                // Edge of board.
                if (activeCell.X == 8)
                    Shift(0, activeCell.Y + 1);
                else
                    ShiftRight();
            }
            return;
        }
        #endregion

        #region Solve

        Random random = new Random();
        private bool SolveCell()
        {
            if (activeCell.IsLocked)
            {
                ShiftRight();
                return SolveCell();
            }
            // Remember this cell if we have to return to it later on.
            SudokuCell currentCell = activeCell;
            int value = 0;
            List<int> possNums = new List<int>{ 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Try a new value from remaining possibilities.
            do
            {
                // No answers left, return false and go back to an earlier cell.
                if (possNums.Count < 1)
                {
                    currentCell.Value = 0;
                    return false;
                }
                activeCell = currentCell;

                value = possNums[random.Next(0, possNums.Count)];         
                // Remove value from list of possibilities.
                possNums.Remove(value);
                if (!ModifyCell(activeCell, value))
                    continue;
                // Active cell moves on.
                ShiftOpen();
                if (currentCell == activeCell && IsValidMove(currentCell, value))
                {
                    currentCell.Text = value.ToString();
                    return true;
                }
            } while (!IsValidMove(currentCell, value) || !SolveCell());

            currentCell.Text = value.ToString();
            return true;
        }
        /// <summary>
        /// Solve the game.
        /// </summary>
        public void Solve()
        {
            // Start in the top left corner.
            Shift(0, 0);
            SolveCell();
            Console.WriteLine("Made it out!");
        }

        /// <summary>
        /// Checks if a new number for a cell is a valid one, based on its neighbors.
        /// </summary>
        /// <returns>true if valid, false if not</returns>
        private bool IsValidMove(SudokuCell cell, int value)
        {
            int x = cell.X;
            int y = cell.Y;
            // Check columns and rows don't have duplicates.
            for (int i = 0; i < 9; i++)
            {
                if (i != x && cells[i, y].Value == value)
                    return false;

                if (i != y && cells[x, i].Value == value)
                    return false;
            }
            // Check boxes don't have duplicates.
            // Ex: go from 5 - (2) to 5 - (2) + 3
            for (int i = x - (x % 3); i < x - (x % 3) + 3; i++)
            {
                for (int j = y - (y % 3); j < y - (y % 3) + 3; j++)
                {
                    if (i != x && j != y && cells[i, j].Value == value)
                        return false;
                }
            }
            return true;
        }
        #endregion

        #region Locking
        /// <summary>
        /// Lock all cells on the grid.
        /// </summary>
        public void LockAll()
        {
            foreach (SudokuCell cell in cells)
            {
                LockSingle(cell);
            }
        }
        
        /// <summary>
        /// Lock a single cell on the grid, if it has been filled already.
        /// </summary>
        public void LockSingle(SudokuCell cell)
        {
            if (cell.Text != string.Empty)
            {
                cell.IsLocked = true;
                cell.ForeColor = Color.Black;
            }
        }
        #endregion
    }
}
