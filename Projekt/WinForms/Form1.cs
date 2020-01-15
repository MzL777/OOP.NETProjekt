using DAL;
using DAL.Model;
using DAL.Model.Teams;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using WinForms.Forms;
using WinForms.Resources;
using WinForms.UserControls;

namespace WinForms
{
    public partial class Form1 : Form
    {
        static public LoadingForm loadingForm;

        private bool teamsLoaded = false;
        private bool matchesLoaded = false;

        private Control Ctrl;
        private Bitmap MemoryImage;
        private Color LoadingFormData = Color.FromArgb(255, 0, 0);
        private List<Player> PlayersForSelectedTeam
        {
            get
            {
                //if (playersForSelectedTeam == null)
                //playersForSelectedTeam = Dal.Instance.GetPlayersForTeam(GetFifaCodeFromddlTeamsString(ddlTeams.Text)).ToList();
                //return playersForSelectedTeam;
                return Dal.Instance.GetPlayersForTeam(GetFifaCodeFromFullName(ddlTeams.SelectedItem.ToString()))?.ToList();
            }
        }
        private IList<PlayerUserControl> PUCs = new List<PlayerUserControl>();
        private IDictionary<string, string> imagePaths;

        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadLanguage();
            ShowLoadingForm();
            LoadTeams();
            ddlTeams.SelectedIndexChanged -= ddlTeams_SelectionIndexChanged;
            //lvRangList.MouseWheel += lvRangList_MouseWheel;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (new ClosingForm().ShowDialog() != DialogResult.OK)
            {
                e.Cancel = true;
            }
            else
            {
                var favourites = new string[]
                {
                    "team=", "p1=", "p2=", "p3="
                };
                favourites[0] += ddlTeams.SelectedItem.ToString();

                for (int i = 0; i < flpFavouritePlayers.Controls.Count; i++)
                {
                    favourites[i + 1] += (flpFavouritePlayers.Controls[i] as PlayerUserControl).Controls["lblName"].Text;
                }

                //favourites[1] += ddlTeams.SelectedItem.ToString();
                //favourites[2] += ddlTeams.SelectedItem.ToString();
                //favourites[3] += ddlTeams.SelectedItem.ToString();

                if (!Dal.Instance.SaveFavouritesToFile(favourites))
                {
                    MessageBox.Show("Nije moguće spremiti omiljeni tim i igrače u datoteku!", "Pogreška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (!Dal.Instance.SaveImagesToFile(imagePaths))
                {
                    MessageBox.Show("Nije moguće spremiti lokacije slika igrača u datoteku!", "Pogreška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadFavourites()
        {
            var data = Dal.Instance.ReadFavouriteTeamAndPlayersFromFile();

            if (data == null) return;

            if (data.ContainsKey("team") && data["team"] != null)
            {
                // unsubscribe to event so it doesn't get called twice
                ddlTeams.SelectedIndexChanged -= ddlTeams_SelectionIndexChanged;

                var favouriteTeam = Dal.Instance.GetTeams().FirstOrDefault(x => x.ToString().Equals(data["team"]));
                if (favouriteTeam != null)
                {
                    ddlTeams.SelectedItem = favouriteTeam.ToString();
                }
                else
                {
                    ddlTeams.SelectedItem = Dal.Instance.GetTeams().First();
                }
            }
            else return;

            var fifaCode = GetFifaCodeFromFullName(data["team"]);
            if (fifaCode == null || fifaCode == string.Empty)
            {
                return;
            }
            var players = Dal.Instance.GetPlayersForTeam(fifaCode);

            if (players == null) return;
            players.ToList().ForEach(x => x.Favourite = false);

            if (data.ContainsKey("p1") && data["p1"] != null)
            {
                var p = players.FirstOrDefault(x => x.Name.Equals(data["p1"]));
                if (p != null)
                {
                    p.Favourite = true;
                }
            }
            if (data.ContainsKey("p2") && data["p2"] != null)
            {
                var p = players.FirstOrDefault(x => x.Name.Equals(data["p2"]));
                if (p != null)
                {
                    p.Favourite = true;
                }
            }
            if (data.ContainsKey("p3") && data["p3"] != null)
            {
                var p = players.FirstOrDefault(x => x.Name.Equals(data["p3"]));
                if (p != null)
                {
                    p.Favourite = true;
                }
            }

            // resubscribe to event
            ddlTeams.SelectedIndexChanged += ddlTeams_SelectionIndexChanged;
            ddlTeams_SelectionIndexChanged(this, new EventArgs());
        }
        private void LoadRangList()
        {
            lvRangList.Items.Clear();
            ImageList il = new ImageList
            {
                ImageSize = new Size(42, 42)
            };

            if (imagePaths == null)
            {
                imagePaths = Dal.Instance.LoadImagesFromFile();
            }

            try
            {
                foreach (var player in PlayersForSelectedTeam)
                {
                    if (imagePaths.ContainsKey(player.Name))
                    {
                        try
                        {
                            il.Images.Add(player.Name, Image.FromFile(imagePaths[player.Name]));
                            PUCs.FirstOrDefault(x => x.PlayerName == player.Name).pbPlayerPicture.ImageLocation = imagePaths[player.Name];
                        }
                        catch
                        {
                            il.Images.Add(player.Name, WinForms.Properties.Resources.person);

                        }
                    }
                    else
                    {
                        il.Images.Add(player.Name, WinForms.Properties.Resources.person);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            lvRangList.SmallImageList = il;

            foreach (var player in PlayersForSelectedTeam)
            {
                lvRangList.Items.Add(new ListViewItem(new string[] { string.Empty, player.ShirtNumber.ToString(), player.Name, player.Position.ToString(), player.Goals.ToString(), player.YellowCards.ToString(), player.Captain.ToString() }, player.Name));
            }
        }
        private void LoadLanguage()
        {
            var language = Dal.Instance.ReadLanguageFromFile();
            if (language != null && language != string.Empty)
            {
                SetCulture(language);
            }
            else
            {
                ShowLanguageForm();
            }
        }
        private void LoadTeams()
        {
            bwGetTeams.RunWorkerAsync();
        }

        private void ShowLoadingForm()
        {
            if (!bwLoading.IsBusy)
            {
                bwLoading.RunWorkerAsync();
            }
            else
            {
                // cross-thread control/form property manipulation - .BeginInvoke
                loadingForm.BeginInvoke((Action)(() => loadingForm.Visible = true));
            }
        }
        private void HideLoadingForm()
        {
            if (bwLoading.IsBusy)
            {
                // cross-thread control/form property manipulation - .BeginInvoke
                loadingForm.BeginInvoke((Action)(() => loadingForm.Visible = false));
            }
        }
        private void ShowLanguageForm()
        {
            var languageForm = new SettingsForm();
            if (languageForm.ShowDialog() == DialogResult.OK)
            {
                SetCulture(languageForm.Language);
                Dal.Instance.SaveLanguageToFile(GetLanguageAbbreviation(Thread.CurrentThread.CurrentCulture.ToString()));
            }
            else
            {
                Environment.Exit(0);
            }
        }


        // main form
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabPageMatches || tabControl.SelectedTab == tabPagePlayersRangList)
            {
                btnPrint.Enabled = true;
            }
            else
            {
                btnPrint.Enabled = false;
            }
        }

        private void ddlTeams_SelectionIndexChanged(object sender, EventArgs e)
        {
            ShowLoadingForm();
            GetPlayerUserControls();
            GetMatchUserControls();
            LoadRangList();
            HideLoadingForm();
        }


        // favourite/other players tab
        private void flpOtherPlayers_DragDrop(object sender, DragEventArgs e)
        {
            var draggedPUC = e.Data.GetData(typeof(PlayerUserControl)) as PlayerUserControl;

            if (!draggedPUC.Favourite)
            {
                Puc_MouseUp(draggedPUC, new MouseEventArgs(MouseButtons.Left, 1, Control.MousePosition.X, Control.MousePosition.Y, 0));
                return;
            }

            var selectedFavPUCs = PUCs.Where(x => (x.Selected_ && x.Favourite) || x == draggedPUC).ToList();

            selectedFavPUCs.ForEach(x => x.Favourite = x.Selected_ = false);

            flpOtherPlayers.Controls.AddRange(selectedFavPUCs.ToArray());
        }
        private void flpOtherPlayers_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }


        private void flpFavouritePlayers_DragDrop(object sender, DragEventArgs e)
        {
            if (PlayerUserControl.NumberOfFavourites >= PlayerUserControl.MAX_FAVOURITES) return;

            var draggedPUC = e.Data.GetData(typeof(PlayerUserControl)) as PlayerUserControl;

            if (draggedPUC.Favourite)
            {
                Puc_MouseUp(draggedPUC, new MouseEventArgs(MouseButtons.Left, 1, Control.MousePosition.X, Control.MousePosition.Y, 0));
                return;
            }
            var selectedOtherPUCs = PUCs.Where(x => x.Selected_ && !x.Favourite).ToList();

            draggedPUC.Favourite = true;
            draggedPUC.Selected_ = false;

            flpFavouritePlayers.Controls.Add(draggedPUC);

            selectedOtherPUCs.ForEach(x =>
            {
                x.Favourite = true;
                x.Selected_ = false;
            });

            //draggedPUC.Favourite = true;//ForEach(x => x.Favourite = true);

            flpFavouritePlayers.Controls.AddRange(selectedOtherPUCs.Where(x => x.Favourite).ToArray());

        }
        private void flpFavouritePlayers_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }


        private void GetPlayerUserControls()
        {
            CleanupPlayerUserControls();

            PlayerUserControl.NumberOfFavourites = 0;

            if (PlayersForSelectedTeam == null) return;

            // create new player user controls
            for (int i = 0; i < PlayersForSelectedTeam.Count; i++)
            {
                var player = PlayersForSelectedTeam[i];
                var puc = new PlayerUserControl(ref player);

                if (PlayersForSelectedTeam[i].Favourite)
                {
                    PlayerUserControl.NumberOfFavourites++;
                    flpFavouritePlayers.Controls.Add(puc);
                }
                else
                {
                    flpOtherPlayers.Controls.Add(puc);
                }

                // add event handlers
                puc.MouseDown += Puc_MouseDown;
                puc.MouseUp += Puc_MouseUp;
                puc.tsmiAddToFavourites.Click += TsmiAddToFavourites_Click;
                puc.tsmiSelect.Click += TsmiSelect_Click;
                puc.tsmiAddPicture.Click += TsmiAddPicture_Click;
                puc.imgFavourite.MouseClick += ImgFavourite_MouseClick;
                puc.pbPlayerPicture.MouseClick += PbPlayerPicture_MouseClick;

                foreach (Control control in puc.Controls)
                {
                    control.MouseDown += Puc_MouseDown;
                    control.MouseUp += Puc_MouseUp;
                }

                puc.imgFavourite.MouseDown -= Puc_MouseDown;
                puc.imgFavourite.MouseUp -= Puc_MouseUp;

                puc.pbPlayerPicture.MouseDown -= Puc_MouseDown;
                puc.pbPlayerPicture.MouseUp -= Puc_MouseUp;

                PUCs.Add(puc);
            }
        }

        private void CleanupPlayerUserControls()
        {
            // cleanup loaded player user controls
            PUCs.ToList().ForEach(x =>
            {
                x.MouseDown -= Puc_MouseDown;
                x.MouseUp -= Puc_MouseUp;
                //puc.QueryContinueDrag += Puc_QueryContinueDrag;
                x.tsmiAddToFavourites.Click -= TsmiAddToFavourites_Click;
                x.tsmiSelect.Click -= TsmiSelect_Click;
                x.imgFavourite.MouseClick -= ImgFavourite_MouseClick;
            });
            PUCs.Clear();
            flpFavouritePlayers.Controls.Clear();
            flpOtherPlayers.Controls.Clear();
        }
        private void PbPlayerPicture_MouseClick(object sender, MouseEventArgs e)
        {
            var ofd = new OpenFileDialog()
            {
                Filter = Resources._Resources.Images + "|*.jpg;*.jpeg;*.png;*.bmp;*.gif;"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (sender is PlayerUserControl)
                {
                    ShowImageInForm(sender as PlayerUserControl, ofd.FileName.Replace("\\", "/"));
                }
                else
                {
                    ShowImageInForm((sender as Control).Parent as PlayerUserControl, ofd.FileName.Replace("\\", "/"));
                }
            }
        }
        private void ShowImageInForm(PlayerUserControl sender, string fileName)
        {
            //sender.pbPlayerPicture.ImageLocation = fileName;
            imagePaths[sender.PlayerName] = fileName;
            //imagePaths.Add(sender.PlayerName, fileName);
            //lvRangList.SmallImageList.Images.Add(sender.PlayerName, Image.FromFile(fileName));
            LoadRangList();
        }


        private void TsmiAddPicture_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                var menuStrip = menuItem.Owner as ContextMenuStrip;
                if (menuStrip != null)
                {
                    var source = menuStrip.SourceControl as PlayerUserControl;
                    if (source != null)
                    {
                        PbPlayerPicture_MouseClick(source, null);
                    }
                }
            }
        }
        private void TsmiSelect_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                var menuStrip = menuItem.Owner as ContextMenuStrip;
                if (menuStrip != null)
                {
                    var source = menuStrip.SourceControl as PlayerUserControl;
                    if (source != null)
                    {
                        if (Form.ModifierKeys != Keys.Control)
                        {
                            if (source.Selected_)
                            {
                                source.Selected_ = false;
                            }
                            else
                            {
                                // deselect all selected items
                                PUCs.Where(x => x.Selected_).ToList().ForEach(x => x.Selected_ = false);
                                source.Selected_ = true;
                            }
                        }
                        else
                        {
                            source.Selected_ = !source.Selected_;
                        }
                    }
                }
            }
        }
        private void TsmiAddToFavourites_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                var menuStrip = menuItem.Owner as ContextMenuStrip;
                if (menuStrip != null)
                {
                    var source = menuStrip.SourceControl as PlayerUserControl;
                    if (source != null)
                    {
                        source.Favourite = !source.Favourite;

                        AddPlayerUserControlToCorrespondingPanel(source);
                    }
                }
            }
        }
        private void ImgFavourite_MouseClick(object sender, MouseEventArgs e)
        {
            var puc = (sender as Control).Parent as PlayerUserControl;
            puc.Favourite = !puc.Favourite;
            puc.Selected_ = false;
            AddPlayerUserControlToCorrespondingPanel(puc);
        }
        private void AddPlayerUserControlToCorrespondingPanel(PlayerUserControl puc)
        {
            if (puc.Favourite)
            {
                if (!flpFavouritePlayers.Controls.Contains(puc))
                {
                    flpFavouritePlayers.Controls.Add(puc);
                }
            }
            else
            {
                if (!flpOtherPlayers.Controls.Contains(puc))
                {
                    flpOtherPlayers.Controls.Add(puc);
                }
            }

            //flpFavouritePlayers.Controls.AddRange(selectedOtherPUCs.Where(x => x.Favourite).ToArray());
        }
        private void Puc_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            PlayerUserControl puc;
            if (sender is PlayerUserControl)
            {
                puc = sender as PlayerUserControl;
            }
            else
            {
                puc = (sender as Control).Parent as PlayerUserControl;
            }

            bool value = puc.Selected_;


            if (Form.ModifierKeys != Keys.Control)
            {
                // deselect all selected items
                PUCs.Where(x => x.Selected_).ToList().ForEach(x => x.Selected_ = false);
            }

            puc.Selected_ = !value;
        }
        private void Puc_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            PlayerUserControl puc;
            if (sender is PlayerUserControl)
            {
                puc = sender as PlayerUserControl;
            }
            else
            {
                puc = (sender as Control).Parent as PlayerUserControl;
            }

            DoDragDrop(puc, DragDropEffects.All);
        }


        // matches tab
        private void GetMatchUserControls()
        {
            if (!matchesLoaded) return;

            flpMatches.Controls.Clear();

            Dal.Instance.GetMatches();
            var matches = Dal.Instance.GetMatchesForTeam(GetFifaCodeFromFullName(ddlTeams.SelectedItem.ToString())).ToList();
            matches.Sort(CustomComparer.CompareMatchByAttendanceDesc);
            var matchUserControls = new List<MatchUserControl>();
            matches.ForEach(x => matchUserControls.Add(new MatchUserControl(ref x)));

            flpMatches.Controls.AddRange(matchUserControls.ToArray());
        }


        // player rang list tab
        private void lvRangList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.lvRangList.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }
        private void lvRangList_MouseWheel(object sender, MouseEventArgs e)
        {
            tabPagePlayersRangList.AutoScrollPosition = new Point(-tabPagePlayersRangList.AutoScrollPosition.X, -tabPagePlayersRangList.AutoScrollPosition.Y - e.Delta);
        }


        // background workers
        private void bwGetTeams_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = Dal.Instance.GetTeams();
        }
        private void bwGetTeams_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ddlTeams.DataSource = (e.Result as IEnumerable<Team>).Select(x => x.ToString()).ToList();
            teamsLoaded = true;

            bwGetMatches.RunWorkerAsync(GetFifaCodeFromFullName(ddlTeams.Text));
        }

        private void bwLoading_DoWork(object sender, DoWorkEventArgs e)
        {
            Application.Run(new LoadingForm(LoadingFormData));
        }

        private void bwGetMatches_DoWork(object sender, DoWorkEventArgs e)
        {
            //Dal.Instance.GetMatches();
            var matches = Dal.Instance.GetMatchesForTeam((string)e.Argument).ToList();
            matches.Sort(CustomComparer.CompareMatchByAttendanceDesc);
            var matchUserControls = new List<MatchUserControl>();
            matches.ForEach(x => matchUserControls.Add(new MatchUserControl(ref x)));
            e.Result = matchUserControls;
        }
        private void bwGetMatches_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //matches
            flpMatches.Controls.Clear();
            flpMatches.Controls.AddRange((e.Result as List<MatchUserControl>).ToArray());
            matchesLoaded = true;

            ddlTeams.SelectedIndexChanged += ddlTeams_SelectionIndexChanged;
            LoadFavourites();
            GetMatchUserControls();
            LoadRangList();
        }


        //helper functions
        private string GetFifaCodeFromFullName(string team)
        {
            if (team.IndexOf('(') != -1 && team.IndexOf(')') != -1)
            {
                return team.Substring(team.IndexOf('(') + 1, team.IndexOf(')') - team.IndexOf('(') - 1);
            }
            return Dal.Instance.GetTeams().First().FifaCode;
        }
        private string GetLanguageAbbreviation(string language)
        {
            if (string.IsNullOrEmpty(language))
            {
                return string.Empty;
            }
            if (language?.Length > 2)
            {
                language = language.Substring(0, 2);
            }
            return language.ToLower();
        }
        private void SetCulture(string culture)
        {
            culture = GetLanguageAbbreviation(culture);

            if (culture != string.Empty && culture != Thread.CurrentThread.CurrentCulture.Name)
            {
                //globalizacija - time date
                Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
                //lokalizacija - labels
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            }
            this.Controls.Clear();

            // unsubscribe or events will be called twice
            this.FormClosing -= Form1_FormClosing;
            this.Load -= Form1_Load;
            this.InitializeComponent();

            if (teamsLoaded)
            {
                bwGetTeams_RunWorkerCompleted(this, new RunWorkerCompletedEventArgs(Dal.Instance.GetTeams(), null, false));
            }
            lblCulture.Text = culture;
        }
        private void btnChangeLanuage_Click(object sender, EventArgs e)
        {
            ShowLanguageForm();
        }


        // printing
        private void btnPrint_Click(object sender, EventArgs e)
        {
            tabPagePlayersRangList.AutoScrollPosition = new Point(-tabPagePlayersRangList.AutoScrollPosition.X, 0);

            if (tabControl.SelectedTab == tabPagePlayersRangList)
            {
                Print(lvRangList);
            }
            else if (tabControl.SelectedTab == tabPageMatches)
            {
                Print(flpMatches);
            }
            else return;
        }
        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(MemoryImage, (e.PageBounds.Width / 2) - (Ctrl.Width / 2), Ctrl.Location.Y);
        }
        private void printDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (e.PrintAction != PrintAction.PrintToPreview)
            {
                MessageBox.Show(_Resources.PrintFinished);
            }
        }


        public void Print(Control ctrl)
        {
            Ctrl = ctrl;
            GetPrintArea(ctrl);
            printDocument.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
            printPreviewDialog.Document = printDocument;
            printPreviewDialog.ShowDialog();
        }
        public void GetPrintArea(Control pnl)
        {
            MemoryImage = new Bitmap(pnl.Width, pnl.Height);
            pnl.DrawToBitmap(MemoryImage, new Rectangle(0, 0, pnl.Width, pnl.Height));
        }


    }
}
