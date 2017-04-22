namespace VPhotoLoader.Forms
{
    partial class ChooseForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.sources = new System.Windows.Forms.CheckedListBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.srcMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUnselectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.srcMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // sources
            // 
            this.sources.CheckOnClick = true;
            this.sources.FormattingEnabled = true;
            this.sources.Location = new System.Drawing.Point(12, 12);
            this.sources.Name = "sources";
            this.sources.Size = new System.Drawing.Size(268, 214);
            this.sources.TabIndex = 0;
            this.sources.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sources_MouseDown);
            this.sources.MouseEnter += new System.EventHandler(this.sources_MouseEnter);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(53, 232);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(164, 232);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // srcMenu
            // 
            this.srcMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSelectAll,
            this.menuUnselectAll});
            this.srcMenu.Name = "srcMenu";
            this.srcMenu.Size = new System.Drawing.Size(192, 48);
            // 
            // menuSelectAll
            // 
            this.menuSelectAll.Image = global::VPhotoLoader.Properties.Resources.check_16x16;
            this.menuSelectAll.Name = "menuSelectAll";
            this.menuSelectAll.Size = new System.Drawing.Size(191, 22);
            this.menuSelectAll.Text = "Выбрать все";
            this.menuSelectAll.Click += new System.EventHandler(this.menuSelectAll_Click);
            // 
            // menuUnselectAll
            // 
            this.menuUnselectAll.Image = global::VPhotoLoader.Properties.Resources.uncheck_16x16;
            this.menuUnselectAll.Name = "menuUnselectAll";
            this.menuUnselectAll.Size = new System.Drawing.Size(191, 22);
            this.menuUnselectAll.Text = "Снять выбор со всех";
            this.menuUnselectAll.Click += new System.EventHandler(this.menuUnselectAll_Click);
            // 
            // ChooseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 267);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.sources);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChooseForm";
            this.Text = "Выберите элементы";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HotKeys);
            this.srcMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox sources;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ContextMenuStrip srcMenu;
        private System.Windows.Forms.ToolStripMenuItem menuSelectAll;
        private System.Windows.Forms.ToolStripMenuItem menuUnselectAll;
    }
}