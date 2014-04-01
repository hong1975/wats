namespace WatsEmiReportTool
{
    partial class PairReportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PairReportForm));
            this.ExportButton = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.ChannelCombinationGrid = new System.Windows.Forms.DataGridView();
            this.CombinationNoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LinkIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChannelIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TXColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RXColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResultColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubBandColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HorizontalBPictureBox = new System.Windows.Forms.PictureBox();
            this.VerticalBPictureBox = new System.Windows.Forms.PictureBox();
            this.HorizontalAPictureBox = new System.Windows.Forms.PictureBox();
            this.VerticalAPictureBox = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.EngineerALabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.DateALabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.LongtitudeALabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LatitudeALabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SiteAIDLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.EngineerBLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.DateBLabel = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.LongtitudeBLabel = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.LatitudeBLabel = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.SiteBIDLabel = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.VerticalALabel = new System.Windows.Forms.Label();
            this.VerticalBLabel = new System.Windows.Forms.Label();
            this.HorizontalALabel = new System.Windows.Forms.Label();
            this.HorizontalBLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.FrequencyBandComboxB = new System.Windows.Forms.ComboBox();
            this.FrequencyBandComboxA = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.GraphSpanEditor = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ChannelCombinationGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HorizontalBPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VerticalBPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HorizontalAPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VerticalAPictureBox)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExportButton
            // 
            this.ExportButton.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportButton.Location = new System.Drawing.Point(678, 562);
            this.ExportButton.Margin = new System.Windows.Forms.Padding(2);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(131, 33);
            this.ExportButton.TabIndex = 39;
            this.ExportButton.Text = "Export To Excel";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(5, 365);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(165, 14);
            this.label9.TabIndex = 38;
            this.label9.Text = "Available Channels Combination";
            // 
            // ChannelCombinationGrid
            // 
            this.ChannelCombinationGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ChannelCombinationGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CombinationNoColumn,
            this.LinkIDColumn,
            this.ChannelIDColumn,
            this.TXColumn,
            this.RXColumn,
            this.ResultColumn,
            this.SubBandColumn});
            this.ChannelCombinationGrid.Location = new System.Drawing.Point(10, 385);
            this.ChannelCombinationGrid.Margin = new System.Windows.Forms.Padding(2);
            this.ChannelCombinationGrid.Name = "ChannelCombinationGrid";
            this.ChannelCombinationGrid.ReadOnly = true;
            this.ChannelCombinationGrid.RowHeadersVisible = false;
            this.ChannelCombinationGrid.RowTemplate.Height = 23;
            this.ChannelCombinationGrid.Size = new System.Drawing.Size(799, 170);
            this.ChannelCombinationGrid.TabIndex = 37;
            this.ChannelCombinationGrid.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.ChannelCombinationGrid_CellPainting);
            // 
            // CombinationNoColumn
            // 
            this.CombinationNoColumn.HeaderText = "Combination No.";
            this.CombinationNoColumn.Name = "CombinationNoColumn";
            this.CombinationNoColumn.ReadOnly = true;
            this.CombinationNoColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CombinationNoColumn.Width = 140;
            // 
            // LinkIDColumn
            // 
            this.LinkIDColumn.HeaderText = "Link ID";
            this.LinkIDColumn.Name = "LinkIDColumn";
            this.LinkIDColumn.ReadOnly = true;
            this.LinkIDColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LinkIDColumn.Width = 140;
            // 
            // ChannelIDColumn
            // 
            this.ChannelIDColumn.HeaderText = "Channel ID";
            this.ChannelIDColumn.Name = "ChannelIDColumn";
            this.ChannelIDColumn.ReadOnly = true;
            this.ChannelIDColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ChannelIDColumn.Width = 140;
            // 
            // TXColumn
            // 
            this.TXColumn.HeaderText = "TX(RX)";
            this.TXColumn.Name = "TXColumn";
            this.TXColumn.ReadOnly = true;
            this.TXColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TXColumn.Width = 130;
            // 
            // RXColumn
            // 
            this.RXColumn.HeaderText = "RX(TX)";
            this.RXColumn.Name = "RXColumn";
            this.RXColumn.ReadOnly = true;
            this.RXColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RXColumn.Width = 130;
            // 
            // ResultColumn
            // 
            this.ResultColumn.HeaderText = "Result";
            this.ResultColumn.Name = "ResultColumn";
            this.ResultColumn.ReadOnly = true;
            this.ResultColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ResultColumn.Width = 120;
            // 
            // SubBandColumn
            // 
            this.SubBandColumn.HeaderText = "Sub Band";
            this.SubBandColumn.Name = "SubBandColumn";
            this.SubBandColumn.ReadOnly = true;
            this.SubBandColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SubBandColumn.Width = 120;
            // 
            // HorizontalBPictureBox
            // 
            this.HorizontalBPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.HorizontalBPictureBox.Location = new System.Drawing.Point(571, 211);
            this.HorizontalBPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.HorizontalBPictureBox.Name = "HorizontalBPictureBox";
            this.HorizontalBPictureBox.Size = new System.Drawing.Size(238, 111);
            this.HorizontalBPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.HorizontalBPictureBox.TabIndex = 36;
            this.HorizontalBPictureBox.TabStop = false;
            this.HorizontalBPictureBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.HorizontalBPictureBox_MouseDoubleClick);
            // 
            // VerticalBPictureBox
            // 
            this.VerticalBPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.VerticalBPictureBox.Location = new System.Drawing.Point(328, 211);
            this.VerticalBPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.VerticalBPictureBox.Name = "VerticalBPictureBox";
            this.VerticalBPictureBox.Size = new System.Drawing.Size(238, 111);
            this.VerticalBPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.VerticalBPictureBox.TabIndex = 35;
            this.VerticalBPictureBox.TabStop = false;
            this.VerticalBPictureBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.VerticalBPictureBox_MouseDoubleClick);
            // 
            // HorizontalAPictureBox
            // 
            this.HorizontalAPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.HorizontalAPictureBox.Location = new System.Drawing.Point(571, 53);
            this.HorizontalAPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.HorizontalAPictureBox.Name = "HorizontalAPictureBox";
            this.HorizontalAPictureBox.Size = new System.Drawing.Size(238, 112);
            this.HorizontalAPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.HorizontalAPictureBox.TabIndex = 33;
            this.HorizontalAPictureBox.TabStop = false;
            this.HorizontalAPictureBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.HorizontalAPictureBox_MouseDoubleClick);
            // 
            // VerticalAPictureBox
            // 
            this.VerticalAPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.VerticalAPictureBox.Location = new System.Drawing.Point(328, 53);
            this.VerticalAPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.VerticalAPictureBox.Name = "VerticalAPictureBox";
            this.VerticalAPictureBox.Size = new System.Drawing.Size(238, 112);
            this.VerticalAPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.VerticalAPictureBox.TabIndex = 32;
            this.VerticalAPictureBox.TabStop = false;
            this.VerticalAPictureBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.VerticalAPictureBox_MouseDoubleClick);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.0231F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.9769F));
            this.tableLayoutPanel2.Controls.Add(this.EngineerALabel, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.label10, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.DateALabel, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.LongtitudeALabel, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.LatitudeALabel, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.SiteAIDLabel, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel2.Location = new System.Drawing.Point(10, 53);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(299, 112);
            this.tableLayoutPanel2.TabIndex = 31;
            // 
            // EngineerALabel
            // 
            this.EngineerALabel.AutoSize = true;
            this.EngineerALabel.BackColor = System.Drawing.Color.White;
            this.EngineerALabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EngineerALabel.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EngineerALabel.ForeColor = System.Drawing.Color.Blue;
            this.EngineerALabel.Location = new System.Drawing.Point(96, 92);
            this.EngineerALabel.Margin = new System.Windows.Forms.Padding(3);
            this.EngineerALabel.Name = "EngineerALabel";
            this.EngineerALabel.Size = new System.Drawing.Size(199, 16);
            this.EngineerALabel.TabIndex = 9;
            this.EngineerALabel.Text = "Site ID";
            this.EngineerALabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(4, 92);
            this.label10.Margin = new System.Windows.Forms.Padding(3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 16);
            this.label10.TabIndex = 8;
            this.label10.Text = "Engineer";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DateALabel
            // 
            this.DateALabel.AutoSize = true;
            this.DateALabel.BackColor = System.Drawing.Color.White;
            this.DateALabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DateALabel.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateALabel.ForeColor = System.Drawing.Color.Blue;
            this.DateALabel.Location = new System.Drawing.Point(96, 70);
            this.DateALabel.Margin = new System.Windows.Forms.Padding(3);
            this.DateALabel.Name = "DateALabel";
            this.DateALabel.Size = new System.Drawing.Size(199, 15);
            this.DateALabel.TabIndex = 7;
            this.DateALabel.Text = "Site ID";
            this.DateALabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(4, 70);
            this.label7.Margin = new System.Windows.Forms.Padding(3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 15);
            this.label7.TabIndex = 6;
            this.label7.Text = "Date";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LongtitudeALabel
            // 
            this.LongtitudeALabel.AutoSize = true;
            this.LongtitudeALabel.BackColor = System.Drawing.Color.White;
            this.LongtitudeALabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LongtitudeALabel.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LongtitudeALabel.ForeColor = System.Drawing.Color.Blue;
            this.LongtitudeALabel.Location = new System.Drawing.Point(96, 48);
            this.LongtitudeALabel.Margin = new System.Windows.Forms.Padding(3);
            this.LongtitudeALabel.Name = "LongtitudeALabel";
            this.LongtitudeALabel.Size = new System.Drawing.Size(199, 15);
            this.LongtitudeALabel.TabIndex = 5;
            this.LongtitudeALabel.Text = "Site ID";
            this.LongtitudeALabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 48);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Longtitude";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LatitudeALabel
            // 
            this.LatitudeALabel.AutoSize = true;
            this.LatitudeALabel.BackColor = System.Drawing.Color.White;
            this.LatitudeALabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LatitudeALabel.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LatitudeALabel.ForeColor = System.Drawing.Color.Blue;
            this.LatitudeALabel.Location = new System.Drawing.Point(96, 26);
            this.LatitudeALabel.Margin = new System.Windows.Forms.Padding(3);
            this.LatitudeALabel.Name = "LatitudeALabel";
            this.LatitudeALabel.Size = new System.Drawing.Size(199, 15);
            this.LatitudeALabel.TabIndex = 3;
            this.LatitudeALabel.Text = "Site ID";
            this.LatitudeALabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 26);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Latitude";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SiteAIDLabel
            // 
            this.SiteAIDLabel.AutoSize = true;
            this.SiteAIDLabel.BackColor = System.Drawing.Color.White;
            this.SiteAIDLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SiteAIDLabel.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SiteAIDLabel.ForeColor = System.Drawing.Color.Blue;
            this.SiteAIDLabel.Location = new System.Drawing.Point(96, 4);
            this.SiteAIDLabel.Margin = new System.Windows.Forms.Padding(3);
            this.SiteAIDLabel.Name = "SiteAIDLabel";
            this.SiteAIDLabel.Size = new System.Drawing.Size(199, 15);
            this.SiteAIDLabel.TabIndex = 1;
            this.SiteAIDLabel.Text = "Site ID";
            this.SiteAIDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Site ID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 40;
            this.label2.Text = "Graph Span";
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(172, 11);
            this.UpdateButton.Margin = new System.Windows.Forms.Padding(2);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(73, 23);
            this.UpdateButton.TabIndex = 43;
            this.UpdateButton.Text = "Update";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.0231F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.9769F));
            this.tableLayoutPanel1.Controls.Add(this.EngineerBLabel, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.DateBLabel, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.LongtitudeBLabel, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label14, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.LatitudeBLabel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label16, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.SiteBIDLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label18, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 210);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(299, 112);
            this.tableLayoutPanel1.TabIndex = 44;
            // 
            // EngineerBLabel
            // 
            this.EngineerBLabel.AutoSize = true;
            this.EngineerBLabel.BackColor = System.Drawing.Color.White;
            this.EngineerBLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EngineerBLabel.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EngineerBLabel.ForeColor = System.Drawing.Color.Blue;
            this.EngineerBLabel.Location = new System.Drawing.Point(96, 92);
            this.EngineerBLabel.Margin = new System.Windows.Forms.Padding(3);
            this.EngineerBLabel.Name = "EngineerBLabel";
            this.EngineerBLabel.Size = new System.Drawing.Size(199, 16);
            this.EngineerBLabel.TabIndex = 9;
            this.EngineerBLabel.Text = "Site ID";
            this.EngineerBLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(4, 92);
            this.label8.Margin = new System.Windows.Forms.Padding(3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 16);
            this.label8.TabIndex = 8;
            this.label8.Text = "Engineer";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DateBLabel
            // 
            this.DateBLabel.AutoSize = true;
            this.DateBLabel.BackColor = System.Drawing.Color.White;
            this.DateBLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DateBLabel.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateBLabel.ForeColor = System.Drawing.Color.Blue;
            this.DateBLabel.Location = new System.Drawing.Point(96, 70);
            this.DateBLabel.Margin = new System.Windows.Forms.Padding(3);
            this.DateBLabel.Name = "DateBLabel";
            this.DateBLabel.Size = new System.Drawing.Size(199, 15);
            this.DateBLabel.TabIndex = 7;
            this.DateBLabel.Text = "Site ID";
            this.DateBLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.White;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(4, 70);
            this.label12.Margin = new System.Windows.Forms.Padding(3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 15);
            this.label12.TabIndex = 6;
            this.label12.Text = "Date";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LongtitudeBLabel
            // 
            this.LongtitudeBLabel.AutoSize = true;
            this.LongtitudeBLabel.BackColor = System.Drawing.Color.White;
            this.LongtitudeBLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LongtitudeBLabel.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LongtitudeBLabel.ForeColor = System.Drawing.Color.Blue;
            this.LongtitudeBLabel.Location = new System.Drawing.Point(96, 48);
            this.LongtitudeBLabel.Margin = new System.Windows.Forms.Padding(3);
            this.LongtitudeBLabel.Name = "LongtitudeBLabel";
            this.LongtitudeBLabel.Size = new System.Drawing.Size(199, 15);
            this.LongtitudeBLabel.TabIndex = 5;
            this.LongtitudeBLabel.Text = "Site ID";
            this.LongtitudeBLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.White;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(4, 48);
            this.label14.Margin = new System.Windows.Forms.Padding(3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(85, 15);
            this.label14.TabIndex = 4;
            this.label14.Text = "Longtitude";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LatitudeBLabel
            // 
            this.LatitudeBLabel.AutoSize = true;
            this.LatitudeBLabel.BackColor = System.Drawing.Color.White;
            this.LatitudeBLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LatitudeBLabel.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LatitudeBLabel.ForeColor = System.Drawing.Color.Blue;
            this.LatitudeBLabel.Location = new System.Drawing.Point(96, 26);
            this.LatitudeBLabel.Margin = new System.Windows.Forms.Padding(3);
            this.LatitudeBLabel.Name = "LatitudeBLabel";
            this.LatitudeBLabel.Size = new System.Drawing.Size(199, 15);
            this.LatitudeBLabel.TabIndex = 3;
            this.LatitudeBLabel.Text = "Site ID";
            this.LatitudeBLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.White;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(4, 26);
            this.label16.Margin = new System.Windows.Forms.Padding(3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(85, 15);
            this.label16.TabIndex = 2;
            this.label16.Text = "Latitude";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SiteBIDLabel
            // 
            this.SiteBIDLabel.AutoSize = true;
            this.SiteBIDLabel.BackColor = System.Drawing.Color.White;
            this.SiteBIDLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SiteBIDLabel.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SiteBIDLabel.ForeColor = System.Drawing.Color.Blue;
            this.SiteBIDLabel.Location = new System.Drawing.Point(96, 4);
            this.SiteBIDLabel.Margin = new System.Windows.Forms.Padding(3);
            this.SiteBIDLabel.Name = "SiteBIDLabel";
            this.SiteBIDLabel.Size = new System.Drawing.Size(199, 15);
            this.SiteBIDLabel.TabIndex = 1;
            this.SiteBIDLabel.Text = "Site ID";
            this.SiteBIDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.White;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(4, 4);
            this.label18.Margin = new System.Windows.Forms.Padding(3);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(85, 15);
            this.label18.TabIndex = 0;
            this.label18.Text = "Site ID";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // VerticalALabel
            // 
            this.VerticalALabel.AutoSize = true;
            this.VerticalALabel.Location = new System.Drawing.Point(325, 37);
            this.VerticalALabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.VerticalALabel.Name = "VerticalALabel";
            this.VerticalALabel.Size = new System.Drawing.Size(42, 14);
            this.VerticalALabel.TabIndex = 45;
            this.VerticalALabel.Text = "label6";
            // 
            // VerticalBLabel
            // 
            this.VerticalBLabel.AutoSize = true;
            this.VerticalBLabel.Location = new System.Drawing.Point(325, 194);
            this.VerticalBLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.VerticalBLabel.Name = "VerticalBLabel";
            this.VerticalBLabel.Size = new System.Drawing.Size(42, 14);
            this.VerticalBLabel.TabIndex = 46;
            this.VerticalBLabel.Text = "label6";
            // 
            // HorizontalALabel
            // 
            this.HorizontalALabel.AutoSize = true;
            this.HorizontalALabel.Location = new System.Drawing.Point(571, 37);
            this.HorizontalALabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.HorizontalALabel.Name = "HorizontalALabel";
            this.HorizontalALabel.Size = new System.Drawing.Size(42, 14);
            this.HorizontalALabel.TabIndex = 47;
            this.HorizontalALabel.Text = "label6";
            // 
            // HorizontalBLabel
            // 
            this.HorizontalBLabel.AutoSize = true;
            this.HorizontalBLabel.Location = new System.Drawing.Point(571, 194);
            this.HorizontalBLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.HorizontalBLabel.Name = "HorizontalBLabel";
            this.HorizontalBLabel.Size = new System.Drawing.Size(42, 14);
            this.HorizontalBLabel.TabIndex = 48;
            this.HorizontalBLabel.Text = "label6";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 326);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 14);
            this.label6.TabIndex = 49;
            this.label6.Text = "Frequency Band";
            // 
            // FrequencyBandComboxB
            // 
            this.FrequencyBandComboxB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FrequencyBandComboxB.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FrequencyBandComboxB.FormattingEnabled = true;
            this.FrequencyBandComboxB.Location = new System.Drawing.Point(116, 326);
            this.FrequencyBandComboxB.Margin = new System.Windows.Forms.Padding(2);
            this.FrequencyBandComboxB.Name = "FrequencyBandComboxB";
            this.FrequencyBandComboxB.Size = new System.Drawing.Size(193, 21);
            this.FrequencyBandComboxB.TabIndex = 50;
            this.FrequencyBandComboxB.SelectedIndexChanged += new System.EventHandler(this.FrequencyBandComboxB_SelectedIndexChanged);
            // 
            // FrequencyBandComboxA
            // 
            this.FrequencyBandComboxA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FrequencyBandComboxA.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FrequencyBandComboxA.FormattingEnabled = true;
            this.FrequencyBandComboxA.Location = new System.Drawing.Point(116, 169);
            this.FrequencyBandComboxA.Margin = new System.Windows.Forms.Padding(2);
            this.FrequencyBandComboxA.Name = "FrequencyBandComboxA";
            this.FrequencyBandComboxA.Size = new System.Drawing.Size(193, 21);
            this.FrequencyBandComboxA.TabIndex = 52;
            this.FrequencyBandComboxA.SelectedIndexChanged += new System.EventHandler(this.FrequencyBandComboxA_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 171);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 14);
            this.label11.TabIndex = 51;
            this.label11.Text = "Frequency Band";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(132, 16);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 14);
            this.label4.TabIndex = 42;
            this.label4.Text = "MHz";
            // 
            // GraphSpanEditor
            // 
            this.GraphSpanEditor.Location = new System.Drawing.Point(91, 13);
            this.GraphSpanEditor.Margin = new System.Windows.Forms.Padding(2);
            this.GraphSpanEditor.Name = "GraphSpanEditor";
            this.GraphSpanEditor.Size = new System.Drawing.Size(41, 22);
            this.GraphSpanEditor.TabIndex = 41;
            this.GraphSpanEditor.Text = "300";
            // 
            // PairReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 603);
            this.Controls.Add(this.FrequencyBandComboxA);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.FrequencyBandComboxB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.HorizontalBLabel);
            this.Controls.Add(this.HorizontalALabel);
            this.Controls.Add(this.VerticalBLabel);
            this.Controls.Add(this.VerticalALabel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.GraphSpanEditor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ExportButton);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.ChannelCombinationGrid);
            this.Controls.Add(this.HorizontalBPictureBox);
            this.Controls.Add(this.VerticalBPictureBox);
            this.Controls.Add(this.HorizontalAPictureBox);
            this.Controls.Add(this.VerticalAPictureBox);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PairReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "1 Hop Report Detail";
            this.Load += new System.EventHandler(this.PairReportForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PairReportForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.ChannelCombinationGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HorizontalBPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VerticalBPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HorizontalAPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VerticalAPictureBox)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView ChannelCombinationGrid;
        private System.Windows.Forms.PictureBox HorizontalBPictureBox;
        private System.Windows.Forms.PictureBox VerticalBPictureBox;
        private System.Windows.Forms.PictureBox HorizontalAPictureBox;
        private System.Windows.Forms.PictureBox VerticalAPictureBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label EngineerALabel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label DateALabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label LongtitudeALabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LatitudeALabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label SiteAIDLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label EngineerBLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label DateBLabel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label LongtitudeBLabel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label LatitudeBLabel;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label SiteBIDLabel;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label VerticalALabel;
        private System.Windows.Forms.Label VerticalBLabel;
        private System.Windows.Forms.Label HorizontalALabel;
        private System.Windows.Forms.Label HorizontalBLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox FrequencyBandComboxB;
        private System.Windows.Forms.ComboBox FrequencyBandComboxA;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridViewTextBoxColumn CombinationNoColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LinkIDColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChannelIDColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TXColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RXColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResultColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubBandColumn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox GraphSpanEditor;


    }
}