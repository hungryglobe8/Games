using System;
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
        public Form1()
        {
            InitializeComponent();

            ConnectCells();
        }

        // Must be initialized before connect cells.
        readonly SudokuGrid grid = new SudokuGrid(9);

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
                cell.Click += cell_clicked;

                gamePanel.Controls.Add(cell);
            }
        }

        private void cell_clicked(object sender, EventArgs e)
        {
            var cell = sender as SudokuCell;
            Console.WriteLine("val is: " + cell.Value);
            grid.SelectCell(cell);
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
                        break;

                    case Keys.Down:
                        grid.ShiftDown();
                        break;

                    case Keys.Left:
                    case Keys.Shift | Keys.Tab:
                        grid.ShiftLeft();
                        break;

                    case Keys.Right:
                    case Keys.Tab:
                        grid.ShiftRight();
                        break;

                    default:
                        return base.ProcessCmdKey(ref msg, keyData);
                }
                grid.activeCell.Focus();
                return true;
            }
            return false;
        }

        private void cell_keyPressed(object sender, KeyPressEventArgs e)
        {
            var cell = sender as SudokuCell;

            // Add the pressed key value in the cell only if it is a number
            if (int.TryParse(e.KeyChar.ToString(), out int value))
            {
                grid.ModifyCell(cell, value);
                grid.activeCell.Focus();
            }
        }

        /// <summary>
        /// Lock all cells as concrete answers, with black text.
        /// Cannot be modified after locking, until clearing the board.
        /// </summary>
        private void lockButton_Click(object sender, EventArgs e)
        {
            grid.LockAll();
        }

        /// <summary>
        /// Fill in the board with a valid solution.
        /// Lock all answers and disable solve button.
        /// </summary>
        private void solveButton_Click(object sender, EventArgs e)
        {
            grid.Solve();
            solveButton.Enabled = false;
            lockButton_Click(sender, e);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            grid.Clear();
            solveButton.Enabled = true;
        }
    }
}
