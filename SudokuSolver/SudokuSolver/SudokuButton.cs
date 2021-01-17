using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    class SudokuButton : Button
    {
        public SudokuCell Cell { get; set; }

        public SudokuButton(SudokuCell cell)
        {
            Cell = cell;
        }

        /// <summary>
        /// Get rid of ugly tab box.
        /// </summary>
        protected override bool ShowFocusCues { get { return false; } }
    }
}
