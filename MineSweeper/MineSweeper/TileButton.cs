using Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    class TileButton : Button
    {
        public Tile Tile { private set; get; }
        public GameWindow Game { get; }

        public TileButton(GameWindow game, Tile tile)
        {
            Tile = tile;
            Game = game;
        }

        /// <summary>
        /// Change the graphics of a given tile based on its state.
        /// </summary>
        /// <param name="tile"></param>
        public void ReplaceImage()
        {
            switch (Tile.state)
            {
                case State.Revealed:
                    if (Tile.IsArmed)
                        Image = Image.FromFile("../../Images/Bomb.bmp");
                    else
                    {
                        var colors = new Dictionary<int, Color>(){
                            {0, Color.Black },
                            {1, Color.Blue },
                            {2, Color.Green },
                            {3, Color.OrangeRed },
                            {4, Color.BlueViolet },
                            {5, Color.Brown },
                            {6, Color.Teal },
                            {7, Color.Red },
                            {8, Color.Blue }
                        };
                        int danger = Tile.GetDanger();
                        Text = danger.ToString();
                        ForeColor = colors[danger];
                    }
                    break;

                case State.Flagged:
                    Image = Image.FromFile("../../Images/Flag.bmp");
                    break;

                case State.Unopened:
                    Image = null;
                    break;

                default:
                    throw new Exception("Invalid state.");
            }
        }

        /// <summary>
        /// Remove the functionality of a button if it has been revealed.
        /// In the future do nothing with this TileButton.
        /// </summary>
        public void RemoveFunctionality() => MouseUp -= Game.Button_MouseUp;
    }
}
