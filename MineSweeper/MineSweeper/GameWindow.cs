using Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class GameWindow : Form
    {
        private Field field;
        private bool firstClick = false;
        public GameWindow()
        {
            InitializeComponent();
            field = new Field(10, 10, 15);

            //first click must be handled before population
            field.PopulateField(10);
            CreateBoard(field);
        }

        private void GameWindow_Load(object sender, EventArgs e)
        {

        }

        private void CreateBoard(Field field)
        {
            int BoardWidth = field.Width;
            int BoardHeight = field.Height;
            //Irrelevant since introducting game panels.
            int BUTTON_SIZE = 20;
            for (int x = 0; x < BoardWidth; x++)
            {
                for (int y = 0; y < BoardHeight; y++)
                {
                    Tile tile = field.GetTile(x, y);
                    Button button = new Button
                    smallGamePanel.Controls.Add(button, x, y);
                    button.Dock = DockStyle.Fill;
                    button.MouseUp += (sender, e) => Button_MouseUp(sender, e, tile);
                    //b.EnabledChanged += Tile_EnableChanged;
                    button.Margin = new Padding(0);
                }
            }
        }

        private void Button_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e, Tile tile)
        {
            var button = (Button)sender;
            // double click
            //TODO
            //left click
            if (e.Button == MouseButtons.Left)
            {
                tile.LeftClick();
                // Recursively find all neighbors of 0 danger tiles.
                if (tile.GetDanger() == 0 && (tile.state == State.Unopened))
                {
                    IList<Tile> neighbors = field.GetNeighbors(tile.X, tile.Y);
                    foreach (Tile neighbor in neighbors)
                    {
                        if (neighbor.state == State.Unopened)
                            Button_MouseUp(neighbor.button, e, neighbor);
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

                default:
                    button.Image = null;
                    break;
            }
        }

        private void Tile_EnableChanged(object sender, System.EventArgs e)
        {
            var button = (Button)sender;
            button.ForeColor = button.Enabled == false ? Color.Blue : Color.Red;
            button.BackColor = Color.AliceBlue;
        }

        private void EndGameButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.Text = "Ended";
            field.RevealAll();
        }
    }
}
