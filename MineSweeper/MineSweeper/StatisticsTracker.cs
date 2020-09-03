using MineSweeper.Properties;
using System;
using System.Linq;

namespace MineSweeper
{
    public class StatisticsTracker
    {
        /// <summary>
        /// This increases the win counter kept by settings based on the size of the game.
        /// </summary>
        public void IncreaseWinCounter(GameSize size)
        {
            switch (size)
            {
                case GameSize.small:
                    Settings.Default.SmallWinsData++;
                    break;
                case GameSize.medium:
                    Settings.Default.MediumWinsData++;
                    break;
                case GameSize.large:
                    Settings.Default.LargeWinsData++;
                    break;
            }
            Settings.Default.Save();
        }

        /// <summary>
        /// This increases the loss counter kept by settings based on the size of the game.
        /// </summary>
        public void IncreaseLossCounter(GameSize size)
        {
            switch (size)
            {
                case GameSize.small:
                    Settings.Default.SmallLossData++;
                    break;
                case GameSize.medium:
                    Settings.Default.MediumLossData++;
                    break;
                case GameSize.large:
                    Settings.Default.LargeLossData++;
                    break;
            }
            Settings.Default.Save();
        }
    }
}
