using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class SudokuCell : Button
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public int Value { get; private set; }
        public bool IsLocked { get; private set; }
        public bool IsValid { get; set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public SudokuCell(int x, int y)
        {
            X = x;
            Y = y;
            IsValid = true;
            IsLocked = false;
        }

        protected override bool ShowFocusCues
        {
            get
            {
                return false;
            }
        }

        public void Clear()
        {
            this.Value = 0;
            this.ForeColor = SystemColors.ControlDarkDark;
            this.Text = string.Empty;
            this.IsValid = true;
            this.IsLocked = false;
        }

        public void SetValue(int value, bool valid)
        {
            if (!IsLocked)
            {
                this.Value = value;
                this.IsValid = valid;
                if (!valid)
                {
                    this.ForeColor = Color.Red;
                }
                else
                {
                    this.ForeColor = SystemColors.ControlDarkDark;
                }
                this.Text = value.ToString();
            }

        }

        /// <summary>
        /// Lock a single cell on the grid, if it has a value.
        /// </summary>
        public void Lock()
        {
            if (Value != 0)
            {
                IsLocked = true;
                // Change the color of locked cells.
                ForeColor = Color.Black;
            }
        }

        public bool Equals(int x, int y)
        {
            return X == x && Y == y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is SudokuCell))
                return false;

            SudokuCell other = obj as SudokuCell;
            return this.X == other.X && this.Y == other.Y;
        }
    }
}
