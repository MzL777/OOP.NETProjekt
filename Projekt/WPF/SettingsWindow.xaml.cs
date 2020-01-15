using DAL;
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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public bool Fullscreen { get; set; } = false;
        public string langAbr { get; set; }

        public SettingsWindow(bool flscrn = false, string lang = "")
        {
            InitializeComponent();

            Fullscreen = flscrn;
            langAbr = lang;

            if (Fullscreen)
            {
                rb1.IsChecked = true;
                rb2.IsChecked = false;
            }
            else
            {
                rb1.IsChecked = false;
                rb2.IsChecked = true;
            }
            var langs = Dal.Instance.GetLanguages();

            ddlLanguage.ItemsSource = langs;

            if (langAbr != null && langAbr.Trim() != string.Empty)
            {
                ddlLanguage.SelectedItem = langs.First(x => x.ToString().Substring(0, 2).ToLower().Equals(langAbr));
            }
            else
            {
                ddlLanguage.SelectedIndex = 0;
            }

        }

        private void Rb1_Checked(object sender, RoutedEventArgs e)
        {
            Fullscreen = true;
        }

        private void Rb2_Checked(object sender, RoutedEventArgs e)
        {
            Fullscreen = false;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            langAbr = ddlLanguage.SelectedValue.ToString().Substring(0, 2).ToLower();
            DialogResult = true;
        }
    }
}
