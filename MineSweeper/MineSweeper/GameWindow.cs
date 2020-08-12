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
        private List<Button> buttons;

        public GameWindow()
        {
            InitializeComponent();
            Field field = new Field(4, 4, 6);
            field.PopulateField(10);
            CreateBoard(field);
        }

        private void GameWindow_Load(object sender, EventArgs e)
        {

        }

        private void CreateBoard(Field field)
        {
            int BoardWidth = 4;
            int BoardHeight = 4;
            int BUTTON_SIZE = 20;
            for (int x = 0; x < BoardWidth; x++)
            {
                for (int y = 0; y < BoardHeight; y++)
                {
                    Button b = new Button();
                    Tile tile = field.GetTile(x, y);
                    b.MouseUp += (sender, e) => Tile_MouseUp(sender, e, tile);
                    b.EnabledChanged += Tile_EnableChanged;
                    b.Location = new Point(BUTTON_SIZE * (x + 1), BUTTON_SIZE * (y + 1));
                    b.Size = new Size(BUTTON_SIZE, BUTTON_SIZE);
                    
                    //bomb image
                    //if (tile.IsArmed)
                    //    b.Image = Image.FromFile("../../Images/Bomb.bmp");
                    ////num surrounding mines
                    //else
                    //    b.Text = tile.GetDanger().ToString();

                    Controls.Add(b);
                }
            }
        }

        private void Tile_EnableChanged(object sender, System.EventArgs e)
        {
            var button = (Button)sender;
            button.ForeColor = button.Enabled == false ? Color.Blue : Color.Red;
            button.BackColor = Color.AliceBlue;
        }

        private void Tile_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e, Tile tile)
        {
            var clickedButton = (Button)sender;
            // Reveal state.
            if (clickedButton.Enabled)
            {
                // Multi click on revealed square reveals neighbors.
                // TODO: incorporate into MouseDown event.
                //if (e.Button == MouseButtons.Left && e.Button == MouseButtons.Right)
                //{
                //    MessageBox.Show("Double click used properly!");
                //}

                if (e.Button == MouseButtons.Left && !tile.IsFlagged)
                {
                    //bomb
                    if (tile.IsArmed)
                    {
                        clickedButton.Image = Image.FromFile("../../Images/Bomb.bmp");
                        //GameOver();
                    }
                    //normal
                    else
                    {
                        var colors = new Dictionary<int, Color>(){
                            {1, Color.Blue },
                            {2, Color.Green },
                            {3, Color.OrangeRed },
                            {4, Color.BlueViolet },
                            {5, Color.Brown },
                            {6, Color.Teal }
                        };
                        int danger = tile.GetDanger();
                        if (danger != 0)
                        {
                            clickedButton.Text = danger.ToString();
                            clickedButton.ForeColor = colors[danger];
                        }
                    }

                    //deactivate
                    //clickedButton.Enabled = false;
                }
                // Flag or deflag tile.
                else if (e.Button == MouseButtons.Right)
                {
                    if (!tile.IsFlagged)
                    {
                        clickedButton.Image = Image.FromFile("../../Images/Flag.bmp");
                        tile.IsFlagged = true;
                    }
                    else
                    {
                        clickedButton.Image = null;
                        tile.IsFlagged = false;
                    }
                } 
            }   
        }
    }
}
