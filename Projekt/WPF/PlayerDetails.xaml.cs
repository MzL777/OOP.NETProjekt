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
using DAL.Model;

namespace WPF
{
    /// <summary>
    /// Interaction logic for PlayerDetails.xaml
    /// </summary>
    public partial class PlayerDetails : Window
    {
        private Player statistics;
        public Player Statistics
        {
            get => statistics;
            set
            {
                statistics = value;
                LoadStatistics();
            }
        }
        public string ImagePath
        {
            set
            {
                try
                {
                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.UriSource = new Uri(value);
                    bmp.EndInit();
                    imgPerson.Source = bmp;
                }
                catch { }
            }
        }

        private void LoadStatistics()
        {
            lblName.Content = statistics.Name;
            lblNumber.Content = statistics.ShirtNumber;
            lblPosition.Content = statistics.Position;
            lblCaptain.Content = statistics.Captain.Value ? Properties.Resources.yes : Properties.Resources.no;
            lblGoals.Content = statistics.Goals;
            lblYellowCards.Content = statistics.YellowCards;
            ImagePath = "./images/person.png";
        }

        public PlayerDetails()
        {
            InitializeComponent();
        }

    }
}
