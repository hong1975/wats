using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsEMIAnalyzer.EMI;
using WatsEmiReportTool;
using WatsEMIAnalyzer.Bindings;
using WatsEMIAnalyzer.Model;

namespace WatsEMIAnalyzer
{
    public partial class TableViewForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        enum PolarizationType
        {
            Both,
            Vertical,
            Horizontal
        }

        private TreeNode mAnalysisViewNode;
        private BindingTask mTask;
        private List<EMIFileData> mEmiFileDatas;
        private List<ChannelSetting> mChannelSettings;

        private PolarizationType mPolarizationType;        

        public TableViewForm(TreeNode analysisViewNode)
        {
            InitializeComponent();

            mAnalysisViewNode = analysisViewNode;
            mTask = ((TaskModel)analysisViewNode.Parent.Parent.Tag).mTask;
        }

        private void TableViewForm_Load(object sender, EventArgs e)
        {
            mEmiFileDatas = Utility.GetTaskAvailableEmiFiles(mTask);
            mChannelSettings = DataCenter.Instance().ChannelSettings[mTask.ChannelSettingID];

            ShowTableData();
        }

        private void ShowTableData()
        {
            int i = 1;
            WatsEmiDataManager watsEmiDataManager;
            double azimuth;
            int rowId;
            ChannelSetting channelSetting;
            WatsEmiData watsEmiData;
            ChannelPower channelPower;

            DataTable.Rows.Clear();
            foreach (EMIFileData emiFileData in mEmiFileDatas)
            {
                watsEmiDataManager = WatsEmiReportTool.Utility.GetEmiDataManager(emiFileData, mChannelSettings);
                foreach (KeyValuePair<double, Dictionary<ChannelSetting, WatsEmiData>> pair in watsEmiDataManager.AllChannelSamples)
                {
                    azimuth = pair.Key;
                    
                    if ((BothToolStripButton.Checked || VerticalToolStripButton.Checked) && FilterToolStripButton.Checked
                        || UnFilterToolStripButton.Checked)
                    {
                        //vertical channel
                        foreach (KeyValuePair<ChannelSetting, WatsEmiData> subPair in pair.Value)
                        {
                            channelSetting = subPair.Key;
                            watsEmiData = subPair.Value;

                            channelPower = new ChannelPower(emiFileData.SA_RBW, channelSetting, new LimitSetting(), watsEmiData);

                            rowId = DataTable.Rows.Add();
                            DataTable.Rows[rowId].Cells[0].Value = i.ToString();                  //No.
                            DataTable.Rows[rowId].Cells[1].Value = emiFileData.Site_ID;           //Site Name
                            DataTable.Rows[rowId].Cells[2].Value = emiFileData.Site_ID;           //Site ID
                            DataTable.Rows[rowId].Cells[3].Value = "Vertical";                    //Polarization
                            DataTable.Rows[rowId].Cells[4].Value = azimuth.ToString();            //Azimuth
                            DataTable.Rows[rowId].Cells[5].Value = channelSetting.ChannelName;    //CH No
                            DataTable.Rows[rowId].Cells[6].Value = channelSetting.StartFreq.ToString();   //Start Freq
                            DataTable.Rows[rowId].Cells[7].Value = channelSetting.EndFreq.ToString();     //End Freq
                            DataTable.Rows[rowId].Cells[8].Value = channelSetting.CenterFreq.ToString();  //Center Freq
                            DataTable.Rows[rowId].Cells[9].Value = channelSetting.BandWidth.ToString();   //Bandwidth
                            DataTable.Rows[rowId].Cells[10].Value = channelPower.VPower.ToString();       //Channel Power
                            //DataTable.Rows[rowId].Cells[11].Value = ...                                 //Color

                            i++;
                        }

                        //vertical channel pair
                        foreach (KeyValuePair<ChannelSetting, WatsEmiData> subPair in pair.Value)
                        {
                            channelSetting = subPair.Key;
                            watsEmiData = subPair.Value;

                            channelPower = new ChannelPower(emiFileData.SA_RBW, channelSetting, new LimitSetting(), watsEmiData);

                            rowId = DataTable.Rows.Add();
                            DataTable.Rows[rowId].Cells[0].Value = i.ToString();                  //No.
                            DataTable.Rows[rowId].Cells[1].Value = emiFileData.Site_ID;           //Site Name
                            DataTable.Rows[rowId].Cells[2].Value = emiFileData.Site_ID;           //Site ID
                            DataTable.Rows[rowId].Cells[3].Value = "Vertical";                    //Polarization
                            DataTable.Rows[rowId].Cells[4].Value = azimuth.ToString();            //Azimuth
                            DataTable.Rows[rowId].Cells[5].Value = channelSetting.Pair.ChannelName;    //CH No
                            DataTable.Rows[rowId].Cells[6].Value = channelSetting.Pair.StartFreq.ToString();   //Start Freq
                            DataTable.Rows[rowId].Cells[7].Value = channelSetting.Pair.EndFreq.ToString();     //End Freq
                            DataTable.Rows[rowId].Cells[8].Value = channelSetting.Pair.CenterFreq.ToString();  //Center Freq
                            DataTable.Rows[rowId].Cells[9].Value = channelSetting.Pair.BandWidth.ToString();   //Bandwidth
                            DataTable.Rows[rowId].Cells[10].Value = channelPower.VPairPower.ToString();       //Channel Power
                            //DataTable.Rows[rowId].Cells[11].Value = ...                                 //Color

                            i++;
                        }
                    }

                    if ((BothToolStripButton.Checked || HorizontalToolStripButton.Checked) && FilterToolStripButton.Checked
                        || UnFilterToolStripButton.Checked)
                    {
                        //horizontal channel
                        foreach (KeyValuePair<ChannelSetting, WatsEmiData> subPair in pair.Value)
                        {
                            channelSetting = subPair.Key;
                            watsEmiData = subPair.Value;

                            channelPower = new ChannelPower(emiFileData.SA_RBW, channelSetting, new LimitSetting(), watsEmiData);

                            rowId = DataTable.Rows.Add();
                            DataTable.Rows[rowId].Cells[0].Value = i.ToString();                  //No.
                            DataTable.Rows[rowId].Cells[1].Value = emiFileData.Site_ID;           //Site Name
                            DataTable.Rows[rowId].Cells[2].Value = emiFileData.Site_ID;           //Site ID
                            DataTable.Rows[rowId].Cells[3].Value = "Horizontal";                  //Polarization
                            DataTable.Rows[rowId].Cells[4].Value = azimuth.ToString();            //Azimuth
                            DataTable.Rows[rowId].Cells[5].Value = channelSetting.ChannelName;    //CH No
                            DataTable.Rows[rowId].Cells[6].Value = channelSetting.StartFreq.ToString();   //Start Freq
                            DataTable.Rows[rowId].Cells[7].Value = channelSetting.EndFreq.ToString();     //End Freq
                            DataTable.Rows[rowId].Cells[8].Value = channelSetting.CenterFreq.ToString();  //Center Freq
                            DataTable.Rows[rowId].Cells[9].Value = channelSetting.BandWidth.ToString();   //Bandwidth
                            DataTable.Rows[rowId].Cells[10].Value = channelPower.HPower.ToString();       //Channel Power
                            //DataTable.Rows[rowId].Cells[11].Value = ...                                 //Color

                            i++;
                        }

                        //horizontal channel pair
                        foreach (KeyValuePair<ChannelSetting, WatsEmiData> subPair in pair.Value)
                        {
                            channelSetting = subPair.Key;
                            watsEmiData = subPair.Value;

                            channelPower = new ChannelPower(emiFileData.SA_RBW, channelSetting, new LimitSetting(), watsEmiData);

                            rowId = DataTable.Rows.Add();
                            DataTable.Rows[rowId].Cells[0].Value = i.ToString();                  //No.
                            DataTable.Rows[rowId].Cells[1].Value = emiFileData.Site_ID;           //Site Name
                            DataTable.Rows[rowId].Cells[2].Value = emiFileData.Site_ID;           //Site ID
                            DataTable.Rows[rowId].Cells[3].Value = "Horizontal";                  //Polarization
                            DataTable.Rows[rowId].Cells[4].Value = azimuth.ToString();            //Azimuth
                            DataTable.Rows[rowId].Cells[5].Value = channelSetting.Pair.ChannelName;    //CH No
                            DataTable.Rows[rowId].Cells[6].Value = channelSetting.Pair.StartFreq.ToString();   //Start Freq
                            DataTable.Rows[rowId].Cells[7].Value = channelSetting.Pair.EndFreq.ToString();     //End Freq
                            DataTable.Rows[rowId].Cells[8].Value = channelSetting.Pair.CenterFreq.ToString();  //Center Freq
                            DataTable.Rows[rowId].Cells[9].Value = channelSetting.Pair.BandWidth.ToString();   //Bandwidth
                            DataTable.Rows[rowId].Cells[10].Value = channelPower.HPairPower.ToString();       //Channel Power
                            //DataTable.Rows[rowId].Cells[11].Value = ...                                 //Color

                            i++;
                        }
                    }
                }
            }
        }

