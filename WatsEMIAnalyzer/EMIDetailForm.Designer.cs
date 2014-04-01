namespace WatsEMIAnalyzer
{
    partial class EMIDetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMIDetailForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.DetailInformationTabControl = new System.Windows.Forms.TabControl();
            this.GeneralTabPage = new System.Windows.Forms.TabPage();
            this.GeneralListView = new System.Windows.Forms.ListView();
            this.SiteTabPage = new System.Windows.Forms.TabPage();
            this.SiteListView = new System.Windows.Forms.ListView();
            this.InstrumentsTabPage = new System.Windows.Forms.TabPage();
            this.InstrumentsListView = new System.Windows.Forms.ListView();
            this.SpectrumTabPage = new System.Windows.Forms.TabPage();
            this.MeasureTabPage = new System.Windows.Forms.TabPage();
            this.DataTabPage = new System.Windows.Forms.TabPage();
            this.DataGridView = new System.Windows.Forms.DataGridView();
            this.OKButton = new System.Windows.Forms.Button();
            this.SpectrumListView = new System.Windows.Forms.ListView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PolarizationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AzimuthColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FrequencyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SampleCountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MeasureListView = new System.Windows.Forms.ListView();
            this.tableLayoutPanel1.SuspendLayout();
            this.DetailInformationTabControl.SuspendLayout();
            this.GeneralTabPage.SuspendLayout();
            this.SiteTabPage.SuspendLayout();
            this.InstrumentsTabPage.SuspendLayout();
            this.SpectrumTabPage.SuspendLayout();
            this.MeasureTabPage.SuspendLayout();
            this.DataTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.DetailInformationTabControl, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.OKButton, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(533, 498);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // DetailInformationTabControl
            // 
            this.DetailInformationTabControl.Controls.Add(this.GeneralTabPage);
            this.DetailInformationTabControl.Controls.Add(this.SiteTabPage);
            this.DetailInformationTabControl.Controls.Add(this.InstrumentsTabPage);
            this.DetailInformationTabControl.Controls.Add(this.SpectrumTabPage);
            this.DetailInformationTabControl.Controls.Add(this.MeasureTabPage);
            this.DetailInformationTabControl.Controls.Add(this.DataTabPage);
            this.DetailInformationTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailInformationTabControl.Location = new System.Drawing.Point(3, 3);
            this.DetailInformationTabControl.Name = "DetailInformationTabControl";
            this.DetailInformationTabControl.SelectedIndex = 0;
            this.DetailInformationTabControl.Size = new System.Drawing.Size(527, 452);
            this.DetailInformationTabControl.TabIndex = 0;
            // 
            // GeneralTabPage
            // 
            this.GeneralTabPage.Controls.Add(this.GeneralListView);
            this.GeneralTabPage.Location = new System.Drawing.Point(4, 23);
            this.GeneralTabPage.Name = "GeneralTabPage";
            this.GeneralTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.GeneralTabPage.Size = new System.Drawing.Size(519, 425);
            this.GeneralTabPage.TabIndex = 0;
            this.GeneralTabPage.Text = "General";
            this.GeneralTabPage.UseVisualStyleBackColor = true;
            // 
            // GeneralListView
            // 
            this.GeneralListView.GridLines = true;
            this.GeneralListView.Location = new System.Drawing.Point(15, 15);
            this.GeneralListView.Name = "GeneralListView";
            this.GeneralListView.Size = new System.Drawing.Size(489, 395);
            this.GeneralListView.TabIndex = 1;
            this.GeneralListView.UseCompatibleStateImageBehavior = false;
            this.GeneralListView.View = System.Windows.Forms.View.Details;
            // 
            // SiteTabPage
            // 
            this.SiteTabPage.Controls.Add(this.SiteListView);
            this.SiteTabPage.Location = new System.Drawing.Point(4, 23);
            this.SiteTabPage.Name = "SiteTabPage";
            this.SiteTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.SiteTabPage.Size = new System.Drawing.Size(519, 425);
            this.SiteTabPage.TabIndex = 1;
            this.SiteTabPage.Text = "Site";
            this.SiteTabPage.UseVisualStyleBackColor = true;
            // 
            // SiteListView
            // 
            this.SiteListView.GridLines = true;
            this.SiteListView.Location = new System.Drawing.Point(15, 15);
            this.SiteListView.Name = "SiteListView";
            this.SiteListView.Size = new System.Drawing.Size(489, 395);
            this.SiteListView.TabIndex = 2;
            this.SiteListView.UseCompatibleStateImageBehavior = false;
            this.SiteListView.View = System.Windows.Forms.View.Details;
            // 
            // InstrumentsTabPage
            // 
            this.InstrumentsTabPage.Controls.Add(this.InstrumentsListView);
            this.InstrumentsTabPage.Location = new System.Drawing.Point(4, 23);
            this.InstrumentsTabPage.Name = "InstrumentsTabPage";
            this.InstrumentsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.InstrumentsTabPage.Size = new System.Drawing.Size(519, 425);
            this.InstrumentsTabPage.TabIndex = 2;
            this.InstrumentsTabPage.Text = "Instruments";
            this.InstrumentsTabPage.UseVisualStyleBackColor = true;
            // 
            // InstrumentsListView
            // 
            this.InstrumentsListView.GridLines = true;
            this.InstrumentsListView.Location = new System.Drawing.Point(15, 15);
            this.InstrumentsListView.Name = "InstrumentsListView";
            this.InstrumentsListView.Size = new System.Drawing.Size(489, 395);
            this.InstrumentsListView.TabIndex = 3;
            this.InstrumentsListView.UseCompatibleStateImageBehavior = false;
            this.InstrumentsListView.View = System.Windows.Forms.View.Details;
            // 
            // SpectrumTabPage
            // 
            this.SpectrumTabPage.Controls.Add(this.SpectrumListView);
            this.SpectrumTabPage.Location = new System.Drawing.Point(4, 23);
            this.SpectrumTabPage.Name = "SpectrumTabPage";
            this.SpectrumTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.SpectrumTabPage.Size = new System.Drawing.Size(519, 425);
            this.SpectrumTabPage.TabIndex = 3;
            this.SpectrumTabPage.Text = "Spectrum";
            this.SpectrumTabPage.UseVisualStyleBackColor = true;
            // 
            // MeasureTabPage
            // 
            this.MeasureTabPage.Controls.Add(this.MeasureListView);
            this.MeasureTabPage.Location = new System.Drawing.Point(4, 23);
            this.MeasureTabPage.Name = "MeasureTabPage";
            this.MeasureTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.MeasureTabPage.Size = new System.Drawing.Size(519, 425);
            this.MeasureTabPage.TabIndex = 4;
            this.MeasureTabPage.Text = "Measure";
            this.MeasureTabPage.UseVisualStyleBackColor = true;
            // 
            // DataTabPage
            // 
            this.DataTabPage.Controls.Add(this.DataGridView);
            this.DataTabPage.Location = new System.Drawing.Point(4, 23);
            this.DataTabPage.Name = "DataTabPage";
            this.DataTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.DataTabPage.Size = new System.Drawing.Size(519, 425);
            this.DataTabPage.TabIndex = 5;
            this.DataTabPage.Text = "Data";
            this.DataTabPage.UseVisualStyleBackColor = true;
            // 
            // DataGridView
            // 
            this.DataGridView.AllowUserToAddRows = false;
            this.DataGridView.AllowUserToDeleteRows = false;
            this.DataGridView.AllowUserToResizeRows = false;
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NoColumn,
            this.PolarizationColumn,
            this.AzimuthColumn,
            this.FrequencyColumn,
            this.SampleCountColumn,
            this.TimeColumn});
            this.DataGridView.Location = new System.Drawing.Point(15, 15);
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.RowHeadersVisible = false;
            this.DataGridView.RowTemplate.Height = 20;
            this.DataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView.Size = new System.Drawing.Size(489, 395);
            this.DataGridView.TabIndex = 2;
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.OKButton.Location = new System.Drawing.Point(458, 461);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(72, 34);
            this.OKButton.TabIndex = 1;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // SpectrumListView
            // 
            this.SpectrumListView.GridLines = true;
            this.SpectrumListView.Location = new System.Drawing.Point(15, 15);
            this.SpectrumListView.Name = "SpectrumListView";
            this.SpectrumListView.Size = new System.Drawing.Size(489, 395);
            this.SpectrumListView.TabIndex = 4;
            this.SpectrumListView.UseCompatibleStateImageBehavior = false;
            this.SpectrumListView.View = System.Windows.Forms.View.Details;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "No";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 40;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Polarization";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 80;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Azimuth";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 60;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Frequency";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 200;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Sample Count";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 200;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Time";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 200;
            // 
            // NoColumn
            // 
            this.NoColumn.HeaderText = "No";
            this.NoColumn.Name = "NoColumn";
            this.NoColumn.ReadOnly = true;
            this.NoColumn.Width = 40;
            // 
            // PolarizationColumn
            // 
            this.PolarizationColumn.HeaderText = "Polarization";
            this.PolarizationColumn.Name = "PolarizationColumn";
            this.PolarizationColumn.ReadOnly = true;
            this.PolarizationColumn.Width = 80;
            // 
            // AzimuthColumn
            // 
            this.AzimuthColumn.HeaderText = "Azimuth";
            this.AzimuthColumn.Name = "AzimuthColumn";
            this.AzimuthColumn.ReadOnly = true;
            this.AzimuthColumn.Width = 60;
            // 
            // FrequencyColumn
            // 
            this.FrequencyColumn.HeaderText = "Frequency";
            this.FrequencyColumn.Name = "FrequencyColumn";
            this.FrequencyColumn.ReadOnly = true;
            this.FrequencyColumn.Width = 200;
            // 
            // SampleCountColumn
            // 
            this.SampleCountColumn.HeaderText = "Sample Count";
            this.SampleCountColumn.Name = "SampleCountColumn";
            this.SampleCountColumn.ReadOnly = true;
            this.SampleCountColumn.Width = 200;
            // 
            // TimeColumn
            // 
            this.TimeColumn.HeaderText = "Time";
            this.TimeColumn.Name = "TimeColumn";
            this.TimeColumn.ReadOnly = true;
            this.TimeColumn.Width = 200;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Attribute";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 150;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn8.HeaderText = "Value";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "No";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 40;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "Polarization";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 80;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "Azimuth";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 60;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "Frequency";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Width = 200;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.HeaderText = "Sample Count";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.Width = 200;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.HeaderText = "Time";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.Width = 200;
            // 
            // MeasureListView
            // 
            this.MeasureListView.Location = new System.Drawing.Point(15, 15);
            this.MeasureListView.MultiSelect = false;
            this.MeasureListView.Name = "MeasureListView";
            this.MeasureListView.Size = new System.Drawing.Size(489, 395);
            this.MeasureListView.TabIndex = 5;
            this.MeasureListView.UseCompatibleStateImageBehavior = false;
            this.MeasureListView.View = System.Windows.Forms.View.Details;
            // 
            // EMIDetailForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 498);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Calibri", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EMIDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EMI Detail Information";
            this.Load += new System.EventHandler(this.EMIDetailForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.DetailInformationTabControl.ResumeLayout(false);
            this.GeneralTabPage.ResumeLayout(false);
            this.SiteTabPage.ResumeLayout(false);
            this.InstrumentsTabPage.ResumeLayout(false);
            this.SpectrumTabPage.ResumeLayout(false);
            this.MeasureTabPage.ResumeLayout(false);
            this.DataTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl DetailInformationTabControl;
        private System.Windows.Forms.TabPage GeneralTabPage;
        private System.Windows.Forms.TabPage SiteTabPage;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.TabPage InstrumentsTabPage;
        private System.Windows.Forms.TabPage SpectrumTabPage;
        private System.Windows.Forms.TabPage MeasureTabPage;
        private System.Windows.Forms.TabPage DataTabPage;
        private System.Windows.Forms.DataGridView DataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PolarizationColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AzimuthColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FrequencyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SampleCountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.ListView GeneralListView;
        private System.Windows.Forms.ListView SiteListView;
        private System.Windows.Forms.ListView InstrumentsListView;
        private System.Windows.Forms.ListView SpectrumListView;
        private System.Windows.Forms.ListView MeasureListView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

    }
}