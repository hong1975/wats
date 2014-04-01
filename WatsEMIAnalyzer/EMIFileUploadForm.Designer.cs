namespace WatsEMIAnalyzer
{
    partial class EMIFileUploadForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMIFileUploadForm));
            this.label2 = new System.Windows.Forms.Label();
            this.EMIFileCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.ScanFolderButton = new System.Windows.Forms.Button();
            this.AddFilesButton = new System.Windows.Forms.Button();
            this.UploadButton = new System.Windows.Forms.Button();
            this.SelectAllCheckBox = new System.Windows.Forms.CheckBox();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 14);
            this.label2.TabIndex = 7;
            this.label2.Text = "EMI Files";
            // 
            // EMIFileCheckedListBox
            // 
            this.EMIFileCheckedListBox.FormattingEnabled = true;
            this.EMIFileCheckedListBox.HorizontalScrollbar = true;
            this.EMIFileCheckedListBox.Location = new System.Drawing.Point(12, 32);
            this.EMIFileCheckedListBox.Name = "EMIFileCheckedListBox";
            this.EMIFileCheckedListBox.Size = new System.Drawing.Size(420, 140);
            this.EMIFileCheckedListBox.TabIndex = 8;
            // 
            // ScanFolderButton
            // 
            this.ScanFolderButton.Location = new System.Drawing.Point(12, 178);
            this.ScanFolderButton.Name = "ScanFolderButton";
            this.ScanFolderButton.Size = new System.Drawing.Size(91, 33);
            this.ScanFolderButton.TabIndex = 9;
            this.ScanFolderButton.Text = "Scan Folder";
            this.ScanFolderButton.UseVisualStyleBackColor = true;
            this.ScanFolderButton.Click += new System.EventHandler(this.ScanFolderButton_Click);
            // 
            // AddFilesButton
            // 
            this.AddFilesButton.Location = new System.Drawing.Point(109, 178);
            this.AddFilesButton.Name = "AddFilesButton";
            this.AddFilesButton.Size = new System.Drawing.Size(91, 33);
            this.AddFilesButton.TabIndex = 12;
            this.AddFilesButton.Text = "Add Files ...";
            this.AddFilesButton.UseVisualStyleBackColor = true;
            this.AddFilesButton.Click += new System.EventHandler(this.AddFilesButton_Click);
            // 
            // UploadButton
            // 
            this.UploadButton.Location = new System.Drawing.Point(341, 178);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(91, 33);
            this.UploadButton.TabIndex = 13;
            this.UploadButton.Text = "Upload";
            this.UploadButton.UseVisualStyleBackColor = true;
            this.UploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // SelectAllCheckBox
            // 
            this.SelectAllCheckBox.AutoSize = true;
            this.SelectAllCheckBox.Checked = true;
            this.SelectAllCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SelectAllCheckBox.Location = new System.Drawing.Point(355, 9);
            this.SelectAllCheckBox.Name = "SelectAllCheckBox";
            this.SelectAllCheckBox.Size = new System.Drawing.Size(77, 18);
            this.SelectAllCheckBox.TabIndex = 10;
            this.SelectAllCheckBox.Text = "Select All";
            this.SelectAllCheckBox.UseVisualStyleBackColor = true;
            this.SelectAllCheckBox.CheckedChanged += new System.EventHandler(this.SelectAllCheckBox_CheckedChanged);
            // 
            // RemoveButton
            // 
            this.RemoveButton.Location = new System.Drawing.Point(206, 178);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(91, 33);
            this.RemoveButton.TabIndex = 14;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // EMIFileUploadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 218);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.UploadButton);
            this.Controls.Add(this.AddFilesButton);
            this.Controls.Add(this.SelectAllCheckBox);
            this.Controls.Add(this.ScanFolderButton);
            this.Controls.Add(this.EMIFileCheckedListBox);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "EMIFileUploadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EMI Files Upload";
            this.Load += new System.EventHandler(this.EMIFileUploadForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EMIFileUploadForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox EMIFileCheckedListBox;
        private System.Windows.Forms.Button ScanFolderButton;
        private System.Windows.Forms.Button AddFilesButton;
        private System.Windows.Forms.Button UploadButton;
        private System.Windows.Forms.CheckBox SelectAllCheckBox;
        private System.Windows.Forms.Button RemoveButton;


    }
}