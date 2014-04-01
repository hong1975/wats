namespace WatsEMIAnalyzer
{
    partial class ScanProgressForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanProgressForm));
            this.ScanInfoLabel = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.ScanProgressBar = new System.Windows.Forms.ProgressBar();
            this.EMICountLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ScanInfoLabel
            // 
            this.ScanInfoLabel.AutoSize = true;
            this.ScanInfoLabel.Location = new System.Drawing.Point(10, 12);
            this.ScanInfoLabel.Name = "ScanInfoLabel";
            this.ScanInfoLabel.Size = new System.Drawing.Size(76, 14);
            this.ScanInfoLabel.TabIndex = 0;
            this.ScanInfoLabel.Text = "Uploading ...";
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(305, 58);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(53, 28);
            this.CancelButton.TabIndex = 2;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // ScanProgressBar
            // 
            this.ScanProgressBar.Location = new System.Drawing.Point(13, 34);
            this.ScanProgressBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ScanProgressBar.Name = "ScanProgressBar";
            this.ScanProgressBar.Size = new System.Drawing.Size(345, 16);
            this.ScanProgressBar.TabIndex = 1;
            // 
            // EMICountLabel
            // 
            this.EMICountLabel.AutoSize = true;
            this.EMICountLabel.Location = new System.Drawing.Point(13, 58);
            this.EMICountLabel.Name = "EMICountLabel";
            this.EMICountLabel.Size = new System.Drawing.Size(125, 14);
            this.EMICountLabel.TabIndex = 3;
            this.EMICountLabel.Text = "0 EMI files was found.";
            // 
            // ScanProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 98);
            this.Controls.Add(this.EMICountLabel);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ScanProgressBar);
            this.Controls.Add(this.ScanInfoLabel);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ScanProgressForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Scanning ...";
            this.Load += new System.EventHandler(this.ScanProgressForm_Load);
            this.Shown += new System.EventHandler(this.ScanProgressForm_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScanProgressForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ScanInfoLabel;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.ProgressBar ScanProgressBar;
        private System.Windows.Forms.Label EMICountLabel;
    }
}