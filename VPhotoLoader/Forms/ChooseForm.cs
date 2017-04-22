using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VPhotoLoader.Api;
using System.Collections;
using VPhotoLoader.Core;

namespace VPhotoLoader.Forms
{
    public partial class ChooseForm : Form
    {
        private ICheckable[] _items;

        public ChooseForm(ICheckable[] items)
        {
            InitializeComponent();
            _items = items;

            foreach (var item in _items)
            {
                sources.Items.Add(item.ToString(), item.Check);
            }

            this.Text = string.Format("{0} ({1})", this.Text, _items.Count());
        }

        private void HotKeys(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.btnOk_Click(this, EventArgs.Empty);
            else if (e.KeyCode == Keys.Escape)
                this.btnCancel_Click(this, EventArgs.Empty);
            else if (e.Control == true && e.KeyCode == Keys.A)
            {
                for (int i = 0; i < sources.Items.Count; i++)
                {
                    sources.SetItemChecked(i, true);
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                _items[i].Check = sources.GetItemChecked(i);
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void sources_MouseEnter(object sender, EventArgs e)
        {
            sources.Focus();
        }

        private void sources_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && sources.Items.Count > 0)
            {
                srcMenu.Show(sources, e.Location);
            }
        }

        private void menuSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < sources.Items.Count; i++)
            {
                sources.SetItemChecked(i, true);
            }
        }

        private void menuUnselectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < sources.Items.Count; i++)
            {
                sources.SetItemChecked(i, false);
            }
        }
    }
}
