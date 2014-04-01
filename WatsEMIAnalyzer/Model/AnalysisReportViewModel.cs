using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WatsEMIAnalyzer.Model
{
    public class AnalysisReportViewModel : Model
    {
        private ReportViewForm mReportViewForm;
        private TreeNode mAnalysisNode; 

        public ReportViewForm ReportForm
        {
            get { return mReportViewForm; }
        }

        public AnalysisReportViewModel(TreeNode analysisNode)
        {
            mAnalysisNode = analysisNode;
            mReportViewForm = new ReportViewForm(analysisNode);
        }
    }
}
