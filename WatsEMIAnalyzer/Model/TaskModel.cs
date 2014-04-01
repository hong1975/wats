using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatsEmiReportTool;
using WatsEMIAnalyzer.Bindings;

namespace WatsEMIAnalyzer.Model
{
    [Serializable]
    public class TaskModel:Model
    {
        private List<AnalysisModel> mAnalysisModelList;

        public List<AnalysisModel> AnalysisModelList
        {
            get { return mAnalysisModelList; }
            set { mAnalysisModelList = value; }
        }

        public TaskModel(BindingTask task)
        {
            mTask = task;
        }

        public BindingTask mTask;
    }
}
