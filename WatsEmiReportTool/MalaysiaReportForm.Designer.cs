namespace WatsEmiReportTool
{
    partial class MalaysiaReportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MalaysiaReportForm));
            this.ExportButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.EmiComboBox = new System.Windows.Forms.ComboBox();
            this.AzimuthComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PanelA = new System.Windows.Forms.Panel();
            this.VerticalPictureBox = new System.Windows.Forms.PictureBox();
            this.PanelB = new System.Windows.Forms.Panel();
            this.HorizontalPictureBox = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.LongtitudeLabel = new System.Windows.Forms.Label();
            this.SiteIDLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.Label25 = new System.Windows.Forms.Label();
            this.Label23 = new System.Windows.Forms.Label();
            this.Label24 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.SiteNameLabel = new System.Windows.Forms.Label();
            this.AddressLabel = new System.Windows.Forms.Label();
            this.LatitudeLabel = new System.Windows.Forms.Label();
            this.ScanHeightLabel = new System.Windows.Forms.Label();
            this.DateLabel = new System.Windows.Forms.Label();
            this.PChannelLimitLabel = new System.Windows.Forms.Label();
            this.EngineerLabel = new System.Windows.Forms.Label();
            this.LevelLimitLabel = new System.Windows.Forms.Label();
            this.PanelA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VerticalPictureBox)).BeginInit();
            this.PanelB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HorizontalPictureBox)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExportButton
            // 
            this.ExportButton.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportButton.Location = new System.Drawing.Point(849, 721);
            this.ExportButton.Margin = new System.Windows.Forms.Padding(2);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(110, 32);
            this.ExportButton.TabIndex = 38;
            this.ExportButton.Text = "Export to Word";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 18);
            this.label1.TabIndex = 39;
            this.label1.Text = "EMI File:";
            // 
            // EmiComboBox
            // 
            this.EmiComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EmiComboBox.Font = new System.Drawing.Font("Calibri", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmiComboBox.FormattingEnabled = true;
            this.EmiComboBox.Location = new System.Drawing.Point(79, 11);
            this.EmiComboBox.Name = "EmiComboBox";
            this.EmiComboBox.Size = new System.Drawing.Size(306, 22);
            this.EmiComboBox.TabIndex = 40;
            this.EmiComboBox.SelectedIndexChanged += new System.EventHandler(this.EmiComboBox_SelectedIndexChanged);
            // 
            // AzimuthComboBox
            // 
            this.AzimuthComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AzimuthComboBox.Font = new System.Drawing.Font("Calibri", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AzimuthComboBox.FormattingEnabled = true;
            this.AzimuthComboBox.Location = new System.Drawing.Point(121, 161);
            this.AzimuthComboBox.Name = "AzimuthComboBox";
            this.AzimuthComboBox.Size = new System.Drawing.Size(155, 22);
            this.AzimuthComboBox.TabIndex = 42;
            this.AzimuthComboBox.SelectedIndexChanged += new System.EventHandler(this.AzimuthComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 18);
            this.label2.TabIndex = 41;
            this.label2.Text = "Select Azimuth:";
            // 
            // PanelA
            // 
            this.PanelA.AutoScroll = true;
            this.PanelA.Controls.Add(this.VerticalPictureBox);
            this.PanelA.Location = new System.Drawing.Point(15, 193);
            this.PanelA.Name = "PanelA";
            this.PanelA.Size = new System.Drawing.Size(466, 516);
            this.PanelA.TabIndex = 44;
            // 
            // VerticalPictureBox
            // 
            this.VerticalPictureBox.BackColor = System.Drawing.Color.White;
            this.VerticalPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VerticalPictureBox.Location = new System.Drawing.Point(0, 0);
            this.VerticalPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.VerticalPictureBox.Name = "VerticalPictureBox";
            this.VerticalPictureBox.Size = new System.Drawing.Size(466, 516);
            this.VerticalPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.VerticalPictureBox.TabIndex = 0;
            this.VerticalPictureBox.TabStop = false;
            this.VerticalPictureBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.VerticalPictureBox_MouseDoubleClick);
            // 
            // PanelB
            // 
            this.PanelB.AutoScroll = true;
            this.PanelB.Controls.Add(this.HorizontalPictureBox);
            this.PanelB.Location = new System.Drawing.Point(493, 193);
            this.PanelB.Name = "PanelB";
            this.PanelB.Size = new System.Drawing.Size(466, 516);
            this.PanelB.TabIndex = 45;
            // 
            // HorizontalPictureBox
            // 
            this.HorizontalPictureBox.BackColor = System.Drawing.Color.White;
            this.HorizontalPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HorizontalPictureBox.Location = new System.Drawing.Point(0, 0);
            this.HorizontalPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.HorizontalPictureBox.Name = "HorizontalPictureBox";
            this.HorizontalPictureBox.Size = new System.Drawing.Size(466, 516);
            this.HorizontalPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.HorizontalPictureBox.TabIndex = 1;
            this.HorizontalPictureBox.TabStop = false;
            this.HorizontalPictureBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.HorizontalPictureBox_MouseDoubleClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.66246F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.28014F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.89208F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.26636F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.707233F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.56257F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.610792F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.01837F));
            this.tableLayoutPanel1.Controls.Add(this.LongtitudeLabel, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.SiteIDLabel, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label9, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.Label25, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.Label23, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.Label24, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.label11, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.label12, 7, 3);
            this.tableLayoutPanel1.Controls.Add(this.SiteNameLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.AddressLabel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.LatitudeLabel, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.ScanHeightLabel, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.DateLabel, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.PChannelLimitLabel, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.EngineerLabel, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.LevelLimitLabel, 6, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 42);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(944, 100);
            this.tableLayoutPanel1.TabIndex = 46;
            // 
            // LongtitudeLabel
            // 
            this.LongtitudeLabel.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.LongtitudeLabel, 5);
            this.LongtitudeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LongtitudeLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LongtitudeLabel.ForeColor = System.Drawing.Color.Blue;
            this.LongtitudeLabel.Location = new System.Drawing.Point(600, 40);
            this.LongtitudeLabel.Margin = new System.Windows.Forms.Padding(1);
            this.LongtitudeLabel.Name = "LongtitudeLabel";
            this.LongtitudeLabel.Size = new System.Drawing.Size(342, 16);
            this.LongtitudeLabel.TabIndex = 12;
            this.LongtitudeLabel.Text = "label13";
            this.LongtitudeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SiteIDLabel
            // 
            this.SiteIDLabel.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.SiteIDLabel, 5);
            this.SiteIDLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SiteIDLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SiteIDLabel.ForeColor = System.Drawing.Color.Blue;
            this.SiteIDLabel.Location = new System.Drawing.Point(600, 2);
            this.SiteIDLabel.Margin = new System.Windows.Forms.Padding(1);
            this.SiteIDLabel.Name = "SiteIDLabel";
            this.SiteIDLabel.Size = new System.Drawing.Size(342, 16);
            this.SiteIDLabel.TabIndex = 16;
            this.SiteIDLabel.Text = "label13";
            this.SiteIDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(499, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 17);
            this.label7.TabIndex = 1;
            this.label7.Text = "Longitude";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(31, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Site Name";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(36, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Address";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(36, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Latitude";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(16, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 17);
            this.label6.TabIndex = 3;
            this.label6.Text = "Scan Height(m)";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(25, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 17);
            this.label8.TabIndex = 4;
            this.label8.Text = "Date(y-m-d)";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(510, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 17);
            this.label9.TabIndex = 5;
            this.label9.Text = "Site ID";
            // 
            // Label25
            // 
            this.Label25.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Label25.AutoSize = true;
            this.Label25.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label25.Location = new System.Drawing.Point(483, 58);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(101, 17);
            this.Label25.TabIndex = 6;
            this.Label25.Text = "PCHANNEL Limit";
            // 
            // Label23
            // 
            this.Label23.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Label23.AutoSize = true;
            this.Label23.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label23.Location = new System.Drawing.Point(503, 79);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(60, 17);
            this.Label23.TabIndex = 7;
            this.Label23.Text = "Engineer";
            // 
            // Label24
            // 
            this.Label24.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Label24.AutoSize = true;
            this.Label24.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label24.Location = new System.Drawing.Point(731, 58);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(80, 17);
            this.Label24.TabIndex = 8;
            this.Label24.Text = "PLEVEL Limit";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(681, 58);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 17);
            this.label11.TabIndex = 9;
            this.label11.Text = "dBm";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(910, 58);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(24, 17);
            this.label12.TabIndex = 10;
            this.label12.Text = "dB";
            // 
            // SiteNameLabel
            // 
            this.SiteNameLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SiteNameLabel.AutoSize = true;
            this.SiteNameLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SiteNameLabel.ForeColor = System.Drawing.Color.Blue;
            this.SiteNameLabel.Location = new System.Drawing.Point(268, 1);
            this.SiteNameLabel.Name = "SiteNameLabel";
            this.SiteNameLabel.Size = new System.Drawing.Size(61, 18);
            this.SiteNameLabel.TabIndex = 11;
            this.SiteNameLabel.Text = "label13";
            // 
            // AddressLabel
            // 
            this.AddressLabel.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.AddressLabel, 7);
            this.AddressLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AddressLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressLabel.ForeColor = System.Drawing.Color.Blue;
            this.AddressLabel.Location = new System.Drawing.Point(130, 21);
            this.AddressLabel.Margin = new System.Windows.Forms.Padding(1);
            this.AddressLabel.Name = "AddressLabel";
            this.AddressLabel.Size = new System.Drawing.Size(812, 16);
            this.AddressLabel.TabIndex = 12;
            this.AddressLabel.Text = "label13";
            this.AddressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LatitudeLabel
            // 
            this.LatitudeLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LatitudeLabel.AutoSize = true;
            this.LatitudeLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LatitudeLabel.ForeColor = System.Drawing.Color.Blue;
            this.LatitudeLabel.Location = new System.Drawing.Point(268, 39);
            this.LatitudeLabel.Name = "LatitudeLabel";
            this.LatitudeLabel.Size = new System.Drawing.Size(61, 18);
            this.LatitudeLabel.TabIndex = 13;
            this.LatitudeLabel.Text = "label13";
            // 
            // ScanHeightLabel
            // 
            this.ScanHeightLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ScanHeightLabel.AutoSize = true;
            this.ScanHeightLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScanHeightLabel.ForeColor = System.Drawing.Color.Blue;
            this.ScanHeightLabel.Location = new System.Drawing.Point(289, 58);
            this.ScanHeightLabel.Name = "ScanHeightLabel";
            this.ScanHeightLabel.Size = new System.Drawing.Size(19, 18);
            this.ScanHeightLabel.TabIndex = 14;
            this.ScanHeightLabel.Text = "0";
            // 
            // DateLabel
            // 
            this.DateLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DateLabel.AutoSize = true;
            this.DateLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateLabel.ForeColor = System.Drawing.Color.Blue;
            this.DateLabel.Location = new System.Drawing.Point(268, 77);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(61, 21);
            this.DateLabel.TabIndex = 15;
            this.DateLabel.Text = "label13";
            // 
            // PChannelLimitLabel
            // 
            this.PChannelLimitLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PChannelLimitLabel.AutoSize = true;
            this.PChannelLimitLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PChannelLimitLabel.ForeColor = System.Drawing.Color.Blue;
            this.PChannelLimitLabel.Location = new System.Drawing.Point(607, 58);
            this.PChannelLimitLabel.Name = "PChannelLimitLabel";
            this.PChannelLimitLabel.Size = new System.Drawing.Size(61, 18);
            this.PChannelLimitLabel.TabIndex = 17;
            this.PChannelLimitLabel.Text = "label13";
            // 
            // EngineerLabel
            // 
            this.EngineerLabel.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.EngineerLabel, 5);
            this.EngineerLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EngineerLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EngineerLabel.ForeColor = System.Drawing.Color.Blue;
            this.EngineerLabel.Location = new System.Drawing.Point(600, 78);
            this.EngineerLabel.Margin = new System.Windows.Forms.Padding(1);
            this.EngineerLabel.Name = "EngineerLabel";
            this.EngineerLabel.Size = new System.Drawing.Size(342, 20);
            this.EngineerLabel.TabIndex = 18;
            this.EngineerLabel.Text = "label13";
            this.EngineerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LevelLimitLabel
            // 
            this.LevelLimitLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LevelLimitLabel.AutoSize = true;
            this.LevelLimitLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LevelLimitLabel.ForeColor = System.Drawing.Color.Blue;
            this.LevelLimitLabel.Location = new System.Drawing.Point(830, 58);
            this.LevelLimitLabel.Name = "LevelLimitLabel";
            this.LevelLimitLabel.Size = new System.Drawing.Size(61, 18);
            this.LevelLimitLabel.TabIndex = 19;
            this.LevelLimitLabel.Text = "label13";
            // 
            // MalaysiaReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 764);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.PanelB);
            this.Controls.Add(this.PanelA);
            this.Controls.Add(this.AzimuthComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.EmiComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ExportButton);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MalaysiaReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Malaysia Report Detail";
            this.Load += new System.EventHandler(this.MalaysiaReportForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MalaysiaReportForm_FormClosing);
            this.PanelA.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VerticalPictureBox)).EndInit();
            this.PanelB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HorizontalPictureBox)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox EmiComboBox;
        private System.Windows.Forms.ComboBox AzimuthComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel PanelA;
        private System.Windows.Forms.Panel PanelB;
        private System.Windows.Forms.PictureBox VerticalPictureBox;
        private System.Windows.Forms.PictureBox HorizontalPictureBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label LongtitudeLabel;
        private System.Windows.Forms.Label SiteIDLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label Label25;
        private System.Windows.Forms.Label Label23;
        private System.Windows.Forms.Label Label24;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label SiteNameLabel;
        private System.Windows.Forms.Label AddressLabel;
        private System.Windows.Forms.Label LatitudeLabel;
        private System.Windows.Forms.Label ScanHeightLabel;
        private System.Windows.Forms.Label DateLabel;
        private System.Windows.Forms.Label PChannelLimitLabel;
        private System.Windows.Forms.Label EngineerLabel;
        private System.Windows.Forms.Label LevelLimitLabel;

    }
}