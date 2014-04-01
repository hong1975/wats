namespace WatsEMIAnalyzer
{
    partial class AdminForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminForm));
            this.AdminTabControl = new System.Windows.Forms.TabControl();
            this.RegionManagmentTabPage = new System.Windows.Forms.TabPage();
            this.RegionTreeView = new System.Windows.Forms.TreeView();
            this.UserManagementTabPage = new System.Windows.Forms.TabPage();
            this.UsersTabControl = new System.Windows.Forms.TabControl();
            this.ActiveUsersTabPage = new System.Windows.Forms.TabPage();
            this.LockButton = new System.Windows.Forms.Button();
            this.RemoveActiveUserButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.ActiveUsersListBox = new System.Windows.Forms.ListBox();
            this.LockedUsersTabPage = new System.Windows.Forms.TabPage();
            this.UnLockButton = new System.Windows.Forms.Button();
            this.RemoveLockedUserButton = new System.Windows.Forms.Button();
            this.LockedUsersListBox = new System.Windows.Forms.ListBox();
            this.GlobalContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddReagionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.RegionContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RemoveRegionToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RenameRegionToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ManagerContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RemoveManagerToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ManagersContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AssignManagersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AdminTabControl.SuspendLayout();
            this.RegionManagmentTabPage.SuspendLayout();
            this.UserManagementTabPage.SuspendLayout();
            this.UsersTabControl.SuspendLayout();
            this.ActiveUsersTabPage.SuspendLayout();
            this.LockedUsersTabPage.SuspendLayout();
            this.GlobalContextMenu.SuspendLayout();
            this.RegionContextMenu.SuspendLayout();
            this.ManagerContextMenu.SuspendLayout();
            this.ManagersContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // AdminTabControl
            // 
            this.AdminTabControl.Controls.Add(this.RegionManagmentTabPage);
            this.AdminTabControl.Controls.Add(this.UserManagementTabPage);
            this.AdminTabControl.Location = new System.Drawing.Point(10, 11);
            this.AdminTabControl.Name = "AdminTabControl";
            this.AdminTabControl.SelectedIndex = 0;
            this.AdminTabControl.Size = new System.Drawing.Size(322, 481);
            this.AdminTabControl.TabIndex = 0;
            // 
            // RegionManagmentTabPage
            // 
            this.RegionManagmentTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.RegionManagmentTabPage.Controls.Add(this.RegionTreeView);
            this.RegionManagmentTabPage.Location = new System.Drawing.Point(4, 23);
            this.RegionManagmentTabPage.Name = "RegionManagmentTabPage";
            this.RegionManagmentTabPage.Size = new System.Drawing.Size(314, 454);
            this.RegionManagmentTabPage.TabIndex = 0;
            this.RegionManagmentTabPage.Text = "Region Management";
            // 
            // RegionTreeView
            // 
            this.RegionTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RegionTreeView.Location = new System.Drawing.Point(0, 0);
            this.RegionTreeView.Margin = new System.Windows.Forms.Padding(0);
            this.RegionTreeView.Name = "RegionTreeView";
            this.RegionTreeView.Size = new System.Drawing.Size(314, 454);
            this.RegionTreeView.TabIndex = 0;
            this.RegionTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.RegionTreeView_NodeMouseClick);
            // 
            // UserManagementTabPage
            // 
            this.UserManagementTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.UserManagementTabPage.Controls.Add(this.UsersTabControl);
            this.UserManagementTabPage.Location = new System.Drawing.Point(4, 23);
            this.UserManagementTabPage.Margin = new System.Windows.Forms.Padding(0);
            this.UserManagementTabPage.Name = "UserManagementTabPage";
            this.UserManagementTabPage.Size = new System.Drawing.Size(314, 454);
            this.UserManagementTabPage.TabIndex = 1;
            this.UserManagementTabPage.Text = "User Managment";
            // 
            // UsersTabControl
            // 
            this.UsersTabControl.Controls.Add(this.ActiveUsersTabPage);
            this.UsersTabControl.Controls.Add(this.LockedUsersTabPage);
            this.UsersTabControl.Location = new System.Drawing.Point(0, 0);
            this.UsersTabControl.Multiline = true;
            this.UsersTabControl.Name = "UsersTabControl";
            this.UsersTabControl.SelectedIndex = 0;
            this.UsersTabControl.Size = new System.Drawing.Size(315, 451);
            this.UsersTabControl.TabIndex = 0;
            // 
            // ActiveUsersTabPage
            // 
            this.ActiveUsersTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.ActiveUsersTabPage.Controls.Add(this.LockButton);
            this.ActiveUsersTabPage.Controls.Add(this.RemoveActiveUserButton);
            this.ActiveUsersTabPage.Controls.Add(this.AddButton);
            this.ActiveUsersTabPage.Controls.Add(this.ActiveUsersListBox);
            this.ActiveUsersTabPage.Location = new System.Drawing.Point(4, 23);
            this.ActiveUsersTabPage.Name = "ActiveUsersTabPage";
            this.ActiveUsersTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ActiveUsersTabPage.Size = new System.Drawing.Size(307, 424);
            this.ActiveUsersTabPage.TabIndex = 0;
            this.ActiveUsersTabPage.Text = "Active Users";
            // 
            // LockButton
            // 
            this.LockButton.Enabled = false;
            this.LockButton.Location = new System.Drawing.Point(227, 75);
            this.LockButton.Name = "LockButton";
            this.LockButton.Size = new System.Drawing.Size(74, 29);
            this.LockButton.TabIndex = 3;
            this.LockButton.Text = "Lock";
            this.LockButton.UseVisualStyleBackColor = true;
            this.LockButton.Click += new System.EventHandler(this.LockButton_Click);
            // 
            // RemoveActiveUserButton
            // 
            this.RemoveActiveUserButton.Enabled = false;
            this.RemoveActiveUserButton.Location = new System.Drawing.Point(227, 40);
            this.RemoveActiveUserButton.Name = "RemoveActiveUserButton";
            this.RemoveActiveUserButton.Size = new System.Drawing.Size(74, 29);
            this.RemoveActiveUserButton.TabIndex = 2;
            this.RemoveActiveUserButton.Text = "Remove";
            this.RemoveActiveUserButton.UseVisualStyleBackColor = true;
            this.RemoveActiveUserButton.Click += new System.EventHandler(this.RemoveActiveUserButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(227, 6);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(74, 29);
            this.AddButton.TabIndex = 1;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // ActiveUsersListBox
            // 
            this.ActiveUsersListBox.FormattingEnabled = true;
            this.ActiveUsersListBox.HorizontalScrollbar = true;
            this.ActiveUsersListBox.ItemHeight = 14;
            this.ActiveUsersListBox.Location = new System.Drawing.Point(0, 0);
            this.ActiveUsersListBox.Name = "ActiveUsersListBox";
            this.ActiveUsersListBox.Size = new System.Drawing.Size(221, 438);
            this.ActiveUsersListBox.TabIndex = 0;
            this.ActiveUsersListBox.SelectedIndexChanged += new System.EventHandler(this.ActiveUsersListBox_SelectedIndexChanged);
            // 
            // LockedUsersTabPage
            // 
            this.LockedUsersTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.LockedUsersTabPage.Controls.Add(this.UnLockButton);
            this.LockedUsersTabPage.Controls.Add(this.RemoveLockedUserButton);
            this.LockedUsersTabPage.Controls.Add(this.LockedUsersListBox);
            this.LockedUsersTabPage.ForeColor = System.Drawing.Color.Black;
            this.LockedUsersTabPage.Location = new System.Drawing.Point(4, 23);
            this.LockedUsersTabPage.Name = "LockedUsersTabPage";
            this.LockedUsersTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.LockedUsersTabPage.Size = new System.Drawing.Size(307, 424);
            this.LockedUsersTabPage.TabIndex = 1;
            this.LockedUsersTabPage.Text = "LockedUsers";
            // 
            // UnLockButton
            // 
            this.UnLockButton.Enabled = false;
            this.UnLockButton.Location = new System.Drawing.Point(227, 40);
            this.UnLockButton.Name = "UnLockButton";
            this.UnLockButton.Size = new System.Drawing.Size(74, 29);
            this.UnLockButton.TabIndex = 7;
            this.UnLockButton.Text = "UnLock";
            this.UnLockButton.UseVisualStyleBackColor = true;
            this.UnLockButton.Click += new System.EventHandler(this.UnLockButton_Click);
            // 
            // RemoveLockedUserButton
            // 
            this.RemoveLockedUserButton.Enabled = false;
            this.RemoveLockedUserButton.Location = new System.Drawing.Point(227, 6);
            this.RemoveLockedUserButton.Name = "RemoveLockedUserButton";
            this.RemoveLockedUserButton.Size = new System.Drawing.Size(74, 29);
            this.RemoveLockedUserButton.TabIndex = 6;
            this.RemoveLockedUserButton.Text = "Remove";
            this.RemoveLockedUserButton.UseVisualStyleBackColor = true;
            this.RemoveLockedUserButton.Click += new System.EventHandler(this.RemoveLockedUserButton_Click);
            // 
            // LockedUsersListBox
            // 
            this.LockedUsersListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.LockedUsersListBox.FormattingEnabled = true;
            this.LockedUsersListBox.HorizontalScrollbar = true;
            this.LockedUsersListBox.ItemHeight = 14;
            this.LockedUsersListBox.Location = new System.Drawing.Point(0, 0);
            this.LockedUsersListBox.Name = "LockedUsersListBox";
            this.LockedUsersListBox.Size = new System.Drawing.Size(221, 438);
            this.LockedUsersListBox.TabIndex = 4;
            this.LockedUsersListBox.SelectedIndexChanged += new System.EventHandler(this.LockedUsersListBox_SelectedIndexChanged);
            // 
            // GlobalContextMenu
            // 
            this.GlobalContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddReagionToolStripMenuItem});
            this.GlobalContextMenu.Name = "GlobalContextMenu";
            this.GlobalContextMenu.Size = new System.Drawing.Size(143, 26);
            // 
            // AddReagionToolStripMenuItem
            // 
            this.AddReagionToolStripMenuItem.Name = "AddReagionToolStripMenuItem";
            this.AddReagionToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.AddReagionToolStripMenuItem.Text = "Add Reagion";
            this.AddReagionToolStripMenuItem.Click += new System.EventHandler(this.AddReagionToolStripMenuItem_Click);
            // 
            // ImageList
            // 
            this.ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList.Images.SetKeyName(0, "Global.png");
            this.ImageList.Images.SetKeyName(1, "Manager.jpg");
            this.ImageList.Images.SetKeyName(2, "Region.jpg");
            this.ImageList.Images.SetKeyName(3, "Managers.png");
            // 
            // RegionContextMenu
            // 
            this.RegionContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RemoveRegionToolMenuItem,
            this.RenameRegionToolMenuItem});
            this.RegionContextMenu.Name = "RegionContextMenu";
            this.RegionContextMenu.Size = new System.Drawing.Size(137, 48);
            // 
            // RemoveRegionToolMenuItem
            // 
            this.RemoveRegionToolMenuItem.Name = "RemoveRegionToolMenuItem";
            this.RemoveRegionToolMenuItem.Size = new System.Drawing.Size(136, 22);
            this.RemoveRegionToolMenuItem.Text = "Remove";
            this.RemoveRegionToolMenuItem.Click += new System.EventHandler(this.RemoveRegionToolMenuItem_Click);
            // 
            // RenameRegionToolMenuItem
            // 
            this.RenameRegionToolMenuItem.Name = "RenameRegionToolMenuItem";
            this.RenameRegionToolMenuItem.Size = new System.Drawing.Size(136, 22);
            this.RenameRegionToolMenuItem.Text = "Rename ...";
            this.RenameRegionToolMenuItem.Click += new System.EventHandler(this.RenameRegionToolMenuItem_Click);
            // 
            // ManagerContextMenu
            // 
            this.ManagerContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RemoveManagerToolMenuItem});
            this.ManagerContextMenu.Name = "ManagerContextMenu";
            this.ManagerContextMenu.Size = new System.Drawing.Size(124, 26);
            // 
            // RemoveManagerToolMenuItem
            // 
            this.RemoveManagerToolMenuItem.Name = "RemoveManagerToolMenuItem";
            this.RemoveManagerToolMenuItem.Size = new System.Drawing.Size(123, 22);
            this.RemoveManagerToolMenuItem.Text = "Remove";
            this.RemoveManagerToolMenuItem.Click += new System.EventHandler(this.RemoveManagerToolMenuItem_Click);
            // 
            // ManagersContextMenu
            // 
            this.ManagersContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AssignManagersToolStripMenuItem});
            this.ManagersContextMenu.Name = "ManagersContextMenu";
            this.ManagersContextMenu.Size = new System.Drawing.Size(191, 26);
            // 
            // AssignManagersToolStripMenuItem
            // 
            this.AssignManagersToolStripMenuItem.Name = "AssignManagersToolStripMenuItem";
            this.AssignManagersToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.AssignManagersToolStripMenuItem.Text = "Assign Managers ...";
            this.AssignManagersToolStripMenuItem.Click += new System.EventHandler(this.AssignManagersToolStripMenuItem_Click);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 504);
            this.Controls.Add(this.AdminTabControl);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WATS EMI Analyzer 2.0 - Administration";
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdminForm_FormClosed);
            this.AdminTabControl.ResumeLayout(false);
            this.RegionManagmentTabPage.ResumeLayout(false);
            this.UserManagementTabPage.ResumeLayout(false);
            this.UsersTabControl.ResumeLayout(false);
            this.ActiveUsersTabPage.ResumeLayout(false);
            this.LockedUsersTabPage.ResumeLayout(false);
            this.GlobalContextMenu.ResumeLayout(false);
            this.RegionContextMenu.ResumeLayout(false);
            this.ManagerContextMenu.ResumeLayout(false);
            this.ManagersContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl AdminTabControl;
        private System.Windows.Forms.TabPage RegionManagmentTabPage;
        private System.Windows.Forms.TabPage UserManagementTabPage;
        private System.Windows.Forms.TreeView RegionTreeView;
        private System.Windows.Forms.TabControl UsersTabControl;
        private System.Windows.Forms.TabPage ActiveUsersTabPage;
        private System.Windows.Forms.TabPage LockedUsersTabPage;
        private System.Windows.Forms.Button LockButton;
        private System.Windows.Forms.Button RemoveActiveUserButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.ListBox ActiveUsersListBox;
        private System.Windows.Forms.Button UnLockButton;
        private System.Windows.Forms.Button RemoveLockedUserButton;
        private System.Windows.Forms.ListBox LockedUsersListBox;
        private System.Windows.Forms.ContextMenuStrip GlobalContextMenu;
        private System.Windows.Forms.ImageList ImageList;
        private System.Windows.Forms.ToolStripMenuItem AddReagionToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip RegionContextMenu;
        private System.Windows.Forms.ToolStripMenuItem RemoveRegionToolMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RenameRegionToolMenuItem;
        private System.Windows.Forms.ContextMenuStrip ManagerContextMenu;
        private System.Windows.Forms.ToolStripMenuItem RemoveManagerToolMenuItem;
        private System.Windows.Forms.ContextMenuStrip ManagersContextMenu;
        private System.Windows.Forms.ToolStripMenuItem AssignManagersToolStripMenuItem;


    }
}