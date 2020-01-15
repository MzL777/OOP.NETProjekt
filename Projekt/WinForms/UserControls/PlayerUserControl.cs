using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.Model;

namespace WinForms.UserControls
{
    public partial class PlayerUserControl : UserControl
    {
        private Player player;
        private bool selected_ = false;

        public static readonly int MAX_FAVOURITES = 3;
        public static int NumberOfFavourites { get; set; } = 0;

        public string PlayerName { get => player.Name; }

        public bool Favourite
        {
            get => player.Favourite;
            set
            {
                if (value == Favourite) return;

                if (value)
                {
                    if (NumberOfFavourites < MAX_FAVOURITES)
                    {
                        NumberOfFavourites++;
                        player.Favourite = true;
                        tsmiAddToFavourites.Text = Resources._Resources.RemoveFromFavourites;
                    }
                    else return;
                }
                else
                {
                    NumberOfFavourites--;
                    player.Favourite = false;
                    tsmiAddToFavourites.Text = Resources._Resources.AddToFavourites;
                }

                UpdateFavouriteImage();
            }
        }
        public bool Selected_
        {
            get => selected_;
            set
            {
                if (selected_ == value) return;
                selected_ = value;

                if (selected_)
                {
                    this.BackColor = Color.LightGoldenrodYellow;
                    tsmiSelect.Text = Resources._Resources.Deselect;
                }
                else
                {
                    this.BackColor = default(Color);
                    tsmiSelect.Text = Resources._Resources.Select;
                }
            }
        }

        public PlayerUserControl()
        {
            InitializeComponent();
        }

        public PlayerUserControl(ref Player player)
        {
            InitializeComponent();
            this.player = player;
            LoadPlayerData();
        }

        private void UpdateFavouriteImage()
        {
            if (Favourite)
            {
                imgFavourite.Image = Properties.Resources.favourite;
            }
            else
            {
                imgFavourite.Image = Properties.Resources.not_favourite;
            }
        }

        private void LoadPlayerData()
        {
            lblName.Text = player.Name;
            lblPosition.Text = player.Position.ToString();
            lblShirtNumber.Text = player.ShirtNumber.ToString();

            if (player.Captain.HasValue && player.Captain.Value)
            {
                lblCaptain.Show();
            }

            if (Favourite)
            {
                imgFavourite.Image = Properties.Resources.favourite;
                tsmiAddToFavourites.Text = Resources._Resources.RemoveFromFavourites;
            }
            else
            {
                tsmiAddToFavourites.Text = Resources._Resources.AddToFavourites;
            }
        }
    }
}
