using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class Movement
    {
        private SudokuCell[,] cells;
        private int width, height;

        public Movement(SudokuCell[,] cells)
        {
            this.cells = cells;
            this.width = cells.GetLength(0);
            this.height = cells.GetLength(1);
        }

        /// <summary>
        /// Returns the cell indicated by x and y.
        /// If out of bounds, wraps around to the other side of the grid.
        /// </summary>
        private SudokuCell Select(int x, int y)
        {
            if (x < 0)
                x = width - 1;
            else if (x >  width - 1)
                x = 0;

            if (y < 0)
                y = height - 1;
            else if (y > height - 1)
                y = 0;

            return cells[x, y];
        }
        public SudokuCell Up(SudokuCell cell) => Select(cell.X, cell.Y - 1);
        public SudokuCell Down(SudokuCell cell) => Select(cell.X, cell.Y + 1);
        public SudokuCell Left(SudokuCell cell) => Select(cell.X - 1, cell.Y);
        public SudokuCell Right(SudokuCell cell) => Select(cell.X + 1, cell.Y);
    }
}
