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
                    //b.Click += (sender, e) => Tile_Click(sender, e, tile);
                    b.MouseUp += (sender, e) => Tile_MouseUp(sender, e, tile);
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

        private void Tile_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e, Tile tile)
        {
            var button = (Button)sender;
            // Reveal state.
            if (e.Button == MouseButtons.Left)
            {
                //bomb
                if (tile.IsArmed)
                {
                    button.Image = Image.FromFile("../../Images/Bomb.bmp");
                    //GameOver();
                }
                //normal
                else
                    button.Text = tile.GetDanger().ToString();
            }
            // Flag or deflag tile.
            if (e.Button == MouseButtons.Right)
            {
                button.Text = "";
                button.Image = Image.FromFile("../../Images/Flag.bmp");
            }
        }
    }
}
