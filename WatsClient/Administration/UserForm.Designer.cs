namespace WatsClient.Administration
{
    partial class UserForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.AddUserButton = new System.Windows.Forms.Button();
            this.UserTable = new System.Windows.Forms.DataGridView();
            this.LicenseTreeView = new System.Windows.Forms.TreeView();
            this.AdministratorColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LockedColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LinceseColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UserTable)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.LicenseTreeView);
            this.splitContainer1.Size = new System.Drawing.Size(590, 456);
            this.splitContainer1.SplitterDistance = 439;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.AddUserButton, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.UserTable, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(435, 452);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // AddUserButton
            // 
            this.AddUserButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AddUserButton.Location = new System.Drawing.Point(357, 422);
            this.AddUserButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.AddUserButton.Name = "AddUserButton";
            this.AddUserButton.Size = new System.Drawing.Size(75, 30);
            this.AddUserButton.TabIndex = 0;
            this.AddUserButton.Text = "Add User";
            this.AddUserButton.UseVisualStyleBackColor = true;
            this.AddUserButton.Click += new System.EventHandler(this.AddUserButton_Click);
            // 
            // UserTable
            // 
            this.UserTable.AllowUserToAddRows = false;
            this.UserTable.AllowUserToDeleteRows = false;
            this.UserTable.AllowUserToOrderColumns = true;
            this.UserTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UserTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UserNameColumn,
            this.AdministratorColumn,
            this.LinceseColumn,
            this.LockedColumn});
            this.UserTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserTable.Location = new System.Drawing.Point(3, 3);
            this.UserTable.Name = "UserTable";
            this.UserTable.RowHeadersVisible = false;
            this.UserTable.RowTemplate.Height = 23;
            this.UserTable.Size = new System.Drawing.Size(429, 416);
            this.UserTable.TabIndex = 1;
            // 
            // LicenseTreeView
            // 
            this.LicenseTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LicenseTreeView.Location = new System.Drawing.Point(0, 0);
            this.LicenseTreeView.Name = "LicenseTreeView";
            this.LicenseTreeView.Size = new System.Drawing.Size(143, 452);
            this.LicenseTreeView.TabIndex = 0;
            // 
            // AdministratorColumn
            // 
            this.AdministratorColumn.HeaderText = "Admin User";
            this.AdministratorColumn.Name = "AdministratorColumn";
            this.AdministratorColumn.ReadOnly = true;
            this.AdministratorColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // LockedColumn
            // 
            this.LockedColumn.HeaderText = "Locked";
            this.LockedColumn.Name = "LockedColumn";
            this.LockedColumn.ReadOnly = true;
            this.LockedColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "User Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "License";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // UserNameColumn
            // 
            this.UserNameColumn.HeaderText = "User Name";
            this.UserNameColumn.Name = "UserNameColumn";
            this.UserNameColumn.ReadOnly = true;
            // 
            // LinceseColumn
            // 
            this.LinceseColumn.HeaderText = "License";
            this.LinceseColumn.Name = "LinceseColumn";
            this.LinceseColumn.ReadOnly = true;
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 456);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Name = "UserForm";
            this.Text = "Users && Licensing";
            this.Load += new System.EventHandler(this.UserForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UserTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button AddUserButton;
        private System.Windows.Forms.DataGridView UserTable;
        private System.Windows.Forms.TreeView LicenseTreeView;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserNameColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn AdministratorColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LinceseColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn LockedColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}