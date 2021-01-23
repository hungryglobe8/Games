using System.Drawing;
using System.Windows.Forms;

namespace SudokuSolver
{
    class SudokuButton : Button
    {
        public SudokuCell Cell { get; private set; }

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
            Cell.ValueChanged += cell_ValueChanged;

            Name = cell.ToString();
        }

        /// <summary>
        /// Change a cell's value, text and color, based on cell state.
        ///     invalid - red takes highest priority
        ///     locked - solid black is next priority
        ///     normal - dark grey
        /// </summary>
        private void cell_ValueChanged()
        {
            if (!Cell.IsValid)
                ForeColor = Color.Red;
            else if (Cell.IsLocked)
                ForeColor = Color.Black;
            else
                ForeColor = SystemColors.ControlDarkDark;
            // The formatting for painting text must be overridden in SudokuButton's OnPaint.
            //Text = (Cell.Value == 0) ? string.Empty : Cell.Value.ToString();
        }

        /// <summary>
        /// Get rid of ugly tab box.
        /// </summary>
        protected override bool ShowFocusCues { get { return false; } }

        /// <summary>
        /// Change the painting behavior to fit text that would normally be too big.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Normal painting behavior.
            base.OnPaint(e);
            
            //TODO- Align possible values behavior.
            // Center text behavior.
            string text = (Cell.Value == 0) ? string.Empty : Cell.Value.ToString();
            if (!string.IsNullOrEmpty(text))
            {
                StringFormat stringFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                e.Graphics.DrawString(text, Font, new SolidBrush(ForeColor), ClientRectangle, stringFormat);
            }
        }
    }
}
