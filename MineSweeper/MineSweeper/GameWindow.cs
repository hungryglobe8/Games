using Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class GameWindow : Form
    {
        private Field field;
        private Dictionary<Tile, Button> connections = new Dictionary<Tile, Button>();
        private Dictionary<Button, Tile> b_connections = new Dictionary<Button, Tile>();
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

            //first click must be handled before population
            field.PopulateField(10);
            CreateBoard(x, y, field);
        }

        private void CreateBoard(int numCols, int numRows, Field field)
        {
            int BoardWidth = field.Width;
            int BoardHeight = field.Height;
            // 
            // Make GamePanel
            // 
            this.smallGamePanel.ColumnCount = numCols;
            this.smallGamePanel.RowCount = numRows;
            float BUTTON_SIZE = 20F;
            for (int i = 0; i < numCols; i++)
            {
                this.smallGamePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, BUTTON_SIZE));
                this.smallGamePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, BUTTON_SIZE));
            }
            this.smallGamePanel.Size = new System.Drawing.Size(x * 20, y * 20);

            for (int x = 0; x < BoardWidth; x++)
            {
                for (int y = 0; y < BoardHeight; y++)
                {
                    // Add a new connection.
                    Tile tile = field.GetTile(x, y);
                    Button button = new Button();
                    connections.Add(tile, button);
                    b_connections.Add(button, tile);

                    smallGamePanel.Controls.Add(button, x, y);
                    button.Dock = DockStyle.Fill;
                    //button.MouseUp += (sender, e) => Button_MouseUp(sender, e, tile);
                    button.MouseUp += Button_MouseUp;
                    button.Margin = new Padding(0);
                }
            }
        }

        private void Button_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var button = (Button)sender;
            var tile = b_connections[button];
            // double click
            //TODO
            //left click
            if (e.Button == MouseButtons.Left)
            {
                RemoveFunctionality(tile);
                // End game.
                if (tile.IsArmed)
                {
                    GameOver();
                    return;
                }

                tile.LeftClick();
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
            //right click
            else if (e.Button == MouseButtons.Right)
            {
                tile.RightClick();
            }

            ReplaceImage(button, tile);

        }

        private void RemoveFunctionality(Tile tile)
        {
            Button button = connections[tile];
            button.MouseUp -= Button_MouseUp;
        }

        /// <summary>
        /// Change the graphics of a given tile based on its state.
        /// </summary>
        /// <param name="tile"></param>
        private void ReplaceImage(Button button, Tile tile)
        {
            switch (tile.state)
            {
                case State.Revealed:
                    if (tile.IsArmed)
                        button.Image = Image.FromFile("../../Images/Bomb.bmp");
                    else
                    {
                        var colors = new Dictionary<int, Color>(){
                            {0, Color.Black },
                            {1, Color.Blue },
                            {2, Color.Green },
                            {3, Color.OrangeRed },
                            {4, Color.BlueViolet },
                            {5, Color.Brown },
                            {6, Color.Teal }
                        };
                        int danger = tile.GetDanger();
                        button.Text = danger.ToString();
                        button.ForeColor = colors[danger];
                    }
                    break;

                case State.Flagged:
                    button.Image = Image.FromFile("../../Images/Flag.bmp");
                    break;

                case State.Unopened:
                    button.Image = null;
                    break;

                default:
                    throw new Exception("Invalid state.");
            }
        }

        /// <summary>
        /// Reveal all unflagged mines and disable the game.
        /// </summary>
        private void GameOver()
        {
            foreach (Tile mine in field.GetMines())
            {
                mine.LeftClick();
                ReplaceImage(connections[mine], mine);
            }

            // tile in unopened tiles?
            foreach (Tile tile in field.GetTiles())
            {
                RemoveFunctionality(tile);
            }
        }

        /// <summary>
        /// User chooses to end game by pressing top button.
        /// </summary>
        private void EndGameButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.Text = "Ended";
            GameOver();
        }

        private void GameWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
