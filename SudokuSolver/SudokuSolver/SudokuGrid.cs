using System;
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
        /// Check that a value is within the grid.
        /// </summary>
        private bool ValueInRange(int value)
        {
            int MIN = 0;
            int MAX = 9;
            return value > MIN && value < MAX;
        }

        public void ShiftUp()
        {
            cells[activeCell.X, ValueInRange(activeCell.Y - 1) ? activeCell.Y - 1 : 0].Focus();
        }
        #endregion
    }
}
