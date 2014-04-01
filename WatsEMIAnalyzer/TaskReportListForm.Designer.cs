namespace WatsEMIAnalyzer
{
    partial class TaskReportListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskReportListForm));
            this.ReportListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // ReportListBox
            // 
            this.ReportListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportListBox.FormattingEnabled = true;
            this.ReportListBox.HorizontalScrollbar = true;
            this.ReportListBox.ItemHeight = 14;
            this.ReportListBox.Location = new System.Drawing.Point(0, 0);
            this.ReportListBox.Name = "ReportListBox";
            this.ReportListBox.Size = new System.Drawing.Size(457, 326);
            this.ReportListBox.TabIndex = 1;
            this.ReportListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ReportListBox_MouseDoubleClick);
            // 
            // TaskReportListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 329);
            this.Controls.Add(this.ReportListBox);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TaskReportListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reports";
            this.Load += new System.EventHandler(this.TaskReportList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox ReportListBox;
    }
}