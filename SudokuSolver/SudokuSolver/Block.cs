using System;
using System.Collections.Generic;

namespace SudokuSolver
{
    public static class Block
    {
        /// <summary>
        /// Get a list of sudoku cells with matching X as activeCell.
        /// </summary>
        public static IList<SudokuCell> GetHorizontal(SudokuGrid grid) => GetCells(grid, Direction.Right);
        /// <summary>
        /// Get a list of sudoku cells with matching Y as activeCell.
        /// </summary>
        public static IList<SudokuCell> GetVertical(SudokuGrid grid) => GetCells(grid, Direction.Down);
        public static IList<SudokuCell> BottomLeftToTopRight(SudokuGrid grid) => GetDiagonal(grid, Direction.Up, Direction.Right);
        public static IList<SudokuCell> TopLeftToBottomRight(SudokuGrid grid) => GetDiagonal(grid, Direction.Down, Direction.Right);

        private static IList<SudokuCell> GetCells(SudokuGrid grid, Direction dir)
        {
            List<SudokuCell> cellList = new List<SudokuCell>();
            for (int i = 0; i < grid.size; i++)
            {
                // Add and shift the active cell.
                cellList.Add(grid.activeCell);
                grid.Shift(dir);
            }
            return cellList;
        }

        private static IList<SudokuCell> GetDiagonal(SudokuGrid grid, Direction vertical, Direction horizontal)
        {
            List<SudokuCell> cellList = new List<SudokuCell>();
            for (int i = 0; i < grid.size; i++)
            {
                cellList.Add(grid.activeCell);
                grid.Shift(vertical);
                grid.Shift(horizontal);
            }
            return cellList;
        }

        public static IList<SudokuCell> GetBox(SudokuCell cell, SudokuGrid grid)
        {
            List<SudokuCell> cellList = new List<SudokuCell>();
            int x = cell.X;
            int y = cell.Y;
            int width = grid.width;
            int height = grid.height;
            // Check boxes don't have duplicates.
            // Ex: go from 5 - (2) to 5 - (2) + 3
            for (int i = x - (x % width); i < x - (x % width) + width; i++)
            {
                for (int j = y - (y % height); j < y - (y % height) + height; j++)
                {
                    cellList.Add(grid.cells[i, j]);
                }
            }
            return cellList;
        }
    }
}
