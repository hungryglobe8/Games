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
        public bool IsValid => Conflicts.Count == 0;
        public IList<SudokuCell> Conflicts { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public SudokuCell(int x, int y)
        {
            X = x;
            Y = y;
            IsLocked = false;
            Conflicts = new List<SudokuCell>();
        }

        /// <summary>
        /// Get rid of ugly tab box.
        /// </summary>
        protected override bool ShowFocusCues { get { return false; } }

        /// <summary>
        /// Change a cell's value, text and color, based on cell state.
        ///     invalid - red takes highest priority
        ///     locked - solid black is next priority
        ///     normal - dark grey
        /// </summary>
        private void Write(int value)
        {
            this.Value = value;
            if (!IsValid)
                this.ForeColor = Color.Red;
            else if (IsLocked)
                this.ForeColor = Color.Black;
            else
                this.ForeColor = SystemColors.ControlDarkDark;
            this.Text = (value == 0) ? string.Empty : value.ToString();
        }

        private bool ValueWillNotChange(int value) => IsLocked || (Value == value);

        /// <summary>
        /// If cell is locked or its value equals newValue,
        /// rewrite in case of collision and return false.
        /// Else cell's value will change,
        /// </summary>
        /// <param name="newValue">new cell Value</param>
        /// <returns>true if cell changed value, false otherwise</returns>
        public bool SetValue(int newValue)
        {
            // Rewrite locked cells in case of conflicts.
            if (ValueWillNotChange(newValue))
            {
                Write(Value);
                return false;
            }    
            else
            {
                Write(newValue);
                return true;
            }
        }

        public void AddConflict(SudokuCell other)
        {
            this.Conflicts.Add(other);
            other.Conflicts.Add(this);
        }

        public void Notify() => Write(Value);

        public void RemoveConflict(SudokuCell other)
        {
            this.Conflicts.Remove(other);
            other.Conflicts.Remove(this);
        }
        /// <summary>
        /// Remove all conflicts from self and all conflict
        /// references to self in other cells.
        /// </summary>
        public void RemoveConflicts()
        {
            foreach (var cell in Conflicts.ToList<SudokuCell>())
            {
                RemoveConflict(cell);
                cell.Notify();
            }
            this.Conflicts.Clear();
        }

        /// <summary>
        /// Lock a single cell, if it has a value.
        /// </summary>
        public void Lock()
        {
            if (Value != 0)
                IsLocked = true;
        }
        public void Unlock() => IsLocked = false;

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
