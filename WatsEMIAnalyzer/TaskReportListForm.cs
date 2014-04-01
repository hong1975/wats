using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsEMIAnalyzer.Bindings;

namespace WatsEMIAnalyzer
{
    public partial class TaskReportListForm : Form
    {
        private Reports mReports;

        public TaskReportListForm(Reports reports)
        {
            mReports = reports;
            InitializeComponent();
        }

        private void TaskReportList_Load(object sender, EventArgs e)
        {
            foreach (Report report in mReports.Report)
            {
                ReportListBox.Items.Insert(0, report);
            }
        }

        private void ReportListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ReportListBox.SelectedIndex == -1)
                return;

            Report report = (Report)ReportListBox.SelectedItem;
            ReportViewForm analysisViewForm = new ReportViewForm(report);
            analysisViewForm.ShowDialog();            
        }
    }
}
