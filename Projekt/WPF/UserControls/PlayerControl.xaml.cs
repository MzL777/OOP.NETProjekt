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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF.UserControls
{
    /// <summary>
    /// Interaction logic for PlayerControl.xaml
    /// </summary>
    public partial class PlayerControl : UserControl
    {
        public string PlayerName { get; private set; }
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
        public Color StaticColor
        {
            set
            {
                statColor1.Color = value;
                statColor2.Color = value;
            }
        }
        public Color DynamicColor
        {
            set
            {
                dynColor1.Color = value;
                lblNumber.Foreground = new SolidColorBrush(value);
            }
        }

        public PlayerControl(string name, string number)
        {
            InitializeComponent();
            PlayerName = lblName.Text = name;
            lblNumber.Content = number;
        }
    }
}
