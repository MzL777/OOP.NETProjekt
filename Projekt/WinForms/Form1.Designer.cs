namespace WinForms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.ddlTeams = new System.Windows.Forms.ComboBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPagePlayers = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.flpOtherPlayers = new System.Windows.Forms.FlowLayoutPanel();
            this.flpFavouritePlayers = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPageMatches = new System.Windows.Forms.TabPage();
            this.flpMatches = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPagePlayersRangList = new System.Windows.Forms.TabPage();
            this.lvRangList = new System.Windows.Forms.ListView();
            this.lvColumnImage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvColumnShirtNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvColumnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvColumnPosition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvColumnGoalsScored = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvColumnYellowCards = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvColumnCaptain = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnChangeLanuage = new System.Windows.Forms.Button();
            this.lblCulture = new System.Windows.Forms.Label();
            this.bwGetMatches = new System.ComponentModel.BackgroundWorker();
            this.bwGetTeams = new System.ComponentModel.BackgroundWorker();
            this.btnPrint = new System.Windows.Forms.Button();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.bwLoading = new System.ComponentModel.BackgroundWorker();
            this.tabControl.SuspendLayout();
            this.tabPagePlayers.SuspendLayout();
            this.tabPageMatches.SuspendLayout();
            this.tabPagePlayersRangList.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // ddlTeams
            // 
            this.ddlTeams.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.ddlTeams.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlTeams.FormattingEnabled = true;
            resources.ApplyResources(this.ddlTeams, "ddlTeams");
            this.ddlTeams.Name = "ddlTeams";
            this.ddlTeams.SelectedIndexChanged += new System.EventHandler(this.ddlTeams_SelectionIndexChanged);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPagePlayers);
            this.tabControl.Controls.Add(this.tabPageMatches);
            this.tabControl.Controls.Add(this.tabPagePlayersRangList);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPagePlayers
            // 
            this.tabPagePlayers.Controls.Add(this.label3);
            this.tabPagePlayers.Controls.Add(this.label2);
            this.tabPagePlayers.Controls.Add(this.flpOtherPlayers);
            this.tabPagePlayers.Controls.Add(this.flpFavouritePlayers);
            resources.ApplyResources(this.tabPagePlayers, "tabPagePlayers");
            this.tabPagePlayers.Name = "tabPagePlayers";
            this.tabPagePlayers.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // flpOtherPlayers
            // 
            this.flpOtherPlayers.AllowDrop = true;
            resources.ApplyResources(this.flpOtherPlayers, "flpOtherPlayers");
            this.flpOtherPlayers.Name = "flpOtherPlayers";
            this.flpOtherPlayers.DragDrop += new System.Windows.Forms.DragEventHandler(this.flpOtherPlayers_DragDrop);
            this.flpOtherPlayers.DragEnter += new System.Windows.Forms.DragEventHandler(this.flpOtherPlayers_DragEnter);
            // 
            // flpFavouritePlayers
            // 
            this.flpFavouritePlayers.AllowDrop = true;
            resources.ApplyResources(this.flpFavouritePlayers, "flpFavouritePlayers");
            this.flpFavouritePlayers.Name = "flpFavouritePlayers";
            this.flpFavouritePlayers.DragDrop += new System.Windows.Forms.DragEventHandler(this.flpFavouritePlayers_DragDrop);
            this.flpFavouritePlayers.DragEnter += new System.Windows.Forms.DragEventHandler(this.flpFavouritePlayers_DragEnter);
            // 
            // tabPageMatches
            // 
            this.tabPageMatches.Controls.Add(this.flpMatches);
            resources.ApplyResources(this.tabPageMatches, "tabPageMatches");
            this.tabPageMatches.Name = "tabPageMatches";
            this.tabPageMatches.UseVisualStyleBackColor = true;
            // 
            // flpMatches
            // 
            resources.ApplyResources(this.flpMatches, "flpMatches");
            this.flpMatches.Name = "flpMatches";
            // 
            // tabPagePlayersRangList
            // 
            resources.ApplyResources(this.tabPagePlayersRangList, "tabPagePlayersRangList");
            this.tabPagePlayersRangList.Controls.Add(this.lvRangList);
            this.tabPagePlayersRangList.Name = "tabPagePlayersRangList";
            this.tabPagePlayersRangList.UseVisualStyleBackColor = true;
            // 
            // lvRangList
            // 
            this.lvRangList.AllowColumnReorder = true;
            this.lvRangList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvColumnImage,
            this.lvColumnShirtNumber,
            this.lvColumnName,
            this.lvColumnPosition,
            this.lvColumnGoalsScored,
            this.lvColumnYellowCards,
            this.lvColumnCaptain});
            resources.ApplyResources(this.lvRangList, "lvRangList");
            this.lvRangList.Name = "lvRangList";
            this.lvRangList.UseCompatibleStateImageBehavior = false;
            this.lvRangList.View = System.Windows.Forms.View.Details;
            this.lvRangList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvRangList_ColumnClick);
            this.lvRangList.MouseWheel += lvRangList_MouseWheel;
            // 
            // lvColumnImage
            // 
            resources.ApplyResources(this.lvColumnImage, "lvColumnImage");
            // 
            // lvColumnShirtNumber
            // 
            resources.ApplyResources(this.lvColumnShirtNumber, "lvColumnShirtNumber");
            // 
            // lvColumnName
            // 
            resources.ApplyResources(this.lvColumnName, "lvColumnName");
            // 
            // lvColumnPosition
            // 
            resources.ApplyResources(this.lvColumnPosition, "lvColumnPosition");
            // 
            // lvColumnGoalsScored
            // 
            resources.ApplyResources(this.lvColumnGoalsScored, "lvColumnGoalsScored");
            // 
            // lvColumnYellowCards
            // 
            resources.ApplyResources(this.lvColumnYellowCards, "lvColumnYellowCards");
            // 
            // lvColumnCaptain
            // 
            resources.ApplyResources(this.lvColumnCaptain, "lvColumnCaptain");
            // 
            // btnChangeLanuage
            // 
            resources.ApplyResources(this.btnChangeLanuage, "btnChangeLanuage");
            this.btnChangeLanuage.Name = "btnChangeLanuage";
            this.btnChangeLanuage.UseVisualStyleBackColor = true;
            this.btnChangeLanuage.Click += new System.EventHandler(this.btnChangeLanuage_Click);
            // 
            // lblCulture
            // 
            resources.ApplyResources(this.lblCulture, "lblCulture");
            this.lblCulture.Name = "lblCulture";
            // 
            // bwGetMatches
            // 
            this.bwGetMatches.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwGetMatches_DoWork);
            this.bwGetMatches.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwGetMatches_RunWorkerCompleted);
            // 
            // bwGetTeams
            // 
            this.bwGetTeams.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwGetTeams_DoWork);
            this.bwGetTeams.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwGetTeams_RunWorkerCompleted);
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // printPreviewDialog
            // 
            resources.ApplyResources(this.printPreviewDialog, "printPreviewDialog");
            this.printPreviewDialog.Document = this.printDocument;
            this.printPreviewDialog.Name = "printPreviewDialog";
            // 
            // printDocument
            // 
            this.printDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument_EndPrint);
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);
            // 
            // bwLoading
            // 
            this.bwLoading.WorkerSupportsCancellation = true;
            this.bwLoading.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoading_DoWork);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lblCulture);
            this.Controls.Add(this.btnChangeLanuage);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.ddlTeams);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPagePlayers.ResumeLayout(false);
            this.tabPagePlayers.PerformLayout();
            this.tabPageMatches.ResumeLayout(false);
            this.tabPagePlayersRangList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddlTeams;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPagePlayers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flpOtherPlayers;
        private System.Windows.Forms.FlowLayoutPanel flpFavouritePlayers;
        private System.Windows.Forms.TabPage tabPageMatches;
        private System.Windows.Forms.FlowLayoutPanel flpMatches;
        private System.Windows.Forms.Button btnChangeLanuage;
        private System.Windows.Forms.Label lblCulture;
        private System.ComponentModel.BackgroundWorker bwGetMatches;
        private System.ComponentModel.BackgroundWorker bwGetTeams;
        private System.Windows.Forms.TabPage tabPagePlayersRangList;
        private System.Windows.Forms.ListView lvRangList;
        private System.Windows.Forms.ColumnHeader lvColumnShirtNumber;
        private System.Windows.Forms.ColumnHeader lvColumnName;
        private System.Windows.Forms.ColumnHeader lvColumnPosition;
        private System.Windows.Forms.ColumnHeader lvColumnGoalsScored;
        private System.Windows.Forms.ColumnHeader lvColumnYellowCards;
        private System.Windows.Forms.ColumnHeader lvColumnCaptain;
        private System.Windows.Forms.ColumnHeader lvColumnImage;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
        private System.Drawing.Printing.PrintDocument printDocument;
        private System.ComponentModel.BackgroundWorker bwLoading;
    }
}

