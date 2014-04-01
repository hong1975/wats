namespace WatsEmiReportTool
{
    partial class LimitSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LimitSettingForm));
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.DeltaPowerCheckBox = new System.Windows.Forms.CheckBox();
            this.ChannelPowerCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DeltaPowerLimitEditor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ChannelPowerLimitEditor = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(127, 82);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(68, 29);
            this.CancelButton.TabIndex = 0;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(201, 82);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(68, 29);
            this.OKButton.TabIndex = 1;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // DeltaPowerCheckBox
            // 
            this.DeltaPowerCheckBox.AutoSize = true;
            this.DeltaPowerCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeltaPowerCheckBox.Location = new System.Drawing.Point(15, 42);
            this.DeltaPowerCheckBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.DeltaPowerCheckBox.Name = "DeltaPowerCheckBox";
            this.DeltaPowerCheckBox.Size = new System.Drawing.Size(137, 22);
            this.DeltaPowerCheckBox.TabIndex = 20;
            this.DeltaPowerCheckBox.Text = "Delta Power     ≤";
            this.DeltaPowerCheckBox.UseVisualStyleBackColor = true;
            // 
            // ChannelPowerCheckBox
            // 
            this.ChannelPowerCheckBox.AutoSize = true;
            this.ChannelPowerCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChannelPowerCheckBox.Location = new System.Drawing.Point(15, 19);
            this.ChannelPowerCheckBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ChannelPowerCheckBox.Name = "ChannelPowerCheckBox";
            this.ChannelPowerCheckBox.Size = new System.Drawing.Size(141, 22);
            this.ChannelPowerCheckBox.TabIndex = 19;
            this.ChannelPowerCheckBox.Text = "Channel Power ≤";
            this.ChannelPowerCheckBox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(208, 44);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 18);
            this.label5.TabIndex = 18;
            this.label5.Text = "dB";
            // 
            // DeltaPowerLimitEditor
            // 
            this.DeltaPowerLimitEditor.Location = new System.Drawing.Point(164, 40);
            this.DeltaPowerLimitEditor.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.DeltaPowerLimitEditor.Name = "DeltaPowerLimitEditor";
            this.DeltaPowerLimitEditor.Size = new System.Drawing.Size(41, 26);
            this.DeltaPowerLimitEditor.TabIndex = 17;
            this.DeltaPowerLimitEditor.Text = "-3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(208, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 18);
            this.label4.TabIndex = 16;
            this.label4.Text = "dBm";
            // 
            // ChannelPowerLimitEditor
            // 
            this.ChannelPowerLimitEditor.Location = new System.Drawing.Point(164, 17);
            this.ChannelPowerLimitEditor.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ChannelPowerLimitEditor.Name = "ChannelPowerLimitEditor";
            this.ChannelPowerLimitEditor.Size = new System.Drawing.Size(41, 26);
            this.ChannelPowerLimitEditor.TabIndex = 15;
            this.ChannelPowerLimitEditor.Text = "-85";
            // 
            // LimitSettingForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 123);
            this.Controls.Add(this.DeltaPowerCheckBox);
            this.Controls.Add(this.ChannelPowerCheckBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.DeltaPowerLimitEditor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ChannelPowerLimitEditor);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.CancelButton);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LimitSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Limit Setting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LimitSettingForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.CheckBox DeltaPowerCheckBox;
        private System.Windows.Forms.CheckBox ChannelPowerCheckBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox DeltaPowerLimitEditor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ChannelPowerLimitEditor;
    }
}