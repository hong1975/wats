namespace WatsEMIAnalyzer
{
    partial class LinkConfigurationManagementForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkConfigurationManagementForm));
            this.OKButton = new System.Windows.Forms.Button();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.ViewButton = new System.Windows.Forms.Button();
            this.CreateButton = new System.Windows.Forms.Button();
            this.LinkConfigurationList = new System.Windows.Forms.ListBox();
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
            this.OKButton.TabIndex = 22;
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
            this.RemoveButton.TabIndex = 21;
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
            this.ViewButton.TabIndex = 20;
            this.ViewButton.Text = "View";
            this.ViewButton.UseVisualStyleBackColor = true;
            this.ViewButton.Click += new System.EventHandler(this.ViewButton_Click);
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(316, 47);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(78, 29);
            this.CreateButton.TabIndex = 19;
            this.CreateButton.Text = "Create";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // LinkConfigurationList
            // 
            this.LinkConfigurationList.FormattingEnabled = true;
            this.LinkConfigurationList.HorizontalScrollbar = true;
            this.LinkConfigurationList.ItemHeight = 14;
            this.LinkConfigurationList.Location = new System.Drawing.Point(10, 12);
            this.LinkConfigurationList.Name = "LinkConfigurationList";
            this.LinkConfigurationList.ScrollAlwaysVisible = true;
            this.LinkConfigurationList.Size = new System.Drawing.Size(300, 242);
            this.LinkConfigurationList.TabIndex = 17;
            this.LinkConfigurationList.SelectedIndexChanged += new System.EventHandler(this.LinkConfigurationList_SelectedIndexChanged);
            // 
            // UploadButton
            // 
            this.UploadButton.Location = new System.Drawing.Point(316, 12);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(78, 29);
            this.UploadButton.TabIndex = 18;
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
            this.CancelButton.TabIndex = 23;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Visible = false;
            // 
            // LinkConfigurationManagementForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 267);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.ViewButton);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.LinkConfigurationList);
            this.Controls.Add(this.UploadButton);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.Name = "LinkConfigurationManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Link Configuration Management";
            this.Load += new System.EventHandler(this.LinkConfigurationManagementForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LinkConfigurationManagementForm_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LinkConfigurationManagementForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Button ViewButton;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.ListBox LinkConfigurationList;
        private System.Windows.Forms.Button UploadButton;
        private System.Windows.Forms.Button CancelButton;

    }
}