using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms.Forms
{
    public partial class ClosingForm : Form
    {
        public ClosingForm()
        {
            InitializeComponent();
            CenterToParent();
            BringToFront();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            // Return Cancel when pressed Escape key or OK when pressed Enter key
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return true;
            }
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

    }
}
