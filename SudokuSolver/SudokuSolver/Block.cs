using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    public struct BlockFlag
    {
        public BlockFlag(bool boxes, bool xSudoku)
        {
            Boxes = boxes;
            XSudoku = xSudoku;
        }

        public bool Boxes { get; }
        public bool XSudoku { get; }
    }

    public static class Block
    {
        public static IDictionary<SudokuCell, IEnumerable<SudokuCell>> FindNeighbors(SudokuGrid grid, BlockFlag gameMode)
        {
            Dictionary<SudokuCell, IEnumerable<SudokuCell>> keyValuePairs = new Dictionary<SudokuCell, IEnumerable<SudokuCell>>();
            foreach (var cell in grid.cells)
            {
                // Focus on the cell under inspection.
                grid.activeCell = cell;
                // Always get horizontal and vertical neighbors.
                IEnumerable<SudokuCell> region = GetHorizontal(grid).Union(GetVertical(grid));
                // Usually get nearby neighbors.
                if (gameMode.Boxes)
                    region = region.Union(GetBox(cell, grid));
                if (gameMode.XSudoku)
                {
                    // Only look at true diagonals in XSudoku.
                    if (grid.activeCell.X + grid.activeCell.Y == grid.size - 1)
                        region = region.Union(BottomLeftToTopRight(grid));
                    if (grid.activeCell.X == grid.activeCell.Y)
                        region = region.Union(TopLeftToBottomRight(grid));
                }
                // Add region to dictionary for easy access later.
                keyValuePairs[cell] = region;
            }
            return keyValuePairs;
        }

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
