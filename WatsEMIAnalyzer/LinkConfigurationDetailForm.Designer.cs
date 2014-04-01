namespace WatsEMIAnalyzer
{
    partial class LinkConfigurationDetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkConfigurationDetailForm));
            this.LinkConfigurationGrid = new System.Windows.Forms.DataGridView();
            this.LinkNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsParallelLinkColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequiredConfigurationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OKButton = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.LinkConfigurationGrid.Location = new System.Drawing.Point(10, 18);
            this.LinkConfigurationGrid.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.LinkConfigurationGrid.Name = "LinkConfigurationGrid";
            this.LinkConfigurationGrid.RowHeadersVisible = false;
            this.LinkConfigurationGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this.LinkConfigurationGrid.RowTemplate.Height = 23;
            this.LinkConfigurationGrid.Size = new System.Drawing.Size(358, 111);
            this.LinkConfigurationGrid.TabIndex = 0;
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
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(306, 135);
            this.OKButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(62, 25);
            this.OKButton.TabIndex = 8;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Parallel Link";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Required Configuration";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 180;
            // 
            // LinkConfigurationDetailForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 167);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.LinkConfigurationGrid);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LinkConfigurationDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Link Configuration Detail";
            ((System.ComponentModel.ISupportInitialize)(this.LinkConfigurationGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView LinkConfigurationGrid;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn LinkNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsParallelLinkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequiredConfigurationColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}