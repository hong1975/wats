using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEmiReportTool
{
    public class WatsEmiData
    {
        public List<WatsEmiSample> mHSamples = new List<WatsEmiSample>();
        public List<WatsEmiSample> mHPairSamples = new List<WatsEmiSample>();
        public List<WatsEmiSample> mVSamples = new List<WatsEmiSample>();
        public List<WatsEmiSample> mVPairSamples = new List<WatsEmiSample>();
    }
}
