using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEmiReportTool
{
    public class ManualConfigConstants
    {
        public const int MC_1_PLUS_0 = 1;
        public const int MC_2_PLUS_0 = 1 << 1;
        public const int MC_3_PLUS_0 = 1 << 2;
        public const int MC_4_PLUS_0 = 1 << 3;
        public const int MC_1_PLUS_1FD = 1 << 4;
        public const int MC_2_PLUS_1FD = 1 << 5;
        public const int MC_3_PLUS_1FD = 1 << 6;
        public const int MC_1_PLUS_1HSB = 1 << 7;
        public const int MC_1_PLUS_1SD = 1 << 8;
        public const int MC_DOUBLE_1_PLUS_1SD = 1 << 9;
        public const int MC_TRIPLE_1_PLUS_1SD = 1 << 10;
        public const int MC_FOURTIMES_1_PLUS_1SD = 1 << 11;

        public static string GetRequiredConfiguration(int manualConfig)
        {
            if (manualConfig == MC_1_PLUS_0)
                return "1+0";
            else if (manualConfig == MC_2_PLUS_0)
                return "2+0";
            else if (manualConfig == MC_3_PLUS_0)
                return "3+0";
            else if (manualConfig == MC_4_PLUS_0)
                return "4+0";
            else if (manualConfig == MC_1_PLUS_1FD)
                return "1+1FD";
            else if (manualConfig == MC_2_PLUS_1FD)
                return "2+1FD";
            else if (manualConfig == MC_3_PLUS_1FD)
                return "3+1FD";
            else if (manualConfig == MC_1_PLUS_1HSB)
                return "1+1HSB";
            else if (manualConfig == MC_1_PLUS_1SD)
                return "1+1SD";
            else if (manualConfig == MC_DOUBLE_1_PLUS_1SD)
                return "2*(1+1SD)";
            else if (manualConfig == MC_TRIPLE_1_PLUS_1SD)
                return "3*(1+1SD)";
            else if (manualConfig == MC_FOURTIMES_1_PLUS_1SD)
                return "4*(1+1SD)";
            else
                return "";
        }
    }
}
