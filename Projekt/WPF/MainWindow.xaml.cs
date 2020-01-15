using DAL;
using DAL.Model;
using DAL.Model.Matches;
using DAL.Model.Teams;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF.UserControls;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Team FavouriteTeam;
        private Team OpposingTeam;
        private Match CurrentMatch;

        private LoadingWindow loadingWindow = new LoadingWindow();
        private static BackgroundWorker bwGetData = new BackgroundWorker(), bwLoading = new BackgroundWorker();

        private ObservableCollection<Team> opposingTeamsColletion;

        private TeamDetails teamDetails;
        private PlayerDetails playerDetails;

        public MainWindow()
        {
            InitBackgroundWorkers();
            LoadSettings();

            ShowLoadingWindow();
            InitializeComponent();

            LoadFavouriteRepresentationFifaCode();
            LoadTeams();
        }

        private void ShowLoadingWindow()
        {
            if (!bwLoading.IsBusy)
            {
                bwLoading.RunWorkerAsync();
            }
            else
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    loadingWindow.Visibility = Visibility.Visible;
                });
            }
        }
        private void HideLoadingWindow()
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                loadingWindow.Visibility = Visibility.Hidden;
            });
        }

        private void LoadTeams()
        {
            bwGetData.RunWorkerAsync();
        }

        private void LoadFavouriteRepresentationFifaCode()
        {

            var data = Dal.Instance.ReadFavouriteTeamAndPlayersFromFile();
            if (data == null)
            {
                return;
            }

            if (data.ContainsKey("team") && data["team"] != null)
            {
            }
        }

        private void InitBackgroundWorkers()
        {
            bwLoading.DoWork += bwLoading_DoWork;
            bwGetData.DoWork += bwGetData_DoWork;
            bwGetData.RunWorkerCompleted += bwGetData_RunWorkerCompleted;
        }

        private void LoadSettings(bool force = false)
        {
            bool hasLang = false;
            string language = Dal.Instance.ReadLanguageFromFile();

            bool hasFullscrn = bool.TryParse(Dal.Instance.ReadFullscreenFromFile(), out bool fullscreen);

            if (language != null && language.Trim() != string.Empty)
            {
                hasLang = true;
            }

            if (force || !hasLang || !hasFullscrn)
            {
                SettingsWindow settings;
                if (hasLang && hasFullscrn)
                {
                    settings = new SettingsWindow(fullscreen, language);
                }
                else
                {
                    settings = new SettingsWindow();
                }
                settings.WindowStartupLocation = WindowStartupLocation.CenterScreen;

                settings.ShowDialog();
                if (settings.DialogResult.HasValue && !settings.DialogResult.Value)
                {
                    if (!force)
                    {
                        Environment.Exit(0);
                    }
                }
                else
                {
                    language = settings.langAbr;
                    fullscreen = settings.Fullscreen;

                    if (force)
                    {
                        MessageBox.Show(Properties.Resources.langChangedInfo, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }


            var culture = new CultureInfo(language);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            if (fullscreen)
            {
                this.WindowState = WindowState.Maximized;
            }

            Dal.Instance.SaveSettingsToFile(language, fullscreen.ToString());
        }


        // background workers
        private void bwGetData_DoWork(object sender, DoWorkEventArgs e)
        {
            Dal.Instance.GetResults();
            Dal.Instance.GetMatches();
            Dal.Instance.GetTeams();
            return;
        }
        private void bwGetData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ddlFavouriteTeam.Items.Clear();
            ddlFavouriteTeam.ItemsSource = Dal.Instance.GetTeams();
            ddlOppositionTeam.ItemsSource = opposingTeamsColletion;
            var data = Dal.Instance.ReadFavouriteTeamAndPlayersFromFile();

            if (data == null) return;

            if (data.ContainsKey("team") && data["team"] != null)
            {
                FavouriteTeam = Dal.Instance.GetTeamByFifaCode(GetFifaCodeFromFullName(data["team"].ToString()));
                ddlFavouriteTeam.SelectedItem = FavouriteTeam;

                opposingTeamsColletion = new ObservableCollection<Team>(Dal.Instance.GetOpponentsForTeam(FavouriteTeam));
                ddlOppositionTeam.ItemsSource = opposingTeamsColletion;
            }
            else
            {
                ddlFavouriteTeam.SelectedIndex = 0;
            }

            HideLoadingWindow();
        }

        private void bwLoading_DoWork(object sender, DoWorkEventArgs e)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                loadingWindow.ShowDialog();
                loadingWindow.BringIntoView();
            });
        }


        //combo boxes
        private void DdlFavouriteTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearStadiumPanels();
            teamDetails?.Close();
            playerDetails?.Close();

            FavouriteTeam = Dal.Instance.GetTeamByFifaCode(GetFifaCodeFromFullName(ddlFavouriteTeam.SelectedValue.ToString()));

            opposingTeamsColletion = new ObservableCollection<Team>(Dal.Instance.GetOpponentsForTeam(GetFifaCodeFromFullName(ddlFavouriteTeam.SelectedValue.ToString())));
            ddlOppositionTeam.ItemsSource = opposingTeamsColletion;
        }
        private void DdlOppositionTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            playerDetails?.Close();
            teamDetails?.Close();
            if (ddlOppositionTeam.SelectedValue == null)
            {
                return;
            }

            var team1 = FavouriteTeam;
            OpposingTeam = opposingTeamsColletion.First(x => x.FifaCode.Equals(GetFifaCodeFromFullName(ddlOppositionTeam.SelectedValue.ToString())));

            // podigni novi prozor - detalji tima
            //if (teamDetails == null || !teamDetails.IsVisible)
            {
                teamDetails = new TeamDetails();
                teamDetails.Show();

                Point position = ddlOppositionTeam.PointToScreen(new Point(0d, 0d));

                teamDetails.Left = position.X + ddlOppositionTeam.Width;
                teamDetails.Top = position.Y;
            }

            teamDetails.Statistics = Dal.Instance.GetResultsForTeam(OpposingTeam);


            //get players for teams, create player controls and place them on stadium
            CurrentMatch = Dal.Instance.GetMatchForTeams(team1, OpposingTeam);
            MatchingTeamStatistics team1stats, team2stats;
            if (CurrentMatch.HomeTeam.Equals(team1))
            {
                team1stats = CurrentMatch.HomeTeamStatistics;
                team2stats = CurrentMatch.AwayTeamStatistics;
            }
            else
            {
                team2stats = CurrentMatch.HomeTeamStatistics;
                team1stats = CurrentMatch.AwayTeamStatistics;
            }

            team1.Players = team1stats.StartingEleven;//.Union(team1stats.Substitutes);
            OpposingTeam.Players = team2stats.StartingEleven;//.Union(team2stats.Substitutes);

            var imagePaths = Dal.Instance.LoadImagesFromFile();

            ClearStadiumPanels();
            AddTeam1PlayersToStadium(team1, imagePaths, Color.FromRgb(0, 0, 255), Color.FromRgb(0, 255, 255));
            AddTeam2PlayersToStadium(OpposingTeam, imagePaths, Color.FromRgb(255, 0, 0), Color.FromRgb(255, 255, 0));

        }


        private void PlayerControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            playerDetails?.Close();
            playerDetails = new PlayerDetails();

            var playerControl = sender as PlayerControl;
            var player = FavouriteTeam.Players.Union(OpposingTeam.Players)?.First(x => x.Name.Equals(playerControl.PlayerName));
            if (player == null)
            {
                return;
            }

            var imagePaths = Dal.Instance.LoadImagesFromFile();

            player.YellowCards = Dal.Instance.GetPlayerYellowCardsForMatch(player, CurrentMatch);
            player.Goals = Dal.Instance.GetPlayerGoalsForMatch(player, CurrentMatch);

            Point position = ddlFavouriteTeam.PointToScreen(new Point(0d, 0d));
            playerDetails.Left = position.X - teamDetails.Width;
            playerDetails.Top = position.Y;
            //playerDetails.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            playerDetails.Statistics = player;

            if (imagePaths.ContainsKey(player.Name) && imagePaths[player.Name] != null)
            {
                playerDetails.ImagePath = imagePaths[player.Name];
            }
            playerDetails.Show();
        }
        private void AddTeam1PlayersToStadium(Team team1, IDictionary<string, string> imagePaths, Color statColor, Color dynColor)
        {
            foreach (var player in team1.Players)
            {

                StackPanel pnl = null;
                switch (player.Position)
                {
                    case Position.Goalie:
                        pnl = pnlTeam1Goalie;
                        break;
                    case Position.Defender:
                        pnl = pnlTeam1Defender;
                        break;
                    case Position.Midfield:
                        pnl = pnlTeam1Midfield;
                        break;
                    case Position.Forward:
                        pnl = pnlTeam1Forward;
                        break;
                }
                PlayerControl playerControl = imagePaths.ContainsKey(player.Name) && imagePaths[player.Name] != null ?
                    new PlayerControl(player.Name, player.ShirtNumber.ToString()) { ImagePath = imagePaths[player.Name], StaticColor = statColor, DynamicColor = dynColor, HorizontalAlignment = HorizontalAlignment.Right } :
                    new PlayerControl(player.Name, player.ShirtNumber.ToString()) { StaticColor = statColor, DynamicColor = dynColor, HorizontalAlignment = HorizontalAlignment.Right };
                playerControl.MouseLeftButtonUp += PlayerControl_MouseLeftButtonUp;

                pnl.Children.Add(playerControl);
            }
        }
        private void AddTeam2PlayersToStadium(Team team2, IDictionary<string, string> imagePaths, Color statColor, Color dynColor)
        {
            foreach (var player in team2.Players)
            {

                StackPanel pnl = null;
                switch (player.Position)
                {
                    case Position.Goalie:
                        pnl = pnlTeam2Goalie;
                        break;
                    case Position.Defender:
                        pnl = pnlTeam2Defender;
                        break;
                    case Position.Midfield:
                        pnl = pnlTeam2Midfield;
                        break;
                    case Position.Forward:
                        pnl = pnlTeam2Forward;
                        break;
                }
                PlayerControl playerControl = imagePaths.ContainsKey(player.Name) && imagePaths[player.Name] != null ?
                    new PlayerControl(player.Name, player.ShirtNumber.ToString()) { ImagePath = imagePaths[player.Name], StaticColor = statColor, DynamicColor = dynColor, HorizontalAlignment = HorizontalAlignment.Left } :
                    new PlayerControl(player.Name, player.ShirtNumber.ToString()) { StaticColor = statColor, DynamicColor = dynColor, HorizontalAlignment = HorizontalAlignment.Left };
                playerControl.MouseLeftButtonUp += PlayerControl_MouseLeftButtonUp;

                pnl.Children.Add(playerControl);
            }
        }
        private void ClearStadiumPanels()
        {
            pnlTeam1Goalie.Children.Clear();
            pnlTeam1Defender.Children.Clear();
            pnlTeam1Midfield.Children.Clear();
            pnlTeam1Forward.Children.Clear();
            pnlTeam2Goalie.Children.Clear();
            pnlTeam2Defender.Children.Clear();
            pnlTeam2Midfield.Children.Clear();
            pnlTeam2Forward.Children.Clear();
        }


        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            LoadSettings(true);
        }
        private void Win1_Closing(object sender, CancelEventArgs e)
        {
            var confirmDialog = new ConfirmDialog();
            confirmDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            confirmDialog.ShowDialog();
            confirmDialog.BringIntoView();
            if (confirmDialog.DialogResult.HasValue && !confirmDialog.DialogResult.Value)
            {
                e.Cancel = true;
                return;
            }

            loadingWindow?.Close();
            teamDetails?.Close();
            playerDetails?.Close();

            Environment.Exit(0);
        }


        private string GetFifaCodeFromFullName(string team)
        {
            return team.Substring(team.IndexOf('(') + 1, team.IndexOf(')') - team.IndexOf('(') - 1);
        }
    }
}
