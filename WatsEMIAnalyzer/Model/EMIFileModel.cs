using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatsEMIAnalyzer.EMI;

namespace WatsEMIAnalyzer.Model
{
    [Serializable]
    public class EMIFileModel: Model
    {
        public EMIFileData mEmiData;

        public EMIFileModel(EMIFileData data)
        {
            mEmiData = data;
        }
    }
}
