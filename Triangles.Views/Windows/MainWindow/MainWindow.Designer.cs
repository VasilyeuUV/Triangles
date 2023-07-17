namespace Triangles.Views.Windows.MainWindow
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            mainMenu = new MenuStrip();
            menuItemFile = new ToolStripMenuItem();
            menuItemLoad = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            menuItemClose = new ToolStripMenuItem();
            pictureBoxMain = new PictureBox();
            mainWindowViewModelBindingSource = new BindingSource(components);
            panelMessage = new Panel();
            lblMessage = new Label();
            menuItemHelp = new ToolStripMenuItem();
            menuItemAbout = new ToolStripMenuItem();
            mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMain).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mainWindowViewModelBindingSource).BeginInit();
            panelMessage.SuspendLayout();
            SuspendLayout();
            // 
            // mainMenu
            // 
            mainMenu.ImageScalingSize = new Size(24, 24);
            mainMenu.Items.AddRange(new ToolStripItem[] { menuItemFile, menuItemHelp });
            mainMenu.Location = new Point(0, 0);
            mainMenu.Name = "mainMenu";
            mainMenu.Size = new Size(778, 33);
            mainMenu.TabIndex = 0;
            mainMenu.Text = "menuStrip1";
            // 
            // menuItemFile
            // 
            menuItemFile.DropDownItems.AddRange(new ToolStripItem[] { menuItemLoad, toolStripMenuItem1, menuItemClose });
            menuItemFile.Name = "menuItemFile";
            menuItemFile.Size = new Size(69, 29);
            menuItemFile.Text = "Файл";
            // 
            // menuItemLoad
            // 
            menuItemLoad.Name = "menuItemLoad";
            menuItemLoad.Size = new Size(270, 34);
            menuItemLoad.Text = "Загрузить";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(267, 6);
            // 
            // menuItemClose
            // 
            menuItemClose.Name = "menuItemClose";
            menuItemClose.Size = new Size(270, 34);
            menuItemClose.Text = "Закрыть";
            menuItemClose.Click += menuItemClose_Click;
            // 
            // pictureBoxMain
            // 
            pictureBoxMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBoxMain.BorderStyle = BorderStyle.Fixed3D;
            pictureBoxMain.Location = new Point(0, 78);
            pictureBoxMain.Name = "pictureBoxMain";
            pictureBoxMain.Size = new Size(752, 350);
            pictureBoxMain.TabIndex = 1;
            pictureBoxMain.TabStop = false;
            // 
            // mainWindowViewModelBindingSource
            // 
            mainWindowViewModelBindingSource.DataSource = typeof(ViewModels.MainWindowViewModels.MainWindowViewModel);
            // 
            // panelMessage
            // 
            panelMessage.AutoSize = true;
            panelMessage.Controls.Add(lblMessage);
            panelMessage.Dock = DockStyle.Top;
            panelMessage.Location = new Point(0, 33);
            panelMessage.Margin = new Padding(10);
            panelMessage.Name = "panelMessage";
            panelMessage.Size = new Size(778, 32);
            panelMessage.TabIndex = 2;
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblMessage.ForeColor = SystemColors.HotTrack;
            lblMessage.Location = new Point(12, 0);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(83, 32);
            lblMessage.TabIndex = 0;
            lblMessage.Text = "label1";
            // 
            // menuItemHelp
            // 
            menuItemHelp.DropDownItems.AddRange(new ToolStripItem[] { menuItemAbout });
            menuItemHelp.Name = "menuItemHelp";
            menuItemHelp.Size = new Size(100, 29);
            menuItemHelp.Text = "Помощь";
            // 
            // menuItemAbout
            // 
            menuItemAbout.Name = "menuItemAbout";
            menuItemAbout.Size = new Size(270, 34);
            menuItemAbout.Text = "О программе";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(778, 444);
            Controls.Add(pictureBoxMain);
            Controls.Add(panelMessage);
            Controls.Add(mainMenu);
            MainMenuStrip = mainMenu;
            Name = "MainForm";
            Text = "U.Vasilyeu AxxonSoft test task";
            Load += Form1_Load;
            mainMenu.ResumeLayout(false);
            mainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMain).EndInit();
            ((System.ComponentModel.ISupportInitialize)mainWindowViewModelBindingSource).EndInit();
            panelMessage.ResumeLayout(false);
            panelMessage.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip mainMenu;
        private ToolStripMenuItem menuItemFile;
        private ToolStripMenuItem menuItemLoad;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem menuItemClose;
        private PictureBox pictureBoxMain;
        private Panel panelMessage;
        private Label lblMessage;
        private BindingSource mainWindowViewModelBindingSource;
        private ToolStripMenuItem menuItemHelp;
        private ToolStripMenuItem menuItemAbout;
    }
}