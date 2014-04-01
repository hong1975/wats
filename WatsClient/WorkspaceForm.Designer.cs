namespace WatsClient
{
    partial class WorkspaceForm
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
            this.WorkspaceTreeView = new System.Windows.Forms.TreeView();
            this.UserAndLicensingContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UserAndLicensingContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // WorkspaceTreeView
            // 
            this.WorkspaceTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WorkspaceTreeView.Location = new System.Drawing.Point(0, 0);
            this.WorkspaceTreeView.Name = "WorkspaceTreeView";
            this.WorkspaceTreeView.Size = new System.Drawing.Size(271, 408);
            this.WorkspaceTreeView.TabIndex = 0;
            this.WorkspaceTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.WorkspaceTreeView_NodeMouseDoubleClick);
            this.WorkspaceTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.WorkspaceTreeView_NodeMouseClick);
            // 
            // UserAndLicensingContextMenuStrip
            // 
            this.UserAndLicensingContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddUserToolStripMenuItem});
            this.UserAndLicensingContextMenuStrip.Name = "UserAndLicensingContextMenuStrip";
            this.UserAndLicensingContextMenuStrip.Size = new System.Drawing.Size(153, 48);
            // 
            // AddUserToolStripMenuItem
            // 
            this.AddUserToolStripMenuItem.Name = "AddUserToolStripMenuItem";
            this.AddUserToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.AddUserToolStripMenuItem.Text = "Add User";
            this.AddUserToolStripMenuItem.Click += new System.EventHandler(this.AddUserToolStripMenuItem_Click);
            // 
            // WorkspaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 408);
            this.CloseButtonVisible = false;
            this.Controls.Add(this.WorkspaceTreeView);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "WorkspaceForm";
            this.Text = "Workspace";
            this.Load += new System.EventHandler(this.WorkspaceForm_Load);
            this.UserAndLicensingContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView WorkspaceTreeView;
        private System.Windows.Forms.ContextMenuStrip UserAndLicensingContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem AddUserToolStripMenuItem;
    }
}