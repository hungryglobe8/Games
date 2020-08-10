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
                    b.Click += PieceButton_Click;
                    b.Location = new Point(BUTTON_SIZE * (x + 1), BUTTON_SIZE * (y + 1));
                    b.Size = new Size(BUTTON_SIZE, BUTTON_SIZE);
                    
                    Tile tile = field.GetTile(x, y);
                    //bomb image
                    if (tile.IsArmed)
                        b.Image = Image.FromFile("../../Images/Icon1.ico");
                    //num surrounding mines
                    else
                        b.Text = tile.GetDanger().ToString();

                    Controls.Add(b);
                }
            }
        }
        private void PieceButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            MessageBox.Show(button.Location.X + "/" + button.Location.Y);
        }
    }
}
