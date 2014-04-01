namespace WatsEmiReportTool
{
    partial class ChannelSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChannelSettingForm));
            this.ChannelSettingGrid = new System.Windows.Forms.DataGridView();
            this.NumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CenterFeqColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BandwidthColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartFreqColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndFreqColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ODUSubBandColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PairCHNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PairCenterFeqColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PairBandwidthColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PairStartFreqColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PairEndFreqColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PairODUSubBandColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OKButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ChannelSettingGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ChannelSettingGrid
            // 
            this.ChannelSettingGrid.AllowUserToAddRows = false;
            this.ChannelSettingGrid.AllowUserToDeleteRows = false;
            this.ChannelSettingGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ChannelSettingGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NumberColumn,
            this.CHNameColumn,
            this.CenterFeqColumn,
            this.BandwidthColumn,
            this.StartFreqColumn,
            this.EndFreqColumn,
            this.ODUSubBandColumn,
            this.PairCHNameColumn,
            this.PairCenterFeqColumn,
            this.PairBandwidthColumn,
            this.PairStartFreqColumn,
            this.PairEndFreqColumn,
            this.PairODUSubBandColumn});
            this.ChannelSettingGrid.Location = new System.Drawing.Point(10, 18);
            this.ChannelSettingGrid.Name = "ChannelSettingGrid";
            this.ChannelSettingGrid.ReadOnly = true;
            this.ChannelSettingGrid.RowHeadersVisible = false;
            this.ChannelSettingGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this.ChannelSettingGrid.RowTemplate.Height = 24;
            this.ChannelSettingGrid.Size = new System.Drawing.Size(768, 232);
            this.ChannelSettingGrid.TabIndex = 5;
            // 
            // NumberColumn
            // 
            this.NumberColumn.HeaderText = "No.";
            this.NumberColumn.Name = "NumberColumn";
            this.NumberColumn.ReadOnly = true;
            this.NumberColumn.Width = 30;
            // 
            // CHNameColumn
            // 
            this.CHNameColumn.HeaderText = "CH name";
            this.CHNameColumn.Name = "CHNameColumn";
            this.CHNameColumn.ReadOnly = true;
            // 
            // CenterFeqColumn
            // 
            this.CenterFeqColumn.HeaderText = "Center Freq";
            this.CenterFeqColumn.Name = "CenterFeqColumn";
            this.CenterFeqColumn.ReadOnly = true;
            // 
            // BandwidthColumn
            // 
            this.BandwidthColumn.HeaderText = "Band Width";
            this.BandwidthColumn.Name = "BandwidthColumn";
            this.BandwidthColumn.ReadOnly = true;
            // 
            // StartFreqColumn
            // 
            this.StartFreqColumn.HeaderText = "Start Freq";
            this.StartFreqColumn.Name = "StartFreqColumn";
            this.StartFreqColumn.ReadOnly = true;
            // 
            // EndFreqColumn
            // 
            this.EndFreqColumn.HeaderText = "End Freq";
            this.EndFreqColumn.Name = "EndFreqColumn";
            this.EndFreqColumn.ReadOnly = true;
            // 
            // ODUSubBandColumn
            // 
            this.ODUSubBandColumn.HeaderText = "ODU Sub band";
            this.ODUSubBandColumn.Name = "ODUSubBandColumn";
            this.ODUSubBandColumn.ReadOnly = true;
            // 
            // PairCHNameColumn
            // 
            this.PairCHNameColumn.HeaderText = "CH name";
            this.PairCHNameColumn.Name = "PairCHNameColumn";
            this.PairCHNameColumn.ReadOnly = true;
            // 
            // PairCenterFeqColumn
            // 
            this.PairCenterFeqColumn.HeaderText = "Center Freq";
            this.PairCenterFeqColumn.Name = "PairCenterFeqColumn";
            this.PairCenterFeqColumn.ReadOnly = true;
            // 
            // PairBandwidthColumn
            // 
            this.PairBandwidthColumn.HeaderText = "Band Width";
            this.PairBandwidthColumn.Name = "PairBandwidthColumn";
            this.PairBandwidthColumn.ReadOnly = true;
            // 
            // PairStartFreqColumn
            // 
            this.PairStartFreqColumn.HeaderText = "Start Freq";
            this.PairStartFreqColumn.Name = "PairStartFreqColumn";
            this.PairStartFreqColumn.ReadOnly = true;
            // 
            // PairEndFreqColumn
            // 
            this.PairEndFreqColumn.HeaderText = "End Freq";
            this.PairEndFreqColumn.Name = "PairEndFreqColumn";
            this.PairEndFreqColumn.ReadOnly = true;
            // 
            // PairODUSubBandColumn
            // 
            this.PairODUSubBandColumn.HeaderText = "ODU Sub band";
            this.PairODUSubBandColumn.Name = "PairODUSubBandColumn";
            this.PairODUSubBandColumn.ReadOnly = true;
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(712, 261);
            this.OKButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(66, 26);
            this.OKButton.TabIndex = 7;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // ChannelSettingForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 297);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.ChannelSettingGrid);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ChannelSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Channel Setting";
            ((System.ComponentModel.ISupportInitialize)(this.ChannelSettingGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ChannelSettingGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CenterFeqColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn BandwidthColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartFreqColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndFreqColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ODUSubBandColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PairCHNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PairCenterFeqColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PairBandwidthColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PairStartFreqColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PairEndFreqColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PairODUSubBandColumn;
        private System.Windows.Forms.Button OKButton;
    }
}