namespace WatsEMIAnalyzer
{
    partial class UploadEMIProgressForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadEMIProgressForm));
            this.StatusLabel = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.UploadProgressBar = new System.Windows.Forms.ProgressBar();
            this.UploadInfoLabel = new System.Windows.Forms.Label();
            this.ShowDetailCheckBox = new System.Windows.Forms.CheckBox();
            this.DetailListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(12, 58);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(147, 15);
            this.StatusLabel.TabIndex = 7;
            this.StatusLabel.Text = "0 EMI files was uploaded.";
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(356, 76);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(62, 30);
            this.CancelButton.TabIndex = 6;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // UploadProgressBar
            // 
            this.UploadProgressBar.Location = new System.Drawing.Point(15, 34);
            this.UploadProgressBar.Name = "UploadProgressBar";
            this.UploadProgressBar.Size = new System.Drawing.Size(403, 18);
            this.UploadProgressBar.TabIndex = 5;
            // 
            // UploadInfoLabel
            // 
            this.UploadInfoLabel.AutoSize = true;
            this.UploadInfoLabel.Location = new System.Drawing.Point(12, 10);
            this.UploadInfoLabel.Name = "UploadInfoLabel";
            this.UploadInfoLabel.Size = new System.Drawing.Size(76, 15);
            this.UploadInfoLabel.TabIndex = 4;
            this.UploadInfoLabel.Text = "Uploading ...";
            // 
            // ShowDetailCheckBox
            // 
            this.ShowDetailCheckBox.AutoSize = true;
            this.ShowDetailCheckBox.Location = new System.Drawing.Point(15, 87);
            this.ShowDetailCheckBox.Name = "ShowDetailCheckBox";
            this.ShowDetailCheckBox.Size = new System.Drawing.Size(92, 19);
            this.ShowDetailCheckBox.TabIndex = 8;
            this.ShowDetailCheckBox.Text = "Show Detail";
            this.ShowDetailCheckBox.UseVisualStyleBackColor = true;
            this.ShowDetailCheckBox.CheckedChanged += new System.EventHandler(this.ShowDetailCheckBox_CheckedChanged);
            // 
            // DetailListBox
            // 
            this.DetailListBox.FormattingEnabled = true;
            this.DetailListBox.HorizontalScrollbar = true;
            this.DetailListBox.ItemHeight = 15;
            this.DetailListBox.Location = new System.Drawing.Point(16, 116);
            this.DetailListBox.Name = "DetailListBox";
            this.DetailListBox.Size = new System.Drawing.Size(401, 214);
            this.DetailListBox.TabIndex = 9;
            // 
            // UploadEMIProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 374);
            this.Controls.Add(this.DetailListBox);
            this.Controls.Add(this.ShowDetailCheckBox);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.UploadProgressBar);
            this.Controls.Add(this.UploadInfoLabel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UploadEMIProgressForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Uploading ...";
            this.Load += new System.EventHandler(this.UploadEMIProgressForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UploadEMIProgressForm_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UploadEMIProgressForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.ProgressBar UploadProgressBar;
        private System.Windows.Forms.Label UploadInfoLabel;
        private System.Windows.Forms.CheckBox ShowDetailCheckBox;
        private System.Windows.Forms.ListBox DetailListBox;
    }
}