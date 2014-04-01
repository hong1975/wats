using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WatsEmiReportTool
{
    public partial class ExportStatusForm : Form
    {
        private Form mReportForm;

        public ExportStatusForm(Form reportForm)
        {
            InitializeComponent();
            mReportForm = reportForm;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("Are you sure cancel export ?", 
                "Warninig", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                return;
            }

            if (mReportForm is ReportForm)
                ((ReportForm)mReportForm).CancelExport();
            else
                ((PairReportForm)mReportForm).CancelExport();

            Hide();
            mReportForm.Visible = true;
        }

        public void UpdateStatus(string status)
        {
            if (status.Equals("Finished"))
            {
                Hide();
                mReportForm.Visible = true;
            }
            else
            {
                StatusLabel.Text = status;
            }

        }

        private void ExportStatusForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("Are you sure cancel export ?",
                "Warninig", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                e.Cancel = true;
                return;
            }

            if (mReportForm is ReportForm)
                ((ReportForm)mReportForm).CancelExport();
            else if (mReportForm is PairReportForm)
                ((PairReportForm)mReportForm).CancelExport();
            else
                ((MalaysiaReportForm)mReportForm).CancelExport();
            Hide();
            mReportForm.Visible = true;
        }

        private void ExportStatusForm_Load(object sender, EventArgs e)
        {

        }
    }
}
