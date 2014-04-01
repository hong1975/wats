namespace WatsEMIAnalyzer
{
    partial class SiteManagementForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SiteManagementForm));
            this.OKButton = new System.Windows.Forms.Button();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.ViewButton = new System.Windows.Forms.Button();
            this.CreateButton = new System.Windows.Forms.Button();
            this.SiteList = new System.Windows.Forms.ListBox();
            this.UploadButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Enabled = false;
            this.OKButton.Location = new System.Drawing.Point(316, 225);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(78, 29);
            this.OKButton.TabIndex = 28;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Visible = false;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // RemoveButton
            // 
            this.RemoveButton.Enabled = false;
            this.RemoveButton.Location = new System.Drawing.Point(316, 117);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(78, 29);
            this.RemoveButton.TabIndex = 27;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // ViewButton
            // 
            this.ViewButton.Enabled = false;
            this.ViewButton.Location = new System.Drawing.Point(316, 82);
            this.ViewButton.Name = "ViewButton";
            this.ViewButton.Size = new System.Drawing.Size(78, 29);
            this.ViewButton.TabIndex = 26;
            this.ViewButton.Text = "View";
            this.ViewButton.UseVisualStyleBackColor = true;
            this.ViewButton.Click += new System.EventHandler(this.ViewButton_Click);
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(316, 47);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(78, 29);
            this.CreateButton.TabIndex = 25;
            this.CreateButton.Text = "Create";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // SiteList
            // 
            this.SiteList.FormattingEnabled = true;
            this.SiteList.HorizontalScrollbar = true;
            this.SiteList.ItemHeight = 14;
            this.SiteList.Location = new System.Drawing.Point(10, 12);
            this.SiteList.Name = "SiteList";
            this.SiteList.ScrollAlwaysVisible = true;
            this.SiteList.Size = new System.Drawing.Size(300, 242);
            this.SiteList.TabIndex = 23;
            this.SiteList.SelectedIndexChanged += new System.EventHandler(this.SiteList_SelectedIndexChanged);
            // 
            // UploadButton
            // 
            this.UploadButton.Location = new System.Drawing.Point(316, 12);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(78, 29);
            this.UploadButton.TabIndex = 24;
            this.UploadButton.Text = "Upload ...";
            this.UploadButton.UseVisualStyleBackColor = true;
            this.UploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(316, 190);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(78, 29);
            this.CancelButton.TabIndex = 29;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Visible = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SiteManagementForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 267);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.ViewButton);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.SiteList);
            this.Controls.Add(this.UploadButton);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "SiteManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Site Management";
            this.Load += new System.EventHandler(this.SiteManagementForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SiteManagementForm_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SiteManagementForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Button ViewButton;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.ListBox SiteList;
        private System.Windows.Forms.Button UploadButton;
        private System.Windows.Forms.Button CancelButton;
    }
}