namespace WatsClient.Settings
{
    partial class EMIForm
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
            this.DetailInformationTabControl = new System.Windows.Forms.TabControl();
            this.GeneralTabPage = new System.Windows.Forms.TabPage();
            this.GeneralListView = new System.Windows.Forms.ListView();
            this.SiteTabPage = new System.Windows.Forms.TabPage();
            this.SiteListView = new System.Windows.Forms.ListView();
            this.InstrumentsTabPage = new System.Windows.Forms.TabPage();
            this.InstrumentsListView = new System.Windows.Forms.ListView();
            this.SpectrumTabPage = new System.Windows.Forms.TabPage();
            this.SpectrumListView = new System.Windows.Forms.ListView();
            this.MeasureTabPage = new System.Windows.Forms.TabPage();
            this.MeasureListView = new System.Windows.Forms.ListView();
            this.DataTabPage = new System.Windows.Forms.TabPage();
            this.DataGridView = new System.Windows.Forms.DataGridView();
            this.NoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PolarizationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AzimuthColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FrequencyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SampleCountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            // DetailInformationTabControl
            // 
            this.DetailInformationTabControl.Controls.Add(this.GeneralTabPage);
            this.DetailInformationTabControl.Controls.Add(this.SiteTabPage);
            this.DetailInformationTabControl.Controls.Add(this.InstrumentsTabPage);
            this.DetailInformationTabControl.Controls.Add(this.SpectrumTabPage);
            this.DetailInformationTabControl.Controls.Add(this.MeasureTabPage);
            this.DetailInformationTabControl.Controls.Add(this.DataTabPage);
            this.DetailInformationTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailInformationTabControl.Location = new System.Drawing.Point(0, 0);
            this.DetailInformationTabControl.Name = "DetailInformationTabControl";
            this.DetailInformationTabControl.SelectedIndex = 0;
            this.DetailInformationTabControl.Size = new System.Drawing.Size(645, 547);
            this.DetailInformationTabControl.TabIndex = 1;
            // 
            // GeneralTabPage
            // 
            this.GeneralTabPage.Controls.Add(this.GeneralListView);
            this.GeneralTabPage.Location = new System.Drawing.Point(4, 23);
            this.GeneralTabPage.Name = "GeneralTabPage";
            this.GeneralTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.GeneralTabPage.Size = new System.Drawing.Size(637, 520);
            this.GeneralTabPage.TabIndex = 0;
            this.GeneralTabPage.Text = "General";
            this.GeneralTabPage.UseVisualStyleBackColor = true;
            // 
            // GeneralListView
            // 
            this.GeneralListView.GridLines = true;
            this.GeneralListView.Location = new System.Drawing.Point(15, 17);
            this.GeneralListView.Name = "GeneralListView";
            this.GeneralListView.Size = new System.Drawing.Size(489, 460);
            this.GeneralListView.TabIndex = 1;
            this.GeneralListView.UseCompatibleStateImageBehavior = false;
            this.GeneralListView.View = System.Windows.Forms.View.Details;
            // 
            // SiteTabPage
            // 
            this.SiteTabPage.Controls.Add(this.SiteListView);
            this.SiteTabPage.Location = new System.Drawing.Point(4, 22);
            this.SiteTabPage.Name = "SiteTabPage";
            this.SiteTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.SiteTabPage.Size = new System.Drawing.Size(637, 521);
            this.SiteTabPage.TabIndex = 1;
            this.SiteTabPage.Text = "Site";
            this.SiteTabPage.UseVisualStyleBackColor = true;
            // 
            // SiteListView
            // 
            this.SiteListView.GridLines = true;
            this.SiteListView.Location = new System.Drawing.Point(15, 17);
            this.SiteListView.Name = "SiteListView";
            this.SiteListView.Size = new System.Drawing.Size(489, 460);
            this.SiteListView.TabIndex = 2;
            this.SiteListView.UseCompatibleStateImageBehavior = false;
            this.SiteListView.View = System.Windows.Forms.View.Details;
            // 
            // InstrumentsTabPage
            // 
            this.InstrumentsTabPage.Controls.Add(this.InstrumentsListView);
            this.InstrumentsTabPage.Location = new System.Drawing.Point(4, 22);
            this.InstrumentsTabPage.Name = "InstrumentsTabPage";
            this.InstrumentsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.InstrumentsTabPage.Size = new System.Drawing.Size(637, 521);
            this.InstrumentsTabPage.TabIndex = 2;
            this.InstrumentsTabPage.Text = "Instruments";
            this.InstrumentsTabPage.UseVisualStyleBackColor = true;
            // 
            // InstrumentsListView
            // 
            this.InstrumentsListView.GridLines = true;
            this.InstrumentsListView.Location = new System.Drawing.Point(15, 17);
            this.InstrumentsListView.Name = "InstrumentsListView";
            this.InstrumentsListView.Size = new System.Drawing.Size(489, 460);
            this.InstrumentsListView.TabIndex = 3;
            this.InstrumentsListView.UseCompatibleStateImageBehavior = false;
            this.InstrumentsListView.View = System.Windows.Forms.View.Details;
            // 
            // SpectrumTabPage
            // 
            this.SpectrumTabPage.Controls.Add(this.SpectrumListView);
            this.SpectrumTabPage.Location = new System.Drawing.Point(4, 22);
            this.SpectrumTabPage.Name = "SpectrumTabPage";
            this.SpectrumTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.SpectrumTabPage.Size = new System.Drawing.Size(637, 521);
            this.SpectrumTabPage.TabIndex = 3;
            this.SpectrumTabPage.Text = "Spectrum";
            this.SpectrumTabPage.UseVisualStyleBackColor = true;
            // 
            // SpectrumListView
            // 
            this.SpectrumListView.GridLines = true;
            this.SpectrumListView.Location = new System.Drawing.Point(15, 17);
            this.SpectrumListView.Name = "SpectrumListView";
            this.SpectrumListView.Size = new System.Drawing.Size(489, 460);
            this.SpectrumListView.TabIndex = 4;
            this.SpectrumListView.UseCompatibleStateImageBehavior = false;
            this.SpectrumListView.View = System.Windows.Forms.View.Details;
            // 
            // MeasureTabPage
            // 
            this.MeasureTabPage.Controls.Add(this.MeasureListView);
            this.MeasureTabPage.Location = new System.Drawing.Point(4, 22);
            this.MeasureTabPage.Name = "MeasureTabPage";
            this.MeasureTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.MeasureTabPage.Size = new System.Drawing.Size(637, 521);
            this.MeasureTabPage.TabIndex = 4;
            this.MeasureTabPage.Text = "Measure";
            this.MeasureTabPage.UseVisualStyleBackColor = true;
            // 
            // MeasureListView
            // 
            this.MeasureListView.Location = new System.Drawing.Point(15, 17);
            this.MeasureListView.MultiSelect = false;
            this.MeasureListView.Name = "MeasureListView";
            this.MeasureListView.Size = new System.Drawing.Size(489, 460);
            this.MeasureListView.TabIndex = 5;
            this.MeasureListView.UseCompatibleStateImageBehavior = false;
            this.MeasureListView.View = System.Windows.Forms.View.Details;
            // 
            // DataTabPage
            // 
            this.DataTabPage.Controls.Add(this.DataGridView);
            this.DataTabPage.Location = new System.Drawing.Point(4, 22);
            this.DataTabPage.Name = "DataTabPage";
            this.DataTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.DataTabPage.Size = new System.Drawing.Size(637, 521);
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
            this.DataGridView.Location = new System.Drawing.Point(15, 17);
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.RowHeadersVisible = false;
            this.DataGridView.RowTemplate.Height = 20;
            this.DataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView.Size = new System.Drawing.Size(489, 461);
            this.DataGridView.TabIndex = 2;
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
            // EMIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 547);
            this.Controls.Add(this.DetailInformationTabControl);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "EMIForm";
            this.Text = "EMI";
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

        private System.Windows.Forms.TabControl DetailInformationTabControl;
        private System.Windows.Forms.TabPage GeneralTabPage;
        private System.Windows.Forms.ListView GeneralListView;
        private System.Windows.Forms.TabPage SiteTabPage;
        private System.Windows.Forms.ListView SiteListView;
        private System.Windows.Forms.TabPage InstrumentsTabPage;
        private System.Windows.Forms.ListView InstrumentsListView;
        private System.Windows.Forms.TabPage SpectrumTabPage;
        private System.Windows.Forms.ListView SpectrumListView;
        private System.Windows.Forms.TabPage MeasureTabPage;
        private System.Windows.Forms.ListView MeasureListView;
        private System.Windows.Forms.TabPage DataTabPage;
        private System.Windows.Forms.DataGridView DataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PolarizationColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AzimuthColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FrequencyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SampleCountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeColumn;
    }
}