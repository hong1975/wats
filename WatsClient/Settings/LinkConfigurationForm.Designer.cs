namespace WatsClient.Settings
{
    partial class LinkConfigurationForm
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
            this.LinkConfigurationGrid = new System.Windows.Forms.DataGridView();
            this.LinkNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsParallelLinkColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequiredConfigurationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.LinkConfigurationGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // LinkConfigurationGrid
            // 
            this.LinkConfigurationGrid.AllowUserToAddRows = false;
            this.LinkConfigurationGrid.AllowUserToDeleteRows = false;
            this.LinkConfigurationGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.LinkConfigurationGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LinkNameColumn,
            this.IsParallelLinkColumn,
            this.RequiredConfigurationColumn});
            this.LinkConfigurationGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LinkConfigurationGrid.Location = new System.Drawing.Point(0, 0);
            this.LinkConfigurationGrid.Margin = new System.Windows.Forms.Padding(2);
            this.LinkConfigurationGrid.Name = "LinkConfigurationGrid";
            this.LinkConfigurationGrid.RowHeadersVisible = false;
            this.LinkConfigurationGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this.LinkConfigurationGrid.RowTemplate.Height = 23;
            this.LinkConfigurationGrid.Size = new System.Drawing.Size(527, 426);
            this.LinkConfigurationGrid.TabIndex = 1;
            // 
            // LinkNameColumn
            // 
            this.LinkNameColumn.HeaderText = "Name";
            this.LinkNameColumn.Name = "LinkNameColumn";
            this.LinkNameColumn.ReadOnly = true;
            // 
            // IsParallelLinkColumn
            // 
            this.IsParallelLinkColumn.HeaderText = "Parallel Link";
            this.IsParallelLinkColumn.Name = "IsParallelLinkColumn";
            this.IsParallelLinkColumn.ReadOnly = true;
            // 
            // RequiredConfigurationColumn
            // 
            this.RequiredConfigurationColumn.HeaderText = "Required Configuration";
            this.RequiredConfigurationColumn.Name = "RequiredConfigurationColumn";
            this.RequiredConfigurationColumn.ReadOnly = true;
            this.RequiredConfigurationColumn.Width = 180;
            // 
            // LinkConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 426);
            this.Controls.Add(this.LinkConfigurationGrid);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "LinkConfigurationForm";
            this.Text = "Link Configuration";
            ((System.ComponentModel.ISupportInitialize)(this.LinkConfigurationGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView LinkConfigurationGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn LinkNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsParallelLinkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequiredConfigurationColumn;
    }
}