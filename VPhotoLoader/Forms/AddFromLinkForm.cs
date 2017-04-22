using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace VPhotoLoader.Forms
{
    public partial class AddFromLinkForm : Form
    {
        public string Result { get; private set; }
        public AddFromLinkForm()
        {
            InitializeComponent();
        }
        
        private void btnOk_Click(object sender, EventArgs e)
        {
            Result = tbLink.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HotKeys(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.btnOk_Click(this, EventArgs.Empty);
            else if (e.KeyCode == Keys.Escape)
                this.btnCancel_Click(this, EventArgs.Empty);
            else if (e.Control == true && e.KeyCode == Keys.A)
            {
                tbLink.SelectAll();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }
    }
}
