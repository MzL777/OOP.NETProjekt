using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.Model.Matches;

namespace WinForms.UserControls
{
    public partial class MatchUserControl : UserControl
    {
        private Match match;

        public MatchUserControl()
        {
            InitializeComponent();
        }

        public MatchUserControl(ref Match match)
        {
            InitializeComponent();

            this.match = match;

            LoadMatchData();
        }

        private void LoadMatchData()
        {
            lblHomeTeamScore.Text = match.HomeTeam.Goals.ToString();
            lblAwayTeamScore.Text = match.AwayTeam.Goals.ToString();
            lblLocation.Text = match.Location;
            lblAttendance.Text = match.Attendance.ToString();

            lblHomeTeam.Text = match.HomeTeam.ToString();
            var awayTeam = match.AwayTeam.ToString();
            lblAwayTeam.Text = awayTeam.Substring(awayTeam.Length - 5, 5) + " " + awayTeam.Substring(0, awayTeam.Length - 6);
        }
    }
}
