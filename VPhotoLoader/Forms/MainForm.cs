using System;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Linq;
using VPhotoLoader.Core;
using VPhotoLoader.VPL;

namespace VPhotoLoader.Forms
{
    public partial class MainForm : Form, IMainView
    {
        #region IMainView
        public event EventHandler LoginPressed;
        public event EventHandler LogoutPressed;
        public event EventHandler AddFromFriensPressed;
        public event EventHandler AddFromGroupsPressed;
        public event EventHandler<PathEventArgs> AddFromLinkPressed;
        public event EventHandler ClearSourcesPressed;
        public event EventHandler<IndexEventArgs> RemoveSourcePressed;
        public event EventHandler<IndexEventArgs> SelectAlbumsPressed;
        public event EventHandler CancelPressed;
        public event EventHandler GetImagesPressed;
        public event EventHandler LoadImagesPressed;
        public event EventHandler<PathEventArgs> ImportSourcesPressed;
        public event EventHandler<PathEventArgs> ExportSourcesPressed;
        public event EventHandler<CheckEventArgs> ItemCheckStateChanged;
        
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public string InfoLabel
        {
            set
            {
                if (value.Length > 30) value = value.Remove(30);
                this.Invoke(new Action<string>((txt) =>
                {
                    lblCurrentProgress.Text = txt;
                    lblImagesInfo.Text = txt;
                }), value);
            }
        }

        public int TaskProgress
        {
            set
            {
                progressBar1.Invoke(new Action<int>((v) =>
                {
                    progressBar1.Value = v;
                }), value);
            }
        }

        public string LoginStatus
        {
            set
            {
                if (this.InvokeRequired)
                {
                    lblAccauntInfo.Invoke(new Action<string>((v) =>
                    {
                        lblAccauntInfo.Text = v;
                    }), value);
                }
                else
                {
                    lblAccauntInfo.Text = value;
                }
            }
        }

        public ICheckable[] Sources
        {
            set
            {
                clbPages.Items.Clear();
                foreach (var item in value)
                {
                    clbPages.Items.Add(item.ToString(), item.Check);
                }
                this.Invalidate();
            }
        }

        #endregion

        public MainForm()
        {
            InitializeComponent();
            openFileDialog1.InitialDirectory = Application.StartupPath;
            saveFileDialog1.InitialDirectory = Application.StartupPath;
        }

        void clbPages_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (ItemCheckStateChanged != null) ItemCheckStateChanged(this, new CheckEventArgs(e.Index, e.NewValue != CheckState.Unchecked));
        }

        #region CheckedListBox

        private void sourceList_MouseEnter(object sender, EventArgs e)
        {
            clbPages.Focus();
        }

        private void sourceList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && clbPages.Items.Count > 0)
            {
                clbPages.SelectedIndex = clbPages.IndexFromPoint(e.Location);
                if (clbPages.SelectedIndex == -1)
                {
                    srcMenu.Items[0].Visible = false;
                    srcMenu.Items[4].Visible = false;
                }
                else
                {
                    srcMenu.Items[0].Visible = true;
                    srcMenu.Items[4].Visible = true;
                }
                srcMenu.Show(clbPages, e.Location);
            }
        }

        #region ContextMenu

        private void srcMenuClr_Click(object sender, EventArgs e)
        {
            if (ClearSourcesPressed != null) ClearSourcesPressed(this, EventArgs.Empty);
        }

        private void srcMenuNone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbPages.Items.Count; i++)
            {
                clbPages.SetItemChecked(i, false);
            }
        }

        private void srcMenulAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbPages.Items.Count; i++)
            {
                clbPages.SetItemChecked(i, true);
            }
        }

        private void srcMenuDel_Click(object sender, EventArgs e)
        {
            if (RemoveSourcePressed != null)
                RemoveSourcePressed(this, new IndexEventArgs(clbPages.SelectedIndex));
        }

        private void selectAlbumsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectAlbumsPressed != null) SelectAlbumsPressed(this, new IndexEventArgs(clbPages.SelectedIndex));
        }

        #endregion

        #endregion


        #region ButtonsClick

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            if (LogoutPressed != null) LogoutPressed(this, EventArgs.Empty);
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            if (LoginPressed != null) LoginPressed(this, EventArgs.Empty);
        }

        private void btnAddFromFiends_Click(object sender, EventArgs e)
        {
            if (AddFromFriensPressed != null) AddFromFriensPressed(this, EventArgs.Empty);
        }

        private void btnAddFromGroups_Click(object sender, EventArgs e)
        {
            if (AddFromGroupsPressed != null) AddFromGroupsPressed(this, EventArgs.Empty);
        }

        private void btnManualAdd_Click(object sender, EventArgs e)
        {
            using (AddFromLinkForm form = new AddFromLinkForm())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                form.Owner = this;
                form.ShowDialog();
                if (form.DialogResult == DialogResult.OK)
                {
                    if (AddFromLinkPressed != null) AddFromLinkPressed(this, new PathEventArgs(form.Result));
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (CancelPressed != null) CancelPressed(this, EventArgs.Empty);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnImageLinks_Click(object sender, EventArgs e)
        {
            if (GetImagesPressed != null) GetImagesPressed(this, new IndicesEventArgs(clbPages.CheckedIndices.Cast<int>()));
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (LoadImagesPressed != null) LoadImagesPressed(this, EventArgs.Empty);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (ImportSourcesPressed != null) ImportSourcesPressed(this, new PathEventArgs(openFileDialog1.FileName));
            }   
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (ExportSourcesPressed != null) ExportSourcesPressed(this, new PathEventArgs(saveFileDialog1.FileName));
            }            
        }


        #endregion

        public void UnlockInterface()
        {
            this.Invoke(new Action(() =>
            {
                grboxImages.Visible = true;
                grboxImages.Enabled = true;
                grboxImagesRun.Visible = false;
                grboxImagesRun.Enabled = false;
            }));
        }

        public void LockInterface()
        {
            this.Invoke(new Action(() =>
            {
                grboxImages.Visible = false;
                grboxImages.Enabled = false;
                grboxImagesRun.Visible = true;
                grboxImagesRun.Enabled = true;
            }));
        }
    }
}
