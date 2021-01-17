using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class Movement
    {
        private SudokuGrid grid;
        public Movement(SudokuGrid grid)
        {
            this.grid = grid;   
        }

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
        public SudokuCell Up(SudokuCell cell) => cell.Y - 1 < 0 ? grid.cells[cell.X, grid.size - 1] : grid.cells[cell.X, cell.Y - 1];
        public SudokuCell Down(SudokuCell cell) => cell.Y + 1 > grid.size ? grid.cells[cell.X, 0] : grid.cells[cell.X, cell.Y + 1];
        public SudokuCell Left(SudokuCell cell) => cell.X - 1 < 0 ? grid.cells[grid.size - 1, cell.Y] : grid.cells[cell.X - 1, cell.Y];
        public SudokuCell Right(SudokuCell cell) => cell.X + 1 < 0 ? grid.cells[0, cell.Y] : grid.cells[cell.X + 1, cell.Y];
    }
}
