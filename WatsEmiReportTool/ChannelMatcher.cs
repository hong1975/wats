using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEmiReportTool
{
    public class ChannelMatcher
    {
        private static ChannelMatcher instance;

        private List<ChannelSetting> mChannels;
        private Dictionary<string, EquipmentParameter> mEquipmentParameters;
        private HashSet<string> mMatchedChannels = new HashSet<string>();

        public static ChannelMatcher Instance
        {
            get 
            {
                if (instance == null)
                    instance = new ChannelMatcher();

                return instance;
            }
        }

        public List<ChannelSetting> Channels
        {
            set 
            {
                if (mChannels == value)
                    return;

                mChannels = value;
                DoMatch();
            }
        }

        public Dictionary<string, EquipmentParameter> EquipmentParameters
        {
            set 
            {
                if (mEquipmentParameters == value)
                    return;
                
                mEquipmentParameters = value;
                DoMatch();
            }
        }

        public bool IsMatch(string channel1Name, string channel2Name)
        {
            return mMatchedChannels.Contains(channel1Name + channel2Name);
        }

        private HashSet<string> GetSameSubBands(List<string> subBands1, List<string> subBands2)
        {
            HashSet<string> sameSubBands = new HashSet<string>();
            foreach (string subBand1 in subBands1)
                foreach (string subBand2 in subBands2)
                    if (subBand1.Equals(subBand2) && !sameSubBands.Contains(subBand1))
                        sameSubBands.Add(subBand1);

            return sameSubBands;
        }

        private void DoMatch()
        {
            mMatchedChannels.Clear();
            if (mEquipmentParameters == null || mChannels == null)
                return;

            foreach (ChannelSetting channel1 in mChannels)
            {
                foreach (ChannelSetting channel2 in mChannels)
                {
                    if (channel1 == channel2)
                        continue;

                    if (mMatchedChannels.Contains(channel1.ChannelName + channel2.ChannelName)
                        || mMatchedChannels.Contains(channel2.ChannelName + channel1.ChannelName))
                        continue;

                    if (channel1.ODUSubBand.Equals(channel2.ODUSubBand, StringComparison.OrdinalIgnoreCase)
                        && channel1.ODUSubBand.IndexOf("/") == -1)
                    {
                        mMatchedChannels.Add(channel1.ChannelName + channel2.ChannelName);
                        mMatchedChannels.Add(channel2.ChannelName + channel1.ChannelName);
                        continue;
                    }

                    List<string> subBands1 = GetSubBands(channel1.ODUSubBand);
                    List<string> subBands2 = GetSubBands(channel2.ODUSubBand);
                    if (subBands1.Count == 0 || subBands2.Count == 0)
                        continue;

                    bool isMatch = false;
                    EquipmentParameter equipmentParameter;
                    HashSet<string> sameSubBands = GetSameSubBands(subBands1, subBands2);
                    foreach (string subBand in sameSubBands)
                    {
                        if (!mEquipmentParameters.ContainsKey(subBand))
                            break;

                        equipmentParameter = mEquipmentParameters[subBand];
                        if ((channel1.StartFreq >= equipmentParameter.LoStart && channel1.EndFreq <= equipmentParameter.LoStop
                                || channel1.StartFreq >= equipmentParameter.HiStart && channel1.EndFreq <= equipmentParameter.HiStop)
                                &&
                                (channel1.Pair.StartFreq >= equipmentParameter.LoStart && channel1.Pair.EndFreq <= equipmentParameter.LoStop
                                || channel1.Pair.StartFreq >= equipmentParameter.HiStart && channel1.Pair.EndFreq <= equipmentParameter.HiStop)
                                && 
                                (channel2.StartFreq >= equipmentParameter.LoStart && channel2.EndFreq <= equipmentParameter.LoStop
                                || channel2.StartFreq >= equipmentParameter.HiStart && channel2.EndFreq <= equipmentParameter.HiStop)
                                &&
                                (channel2.Pair.StartFreq >= equipmentParameter.LoStart && channel2.Pair.EndFreq <= equipmentParameter.LoStop
                                || channel2.Pair.StartFreq >= equipmentParameter.HiStart && channel2.Pair.EndFreq <= equipmentParameter.HiStop)
                            )
                        {
                            isMatch = true;
                            break;
                        }
                    }

                    if (isMatch)
                    {
                        mMatchedChannels.Add(channel1.ChannelName + channel2.ChannelName);
                        mMatchedChannels.Add(channel2.ChannelName + channel1.ChannelName);
                        continue;
                    }

                    isMatch = true;
                    foreach (string subBand in subBands1)
                    {
                        if (!mEquipmentParameters.ContainsKey(subBand))
                        {
                            isMatch = false;
                            break;
                        }

                        equipmentParameter = mEquipmentParameters[subBand];
                        isMatch = channel2.EndFreq <= equipmentParameter.LoStart
                            || channel2.StartFreq >= equipmentParameter.LoStop && channel2.EndFreq <= equipmentParameter.HiStart
                            || channel2.StartFreq >= equipmentParameter.HiStop;
                        if (!isMatch)
                            break;

                        isMatch = channel2.Pair.EndFreq <= equipmentParameter.LoStart
                            || channel2.Pair.StartFreq >= equipmentParameter.LoStop && channel2.Pair.EndFreq <= equipmentParameter.HiStart
                            || channel2.Pair.StartFreq >= equipmentParameter.HiStop;
                        if (!isMatch)
                            break;
                    }
                    if (!isMatch)
                        continue;

                    foreach (string subBand in subBands2)
                    {
                        if (!mEquipmentParameters.ContainsKey(subBand))
                        {
                            isMatch = false;
                            break;
                        }

                        equipmentParameter = mEquipmentParameters[subBand];
                        isMatch = channel1.EndFreq <= equipmentParameter.LoStart
                            || channel1.StartFreq >= equipmentParameter.LoStop && channel1.EndFreq <= equipmentParameter.HiStart
                            || channel1.StartFreq >= equipmentParameter.HiStop;
                        if (!isMatch)
                            break;

                        isMatch = channel1.Pair.EndFreq <= equipmentParameter.LoStart
                            || channel1.Pair.StartFreq >= equipmentParameter.LoStop && channel1.Pair.EndFreq <= equipmentParameter.HiStart
                            || channel1.Pair.StartFreq >= equipmentParameter.HiStop;
                        if (!isMatch)
                            break;
                    }

                    if (!isMatch)
                        continue;

                    mMatchedChannels.Add(channel1.ChannelName + channel2.ChannelName);
                    mMatchedChannels.Add(channel2.ChannelName + channel1.ChannelName);
                }
            }
        }

        private static List<string> GetSubBands(string channelSubBands)
        {
            List<string> subBands = new List<string>();
            string[] subBandArray = channelSubBands.Split(new char[] {'/'});
            if (subBandArray != null)
            {
                foreach (string subBand in subBandArray)
                {
                    subBands.Add(subBand);
                }
            }

            return subBands;
        }
    }
}
