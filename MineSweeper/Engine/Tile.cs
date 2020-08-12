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

        /// <summary>
        /// Create a tile with or without a bomb underneath it.
        /// </summary>
        /// <param name="armed">initial mine state</param>
        public Tile(bool armed = false)
        {
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

        public Image LeftClick(Image oldImage)
        {
            if (oldImage != null)
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
                        {0, Color.AliceBlue },
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
        /// Contains logic for potential click paths of a tile. 
        /// Deactivates a given tile if the user does a left click, revealing a number or a mine.
        /// Right clicking adds or removes a flag image. Flagged tiles can't be left clicked.
        /// TODO: Double clicking reveals all neighbors.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="oldImage"></param>
        /// <returns></returns>
        public Image Click(System.Windows.Forms.MouseEventArgs e, Image oldImage)
        {
            Image result = oldImage;
            if (e.Button == MouseButtons.Left && !IsFlagged)
            {
                //deactivate
                Enabled = false;
                //bomb
                if (this.IsArmed)
                {
                    //GameOver();
                    result = Image.FromFile("../../Images/Bomb.bmp");
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
                    int danger = GetDanger();
                    if (danger != 0)
                    {
                        button.Text = danger.ToString();
                        button.ForeColor = colors[danger];
                    }
                }
            }

            // Flag or deflag tile.
            else if (e.Button == MouseButtons.Right)
            {
                if (!IsFlagged)
                {
                    IsFlagged = true;
                    result = Image.FromFile("../../Images/Flag.bmp");
                }
                else
                {
                    IsFlagged = false;
                    result = null;
                }
            }
            return result;
        }
    }
}
