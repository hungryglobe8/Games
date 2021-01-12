﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class SudokuGrid
    {
        public SudokuCell[,] cells;
        public SudokuCell activeCell;

        public SudokuGrid(int size)
        {
            cells = new SudokuCell[size, size];
            activeCell = null;
        }

        #region FocusShifting
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
        #endregion
    }
}
