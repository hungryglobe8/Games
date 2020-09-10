using Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MineSweeper
{
    /// <summary>
    /// A minesweeper button links a button in windows forms to the logic of a tile.
    /// This class focuses some of the more button-specific interactions away from the GameWindow.
    /// Ideally it would do even more (events).
    /// </summary>
    public class MineSweeperButton : Button
    {
        private readonly IImageProvider _imageProvider;
        private readonly Dictionary<int, Color> _colors = new Dictionary<int, Color>()
        {
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

        public MineSweeperButton(Tile tile, IImageProvider imageProvider)
        {
            Tile = tile;
            _imageProvider = imageProvider;
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
                        Image = _imageProvider.GetImage("Bomb.bmp");
                    else
                    {
                        int danger = Tile.GetDanger();
                        Text = danger.ToString();
                        ForeColor = _colors[danger];
                    }
                    break;

                case State.Flagged:
                    Image = _imageProvider.GetImage("Flag.bmp");
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
