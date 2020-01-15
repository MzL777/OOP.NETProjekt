using DAL.Model.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF
{
    /// <summary>
    /// Interaction logic for TeamDetails.xaml
    /// </summary>
    public partial class TeamDetails : Window
    {
        private Result statistics;
        public Result Statistics
        {
            get => statistics;
            set
            {
                statistics = value;
                LoadStatistics();
            }
        }

        private void LoadStatistics()
        {
            lblName.Content = statistics.Country;
            lblFifaCode.Content = statistics.FifaCode;
            lblGames.Content = statistics.GamesPlayed;
            lblWins.Content = statistics.Wins;
            lblLosses.Content = statistics.Losses;
            lblDraws.Content = statistics.Draws;
            lblPoints.Content = statistics.Points;
            lblGoalsFor.Content = statistics.GoalsFor;
            lblGoalsAgainst.Content = statistics.GoalsAgainst;
            lblGoalDifferential.Content = statistics.GoalDifferential;
        }

        public TeamDetails()
        {
            InitializeComponent();
        }
    }
}
