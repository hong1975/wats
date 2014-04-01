using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEmiReportTool
{
    public class LinkFrequencyCountMap
    {
        private static LinkFrequencyCountMap mInstance = null;
        public static LinkFrequencyCountMap Instance
        {
            get {
                if (mInstance == null)
                    mInstance = new LinkFrequencyCountMap();

                return mInstance;
            }
        }

        private Dictionary<string, int> mFrequencyCountMap = new Dictionary<string, int>();
        private LinkFrequencyCountMap()
        {
            mFrequencyCountMap.Add("1+0", 1);
            mFrequencyCountMap.Add("2+0", 2);
            mFrequencyCountMap.Add("3+0", 3);
            mFrequencyCountMap.Add("4+0", 4);
            mFrequencyCountMap.Add("1+1FD", 2);
            mFrequencyCountMap.Add("2+1FD", 3);
            mFrequencyCountMap.Add("3+1FD", 4);
            mFrequencyCountMap.Add("1+1HSB", 1);
            mFrequencyCountMap.Add("1+1SD", 1);
            mFrequencyCountMap.Add("2*(1+1SD)", 2);
            mFrequencyCountMap.Add("3*(1+1SD)", 3);
            mFrequencyCountMap.Add("4*(1+1SD)", 4);
        }

        public int GetCount(string config)
        {
            if (!mFrequencyCountMap.ContainsKey(config))
                return 0;

            return mFrequencyCountMap[config];
        }

        public int GetCount(int config)
        {
            int count = 0;
            if ((config & ManualConfigConstants.MC_1_PLUS_0) != 0)
                count += 1;
            if ((config & ManualConfigConstants.MC_2_PLUS_0) != 0)
                count += 2;
            if ((config & ManualConfigConstants.MC_3_PLUS_0) != 0)
                count += 3;
            if ((config & ManualConfigConstants.MC_4_PLUS_0) != 0)
                count += 4;
            if ((config & ManualConfigConstants.MC_1_PLUS_1FD) != 0)
                count += 2;
            if ((config & ManualConfigConstants.MC_2_PLUS_1FD) != 0)
                count += 3;
            if ((config & ManualConfigConstants.MC_3_PLUS_1FD) != 0)
                count += 4;
            if ((config & ManualConfigConstants.MC_1_PLUS_1HSB) != 0)
                count += 1;
            if ((config & ManualConfigConstants.MC_1_PLUS_1SD) != 0)
                count += 1;
            if ((config & ManualConfigConstants.MC_DOUBLE_1_PLUS_1SD) != 0)
                count += 2;
            if ((config & ManualConfigConstants.MC_TRIPLE_1_PLUS_1SD) != 0)
                count += 3;
            if ((config & ManualConfigConstants.MC_FOURTIMES_1_PLUS_1SD) != 0)
                count += 4;

            return count;
        }
    }
}
