using Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class GameWindow : Form
    {
        private Field field;
        private readonly Dictionary<Tile, MineSweeperButton> connections = new Dictionary<Tile, MineSweeperButton>();
        private bool firstClick = false;

        public GameWindow(string gameSize)
        {
            InitializeComponent();
            // Pick one of three sizes of games.
            int x, y, numMines;
            switch (gameSize)
            {
                case "small":
                    x = 8;
                    y = 8;
                    numMines = 10;
                    break;
                case "medium":
                    x = 16;
                    y = 16;
                    numMines = 40;
                    break;
                case "large":
                    x = 30;
                    y = 16;
                    numMines = 99;
                    break;
                default:
                    throw new Exception("Game Size is not valid");
            };
            field = new Field(x, y, numMines);

            CreateBoard(field);
        }

        private void CreateBoard(Field field)
        {
            int numCols = field.Width;
            int numRows = field.Height;
            // 
            // Make GamePanel
            // 
            gamePanel.ColumnCount = numCols;
            gamePanel.RowCount = numRows;
            float BUTTON_SIZE = 20F;
            // Remove first column.
            gamePanel.ColumnStyles.RemoveAt(0);
            // Add cols and rows.
            for (int i = 0; i < numCols; i++)
                gamePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, BUTTON_SIZE));
            for (int i = 0; i < numRows; i++)
                gamePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, BUTTON_SIZE));

            // Resize game window to fit game panel.
            gamePanel.Size = new System.Drawing.Size(numCols * 20, numRows * 20);
            ClientSize = new System.Drawing.Size(gamePanel.Size.Width + 60, gamePanel.Size.Height + 100);
            // End game button centered over game panel.
            Point topButton = new Point(gamePanel.Size.Width / 2 + 10, 35);
            endGameButton.Location = topButton;
            // Set flag label and right position.
            flagCounterLabel.Text = field.NumFlags.ToString();
            flagCounterLabel.Location = Point.Add(topButton, new Size(60, 5));
            // Reveal all button left position.
            revealAllButton.Location = Point.Subtract(topButton, new Size(50, -5));
            revealAllBorder.Location = Point.Subtract(topButton, new Size(52, -3));

            // Link buttons to tiles.
            for (int x = 0; x < numCols; x++)
            {
                for (int y = 0; y < numRows; y++)
                {
                    // Add a new connection.
                    Tile tile = field.GetTile(x, y);
                    MineSweeperButton button = new MineSweeperButton(tile);
                    connections.Add(tile, button);

                    gamePanel.Controls.Add(button, x, y);
                    button.MouseUp += Button_MouseUp;
                }
            }
        }

        private void Button_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MineSweeperButton button = (MineSweeperButton)sender;
            Tile tile = button.Tile;
            // double click
            //TODO
            //left click
            if (e.Button == MouseButtons.Left)
            {
                // On first click, populate minefield.
                if (!firstClick)
                {
                    field.PopulateField(tile);
                    firstClick = true;
                }

                tile.LeftClick();
                if (tile.state == State.Revealed)
                {
                    RemoveFunctionality(button);
                    // End game.
                    if (tile.IsArmed)
                    {
                        GameOver(false);
                        return;
                    }

                    // Recursively find all neighbors of 0 danger tiles.
                    if (tile.GetDanger() == 0)
                    {
                        IList<Tile> neighbors = field.GetNeighbors(tile.X, tile.Y);
                        foreach (Tile neighbor in neighbors)
                        {
                            if (neighbor.state == State.Unopened)
                                Button_MouseUp(connections[neighbor], e);
                        }
                    }
                }
            }
            //right click
            else if (e.Button == MouseButtons.Right)
            {
                field.Flag(tile);
                flagCounterLabel.Text = field.NumFlags.ToString();

                // Show game end button.
                if (field.NumFlags == 0)
                {
                    revealAllButton.Show();
                    revealAllBorder.Show();
                }
                else
                {
                    revealAllButton.Hide();
                    revealAllBorder.Hide();
                }
            }

            button.ReplaceImage();
        }

        /// <summary>
        /// Do nothing if the user clicks on a button with its functionality removed.
        /// </summary>
        private void RemoveFunctionality(Button button) => button.MouseUp -= Button_MouseUp;

        /// <summary>
        /// Reveal all mines or tiles and disable the game.
        /// Reveal all tiles if user uses revealAll button (only activated after placing all flags).
        /// </summary>
        private void GameOver(bool revealAll)
        {
            // If no click happened, generate random board.
            if (!firstClick)
            {
                field.PopulateField(field.GetTile(0, 0));
                firstClick = true;
            }

            // Reveal all tiles.
            if (revealAll)
            {
                foreach (Tile tile in field.GetTiles())
                {
                    tile.LeftClick();
                    connections[tile].ReplaceImage();
                }
            }
            // Reveal all mines.
            else
            {
                foreach (Tile mine in field.GetMines())
                {
                    mine.LeftClick();
                    connections[mine].ReplaceImage();
                }
            }
            // Disable other buttons.
            foreach (Button button in gamePanel.Controls)
            {
                RemoveFunctionality(button);
            }
            // End game button reset.
            endGameButton.Text = "Ended";
            endGameButton.Click -= EndGameButton_Click;
            endGameButton.Click += ResetGame_Click;

            // Record user stats (W/L)
        }

        /// <summary>
        /// Game is over. Clicking this button again will reset the game in the same window with a new minefield.
        /// </summary>
        private void ResetGame_Click(object sender, EventArgs e)
        {
            field = new Field(field.Width, field.Height, field.NumMines);
            //field.PopulateField();
            // Remove old values.
            Dictionary<Tile, Button> connections = new Dictionary<Tile, Button>();
            Dictionary<Button, Tile> b_connections = new Dictionary<Button, Tile>();
            gamePanel.Hide();
            gamePanel = new System.Windows.Forms.TableLayoutPanel
            {
                AutoSize = true,
                Name = "smallGamePanel",
                Size = new System.Drawing.Size(200, 100),
                TabIndex = 0
            };
            gamePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            gamePanel.Location = new System.Drawing.Point(30, 85);
            Controls.Add(gamePanel);

            CreateBoard(field);

            // End game button reset.
            endGameButton.Text = "smilyimg";
            endGameButton.Click -= ResetGame_Click;
            endGameButton.Click += EndGameButton_Click;

            // RevealAll reset.
            revealAllBorder.Hide();
            revealAllButton.Hide();

            // First click reset.
            firstClick = false;
        }

        /// <summary>
        /// User chooses to end game by pressing top button.
        /// </summary>
        private void EndGameButton_Click(object sender, EventArgs e) => GameOver(false);

        /// <summary>
        /// Reveal all unflagged tiles. This will end the game one way or another.
        /// </summary>
        private void RevealAllButton_Click(object sender, EventArgs e)
        {
            GameOver(true);
        }

        #region Toolbar
        /// <summary>
        /// Toolbar buttons start a new game or show stats.
        /// </summary>
        private void SmallToolStripMenuItem_Click(object sender, EventArgs e) => MakeNewGameCloseOld("small");
        private void MediumToolStripMenuItem_Click(object sender, EventArgs e) => MakeNewGameCloseOld("medium");
        private void LargeToolStripMenuItem_Click(object sender, EventArgs e) => MakeNewGameCloseOld("large");

        /// <summary>
        /// Make a new game of various sizes. If user has started old game, report it as a loss.
        /// </summary>
        /// <param name="size">new game size</param>
        private void MakeNewGameCloseOld(string size)
        {
            //if (firstClick)
            //report loss
            new GameWindow(size).Show();
            Hide();
        }

        #endregion

        #region Exit Program
        /// <summary>
        /// Terminate program.
        /// </summary>
        private void GameWindow_FormClosed(object sender, FormClosedEventArgs e) => Application.Exit();
        #endregion

    }
}
