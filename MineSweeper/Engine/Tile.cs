using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Engine
{
    /// <summary>
    /// Individual squares on a MineSweeper field.
    /// 
    /// Each tile contains information about its neighbors and current condition.
    /// </summary>
    public class Tile
    {
        private int dangerLevel;
        public Button button;

        // Is there a mine on this space?
        public bool IsArmed { private set; get; }
        public bool IsFlagged { set; get; }
        public bool Enabled { get; private set; }
        public int X { get; }
        public int Y { get; }

        /// <summary>
        /// Create a tile with or without a bomb underneath it.
        /// </summary>
        /// <param name="armed">initial mine state</param>
        public Tile(int x = 0, int y = 0, bool armed = false)
        {
            X = x;
            Y = y;
            IsArmed = armed;
            Enabled = true;
            button = new Button();
            if (IsArmed)
                dangerLevel = 10;
            else
                dangerLevel = 0;
        }

        /// <summary>
        /// Ask a tile how many armed neighbors it has.
        /// If the value is 10, the tile is armed.
        /// </summary>
        public int GetDanger()
        {
            return dangerLevel;
        }

        /// <summary>
        /// Increase the danger level of a tile if it is not armed.
        /// </summary>
        public void DangerUp()
        {
            if (!IsArmed)
                dangerLevel++;
        }

        /// <summary>
        /// Contains logic for left clicking a tile.
        /// Nothing changes if the tile has been clicked or flagged.
        /// Deactivates a given tile if the user does a left click, revealing a number or a mine.
        /// </summary>
        public Image LeftClick(Image oldImage)
        {
            if (!Enabled || IsFlagged)
                return oldImage;

            else
            {
                //deactivate
                Enabled = false;
                //bomb
                if (IsArmed)
                {
                    //GameOver();
                    return Image.FromFile("../../Images/Bomb.bmp");
                }
                //normal
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
                    int danger = GetDanger();
                    button.Text = danger.ToString();
                    button.ForeColor = colors[danger];
                    return null;
                }
            }
        }

        /// <summary>
        /// Contains logic for right clicking a tile.
        /// If a tile has been revealed, do nothing.
        /// Add or remove a flag as appropriate.
        /// </summary>
        public Image RightClick(Image oldImage)
        {
            if (!Enabled)
                return oldImage;

            if (!IsFlagged)
            {
                IsFlagged = true;
                return Image.FromFile("../../Images/Flag.bmp");
            }
            else
            {
                IsFlagged = false;
                return null;
            }
        }
    }
}
