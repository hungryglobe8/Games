using Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MineSweeper
{

    public partial class GameWindow : Form
    {
        const float BUTTON_SIZE = 20F;
        private Field _field;
        private Dictionary<Tile, MineSweeperButton> _connections = new Dictionary<Tile, MineSweeperButton>();
        private readonly IImageProvider _imageProvider;
        private GameSize _size;
        private StatisticsTracker _tracker = new StatisticsTracker();

        #region Constructor
        public GameWindow(string gameSize, IImageProvider imageProvider)
        {
            InitializeComponent();

            _imageProvider = imageProvider;

            if (!Enum.TryParse(gameSize, out GameSize size))
            {
                throw new ArgumentException($"Invalid game size {gameSize}"); 
            }
            _size = size;
            SetupGame(this._size);
        }

        private void SetupGame(GameSize size)
        {
            _field = Field.Create(size);
            CreateBoard(_field);
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
            FastChangeGamePanel(gp =>
            {
                for (int x = 0; x < numCols; x++)
                {
                    for (int y = 0; y < numRows; y++)
                    {
                        // Add a new connection.
                        Tile tile = _field.GetTile(x, y);
                        MineSweeperButton button = new MineSweeperButton(tile, _imageProvider);
                        _connections.Add(tile, button);

                        gp.Controls.Add(button, x, y);
                        button.MouseUp += Button_MouseUp;
                        button.DoubleClick += Button_DoubleClick;
                    }
                }
            });
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
            flagCounterLabel.Text = _field.NumFlagsLeft.ToString();
            flagCounterLabel.Location = Point.Add(topButtonLocation, new Size(60, 5));
        }

        private Point PlaceEndGameButton()
        {
            // End game button centered over game panel.
            Point topButtonLocation = new Point(gamePanel.Size.Width / 2 + 10, 35);
            endGameButton.Image = _imageProvider.GetImage("normal");
            endGameButton.Location = topButtonLocation;

            return topButtonLocation;
        }

        private void MakeGamePanel(int numCols, int numRows)
        {
            FastChangeGamePanel(gp =>
            {
                gp.ColumnCount = numCols;
                gp.RowCount = numRows;
                // Remove first column.
                gp.ColumnStyles.RemoveAt(0);
                // Add cols and rows.
                for (int i = 0; i < numCols; i++)
                    gp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, BUTTON_SIZE));
                for (int i = 0; i < numRows; i++)
                    gp.RowStyles.Add(new RowStyle(SizeType.Absolute, BUTTON_SIZE));

                // Resize game window to fit game panel.
                gp.Size = new Size(numCols * 20, numRows * 20);
            });
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
                var revealedTiles = _field.Reveal(tile);
                // Lose game.
                if (tile.IsArmed)
                {
                    GameOver();
                    return;
                }

                foreach (Tile t in revealedTiles)
                {
                    var neighborButton = _connections[t];
                    RemoveFunctionality(neighborButton);
                    neighborButton.ReplaceImage();
                }

                // If all tiles have been revealed, win game.
                if (_field.FoundAllNormalTiles)
                    GameOver();

                return;
            }
        }

        private void HandleRightClick(Tile tile)
        {
            _field.Flag(tile);
            flagCounterLabel.Text = _field.NumFlagsLeft.ToString();

            // Show game end button.
            if (_field.NumFlagsLeft == 0)
            {
                revealAllButton.Show();
                revealAllBorder.Show();
                endGameButton.Image = _imageProvider.GetImage("Worried");
            }
            else
            {
                revealAllButton.Hide();
                revealAllBorder.Hide();
                endGameButton.Image = _imageProvider.GetImage("normal");
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
            if (_field.FirstClick)
            {
                // Warn user of loss.
                DialogResult dialogResult = MessageBox.Show(
                    "Starting a new game now will result in a loss!\nContinue?", "Start New Game", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                    return;
                //MessageBox.Show()
                _tracker.IncreaseLossCounter(this._size);
            }
            //TODO: spawning a new game window from another game window isn't a great idea,
            //the old one isn't thrown out of memory just because you Hide it...
            //probably should just reinitialize the board: SetupGame?
            new GameWindow(size, _imageProvider).Show();
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
            foreach (Tile tile in _field.GetTiles())
            {
                _field.Reveal(tile);
                _connections[tile].ReplaceImage();
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
            _connections = new Dictionary<Tile, MineSweeperButton>();

            FastChangeGamePanel(gp =>
            {
                gp.Controls.Clear();
            });

            // Make new board with same dimensions.
            SetupGame(_size);

            // End game button reset.
            endGameButton.Click -= ResetGame_Click;
            endGameButton.Click += EndGameButton_Click;

            // RevealAll reset.
            revealAllBorder.Hide();
            revealAllButton.Hide();
        }

        /// <summary>
        /// Toggles visibility of gamePanel before performing an expensive action
        /// </summary>
        /// <param name="action"></param>
        private void FastChangeGamePanel(Action<TableLayoutPanel> action)
        {
            /* I don't think either of these make much difference */
            gamePanel.Visible = false;
            gamePanel.SuspendLayout();

            action(gamePanel);

            gamePanel.ResumeLayout();
            gamePanel.Visible = true;
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
            if (_field.FoundAllNormalTiles)
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
            _tracker.IncreaseWinCounter(_size);
            endGameButton.Image = _imageProvider.GetImage("Cool");
        }

        private void ReportLoss()
        {
            // If no click happened, generate random board.
            if (!_field.FirstClick)
                _field.PopulateField();
            // Reveal all mines.
            foreach (Tile mine in _field.GetMines())
            {
                _field.Reveal(mine);
                _connections[mine].ReplaceImage();
            }
            // Report loss.
            endGameButton.Image = _imageProvider.GetImage("Dead");
            _tracker.IncreaseLossCounter(_size);
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
