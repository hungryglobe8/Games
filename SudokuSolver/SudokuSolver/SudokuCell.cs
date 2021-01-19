﻿using System;
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
        public CellValueChangedArgs(SudokuCell cell)
        {
            Cell = cell;
            CellValue = cell.Value;
        }
        public SudokuCell Cell { get; }
        public int CellValue { get; set; }
    }
    public delegate void CellValueChanged(CellValueChangedArgs e);

#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class SudokuCell
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public int Value { get; private set; }
        public bool IsLocked { get; private set; }
        public bool IsValid { get; set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public event CellValueChanged ValueChanged;

        public SudokuCell(int x, int y)
        {
            X = x;
            Y = y;
            IsLocked = false;
        }

        private bool ValueWillNotChange(int value) => IsLocked || (Value == value);

        /// <summary>
        /// If cell is locked, do not set its value.
        /// </summary>
        public void SetValue(int newValue)
        {
            if (!IsLocked)
                Value = newValue;
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
