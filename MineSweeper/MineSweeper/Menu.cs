using System;
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
