using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WatsEMIAnalyzer
{
    public partial class ExportStatusForm : Form
    {
        private Form mReportForm;
        //private bool mPairReport;

        public ExportStatusForm(Form reportForm/*, bool pairReport*/)
        {
            InitializeComponent();
            mReportForm = reportForm;
            //mPairReport = pairReport;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("Are you sure cancel export ?", 
                "Warninig", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                return;
            }

            ((IExport)mReportForm).CancelExport();

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

            ((IExport)mReportForm).CancelExport();
            
            Hide();
            mReportForm.Visible = true;
        }

        private void ExportStatusForm_Load(object sender, EventArgs e)
        {

        }
    }
}
