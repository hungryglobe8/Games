using Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MineSweeper
{
    public class MineSweeperButton : Button
    {
        public MineSweeperButton(Tile tile)
        {
            Tile = tile;
            Dock = DockStyle.Fill;
            Margin = new Padding(0);
        }

        /// <summary>
        /// Change the graphics of a given button based on its state.
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

        public Tile Tile { get; }
    }
}
