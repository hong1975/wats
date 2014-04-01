namespace WatsEMIAnalyzer
{
    partial class MoveTaskForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveTaskForm));
            this.AlertInfoLabel = new System.Windows.Forms.Label();
            this.RegionLabel = new System.Windows.Forms.Label();
            this.RegionNameEditor = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AlertInfoLabel
            // 
            this.AlertInfoLabel.AutoSize = true;
            this.AlertInfoLabel.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AlertInfoLabel.ForeColor = System.Drawing.Color.Red;
            this.AlertInfoLabel.Location = new System.Drawing.Point(13, 16);
            this.AlertInfoLabel.Name = "AlertInfoLabel";
            this.AlertInfoLabel.Size = new System.Drawing.Size(314, 17);
            this.AlertInfoLabel.TabIndex = 0;
            this.AlertInfoLabel.Text = "5 tasks already exist in region, must be moved to";
            // 
            // RegionLabel
            // 
            this.RegionLabel.AutoSize = true;
            this.RegionLabel.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegionLabel.ForeColor = System.Drawing.Color.Red;
            this.RegionLabel.Location = new System.Drawing.Point(13, 46);
            this.RegionLabel.Name = "RegionLabel";
            this.RegionLabel.Size = new System.Drawing.Size(78, 17);
            this.RegionLabel.TabIndex = 1;
            this.RegionLabel.Text = "new region:";
            // 
            // RegionNameEditor
            // 
            this.RegionNameEditor.Location = new System.Drawing.Point(97, 43);
            this.RegionNameEditor.Name = "RegionNameEditor";
            this.RegionNameEditor.Size = new System.Drawing.Size(223, 22);
            this.RegionNameEditor.TabIndex = 2;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(278, 78);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(64, 25);
            this.OKButton.TabIndex = 3;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(208, 78);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(64, 25);
            this.CancelButton.TabIndex = 4;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // MoveTaskForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 111);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.RegionNameEditor);
            this.Controls.Add(this.RegionLabel);
            this.Controls.Add(this.AlertInfoLabel);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MoveTaskForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Alert";
            this.Load += new System.EventHandler(this.MoveTaskAlertForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MoveTaskAlertForm_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MoveTaskAlertForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AlertInfoLabel;
        private System.Windows.Forms.Label RegionLabel;
        private System.Windows.Forms.TextBox RegionNameEditor;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelButton;

    }
}