namespace Engine
{
    // Tile state as seen by user.
    public enum State { Unopened, Revealed, Flagged };

    /// <summary>
    /// Individual squares on a MineSweeper field.
    /// 
    /// Each tile contains information about its neighbors and current condition.
    /// </summary>
    public class Tile
    {
        private int dangerLevel;
        public State state;

        public bool IsArmed { private set; get; }
        public int X { get; }
        public int Y { get; }

        /// <summary>
        /// Create a tile with or without a bomb underneath it.
        /// Set location and armed properties. If armed, set danger level to 10.
        /// </summary>
        /// <param name="armed">initial mine state</param>
        public Tile(int x = 0, int y = 0, bool armed = false)
        {
            X = x;
            Y = y;
            IsArmed = armed;

            state = State.Unopened;
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
        public void LeftClick()
        {
            if (state == State.Flagged || state == State.Revealed)
                return;

            else
            {
                //reveal
                state = State.Revealed;
                //bomb
                if (IsArmed)
                {
                    //GameOver();
                    //button.Image = Image.FromFile("../../Images/Bomb.bmp");
                }
                //number
                else
                {
                    //int danger = GetDanger();
                    //button.Text = danger.ToString();
                    //button.ForeColor = colors[danger];
                }
            }
        }

        /// <summary>
        /// Contains logic for right clicking a tile.
        /// If a tile has been revealed, do nothing.
        /// Add or remove a flag as appropriate.
        /// </summary>
        public void RightClick()
        {
            if (state == State.Revealed)
                return;

            if (state == State.Flagged)
                state = State.Unopened;
            else
                state = State.Flagged;
        }
    }
}
