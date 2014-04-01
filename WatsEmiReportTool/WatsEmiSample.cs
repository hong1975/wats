using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEmiReportTool
{
    public class WatsEmiSample
    {
        public WatsEmiSample(double Freq, double Rssi)
        {
            mFreq = Freq;
            mRssi = Rssi;
        }

        public double mFreq;
        public double mRssi;
    }
}
