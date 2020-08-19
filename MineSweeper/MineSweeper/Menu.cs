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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            makeSmallButton.Click += StartGame;
            makeMediumButton.Click += StartGame;
            makeLargeButton.Click += StartGame;
        }

        private void StartGame(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Form game = new GameWindow(button.Text);
            Hide();
            game.Show();
        }
    }
}
