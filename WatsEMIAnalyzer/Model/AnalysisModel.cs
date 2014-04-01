using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEMIAnalyzer.Model
{
    [Serializable]
    public class AnalysisModel : Model
    {
        private string mName;

        private AnalysisTableViewModel mTableViewModel;
        private AnalysisMapViewModel mMapViewModel;
        private AnalysisReportViewModel mReportViewModel;

        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        public AnalysisTableViewModel TableViewModel
        {
            get { return mTableViewModel; }
            set { mTableViewModel = value; }
        }

        public AnalysisMapViewModel MapViewModel
        {
            get { return mMapViewModel; }
            set { mMapViewModel = value; }
        }

        public AnalysisReportViewModel ReportViewModel
        {
            get { return mReportViewModel; }
            set { mReportViewModel = value; }
        }
    }
}
