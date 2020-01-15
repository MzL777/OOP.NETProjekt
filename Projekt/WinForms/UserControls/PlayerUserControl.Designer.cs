namespace WinForms.UserControls
{
    partial class PlayerUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerUserControl));
            this.label2 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPosition = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblShirtNumber = new System.Windows.Forms.Label();
            this.imgFavourite = new System.Windows.Forms.PictureBox();
            this.pbPlayerPicture = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddToFavourites = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddPicture = new System.Windows.Forms.ToolStripMenuItem();
            this.lblCaptain = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgFavourite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlayerPicture)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // lblPosition
            // 
            resources.ApplyResources(this.lblPosition, "lblPosition");
            this.lblPosition.Name = "lblPosition";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // lblShirtNumber
            // 
            resources.ApplyResources(this.lblShirtNumber, "lblShirtNumber");
            this.lblShirtNumber.Name = "lblShirtNumber";
            // 
            // imgFavourite
            // 
            this.imgFavourite.Image = global::WinForms.Properties.Resources.not_favourite;
            resources.ApplyResources(this.imgFavourite, "imgFavourite");
            this.imgFavourite.Name = "imgFavourite";
            this.imgFavourite.TabStop = false;
            // 
            // pbPlayerPicture
            // 
            resources.ApplyResources(this.pbPlayerPicture, "pbPlayerPicture");
            this.pbPlayerPicture.Image = global::WinForms.Properties.Resources.person;
            this.pbPlayerPicture.Name = "pbPlayerPicture";
            this.pbPlayerPicture.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSelect,
            this.tsmiAddToFavourites,
            this.tsmiAddPicture});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // tsmiSelect
            // 
            this.tsmiSelect.Name = "tsmiSelect";
            resources.ApplyResources(this.tsmiSelect, "tsmiSelect");
            // 
            // tsmiAddToFavourites
            // 
            this.tsmiAddToFavourites.Name = "tsmiAddToFavourites";
            resources.ApplyResources(this.tsmiAddToFavourites, "tsmiAddToFavourites");
            // 
            // tsmiAddPicture
            // 
            this.tsmiAddPicture.Name = "tsmiAddPicture";
            resources.ApplyResources(this.tsmiAddPicture, "tsmiAddPicture");
            // 
            // lblCaptain
            // 
            resources.ApplyResources(this.lblCaptain, "lblCaptain");
            this.lblCaptain.ForeColor = System.Drawing.Color.Red;
            this.lblCaptain.Name = "lblCaptain";
            // 
            // PlayerUserControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.lblCaptain);
            this.Controls.Add(this.imgFavourite);
            this.Controls.Add(this.lblShirtNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblPosition);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pbPlayerPicture);
            this.Name = "PlayerUserControl";
            ((System.ComponentModel.ISupportInitialize)(this.imgFavourite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlayerPicture)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblShirtNumber;
        internal System.Windows.Forms.PictureBox imgFavourite;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label lblCaptain;
        public System.Windows.Forms.ToolStripMenuItem tsmiSelect;
        public System.Windows.Forms.ToolStripMenuItem tsmiAddToFavourites;
        public System.Windows.Forms.PictureBox pbPlayerPicture;
        public System.Windows.Forms.ToolStripMenuItem tsmiAddPicture;
    }
}
