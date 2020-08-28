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
                // Catch divide by zero exception.
                try
                {
                    double res = (double)numWins / (numWins + numLoss);
                    label.Text = res.ToString();
                }
                catch
                {
                    smallPercentage.Text = "-";
                }
            }
        }
    }
}
