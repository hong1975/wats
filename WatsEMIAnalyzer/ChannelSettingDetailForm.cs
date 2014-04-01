using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsEmiReportTool;

namespace WatsEMIAnalyzer
{
    public partial class ChannelSettingDetailForm : Form
    {
        public ChannelSettingDetailForm(List<ChannelSetting> channelSettings)
        {
            InitializeComponent();

            int rowIndex;
            foreach (ChannelSetting channelSetting in channelSettings)
            {
                rowIndex = ChannelSettingGrid.Rows.Add();
                ChannelSettingGrid.Rows[rowIndex].Cells["NumberColumn"].Value = rowIndex + 1;

                ChannelSettingGrid.Rows[rowIndex].Cells["CHNameColumn"].Value = channelSetting.ChannelName;
                ChannelSettingGrid.Rows[rowIndex].Cells["CenterFeqColumn"].Value = channelSetting.CenterFreq;
                ChannelSettingGrid.Rows[rowIndex].Cells["BandwidthColumn"].Value = channelSetting.BandWidth;
                ChannelSettingGrid.Rows[rowIndex].Cells["StartFreqColumn"].Value = channelSetting.StartFreq;
                ChannelSettingGrid.Rows[rowIndex].Cells["EndFreqColumn"].Value = channelSetting.EndFreq;
                ChannelSettingGrid.Rows[rowIndex].Cells["ODUSubBandColumn"].Value = channelSetting.ODUSubBand;

                ChannelSettingGrid.Rows[rowIndex].Cells["PairCHNameColumn"].Value = channelSetting.Pair.ChannelName;
                ChannelSettingGrid.Rows[rowIndex].Cells["PairCenterFeqColumn"].Value = channelSetting.Pair.CenterFreq;
                ChannelSettingGrid.Rows[rowIndex].Cells["PairBandwidthColumn"].Value = channelSetting.Pair.BandWidth;
                ChannelSettingGrid.Rows[rowIndex].Cells["PairStartFreqColumn"].Value = channelSetting.Pair.StartFreq;
                ChannelSettingGrid.Rows[rowIndex].Cells["PairEndFreqColumn"].Value = channelSetting.Pair.EndFreq;
                ChannelSettingGrid.Rows[rowIndex].Cells["PairODUSubBandColumn"].Value = channelSetting.Pair.ODUSubBand;
            }
        }
    }
}
