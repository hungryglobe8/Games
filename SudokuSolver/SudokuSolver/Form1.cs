﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class Form1 : Form
    {
        SudokuGrid grid = new SudokuGrid(9);

        public Form1()
        {
            InitializeComponent();

            ConnectCells();
        }

        /// <summary>
        /// Provide event listeners and some styling for cells in SudokuGrid.
        /// </summary>
        private void ConnectCells()
        {
            foreach (var cell in grid.cells)
            {
                cell.Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);
                cell.Size = new Size(40, 40);
                cell.ForeColor = SystemColors.ControlDarkDark;
                cell.Location = new Point(cell.X * 40, cell.Y * 40);
                cell.BackColor = ((cell.X / 3) + (cell.Y / 3)) % 2 == 0 ? SystemColors.Control : Color.DarkGray;
                cell.FlatStyle = FlatStyle.Flat;
                cell.FlatAppearance.BorderColor = Color.Black;

                // Assign key press event for each cells
                cell.KeyPress += cell_keyPressed;
                cell.GotFocus += cell_gotFocus;

                gamePanel.Controls.Add(cell);
            }
        }

        /// <summary>
        /// Save the active cell when it gains focus.
        /// </summary>
        private void cell_gotFocus(object sender, EventArgs e)
        {
            grid.activeCell = sender as SudokuCell;
        }

        private void ShowMessage(string key)
        {
            MessageBox.Show("You pressed " + key + " key.");
        }

        /// <summary>
        /// Override cmd key functionality if focus is in gamePanel.
        /// Arrow keys move grid focus directionally.
        /// Tab moves right, shift + tab moves left.
        /// </summary>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Only process cmd keys within game panel.
            if (gamePanel.ContainsFocus)
            {
                // Attempt shifting of focus for arrow keys, tab or shift + tab.
                switch (keyData)
                {
                    case Keys.Up:
                        grid.ShiftUp();
                        return true;

                    case Keys.Down:
                        grid.ShiftDown();
                        return true;

                    case Keys.Left:
                    case Keys.Shift | Keys.Tab:
                        grid.ShiftLeft();
                        return true;

                    case Keys.Right:
                    case Keys.Tab:
                        grid.ShiftRight();
                        return true;

                    default:
                        return base.ProcessCmdKey(ref msg, keyData);
                }
            }
            return false;
        }

        private void cell_keyPressed(object sender, KeyPressEventArgs e)
        {
            var cell = sender as SudokuCell;

            // Do nothing if the cell is locked
            if (cell.IsLocked)
                return;

            // Add the pressed key value in the cell only if it is a number
            if (int.TryParse(e.KeyChar.ToString(), out int value))
            {
                // Clear the cell value if pressed key is zero
                if (value == 0)
                    cell.Clear();
                else
                    cell.Text = value.ToString();

                cell.ForeColor = SystemColors.ControlDarkDark;
            }
        }

        /// <summary>
        /// Lock certain cells of the grid as concrete answers.
        /// </summary>
        private void lockButton_Click(object sender, EventArgs e)
        {
            foreach (SudokuCell cell in grid.cells)
            {
                if (cell.Text != string.Empty)
                {
                    cell.IsLocked = true;
                }
            }
        }

        private void solveButton_Click(object sender, EventArgs e)
        {
            SolveGame(grid.cells);
        }

        private void SolveGame(SudokuCell[,] cells)
        {
            while (true)
            {
                for (int i = 1; i < 10; i++)
                {
                    foreach (SudokuCell cell in cells)
                    {
                        if (!cell.IsLocked && IsValidMove(cell, i - 1, i))
                        {
                            cell.Value = i;
                            cell.Text = i.ToString();
                        }
                    }
                }

                for (int i = 1; i < 10; i++)
                {
                    if (cells[i-1, 0].Value == 0)
                    {
                        continue;
                    }
                }
                
                // All cells are filled.
                break;
            }
            Console.WriteLine("Made it out!");
        }

        private bool IsValidMove(SudokuCell cell, int row, int value)
        {
            for (int i = 0; i < 9; i++)
            {
                if (grid.cells[row, i].Value == value)
                    return false;
            }
            return true;
        }
    }
}
