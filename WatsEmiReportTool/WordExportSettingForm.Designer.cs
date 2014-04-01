namespace WatsEmiReportTool
{
    partial class WordExportSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WordExportSettingForm));
            this.CancelButton = new System.Windows.Forms.Button();
            this.Office2003RadioButton = new System.Windows.Forms.RadioButton();
            this.OKButton = new System.Windows.Forms.Button();
            this.Office2007RadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(74, 83);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(60, 28);
            this.CancelButton.TabIndex = 7;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // Office2003RadioButton
            // 
            this.Office2003RadioButton.AutoSize = true;
            this.Office2003RadioButton.Checked = true;
            this.Office2003RadioButton.Location = new System.Drawing.Point(21, 21);
            this.Office2003RadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.Office2003RadioButton.Name = "Office2003RadioButton";
            this.Office2003RadioButton.Size = new System.Drawing.Size(150, 22);
            this.Office2003RadioButton.TabIndex = 0;
            this.Office2003RadioButton.TabStop = true;
            this.Office2003RadioButton.Text = "Office97, Office 2003";
            this.Office2003RadioButton.UseVisualStyleBackColor = true;
            this.Office2003RadioButton.CheckedChanged += new System.EventHandler(this.Office2003RadioButton_CheckedChanged);
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(139, 83);
            this.OKButton.Margin = new System.Windows.Forms.Padding(2);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(60, 28);
            this.OKButton.TabIndex = 6;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // Office2007RadioButton
            // 
            this.Office2007RadioButton.AutoSize = true;
            this.Office2007RadioButton.Location = new System.Drawing.Point(21, 43);
            this.Office2007RadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.Office2007RadioButton.Name = "Office2007RadioButton";
            this.Office2007RadioButton.Size = new System.Drawing.Size(149, 22);
            this.Office2007RadioButton.TabIndex = 1;
            this.Office2007RadioButton.Text = "Office2007 or higher";
            this.Office2007RadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Office2003RadioButton);
            this.groupBox1.Controls.Add(this.Office2007RadioButton);
            this.groupBox1.Location = new System.Drawing.Point(9, 5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(190, 70);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Format";
            // 
            // WordExportSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 116);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Calibri", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WordExportSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Word Setting";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.RadioButton Office2003RadioButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.RadioButton Office2007RadioButton;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}