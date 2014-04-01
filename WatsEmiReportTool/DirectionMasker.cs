using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEmiReportTool
{
    public class DirectionMasker
    {
        private static DirectionMasker mInstance;
        private Dictionary<int, List<string>> mDirectionTable = new Dictionary<int, List<string>>();
        
        public static DirectionMasker Instance()
        {
            if (mInstance == null)
                mInstance = new DirectionMasker();

            return mInstance;
        }

        public List<List<string>> GetAllCombinateDirectionMasks(List<int> channelNums)
        {
            List<List<string>> allDirectionMasks = new List<List<string>>();
            List<string> curDirectionMasks = new List<string>();

            GetCombinateDirectionMasks(channelNums, 0, 0, allDirectionMasks, curDirectionMasks);

            return allDirectionMasks;
        }

        private void GetCombinateDirectionMasks(List<int> channelNums, int curIndex, int directionMaskIndex,
            List<List<string>> allDirectionMasks, List<string> curDirectionMasks)
        {
            int channelNum = channelNums[curIndex];
            List<string> directionMasks = mDirectionTable[channelNum];
            string directionMask = directionMasks[directionMaskIndex];
            curDirectionMasks.Add(directionMask);

            //深度优先到下一层节点
            bool isRemoved = false;
            if (curIndex + 1 < channelNums.Count)
            {
                GetCombinateDirectionMasks(channelNums, curIndex + 1, 0, allDirectionMasks, curDirectionMasks);
            }
            else
            {
                List<string> copiedDirectionMasks = new List<string>();
                foreach (string mask in curDirectionMasks)
                    copiedDirectionMasks.Add(mask);
                allDirectionMasks.Add(copiedDirectionMasks);
                curDirectionMasks.RemoveAt(curDirectionMasks.Count - 1);
                isRemoved = true;
            }

            if (!isRemoved)
                curDirectionMasks.RemoveAt(curDirectionMasks.Count - 1);

            //到同层下一兄弟节点
            if (directionMaskIndex + 1 < directionMasks.Count)
                GetCombinateDirectionMasks(channelNums, curIndex, directionMaskIndex + 1, allDirectionMasks, curDirectionMasks);
        }

        private DirectionMasker()
        {
            List<string> list1 = new List<string>();
            List<string> list2 = new List<string>();
            List<string> list3 = new List<string>();
            List<string> list4 = new List<string>();

            list1.Add("0");
            list1.Add("1");

            list2.Add("00");
            list2.Add("01");
            list2.Add("10");
            list2.Add("11");

            list3.Add("001");
            list3.Add("010");
            list3.Add("100");
            list3.Add("110");
            list3.Add("101");
            list3.Add("011");

            list4.Add("0011");
            list4.Add("0101");
            list4.Add("0110");
            list4.Add("1001");
            list4.Add("1010");
            list4.Add("1100");

            mDirectionTable.Add(1, list1);
            mDirectionTable.Add(2, list2);
            mDirectionTable.Add(3, list3);
            mDirectionTable.Add(4, list4);
        }
    }
}
