﻿using System;
using System.Collections.Generic;
using System.Drawing;
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

            Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);
            Size = new Size(40, 40);
            ForeColor = SystemColors.ControlDarkDark;
            Location = new Point(Cell.X * 40, Cell.Y * 40);
            
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderColor = Color.Black;
            TabStop = false;

            Name = cell.ToString();
        }
        //Cell.ValueChanged += cell_valueChanged;

        /// <summary>
        /// Change a cell's value, text and color, based on cell state.
        ///     invalid - red takes highest priority
        ///     locked - solid black is next priority
        ///     normal - dark grey
        /// </summary>
        private void Write()
        {
            if (!Cell.IsValid)
                this.ForeColor = Color.Red;
            else if (Cell.IsLocked)
                this.ForeColor = Color.Black;
            else
                this.ForeColor = SystemColors.ControlDarkDark;
            this.Text = (Cell.Value == 0) ? string.Empty : Cell.Value.ToString();
        }

        /// <summary>
        /// Get rid of ugly tab box.
        /// </summary>
        protected override bool ShowFocusCues { get { return false; } }
    }
}
