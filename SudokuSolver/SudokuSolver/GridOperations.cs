using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public static class GridOperations
    {
        /// <summary>
        /// Create empty cells to fill a Sudoku board of size by size.
        /// Regions of width by height are differentiated by shading.
        /// </summary>
        public static SudokuCell[,] Create(int size)
        {
            SudokuCell[,] cells = new SudokuCell[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    cells[i, j] = new SudokuCell(i, j);
                }
            }
            return cells;
        }

        public static IEnumerable<SudokuCell> ToEnumerable<T>(this SudokuCell[,] cells)
        {
            foreach (var cell in cells)
            {
                yield return cell;
            }
        }

        public static bool All(this SudokuCell[,] cells, Func<SudokuCell, bool> condition) => cells.ToEnumerable<SudokuCell>().All(condition);

        public static void Process(this SudokuCell[,] cells, Action<SudokuCell> action)
        {
            foreach (var cell in cells)
            {
                action(cell);
            }
        }

        /// <summary>
        /// Lock all cells on the grid.
        /// </summary>
        public static void LockAll(this SudokuCell[,] cells)
        {
            foreach (var cell in cells)
            {
                cell.SetValue(1);
                cell.Lock();
                //cell.Notify();
            }
        }

        /// <summary>
        /// Resets all cells to their default state:
        ///     unlocked, values set to 0
        /// cellsLeft gets reset.
        /// </summary>
        public static void Clear(this SudokuCell[,] cells)
        {
            foreach (var cell in cells)
            {
                cell.Unlock();
                cell.SetValue(0);
            }
        }
    }
}
