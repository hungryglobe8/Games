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

            CreateCells();
        }

        SudokuCell[,] cells = new SudokuCell[9, 9];
        SudokuCell activeCell = null;
        
        /// <summary>
        /// Create empty cells to fill a Sudoku board.
        /// </summary>
        private void CreateCells()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    // Create 81 cells for with styles and locations based on the index
                    cells[i, j] = new SudokuCell();
                    cells[i, j].Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);
                    cells[i, j].Size = new Size(40, 40);
                    cells[i, j].ForeColor = SystemColors.ControlDarkDark;
                    cells[i, j].Location = new Point(i * 40, j * 40);
                    cells[i, j].BackColor = ((i / 3) + (j / 3)) % 2 == 0 ? SystemColors.Control : Color.DarkGray;
                    cells[i, j].FlatStyle = FlatStyle.Flat;
                    cells[i, j].FlatAppearance.BorderColor = Color.Black;
                    cells[i, j].X = i;
                    cells[i, j].Y = j;

                    // Assign key press event for each cells
                    cells[i, j].KeyPress += cell_keyPressed;
                    cells[i, j].KeyDown += cell_keyDown;
                    cells[i, j].GotFocus += cell_gotFocus;

                    gamePanel.Controls.Add(cells[i, j]);
                }
            }
        }

        /// <summary>
        /// Save the active cell when it gains focus.
        /// </summary>
        private void cell_gotFocus(object sender, EventArgs e)
        {
            activeCell = sender as SudokuCell;
        }

        private void ShowMessage(string key)
        {
            MessageBox.Show("You pressed " + key + " key.");
        }

        /// <summary>
        /// Check that a value is within the grid.
        /// </summary>
        private bool ValueInRange(int value)
        {
            int MIN = 0;
            int MAX = 9;
            return value > MIN && value < MAX;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Only process cmd keys within game panel.
            if (gamePanel.ContainsFocus)
            {
                var oldX = activeCell.X;
                var oldY = activeCell.Y;
                // Check that shifts will be in range [0:9].
                switch (keyData)
                {
                    case Keys.Up:
                        cells[oldX, ValueInRange(oldY - 1) ? oldY - 1: 0].Focus();
                        return true;

                    case Keys.Down:
                        cells[oldX, ValueInRange(oldY + 1) ? oldY + 1 : 8].Focus();
                        return true;

                    case Keys.Left:
                    case Keys.Shift | Keys.Tab:
                        cells[ValueInRange(oldX - 1) ? oldX - 1 : 0, oldY].Focus();
                        return true;

                    case Keys.Right:
                    case Keys.Tab:
                        cells[ValueInRange(oldX + 1) ? oldX + 1 : 8, oldY].Focus();
                        return true;

                    default:
                        return base.ProcessCmdKey(ref msg, keyData);
                }
            }
            return false;
        }

        private void cell_keyDown(object sender, KeyEventArgs e)
        {
            var cell = sender as SudokuCell;

            // Move to a different cell with tab or arrows.
            switch (e.KeyData)
            {
                case Keys.Up:
                    Console.WriteLine("up pressed");
                    break;

                default:
                    Console.WriteLine("other");
                    break;
            }
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
            foreach (SudokuCell cell in cells)
            {
                if (cell.Text != string.Empty)
                {
                    cell.IsLocked = true;
                }
            }
        }

        private void solveButton_Click(object sender, EventArgs e)
        {

        }
    }
}
