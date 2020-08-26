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

        #region Constructor
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
            endGameButton.Image = Image.FromFile("../../Images/normal.png");

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
        #endregion

        #region Mouse Controls
        /// <summary>
        /// Event handler for when the user lifts the mouse button.
        /// If it is left, reveal the tile (possibly ending the game).
        /// If it is right, try to flag or unflag a tile.
        /// </summary>
        private void Button_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MineSweeperButton button = (MineSweeperButton)sender;
            Tile tile = button.Tile;
            // double click
            //TODO
            //left click
            if (e.Button == MouseButtons.Left)
            {
                if (tile.state == State.Unopened)
                { 
                    var revealedTiles = field.Reveal(tile);
                    // Lose game.
                    if (tile.IsArmed)
                    {
                        GameOver();
                        return;
                    }
                    
                    foreach (Tile t in revealedTiles)
                    {
                        var neighborButton = connections[t];
                        RemoveFunctionality(neighborButton);
                        neighborButton.ReplaceImage();
                    }

                    // If all tiles have been revealed, win game.
                    if (field.FoundAllNormalTiles)
                        GameOver();

                    return;
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
        /// Remove event handler from a button.
        /// Do nothing if the user clicks on a button with its functionality removed.
        /// </summary>
        private void RemoveFunctionality(Button button) => button.MouseUp -= Button_MouseUp;
        #endregion
        
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
        
        #region Game Ending Buttons
        /// <summary>
        /// Reveal all unflagged tiles. Ends the game one way or another.
        /// </summary>
        private void RevealAllButton_Click(object sender, EventArgs e)
        {
            // Reveal all unflagged tiles.
            foreach (Tile tile in field.GetTiles())
            {
                field.Reveal(tile);
                connections[tile].ReplaceImage();
            }
            GameOver();
        }

        /// <summary>
        /// User chooses to end game by pressing top button.
        /// </summary>
        private void EndGameButton_Click(object sender, EventArgs e) => GameOver();

        /// <summary>
        /// Game is over. Clicking top middle button again will reset the game in the same window with a new minefield.
        /// TODO: VERY SLOW how to speed up??
        /// </summary>
        private void ResetGame_Click(object sender, EventArgs e)
        {
            // Remove old values.
            Dictionary<Tile, Button> connections = new Dictionary<Tile, Button>();
            gamePanel.Controls.Clear();

            // Make new board with same dimensions.
            field = new Field(field.Width, field.Height, field.NumMines);
            CreateBoard(field);

            // End game button reset.
            endGameButton.Click -= ResetGame_Click;
            endGameButton.Click += EndGameButton_Click;

            // RevealAll reset.
            revealAllBorder.Hide();
            revealAllButton.Hide();
        }
        #endregion

        #region End Game
        /// <summary>
        /// Reveal all mines or tiles and disable the game.
        /// Reveal all tiles if user uses revealAll button (only activated after placing all flags).
        /// </summary>
        private void GameOver()
        {
            //win
            if (field.FoundAllNormalTiles)
                MessageBox.Show("YOU WON!");
            //loss
            else
            {
                endGameButton.Image = Image.FromFile("../../Images/Dead.png");

                // If no click happened, generate random board.
                _ = field.Reveal(new Tile());

                // Reveal all mines.
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
        #endregion

        #region Exit Program
        /// <summary>
        /// Terminate program.
        /// </summary>
        private void GameWindow_FormClosed(object sender, FormClosedEventArgs e) => Application.Exit();
        #endregion

    }
}