        private void FindToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void BothToolStripButton_CheckedChanged(object sender, EventArgs e)
        {
            if (BothToolStripButton.Checked)
            {
                VerticalToolStripButton.Checked = HorizontalToolStripButton.Checked = false;
                BothToolStripButton.CheckOnClick = false;

                ShowTableData();
            }
            else
            {
                BothToolStripButton.CheckOnClick = true;
            }
        }

        private void VerticalToolStripButton_CheckedChanged(object sender, EventArgs e)
        {
            if (VerticalToolStripButton.Checked)
            {
                BothToolStripButton.Checked = HorizontalToolStripButton.Checked = false;
                VerticalToolStripButton.CheckOnClick = false;

                ShowTableData();
            }
            else
            {
                VerticalToolStripButton.CheckOnClick = true;
            }
        }

        private void HorizontalToolStripButton_CheckedChanged(object sender, EventArgs e)
        {
            if (HorizontalToolStripButton.Checked)
            {
                BothToolStripButton.Checked = VerticalToolStripButton.Checked = false; 
                HorizontalToolStripButton.CheckOnClick = false;

                ShowTableData();
            }
            else
            {
                HorizontalToolStripButton.CheckOnClick = true;
            }
        }

        private void FilterToolStripButton_CheckedChanged(object sender, EventArgs e)
        {
            if (FilterToolStripButton.Checked)
            {
                UnFilterToolStripButton.Checked = false;
                FilterToolStripButton.CheckOnClick = false;

                ShowTableData();
            }
            else
            {
                FilterToolStripButton.CheckOnClick = true;
            }
        }

        private void UnFilterToolStripButton_CheckedChanged(object sender, EventArgs e)
        {
            if (UnFilterToolStripButton.Checked)
            {
                FilterToolStripButton.Checked = false;
                UnFilterToolStripButton.CheckOnClick = false;

                ShowTableData();
            }
            else
            {
                UnFilterToolStripButton.CheckOnClick = true;
            }
        }
    }
}
