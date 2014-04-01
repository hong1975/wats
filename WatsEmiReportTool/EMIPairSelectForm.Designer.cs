namespace WatsEmiReportTool
{
    partial class EMIPairSelectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMIPairSelectForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.EMIComboxA = new System.Windows.Forms.ComboBox();
            this.EMIComboxB = new System.Windows.Forms.ComboBox();
            this.AngleLabel = new System.Windows.Forms.Label();
            this.AzimuthALabel = new System.Windows.Forms.Label();
            this.AzimuthBLabel = new System.Windows.Forms.Label();
            this.AzimuthComboxA = new System.Windows.Forms.ComboBox();
            this.AzimuthComboxB = new System.Windows.Forms.ComboBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Site A";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 44);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Site B";
            // 
            // EMIComboxA
            // 
            this.EMIComboxA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EMIComboxA.DropDownWidth = 600;
            this.EMIComboxA.FormattingEnabled = true;
            this.EMIComboxA.Location = new System.Drawing.Point(58, 15);
            this.EMIComboxA.Margin = new System.Windows.Forms.Padding(2);
            this.EMIComboxA.Name = "EMIComboxA";
            this.EMIComboxA.Size = new System.Drawing.Size(239, 26);
            this.EMIComboxA.TabIndex = 2;
            this.EMIComboxA.SelectedIndexChanged += new System.EventHandler(this.EMIComboxA_SelectedIndexChanged);
            // 
            // EMIComboxB
            // 
            this.EMIComboxB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EMIComboxB.DropDownWidth = 600;
            this.EMIComboxB.FormattingEnabled = true;
            this.EMIComboxB.Location = new System.Drawing.Point(57, 41);
            this.EMIComboxB.Margin = new System.Windows.Forms.Padding(2);
            this.EMIComboxB.Name = "EMIComboxB";
            this.EMIComboxB.Size = new System.Drawing.Size(240, 26);
            this.EMIComboxB.TabIndex = 3;
            this.EMIComboxB.SelectedIndexChanged += new System.EventHandler(this.EMIComboxB_SelectedIndexChanged);
            // 
            // AngleLabel
            // 
            this.AngleLabel.AutoSize = true;
            this.AngleLabel.Location = new System.Drawing.Point(10, 86);
            this.AngleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.AngleLabel.Name = "AngleLabel";
            this.AngleLabel.Size = new System.Drawing.Size(127, 18);
            this.AngleLabel.TabIndex = 4;
            this.AngleLabel.Text = "Opposite Azimuth: ";
            // 
            // AzimuthALabel
            // 
            this.AzimuthALabel.AutoSize = true;
            this.AzimuthALabel.Location = new System.Drawing.Point(23, 111);
            this.AzimuthALabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.AzimuthALabel.Name = "AzimuthALabel";
            this.AzimuthALabel.Size = new System.Drawing.Size(72, 18);
            this.AzimuthALabel.TabIndex = 5;
            this.AzimuthALabel.Text = "Azimuth A";
            // 
            // AzimuthBLabel
            // 
            this.AzimuthBLabel.AutoSize = true;
            this.AzimuthBLabel.Location = new System.Drawing.Point(168, 111);
            this.AzimuthBLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.AzimuthBLabel.Name = "AzimuthBLabel";
            this.AzimuthBLabel.Size = new System.Drawing.Size(71, 18);
            this.AzimuthBLabel.TabIndex = 6;
            this.AzimuthBLabel.Text = "Azimuth B";
            // 
            // AzimuthComboxA
            // 
            this.AzimuthComboxA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AzimuthComboxA.DropDownWidth = 100;
            this.AzimuthComboxA.FormattingEnabled = true;
            this.AzimuthComboxA.Location = new System.Drawing.Point(98, 109);
            this.AzimuthComboxA.Margin = new System.Windows.Forms.Padding(2);
            this.AzimuthComboxA.Name = "AzimuthComboxA";
            this.AzimuthComboxA.Size = new System.Drawing.Size(60, 26);
            this.AzimuthComboxA.TabIndex = 7;
            this.AzimuthComboxA.SelectedIndexChanged += new System.EventHandler(this.AzimuthComboxA_SelectedIndexChanged);
            // 
            // AzimuthComboxB
            // 
            this.AzimuthComboxB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AzimuthComboxB.DropDownWidth = 100;
            this.AzimuthComboxB.FormattingEnabled = true;
            this.AzimuthComboxB.Location = new System.Drawing.Point(237, 109);
            this.AzimuthComboxB.Margin = new System.Windows.Forms.Padding(2);
            this.AzimuthComboxB.Name = "AzimuthComboxB";
            this.AzimuthComboxB.Size = new System.Drawing.Size(60, 26);
            this.AzimuthComboxB.TabIndex = 8;
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(230, 146);
            this.OKButton.Margin = new System.Windows.Forms.Padding(2);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(66, 27);
            this.OKButton.TabIndex = 9;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(159, 146);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(66, 27);
            this.CancelButton.TabIndex = 10;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // EMIPairSelectForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 182);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.AzimuthComboxB);
            this.Controls.Add(this.AzimuthComboxA);
            this.Controls.Add(this.AzimuthBLabel);
            this.Controls.Add(this.AzimuthALabel);
            this.Controls.Add(this.AngleLabel);
            this.Controls.Add(this.EMIComboxB);
            this.Controls.Add(this.EMIComboxA);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "EMIPairSelectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pair Selection";
            this.Load += new System.EventHandler(this.EMIPairSelectForm_Load);
            this.Shown += new System.EventHandler(this.EMIPairSelectForm_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EMIPairSelectForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox EMIComboxA;
        private System.Windows.Forms.ComboBox EMIComboxB;
        private System.Windows.Forms.Label AngleLabel;
        private System.Windows.Forms.Label AzimuthALabel;
        private System.Windows.Forms.Label AzimuthBLabel;
        private System.Windows.Forms.ComboBox AzimuthComboxA;
        private System.Windows.Forms.ComboBox AzimuthComboxB;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelButton;
    }
}