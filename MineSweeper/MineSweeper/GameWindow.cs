using Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MineSweeper
{
    public enum GameSize { small, medium, large };

    public partial class GameWindow : Form
    {
        const float BUTTON_SIZE = 20F;
        private Field field;
        private readonly Dictionary<Tile, MineSweeperButton> connections = new Dictionary<Tile, MineSweeperButton>();
        private GameSize size;
        private StatisticsTracker _tracker = new StatisticsTracker();

        #region Constructor
        public GameWindow(string gameSize)
        {
            InitializeComponent();

            // Pick one of three sizes of games.
            switch (gameSize)
            {
                case "small":
                    SetupGame(8, 8, 10, GameSize.small);
                    break;
                case "medium":
                    SetupGame(16, 16, 40, GameSize.medium);
                    break;
                case "large":
                    SetupGame(30, 16, 99, GameSize.large);
                    break;
                default:
                    throw new Exception("Game Size is not valid");
            };
        }

        private void SetupGame(int x, int y, int numMines, GameSize size)
        {
            this.size = size;
            field = new Field(x, y, numMines);

            CreateBoard(field);
        }

        private void CreateBoard(Field field)
        {
            int numCols = field.Width;
            int numRows = field.Height;

            MakeGamePanel(numCols, numRows);

            var topButtonLocation = PlaceEndGameButton();
            SetFlagLabel(topButtonLocation);
            SetRevealButton(topButtonLocation);

            LinkButtonsToTiles(numCols, numRows);
        }

        private void LinkButtonsToTiles(int numCols, int numRows)
        {
            gamePanel.SuspendLayout();
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
                    button.DoubleClick += Button_DoubleClick;
                }
            }
            gamePanel.ResumeLayout();
        }

        private void SetRevealButton(Point topButtonLocation)
        {
            // Reveal all button left position.
            revealAllButton.Location = Point.Subtract(topButtonLocation, new Size(50, -5));
            revealAllBorder.Location = Point.Subtract(topButtonLocation, new Size(52, -3));
        }

        private void SetFlagLabel(Point topButtonLocation)
        {
            // Set flag label and right position.
            flagCounterLabel.Text = field.NumFlagsLeft.ToString();
            flagCounterLabel.Location = Point.Add(topButtonLocation, new Size(60, 5));
        }

        private Point PlaceEndGameButton()
        {
            // End game button centered over game panel.
            Point topButtonLocation = new Point(gamePanel.Size.Width / 2 + 10, 35);
            endGameButton.Image = Image.FromFile("../../Images/normal.png");
            endGameButton.Location = topButtonLocation;

            return topButtonLocation;
        }

        private void MakeGamePanel(int numCols, int numRows)
        {
            gamePanel.ColumnCount = numCols;
            gamePanel.RowCount = numRows;
            // Remove first column.
            gamePanel.ColumnStyles.RemoveAt(0);
            // Add cols and rows.
            for (int i = 0; i < numCols; i++)
                gamePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, BUTTON_SIZE));
            for (int i = 0; i < numRows; i++)
                gamePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, BUTTON_SIZE));

            // Resize game window to fit game panel.
            gamePanel.Size = new Size(numCols * 20, numRows * 20);
            ClientSize = new Size(gamePanel.Size.Width + 60, gamePanel.Size.Height + 100);
        }

        #endregion

        #region Mouse Controls
        /// <summary>
        /// Event handler for when the user lifts the mouse button.
        /// If it is left, reveal the tile (possibly ending the game).
        /// If it is right, try to flag or unflag a tile.
        /// </summary>
        private void Button_MouseUp(object sender, MouseEventArgs e)
        {
            MineSweeperButton button = (MineSweeperButton)sender;
            Tile tile = button.Tile;
            // double click
            //TODO
            if (e.Button == MouseButtons.Left)
            {
                HandleLeftClick(tile);
            }
            else if (e.Button == MouseButtons.Right)
            {
                HandleRightClick(tile);
            }

            button.ReplaceImage();
        }

        void Button_DoubleClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void HandleLeftClick(Tile tile)
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

        private void HandleRightClick(Tile tile)
        {
            field.Flag(tile);
            flagCounterLabel.Text = field.NumFlagsLeft.ToString();

            // Show game end button.
            if (field.NumFlagsLeft == 0)
            {
                revealAllButton.Show();
                revealAllBorder.Show();
                endGameButton.Image = Image.FromFile("../../Images/Worried.png");
            }
            else
            {
                revealAllButton.Hide();
                revealAllBorder.Hide();
                endGameButton.Image = Image.FromFile("../../Images/normal.png");
            }
        }

        /// <summary>
        /// Remove event handler from a button.
        /// Do nothing if the user clicks on a button with its functionality removed.
        /// </summary>
        private void RemoveFunctionality(Button button)
        {
            button.MouseUp -= Button_MouseUp;
            button.DoubleClick -= Button_DoubleClick;
        }
        #endregion

        #region Toolbar
        /// <summary>
        /// Toolbar buttons start a new game or show stats.
        /// </summary>
        private void SmallToolStripMenuItem_Click(object sender, EventArgs e) => MakeNewGameCloseOld("small");
        private void MediumToolStripMenuItem_Click(object sender, EventArgs e) => MakeNewGameCloseOld("medium");
        private void LargeToolStripMenuItem_Click(object sender, EventArgs e) => MakeNewGameCloseOld("large");
        private void StatsButton_Click(object sender, EventArgs e) => new StatsForm().Show();

        /// <summary>
        /// Make a new game of various sizes. If user has started old game, report it as a loss.
        /// </summary>
        /// <param name="size">new game size</param>
        private void MakeNewGameCloseOld(string size)
        {
            if (field.firstClick)
            {
                // Warn user of loss.
                DialogResult dialogResult = MessageBox.Show(
                    "Starting a new game now will result in a loss!\nContinue?", "Start New Game", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                    return;
                //MessageBox.Show()
                _tracker.IncreaseLossCounter(this.size);
            }
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
            SetupGame(field.Width, field.Height, field.NumMines, size);

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
            {
                ReportWin();
            }
            //loss
            else
            {
                ReportLoss();
            }

            DisableButtons();
            ResetEndGameButton();
        }

        private void ReportWin()
        {
            _tracker.IncreaseWinCounter(size);
            endGameButton.Image = Image.FromFile("../../Images/Cool.png");
        }

        private void ReportLoss()
        {
            // If no click happened, generate random board.
            if (!field.firstClick)
                field.PopulateField();
            // Reveal all mines.
            foreach (Tile mine in field.GetMines())
            {
                field.Reveal(mine);
                connections[mine].ReplaceImage();
            }
            // Report loss.
            endGameButton.Image = Image.FromFile("../../Images/Dead.png");
            _tracker.IncreaseLossCounter(size);
        }

        private void DisableButtons()
        {
            // Disable other buttons.
            foreach (Button button in gamePanel.Controls)
            {
                RemoveFunctionality(button);
            }
        }

        private void ResetEndGameButton()
        {
            // End game button reset.
            endGameButton.Click -= EndGameButton_Click;
            endGameButton.Click += ResetGame_Click;
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
