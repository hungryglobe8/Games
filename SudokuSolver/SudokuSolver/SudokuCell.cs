using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    public delegate void Notify();

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
        public event Notify ValueChanged;

        public SudokuCell(int x, int y)
        {
            X = x;
            Y = y;
            IsLocked = false;
            conflicts = new HashSet<SudokuCell>();
        }

        /// <summary>
        /// Set the value of a cell to a newValue.
        /// If cell is locked, or newValue will not change, do nothing.
        /// Returns true if the modification will result in possible conflicts,
        /// i.e. Value is a new non-zero number.
        /// </summary>
        public bool SetValue(int newValue)
        {
            if (newValue == Value || IsLocked)
                return false;
            else 
            {
                Clear();
                Value = newValue;
                OnValueChanged();
                return Value != 0;
            }
        }

        public virtual void OnValueChanged() => ValueChanged?.Invoke();

        /// <summary>
        /// Lock a single cell, if it has a value.
        /// </summary>
        public void Lock()
        {
            if (Value != 0)
            {
                IsLocked = true;
                OnValueChanged();
            }
        }

        public void Clear()
        {
            Value = 0;
            IsLocked = false;
            RemoveConflicts();
            OnValueChanged();
        }

        public void AddConflict(SudokuCell other)
        {
            conflicts.Add(other);
            other.conflicts.Add(this);
            // Notify cells whose conflict count has increased.
            if (conflicts.Count == 1)
                OnValueChanged();
            if (other.conflicts.Count == 1)
                other.OnValueChanged();
        }


        private void RemoveConflicts()
        {
            foreach (var cell in conflicts.ToList())
            {
                conflicts.Remove(cell);
                cell.conflicts.Remove(this);
                OnValueChanged();
                cell.OnValueChanged();
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
