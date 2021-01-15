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
    public partial class SudokuForm : Form
    {
        public SudokuForm()
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
                //TODO-Move design to grid? Call specific due to specific checkboxes?
                cell.BackColor = ((cell.X / 3) + (cell.Y / 3)) % 2 == 0 ? SystemColors.Control : Color.DarkGray;
                cell.FlatStyle = FlatStyle.Flat;
                cell.FlatAppearance.BorderColor = Color.Black;
                cell.TabStop = false;

                // Assign key press event for each cells
                cell.KeyPress += cell_keyPressed;
                cell.Click += cell_clicked;

                gamePanel.Controls.Add(cell);
            }
        }

        /// <summary>
        /// Override cmd key functionality if focus is in gamePanel.
        /// Arrow keys move grid focus directionally, wrapping around.
        /// Tab moves to next open, shift + tab moves to last open.
        /// Backspace moves left and clears.
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Only process cmd keys within game panel.
            if (gamePanel.ContainsFocus)
            {
                // Shift focus by using arrow keys, tab or shift + tab.
                switch (keyData)
                {
                    case Keys.Up:
                        grid.ShiftUp();
                        break;
                    case Keys.Down:
                        grid.ShiftDown();
                        break;
                    case Keys.Left:
                        grid.ShiftLeft();
                        break;
                    case Keys.Right:
                        grid.ShiftRight();
                        break;

                    // Find last open space.
                    case Keys.Shift | Keys.Tab:
                        grid.JumpBackward();
                        break;
                    // Find next open space.
                    case Keys.Tab:
                        grid.JumpForward();
                        break;

                    // Delete last filled cell.
                    case Keys.Back:
                        grid.ShiftLeft();
                        grid.Delete();
                        break;

                    default:
                        return base.ProcessCmdKey(ref msg, keyData);
                }
                grid.activeCell.Focus();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Notify the grid that the active cell has changed.
        /// Focus is called through the fact that SudokuCells are buttons.
        /// </summary>
        private void cell_clicked(object sender, EventArgs e)
        {
            var cell = sender as SudokuCell;
            grid.SelectCell(cell);
        }

        /// <summary>
        /// Notify the grid that a keystroke was made.
        ///     0 - Attempt to delete a cell.
        ///     other numbers - Attempt to modify a cell.
        /// </summary>
        private void cell_keyPressed(object sender, KeyPressEventArgs e)
        {
            var cell = sender as SudokuCell;
            // Add the pressed key value to the cell only if it is a number.
            if (int.TryParse(e.KeyChar.ToString(), out int value))
            {
                if (value == 0)
                    grid.Delete();
                else
                    grid.ModifyCell(cell, value);
                grid.activeCell.Focus();
            }
        }

        /// <summary>
        /// Lock all cells as concrete answers, with black text.
        /// Cannot be modified after locking, except by clearing the board.
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

        /// <summary>
        /// Remove all text and values from the board.
        /// Enable the solve button.
        /// </summary>
        private void clearButton_Click(object sender, EventArgs e)
        {
            grid.ClearBoard();
            solveButton.Enabled = true;
        }
    }
}
