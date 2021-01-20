﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    /// <summary>
    /// SudokuGrid contains the business logic for creating, modifying, and solving a Sudoku game.
    /// </summary>
    public class SudokuGrid
    {
        public SudokuCell[,] cells;
        public SudokuCell activeCell;
        public readonly int width, height, size;
        //private Movement movement;
        private IDictionary<Direction, Func<SudokuCell, SudokuCell>> mapping;
        // For picking possible solution paths.
        private readonly Random random = new Random();
        public bool AllCellsFilled => cells.All(cell => cell.Value != 0);

        /// <summary>
        /// Initializes a new instance of the SudokuSolver.SudokuGrid class. 
        /// </summary>
        public SudokuGrid(int width, int height, int size)
        {
            this.width = width;
            this.height = height;
            this.size = size;

            this.cells = GridOperations.Create(size);
            var movement = new Movement(cells);
            this.mapping = new Dictionary<Direction, Func<SudokuCell, SudokuCell>>
            {
                { Direction.Up, cell => movement.Up(cell) },
                { Direction.Down, cell => movement.Down(cell) },
                { Direction.Left, cell => movement.Left(cell) },
                { Direction.Right, cell => movement.Right(cell) },
                { Direction.JumpForward, cell => movement.JumpForward(cell) },
                { Direction.JumpBackward, cell => movement.JumpBackward(cell) }
            };

            this.activeCell = cells[0, 0];
        }

        //private bool CoorsInBox(int x, int y, int i)
        //{
        //    int x_range = (x - (x % width)) / width;
        //    int y_range = y - (y % height);
        //    return x_range + y_range == i;
        //}


        public void Select(int x, int y) => activeCell = cells[x, y];

        /// <summary>
        /// Return the cell returned by moving according to a specific direction.
        /// </summary>
        public SudokuCell Shift(Direction dir, SudokuCell cell) => mapping[dir](activeCell);

        /// <summary>
        /// Change the active cell of the grid according to a specific direction.
        /// </summary>
        /// <param name="dir"></param>
        public void Shift(Direction dir) => activeCell = Shift(dir, activeCell);

        //private void AddBoxes(SudokuCell cell)
        //{
        //    int x = cell.X;
        //    int y = cell.Y;
        //    // Ex: go from 5 - (2) to 5 - (2) + 3
        //    for (int i = x - (x % width); i < x - (x % width) + width; i++)
        //    {
        //        for (int j = y - (y % height); j < y - (y % height) + height; j++)
        //        {
        //            cell.AddNeighbor(cells[i, j]);
        //        }
        //    }
        //}


        #region ModifyCells
        /// <summary>
        /// Modify a cell, returning true if the cell's value was changed.
        /// Updates known conflicts and jump forward if the cell had none.
        /// </summary>
        /// <param name="value">new value for the cell</param>
        /// <returns>true if the cell's value was changed, false otherwise</returns>
        public bool ModifyCell(int value)
        {
            if (value == activeCell.Value || activeCell.IsLocked)
                return false;

            // Purge old conflicts, find new ones.
            else if (value == 0)
                activeCell.Clear();

            else
            {
                activeCell.Clear();
                // Set activeCell value.
                activeCell.SetValue(value);
                // Conflicts must be updated with change.
                var result = Block.GetHorizontal(this);
                CheckConflicts(activeCell.Value, result);

                //if (result)
                //CheckConflicts(oldValue, activeCell, mapping[Direction.Up]());
                //CheckConflicts(oldValue, ShiftActive(Direction.Left));
                //CheckConflicts(oldValue, ShiftActive(Direction.Up));
                //UpdateConflicts(value);
                // Check validity to jump forward.
                if (activeCell.IsValid)
                    Shift(Direction.JumpForward);

            }
            return true;
        }

        /// <summary>
        /// Mark cells as invalid, if there are more than 2 that share the same value.
        /// </summary>
        private void CheckConflicts(int value, IList<SudokuCell> result)
        {
            foreach (var cell in result)
            {
                if (cell != activeCell && cell.Value == activeCell.Value)
                    activeCell.AddConflict(cell);
            }
        }

        private void CheckConflicts(int oldValue, int newValue)
        {
            int count = 0;
            List<SudokuCell> oldConflicts = new List<SudokuCell>();
            while (count < 9)
            {
                //cell = Shift();
            }
            return;
            //int oldValue = activeCell.Value;
            //SudokuCell row, col = activeCell;
            //for (int i = 1; i < size; i++)
            //{
            //    SudokuCell row = movement.ShiftRight(row);
            //    SudokuCell col = movement.ShiftUp(col);
            //    if (activeCell.Value == row.Value)
            //    {
            //        activeCell.IsValid = false;
            //        row.IsValid = false;
            //    }
            //    else if ()
            //    {
            //        neighbor.IsValid = true;
            //    }
            //activeCell.Notify();
            //neighbor.Notify();
        }
        #endregion

        #region Solve

        private bool TryRandomFromList(IList<int> possNums)
        {
            if (possNums.Count < 1)
            {
                ModifyCell(0);
                return false;
            }

            int value = possNums[random.Next(0, possNums.Count)];
            // Remove value from list of possibilities.
            possNums.Remove(value);

            return ModifyCell(value);
        }
        private bool SolveCell()
        {
            // Remember this cell if we have to return to it later on.
            SudokuCell currentCell = activeCell;
            // Only consider the possibilities currently available.
            List<int> possNums = GetPossibleNums(currentCell);

            do
            {
                activeCell = currentCell;
                // Try another random possibility for the activeCell.
                if (!TryRandomFromList(possNums))
                    return false;

                // Break from loop if no more cells are left.
                if (AllCellsFilled)
                {
                    return true;
                }
            } while (!SolveCell());

            return true;
        }

        /// <summary>
        /// Return a list of numbers without conflicts at a given cell.
        /// </summary>
        /// <returns>list of numbers 1-size which could be valid in a cell</returns>
        private List<int> GetPossibleNums(SudokuCell cell)
        {
            List<int> nums = Enumerable.Range(1, size).ToList();
            for (int i = 1; i < size + 1; i++)
            {
                if (GetConflicts(cell, i).Count != 0)
                    nums.Remove(i);
            }
            return nums;
        }

        /// <summary>
        /// Solve the game.
        /// </summary>
        public void Solve()
        {
            // Start in the top left corner.
            activeCell = cells[0, 0];
            // Move to first empty square (possibly first).
            Shift(Direction.JumpBackward);
            Shift(Direction.JumpForward);
            SolveCell();
        }

        /// <summary>
        /// Checks for conflicts when changing the value of a cell, based on its neighbors.
        /// </summary>
        /// <returns>list of conflicting cells</returns>
        private ISet<SudokuCell> GetConflicts(SudokuCell cell, int value)
        {
            ISet<SudokuCell> conflictingCells = new HashSet<SudokuCell>();
            int x = cell.X;
            int y = cell.Y;
            // Check columns and rows don't have duplicates.
            for (int i = 0; i < size; i++)
            {
                SudokuCell xCell = cells[i, y];
                SudokuCell yCell = cells[x, i];
                SudokuCell leftDiagCell = cells[i, i];
                SudokuCell rightDiagCell = cells[i, size - 1 - i];
                if (cell != xCell && xCell.Value == value)
                    conflictingCells.Add(xCell);

                if (cell != yCell && yCell.Value == value)
                    conflictingCells.Add(yCell);

                // Check diagonals.
                //if (x == y && leftDiagCell != cell && leftDiagCell.Value == value)
                //    conflictingCells.Add(leftDiagCell);
                //if (x + y == size - 1 && rightDiagCell != cell && rightDiagCell.Value == value)
                //    conflictingCells.Add(rightDiagCell);
            }
            // Check boxes don't have duplicates.
            // Ex: go from 5 - (2) to 5 - (2) + 3
            for (int i = x - (x % width); i < x - (x % width) + width; i++)
            {
                for (int j = y - (y % height); j < y - (y % height) + height; j++)
                {
                    if (i != x && j != y && cells[i, j].Value == value)
                        conflictingCells.Add(cells[i, j]);
                }
            }
            return conflictingCells;
        }
        #endregion
    }
}
