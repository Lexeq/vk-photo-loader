namespace VPhotoLoader.Forms
{
    partial class MainForm
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
            this.btnLogIn = new System.Windows.Forms.Button();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblAccauntInfo = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.clbPages = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblText1 = new System.Windows.Forms.Label();
            this.btnAddFromGroups = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnAddFromFiends = new System.Windows.Forms.Button();
            this.btnManualAdd = new System.Windows.Forms.Button();
            this.srcMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.srcMenuDel = new System.Windows.Forms.ToolStripMenuItem();
            this.srcMenulAll = new System.Windows.Forms.ToolStripMenuItem();
            this.srcMenuNone = new System.Windows.Forms.ToolStripMenuItem();
            this.srcMenuClr = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAlbumsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grboxImages = new System.Windows.Forms.GroupBox();
            this.lblImagesInfo = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnImageLinks = new System.Windows.Forms.Button();
            this.grboxImagesRun = new System.Windows.Forms.GroupBox();
            this.lblCurrentProgress = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.srcMenu.SuspendLayout();
            this.grboxImages.SuspendLayout();
            this.grboxImagesRun.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLogIn
            // 
            this.btnLogIn.Location = new System.Drawing.Point(6, 19);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(90, 23);
            this.btnLogIn.TabIndex = 0;
            this.btnLogIn.Text = "Войти";
            this.btnLogIn.UseVisualStyleBackColor = true;
            this.btnLogIn.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // btnLogOut
            // 
            this.btnLogOut.Location = new System.Drawing.Point(104, 19);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(90, 23);
            this.btnLogOut.TabIndex = 1;
            this.btnLogOut.Text = "Выйти";
            this.btnLogOut.UseVisualStyleBackColor = true;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblAccauntInfo);
            this.groupBox1.Controls.Add(this.btnLogIn);
            this.groupBox1.Controls.Add(this.btnLogOut);
            this.groupBox1.Location = new System.Drawing.Point(378, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 64);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Аккаунт";
            // 
            // lblAccauntInfo
            // 
            this.lblAccauntInfo.AutoSize = true;
            this.lblAccauntInfo.Location = new System.Drawing.Point(6, 45);
            this.lblAccauntInfo.Name = "lblAccauntInfo";
            this.lblAccauntInfo.Size = new System.Drawing.Size(99, 13);
            this.lblAccauntInfo.TabIndex = 2;
            this.lblAccauntInfo.Text = "Вход не выполнен";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.clbPages);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.lblText1);
            this.groupBox2.Controls.Add(this.btnAddFromGroups);
            this.groupBox2.Controls.Add(this.btnExport);
            this.groupBox2.Controls.Add(this.btnImport);
            this.groupBox2.Controls.Add(this.btnAddFromFiends);
            this.groupBox2.Controls.Add(this.btnManualAdd);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(351, 262);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Источники изображений";
            // 
            // clbPages
            // 
            this.clbPages.CheckOnClick = true;
            this.clbPages.FormattingEnabled = true;
            this.clbPages.Location = new System.Drawing.Point(10, 19);
            this.clbPages.Name = "clbPages";
            this.clbPages.Size = new System.Drawing.Size(326, 154);
            this.clbPages.TabIndex = 6;
            this.clbPages.ThreeDCheckBoxes = true;
            this.clbPages.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbPages_ItemCheck);
            this.clbPages.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sourceList_MouseDown);
            this.clbPages.MouseEnter += new System.EventHandler(this.sourceList_MouseEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(6, 228);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Список источников:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(6, 202);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "источник:";
            // 
            // lblText1
            // 
            this.lblText1.AutoSize = true;
            this.lblText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblText1.Location = new System.Drawing.Point(7, 186);
            this.lblText1.Name = "lblText1";
            this.lblText1.Size = new System.Drawing.Size(71, 16);
            this.lblText1.TabIndex = 4;
            this.lblText1.Text = "Добавить";
            // 
            // btnAddFromGroups
            // 
            this.btnAddFromGroups.Location = new System.Drawing.Point(170, 190);
            this.btnAddFromGroups.Name = "btnAddFromGroups";
            this.btnAddFromGroups.Size = new System.Drawing.Size(80, 23);
            this.btnAddFromGroups.TabIndex = 2;
            this.btnAddFromGroups.Text = "Мои группы";
            this.btnAddFromGroups.UseVisualStyleBackColor = true;
            this.btnAddFromGroups.Click += new System.EventHandler(this.btnAddFromGroups_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(256, 225);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(80, 23);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "В файл";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(170, 225);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(80, 23);
            this.btnImport.TabIndex = 5;
            this.btnImport.Text = "Из файла";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnAddFromFiends
            // 
            this.btnAddFromFiends.Location = new System.Drawing.Point(256, 190);
            this.btnAddFromFiends.Name = "btnAddFromFiends";
            this.btnAddFromFiends.Size = new System.Drawing.Size(80, 23);
            this.btnAddFromFiends.TabIndex = 1;
            this.btnAddFromFiends.Text = "Мои друзья";
            this.btnAddFromFiends.UseVisualStyleBackColor = true;
            this.btnAddFromFiends.Click += new System.EventHandler(this.btnAddFromFiends_Click);
            // 
            // btnManualAdd
            // 
            this.btnManualAdd.Location = new System.Drawing.Point(84, 190);
            this.btnManualAdd.Name = "btnManualAdd";
            this.btnManualAdd.Size = new System.Drawing.Size(80, 23);
            this.btnManualAdd.TabIndex = 0;
            this.btnManualAdd.Text = "Из ссылки";
            this.btnManualAdd.UseVisualStyleBackColor = true;
            this.btnManualAdd.Click += new System.EventHandler(this.btnManualAdd_Click);
            // 
            // srcMenu
            // 
            this.srcMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.srcMenuDel,
            this.srcMenulAll,
            this.srcMenuNone,
            this.srcMenuClr,
            this.selectAlbumsToolStripMenuItem});
            this.srcMenu.Name = "srcMenu";
            this.srcMenu.Size = new System.Drawing.Size(192, 114);
            // 
            // srcMenuDel
            // 
            this.srcMenuDel.Image = global::VPhotoLoader.Properties.Resources.delete_16x16;
            this.srcMenuDel.Name = "srcMenuDel";
            this.srcMenuDel.Size = new System.Drawing.Size(191, 22);
            this.srcMenuDel.Text = "Удалить";
            this.srcMenuDel.Click += new System.EventHandler(this.srcMenuDel_Click);
            // 
            // srcMenulAll
            // 
            this.srcMenulAll.Image = global::VPhotoLoader.Properties.Resources.check_16x16;
            this.srcMenulAll.Name = "srcMenulAll";
            this.srcMenulAll.Size = new System.Drawing.Size(191, 22);
            this.srcMenulAll.Text = "Выбрать все";
            this.srcMenulAll.Click += new System.EventHandler(this.srcMenulAll_Click);
            // 
            // srcMenuNone
            // 
            this.srcMenuNone.Image = global::VPhotoLoader.Properties.Resources.uncheck_16x16;
            this.srcMenuNone.Name = "srcMenuNone";
            this.srcMenuNone.Size = new System.Drawing.Size(191, 22);
            this.srcMenuNone.Text = "Снять выбор со всех";
            this.srcMenuNone.Click += new System.EventHandler(this.srcMenuNone_Click);
            // 
            // srcMenuClr
            // 
            this.srcMenuClr.Image = global::VPhotoLoader.Properties.Resources.clear_16x16;
            this.srcMenuClr.Name = "srcMenuClr";
            this.srcMenuClr.Size = new System.Drawing.Size(191, 22);
            this.srcMenuClr.Text = "Очистить список";
            this.srcMenuClr.Click += new System.EventHandler(this.srcMenuClr_Click);
            // 
            // selectAlbumsToolStripMenuItem
            // 
            this.selectAlbumsToolStripMenuItem.Name = "selectAlbumsToolStripMenuItem";
            this.selectAlbumsToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.selectAlbumsToolStripMenuItem.Text = "Выбрать альбомы...";
            this.selectAlbumsToolStripMenuItem.Click += new System.EventHandler(this.selectAlbumsToolStripMenuItem_Click);
            // 
            // grboxImages
            // 
            this.grboxImages.Controls.Add(this.lblImagesInfo);
            this.grboxImages.Controls.Add(this.btnLoad);
            this.grboxImages.Controls.Add(this.btnImageLinks);
            this.grboxImages.Location = new System.Drawing.Point(378, 82);
            this.grboxImages.Name = "grboxImages";
            this.grboxImages.Size = new System.Drawing.Size(200, 103);
            this.grboxImages.TabIndex = 4;
            this.grboxImages.TabStop = false;
            this.grboxImages.Text = "Изображения";
            // 
            // lblImagesInfo
            // 
            this.lblImagesInfo.AutoSize = true;
            this.lblImagesInfo.Location = new System.Drawing.Point(7, 45);
            this.lblImagesInfo.Name = "lblImagesInfo";
            this.lblImagesInfo.Size = new System.Drawing.Size(0, 13);
            this.lblImagesInfo.TabIndex = 2;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(6, 65);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(188, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Начать загрузку изображений";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnImageLinks
            // 
            this.btnImageLinks.Location = new System.Drawing.Point(6, 19);
            this.btnImageLinks.Name = "btnImageLinks";
            this.btnImageLinks.Size = new System.Drawing.Size(188, 23);
            this.btnImageLinks.TabIndex = 0;
            this.btnImageLinks.Text = "Получить список изображений";
            this.btnImageLinks.UseVisualStyleBackColor = true;
            this.btnImageLinks.Click += new System.EventHandler(this.btnImageLinks_Click);
            // 
            // grboxImagesRun
            // 
            this.grboxImagesRun.Controls.Add(this.lblCurrentProgress);
            this.grboxImagesRun.Controls.Add(this.progressBar1);
            this.grboxImagesRun.Controls.Add(this.btnCancel);
            this.grboxImagesRun.Location = new System.Drawing.Point(378, 82);
            this.grboxImagesRun.Name = "grboxImagesRun";
            this.grboxImagesRun.Size = new System.Drawing.Size(200, 103);
            this.grboxImagesRun.TabIndex = 4;
            this.grboxImagesRun.TabStop = false;
            this.grboxImagesRun.Text = "Изображения";
            this.grboxImagesRun.Visible = false;
            // 
            // lblCurrentProgress
            // 
            this.lblCurrentProgress.AutoSize = true;
            this.lblCurrentProgress.Location = new System.Drawing.Point(6, 45);
            this.lblCurrentProgress.Name = "lblCurrentProgress";
            this.lblCurrentProgress.Size = new System.Drawing.Size(0, 13);
            this.lblCurrentProgress.TabIndex = 2;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 65);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(188, 22);
            this.progressBar1.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(6, 19);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(188, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отменить";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(6, 19);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(188, 23);
            this.btnSettings.TabIndex = 5;
            this.btnSettings.Text = "Настройки";
            this.btnSettings.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(6, 49);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(188, 23);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnSettings);
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Location = new System.Drawing.Point(378, 191);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 83);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ImagePump";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "vsrc";
            this.openFileDialog1.Filter = "VKPL Files(*.vsrc)|*.vsrc";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "vsrc";
            this.saveFileDialog1.Filter = "VKPL Files(*.vsrc)|*.vsrc";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 287);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grboxImagesRun);
            this.Controls.Add(this.grboxImages);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::VPhotoLoader.Properties.Resources.favicon;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "VKPhotoLoader";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.srcMenu.ResumeLayout(false);
            this.grboxImages.ResumeLayout(false);
            this.grboxImages.PerformLayout();
            this.grboxImagesRun.ResumeLayout(false);
            this.grboxImagesRun.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLogIn;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblAccauntInfo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAddFromGroups;
        private System.Windows.Forms.Button btnAddFromFiends;
        private System.Windows.Forms.Button btnManualAdd;
        private System.Windows.Forms.Label lblText1;
        private System.Windows.Forms.ContextMenuStrip srcMenu;
        private System.Windows.Forms.ToolStripMenuItem srcMenuDel;
        private System.Windows.Forms.ToolStripMenuItem srcMenulAll;
        private System.Windows.Forms.ToolStripMenuItem srcMenuNone;
        private System.Windows.Forms.ToolStripMenuItem srcMenuClr;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grboxImages;
        private System.Windows.Forms.Button btnImageLinks;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox grboxImagesRun;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblCurrentProgress;
        private System.Windows.Forms.Label lblImagesInfo;
        private System.Windows.Forms.ToolStripMenuItem selectAlbumsToolStripMenuItem;
        private System.Windows.Forms.CheckedListBox clbPages;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

