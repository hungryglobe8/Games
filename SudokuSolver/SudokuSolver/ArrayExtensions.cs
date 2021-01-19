using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public static class ArrayExtensions
    {
        public static IEnumerable<SudokuCell> ToEnumerable<T>(this SudokuCell[,] cells)
        {
            foreach (var cell in cells)
            {
                yield return cell;
            }
        }

        public static void Process(this SudokuCell[,] cells, Func<SudokuCell, SudokuCell> action)
        {
            foreach (var cell in cells)
            {
                action(cell);
            }
        }

        public static void LockCells(this IEnumerable<SudokuCell> cells)
        {
            cells.Process(cell => cell.Lock());
        }

        public static void Process(this IEnumerable<SudokuCell> cells, Action<SudokuCell> action)
        {
            foreach (var cell in cells)
            {
                action(cell);
            }
        }

        
    }
}
}
