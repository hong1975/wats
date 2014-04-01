namespace WatsClient.Settings
{
    partial class EquipmentParameterForm
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
            this.EquipmentParameterGrid = new System.Windows.Forms.DataGridView();
            this.TRSpacingColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubBandColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoStartColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoStopColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HiStartColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HiStopColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeftLowBandColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RightLowBandColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeftHighBandColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RightHighBandColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.EquipmentParameterGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // EquipmentParameterGrid
            // 
            this.EquipmentParameterGrid.AllowUserToAddRows = false;
            this.EquipmentParameterGrid.AllowUserToDeleteRows = false;
            this.EquipmentParameterGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.EquipmentParameterGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TRSpacingColumn,
            this.SubBandColumn,
            this.LoStartColumn,
            this.LoStopColumn,
            this.HiStartColumn,
            this.HiStopColumn,
            this.LeftLowBandColumn,
            this.RightLowBandColumn,
            this.LeftHighBandColumn,
            this.RightHighBandColumn});
            this.EquipmentParameterGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EquipmentParameterGrid.Location = new System.Drawing.Point(0, 0);
            this.EquipmentParameterGrid.Name = "EquipmentParameterGrid";
            this.EquipmentParameterGrid.RowTemplate.Height = 23;
            this.EquipmentParameterGrid.Size = new System.Drawing.Size(628, 345);
            this.EquipmentParameterGrid.TabIndex = 1;
            // 
            // TRSpacingColumn
            // 
            this.TRSpacingColumn.HeaderText = "TR Spacing";
            this.TRSpacingColumn.Name = "TRSpacingColumn";
            this.TRSpacingColumn.ReadOnly = true;
            // 
            // SubBandColumn
            // 
            this.SubBandColumn.HeaderText = "Sub Band";
            this.SubBandColumn.Name = "SubBandColumn";
            this.SubBandColumn.ReadOnly = true;
            // 
            // LoStartColumn
            // 
            this.LoStartColumn.HeaderText = "Lo Start";
            this.LoStartColumn.Name = "LoStartColumn";
            this.LoStartColumn.ReadOnly = true;
            // 
            // LoStopColumn
            // 
            this.LoStopColumn.HeaderText = "Lo Stop";
            this.LoStopColumn.Name = "LoStopColumn";
            this.LoStopColumn.ReadOnly = true;
            // 
            // HiStartColumn
            // 
            this.HiStartColumn.HeaderText = "Hi Start";
            this.HiStartColumn.Name = "HiStartColumn";
            this.HiStartColumn.ReadOnly = true;
            // 
            // HiStopColumn
            // 
            this.HiStopColumn.HeaderText = "Hi Stop";
            this.HiStopColumn.Name = "HiStopColumn";
            this.HiStopColumn.ReadOnly = true;
            // 
            // LeftLowBandColumn
            // 
            this.LeftLowBandColumn.HeaderText = "Left Low Band";
            this.LeftLowBandColumn.Name = "LeftLowBandColumn";
            this.LeftLowBandColumn.ReadOnly = true;
            // 
            // RightLowBandColumn
            // 
            this.RightLowBandColumn.HeaderText = "Right Low Band";
            this.RightLowBandColumn.Name = "RightLowBandColumn";
            this.RightLowBandColumn.ReadOnly = true;
            // 
            // LeftHighBandColumn
            // 
            this.LeftHighBandColumn.HeaderText = "Left High Band";
            this.LeftHighBandColumn.Name = "LeftHighBandColumn";
            this.LeftHighBandColumn.ReadOnly = true;
            // 
            // RightHighBandColumn
            // 
            this.RightHighBandColumn.HeaderText = "Right High Band";
            this.RightHighBandColumn.Name = "RightHighBandColumn";
            this.RightHighBandColumn.ReadOnly = true;
            // 
            // EquipmentParameterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 345);
            this.Controls.Add(this.EquipmentParameterGrid);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "EquipmentParameterForm";
            this.Text = "Equipment Parameter";
            ((System.ComponentModel.ISupportInitialize)(this.EquipmentParameterGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView EquipmentParameterGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRSpacingColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubBandColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoStartColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoStopColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn HiStartColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn HiStopColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeftLowBandColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RightLowBandColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeftHighBandColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RightHighBandColumn;
    }
}