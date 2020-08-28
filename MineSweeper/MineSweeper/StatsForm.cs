using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MineSweeper.Properties;
using System.Linq.Expressions;

namespace MineSweeper
{
    public partial class StatsForm : Form
    {
        public StatsForm()
        {
            InitializeComponent();
            CalculatePercentages();
            resetSmallButton.ImageIndex = 0;
            resetMediumButton.ImageIndex = 0;
            resetLargeButton.ImageIndex = 0;
        }

        /// <summary>
        /// Calculate the percentages for each of the stat labels upon loading of the StatsForm.
        /// </summary>
        private void CalculatePercentages()
        {
            var groups = new List<Tuple<int, int, Label>>()
            {
                new Tuple<int, int, Label>(Settings.Default.SmallWinsData, Settings.Default.SmallLossData, smallPercentage),
                new Tuple<int, int, Label>(Settings.Default.MediumWinsData, Settings.Default.MediumLossData, mediumPercentage),
                new Tuple<int, int, Label>(Settings.Default.LargeWinsData, Settings.Default.LargeLossData, largePercentage)
            };

            foreach ((int numWins, int numLoss, Label label) in groups)
            {
                // Handle division by zero.
                double res = (double)numWins / (numWins + numLoss);
                if (res.Equals(double.NaN))
                    label.Text = "-";
                else
                    label.Text = res.ToString("p");
            }
        }

        private void ResetSmallButton_Click(object sender, EventArgs e)
        {
            Settings.Default.SmallWinsData = 0;
            Settings.Default.SmallLossData = 0;
            Settings.Default.Save();
            CalculatePercentages();
        }

        private void ResetMediumButton_Click(object sender, EventArgs e)
        {
            Settings.Default.MediumLossData = 0;
            Settings.Default.MediumWinsData = 0;
            Settings.Default.Save();
            CalculatePercentages();
        }

        private void ResetLargeButton_Click(object sender, EventArgs e)
        {
            Settings.Default.LargeLossData = 0;
            Settings.Default.LargeWinsData = 0;
            Settings.Default.Save();
            CalculatePercentages();
        }
    }
}
