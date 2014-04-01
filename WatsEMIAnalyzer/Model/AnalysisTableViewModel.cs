using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WatsEMIAnalyzer.Model
{
    public class AnalysisTableViewModel : Model
    {
        private TableViewForm mTableViewForm;
        private TreeNode mAnalysisNode; 

        public TableViewForm TableForm
        {
            get { return mTableViewForm; }
        }

        public AnalysisTableViewModel(TreeNode analysisNode)
        {
            mAnalysisNode = analysisNode;
            mTableViewForm = new TableViewForm(analysisNode);
        }
    }
}
