using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public struct Factor
    {
        public int First { get; private set; }
        public int Second { get; private set; }

        public Factor(int first, int second)
        {
            First = first;
            Second = second;
        }
    };

    public static class GridOperations
    {
        /// <summary>
        /// Create empty cells to fill a Sudoku board of size by size.
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

        public static List<Factor> GetFactors(int number)
        {
            var factors = new List<Factor>();
            // Check if our number is an exact square.
            int sqrt = (int)Math.Ceiling(Math.Sqrt(number));
            if (sqrt * sqrt == number)
                factors.Add(new Factor(sqrt, sqrt));

            // Start from 2, as we don't care about 1-factors.
            for (int i = 2; i < sqrt; i++)
            {
                //  We found a pair of factors.
                if (number % i == 0)
                {
                    factors.Add(new Factor(i, number / i));
                    factors.Add(new Factor(number / i, i));
                }
            }
            return factors;
        }

        public static IEnumerable<SudokuCell> ToEnumerable<T>(this SudokuCell[,] cells)
        {
            foreach (var cell in cells)
            {
                yield return cell;
            }
        }

        public static bool All(this SudokuCell[,] cells, Func<SudokuCell, bool> condition) => cells.ToEnumerable<SudokuCell>().All(condition);
        public static bool Any(this SudokuCell[,] cells, Func<SudokuCell, bool> condition) => cells.ToEnumerable<SudokuCell>().Any(condition);

        /// <summary>
        /// Perform an action that has no return on each member of a SudokuCell[,].
        /// </summary>
        /// <param name="cells">2D array of SudokuCells</param>
        /// <param name="action">e.g. cell => cell.Lock()</param>
        private static void Process(this SudokuCell[,] cells, Action<SudokuCell> action)
        {
            foreach (var cell in cells)
            {
                action(cell);
            }
        }

        /// <summary>
        /// Lock all cells with values on the grid.
        /// </summary>
        public static void LockAll(this SudokuCell[,] cells) => cells.Process(cell => cell.Lock());

        /// <summary>
        /// Resets all cells to their default state:
        ///     unlocked, valid, values set to 0
        /// </summary>
        public static void ClearAll(this SudokuCell[,] cells) => cells.Process(cell => cell.Clear());

        /// <summary>
        /// Fills all cells with unique values.
        /// </summary>
        public static void AutoFill(this SudokuCell[,] cells)
        {
            int value = 1;
            cells.Process(cell =>
            {
                cell.SetValue(value);
                value++;
            });
        }
    }
}
