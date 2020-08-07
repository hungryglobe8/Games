using System;
using System.Collections.Generic;
using System.Text;

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
        // Is there a mine on this space?
        public bool IsArmed { private set; get; }

        /// <summary>
        /// Create a tile with or without a bomb underneath it.
        /// </summary>
        /// <param name="armed">initial mine state</param>
        public Tile(bool armed)
        {
            IsArmed = armed;
            dangerLevel = 0;
        }

        /// <summary>
        /// Ask a tile how many armed neighbors it has.
        /// </summary>
        public int GetDanger()
        {
            if (IsArmed)
                return -1;
            return dangerLevel;
        }

        /// <summary>
        /// Increase the danger level of a tile.
        /// </summary>
        public void DangerUp()
        {
            dangerLevel++;
        }
    }
}
