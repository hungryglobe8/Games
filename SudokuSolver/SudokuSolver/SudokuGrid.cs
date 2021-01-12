using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class SudokuGrid
    {
        public SudokuCell[,] cells;
        public SudokuCell activeCell;

        public SudokuGrid(int size)
        {
            cells = CreateCells(size);
            // Set the activeCell to (0, 0).
            Shift(0, 0);
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

        #region ShiftFocus
        /// <summary>
        /// Attempt to shift the focus of the game to a new square, which also changes the activeCell.
        /// If out of bounds, focus should not shift, but game continues running.
        /// </summary>
        /// <param name="x">new x value to try</param>
        /// <param name="y">new y value to try</param>
        private void Shift(int x, int y)
        {
            try
            {
                cells[x, y].Focus();
                // repetitive
                activeCell = cells[x, y];
            }
            catch (Exception)
            {
                // Focus remains unchanged if already at borders of grid.
            }
        }

        public void ShiftUp() => Shift(activeCell.X, activeCell.Y - 1);
        public void ShiftDown() => Shift(activeCell.X, activeCell.Y + 1);
        public void ShiftLeft() => Shift(activeCell.X - 1, activeCell.Y);
        public void ShiftRight() => Shift(activeCell.X + 1, activeCell.Y);
        
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

                value = possNums[random.Next(0, possNums.Count)];
                currentCell.Value = value;
                
                // Remove value from list of possibilities.
                possNums.Remove(value);
                // Active cell moves on.
                ShiftOpen();
                if (currentCell == activeCell)
                    return true;
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

        private bool IsValidMove(SudokuCell cell, int value)
        {
            for (int i = 0; i < 9; i++)
            {
                if (cell.Y == i)
                    continue;

                if (cells[cell.X, i].Value == value)
                    return false;
            }
            return true;
        }
        #endregion
    }
}
