using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        JumpForward,
        JumpBackward
    }

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
        /// Return the cell above or wrap around.
        /// </summary>
        public SudokuCell Up(SudokuCell cell) => Select(cell.X, cell.Y - 1);
        /// <summary>
        /// Return the cell below or wrap around.
        /// </summary>
        public SudokuCell Down(SudokuCell cell) => Select(cell.X, cell.Y + 1);
        /// <summary>
        /// Return the cell to the left or wrap around.
        /// </summary>
        public SudokuCell Left(SudokuCell cell) => Select(cell.X - 1, cell.Y);
        /// <summary>
        /// Return the cell to the right or wrap around.
        /// </summary>
        public SudokuCell Right(SudokuCell cell) => Select(cell.X + 1, cell.Y);
        /// <summary>
        /// Jump to the next available open or invalid cell (right and down).
        /// </summary>
        public SudokuCell JumpForward(SudokuCell cell) => Jump(cell, width - 1, Down, Right);
        /// <summary>
        /// Jump to the last available open or invalid cell (left and up).
        /// </summary>
        public SudokuCell JumpBackward(SudokuCell cell) => Jump(cell, 0, Up, Left);

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

        /// <summary>
        /// Jump to the next available open or invalid cell.
        /// Can jump forward or backward, and past grid's borders.
        /// </summary>
        private SudokuCell Jump(SudokuCell cell, int edge, Func<SudokuCell, SudokuCell> verticalShift, Func<SudokuCell, SudokuCell> horizontalShift)
        {
            // Save the starting cell.
            SudokuCell startCell = cell;
            // Loop until a different empty or invalid cell is reached.
            do
            {
                // Edge of board.
                if (cell.X == edge)
                    cell = verticalShift(cell);
                // Shift left or right.
                cell = horizontalShift(cell);
            } while (cell.Value != 0 || cell.Equals(startCell));
            return cell;
        }
    }
}