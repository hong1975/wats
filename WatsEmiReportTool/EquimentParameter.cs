using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEmiReportTool
{
    [Serializable]
    public class EquipmentParameter
    {
        public double TRSpacing;
        public string SubBand;
        public double LoStart;
        public double LoStop;
        public double HiStart;
        public double HiStop;
        public double LeftLowBand;
        public double RightLowBand;
        public double LeftHighBand;
        public double RightHighBand;
    }
}
