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
        public SudokuGrid grid { get; private set; }

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
                //TODO-Call specific design due to specific checkboxes?
                // Choose one of two backColors based on location and grid boundaries.
                button.BackColor = ((button.Cell.X / grid.width) + (button.Cell.Y / grid.height)) % 2 == 0 ? SystemColors.Control : Color.DarkGray;
                // Assign key press event for each button
                button.KeyPress += cell_keyPressed;
                button.Click += cell_clicked;

                gamePanel.Controls.Add(button);
            }
        }

        /// <summary>
        /// Set the game mode by checking various flags.
        /// </summary>
        /// <returns>BlockFlag which controls the validation of the grid</returns>
        private BlockFlag GetFlags()
        {
            return new BlockFlag(boxesCheck.Checked, xSudokuCheck.Checked);
        }

        /// <summary>
        /// Focus on a specific button by providing the corresponding cell.
        /// </summary>
        private void Select(SudokuCell cell)
        {
            var button = gamePanel.Controls.Find(cell.ToString(), false).First();
            if (button == null)
                throw new ArgumentException("Cell did not have a matching control.");
            button.Focus();
        }

        /// <summary>
        /// Notify the grid that the active cell has changed.
        /// Focus is called through the fact that SudokuCells are buttons.
        /// </summary>
        private void cell_clicked(object sender, EventArgs e)
        {
            var button = sender as SudokuButton;
            grid.Select(button.Cell.X, button.Cell.Y);
        }

        #region KeyPress
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
                        grid.Shift(Direction.Up);
                        break;
                    case Keys.Down:
                        grid.Shift(Direction.Down);
                        break;
                    case Keys.Left:
                        grid.Shift(Direction.Left);
                        break;
                    case Keys.Right:
                        grid.Shift(Direction.Right);
                        break;

                    // Find last open space.
                    case Keys.Shift | Keys.Tab:
                        grid.Shift(Direction.JumpBackward);
                        break;
                    // Find next open space.
                    case Keys.Tab:
                        grid.Shift(Direction.JumpForward);
                        break;

                    // Delete last filled cell.
                    case Keys.Back:
                        grid.Shift(Direction.JumpBackward);
                        grid.ModifyCell(0);
                        break;

                    default:
                        return base.ProcessCmdKey(ref msg, keyData);
                }
                Select(grid.activeCell);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Attempt to modify the grid if a numeric key is pressed.
        /// </summary>
        private void cell_keyPressed(object sender, KeyPressEventArgs e)
        {
            var button = sender as SudokuButton;
            // Add the pressed key value to the cell only if it is a number.
            if (int.TryParse(e.KeyChar.ToString(), out int value))
            {
                grid.ModifyCell(value);
                Select(grid.activeCell);
            }
        }
        #endregion

        /// <summary>
        /// Lock all cells as concrete answers, with black text.
        /// Cannot be modified after locking, except by clearing the board.
        /// </summary>
        private void lockButton_Click(object sender, EventArgs e)
        {
            grid.cells.LockAll();
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
            grid.cells.ClearAll();
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
            // Hide the game panel.
            gamePanel.Visible = false;
            gamePanel.Controls.Clear();
            
            // Get the flags (settings) for the new game.
            BlockFlag gameMode = GetFlags();
            grid = new SudokuGrid(width, height, size, gameMode);

            ConnectCells();
            // Show the game panel after cells are connected again.
            gamePanel.ResumeLayout();
            gamePanel.Visible = true;
            //new SudokuForm(width, height, size).Show();
            //this.Close();
        }
        #endregion
    }
}
