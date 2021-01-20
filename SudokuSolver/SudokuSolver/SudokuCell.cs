using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    public class CellValueChangedArgs : EventArgs
    {
        public int CellValue { get; private set; }
        public bool IsLocked { get; private set; }
        public bool IsValid { get; private set; }

        public CellValueChangedArgs(SudokuCell cell)
        {
            CellValue = cell.Value;
            IsLocked = cell.IsLocked;
            IsValid = cell.IsValid;
        }
    }
    public delegate void CellValueChanged(CellValueChangedArgs e);

#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class SudokuCell
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public int Value { get; private set; }
        public bool IsLocked { get; private set; }
        public bool IsValid => conflicts.Count == 0;
        public int X { get; private set; }
        public int Y { get; private set; }
        public ISet<SudokuCell> conflicts;
        public event CellValueChanged ValueChanged;

        public SudokuCell(int x, int y)
        {
            X = x;
            Y = y;
            IsLocked = false;
            conflicts = new HashSet<SudokuCell>();
        }

        /// <summary>
        /// If cell is locked, do not set its value.
        /// </summary>
        public bool SetValue(int newValue)
        {
            if (newValue == Value || IsLocked)
                return false;
            else 
            {
                Clear();
                Value = newValue;
                return true;
            }
        }

        public void Notify() => ValueChanged.Invoke(new CellValueChangedArgs(this));

        /// <summary>
        /// Lock a single cell, if it has a value.
        /// </summary>
        public void Lock()
        {
            if (Value != 0)
                IsLocked = true;
        }
        public void Unlock() => IsLocked = false;
        public void Clear()
        {
            Value = 0;
            IsLocked = false;
            RemoveConflicts();
        }

        public void AddConflict(SudokuCell other)
        {
            conflicts.Add(other);
            other.conflicts.Add(this);
        }


        private void RemoveConflicts()
        {
            foreach (var cell in conflicts.ToList())
            {
                conflicts.Remove(cell);
                cell.conflicts.Remove(this);
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

        public override string ToString() => $"{X},{Y}";
    }
}
