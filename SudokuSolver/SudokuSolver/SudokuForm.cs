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
        private SudokuGrid grid;

        public SudokuForm(int width, int height, int size)
        {
            InitializeComponent();

            CreateGame(width, height, size);
        }

        /// <summary>
        /// Provide event listeners and some styling for cells in SudokuGrid.
        /// </summary>
        private void ConnectCells()
        {
            foreach (var cell in grid.cells)
            {
                SudokuButton button = new SudokuButton(cell);
                button.Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);
                button.Size = new Size(40, 40);
                button.ForeColor = SystemColors.ControlDarkDark;
                button.Location = new Point(button.Cell.X * 40, button.Cell.Y * 40);
                // Choose one of two backColors based on location.
                button.BackColor = ((button.Cell.X / grid.width) + (button.Cell.Y / grid.height)) % 2 == 0 ? SystemColors.Control : Color.DarkGray;
                //TODO-Move design to grid? Call specific due to specific checkboxes?
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderColor = Color.Black;
                button.TabStop = false;

                // Assign key press event for each cells
                button.KeyPress += cell_keyPressed;
                button.Click += cell_clicked;
                button.Cell.ValueChanged += cell_valueChanged;

                button.Name = $"{button.Cell.X},{button.Cell.Y}";
                gamePanel.Controls.Add(button);
            }
        }

        private void cell_valueChanged(CellValueChangedArgs e)
        {
            var button = gamePanel.Controls.Find($"{e.Cell.X},{e.Cell.Y}", false).FirstOrDefault();
            if (button == null)
                return;

            button.Text = e.CellValue.ToString();
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
                        grid.SelectCell(grid.movement.Up);
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
                        grid.ModifyCell(0);
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
                //grid.SelectCell(cell);
                grid.ModifyCell(value);
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

        #region CreateNewGame
        private void smallToolStripMenuItem_Click(object sender, EventArgs e) => CreateGame(2, 2, 4);
        private void mediumToolStripMenuItem_Click(object sender, EventArgs e) => CreateGame(3, 2, 6);
        private void largeToolStripMenuItem_Click(object sender, EventArgs e) => CreateGame(3, 3, 9);
        /// <summary>
        /// Suspend the visuals of the gamePanel until a board has been generated and is ready to view.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="size"></param>
        private void CreateGame(int width, int height, int size)
        {
            gamePanel.Visible = false;
            gamePanel.Controls.Clear();
            grid = new SudokuGrid(width, height, size);

            ConnectCells();
            gamePanel.ResumeLayout();
            gamePanel.Visible = true;
            //new SudokuForm(width, height, size).Show();
            //this.Close();
        }
        #endregion
    }
}
