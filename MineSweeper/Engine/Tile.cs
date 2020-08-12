using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

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

        public void Click(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (this.Enabled)
            {
                if (e.Button == MouseButtons.Left && !this.IsFlagged)
                {
                    //deactivate
                    Enabled = false;
                    //bomb
                    if (this.IsArmed)
                    {
                        //GameOver();
                        button.Image = Image.FromFile("../../Images/Bomb.bmp");
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
                        button.Image = Image.FromFile("../../Images/Flag.bmp");
                        IsFlagged = true;
                    }
                    else
                    {
                        button.Image = null;
                        IsFlagged = false;
                    }
                }
            }
        }
    }
}
