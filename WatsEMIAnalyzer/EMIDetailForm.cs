using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsEMIAnalyzer.EMI;

namespace WatsEMIAnalyzer
{
    public partial class EMIDetailForm : Form
    {
        private EMIFileData mEmiFileData;

        public EMIDetailForm(EMIFileData emiData)
        {
            mEmiFileData = emiData;
            InitializeComponent();
        }

        private void EMIDetailForm_Load(object sender, EventArgs e)
        {
            GeneralListView.Columns.Add("Attribute", 150, HorizontalAlignment.Left);
            GeneralListView.Columns.Add("Value", GeneralListView.Width - 150, HorizontalAlignment.Left);
            AddItem(GeneralListView, "File Version", mEmiFileData.MajorVersion.ToString() + "." + mEmiFileData.MinorVersion.ToString());
            AddItem(GeneralListView, "Application Name", mEmiFileData.PA_Name);
            AddItem(GeneralListView, "Application Version", mEmiFileData.PA_Version);
            AddItem(GeneralListView, "Test Time", Utility.GetEmiTime(mEmiFileData.PA_TestTime));
            AddItem(GeneralListView, "User Name", mEmiFileData.PA_UserName);
            //AddItem("File Name", mEmiFileData.);
            AddItem(GeneralListView, "File Path", mEmiFileData.PA_DataFile);
            AddItem(GeneralListView, "Project Name", mEmiFileData.PI_ID);
            AddItem(GeneralListView, "Test Mode", (mEmiFileData.PI_TestMode == 0) ? "Manual" : "Auto");
            AddItem(GeneralListView, "Polarization", Utility.GetEmiPolarization(mEmiFileData.PI_AntennaPolarization));

            SiteListView.Columns.Add("Attribute", 150, HorizontalAlignment.Left);
            SiteListView.Columns.Add("Value", SiteListView.Width - 150, HorizontalAlignment.Left);
            AddItem(SiteListView, "Site Name", mEmiFileData.Site_ID);
            AddItem(SiteListView, "Site ID", /*mEmiFileData.Site_ID*/mEmiFileData.Site_SerialNo);
            AddItem(SiteListView, "Address", mEmiFileData.Site_Address);
            AddItem(SiteListView, "Longitude", WatsEmiReportTool.Utility.ConvertLongtitude(mEmiFileData.Site_Longitude));
            AddItem(SiteListView, "Latitude", WatsEmiReportTool.Utility.ConvertLatitude(mEmiFileData.Site_Latitude));
            AddItem(SiteListView, "Altitude", mEmiFileData.Site_Altitude + " Meter");
            AddItem(SiteListView, "Declination", Utility.ConvertMagDeclination(mEmiFileData.Site_MagDeclination));
            //AddItem(SiteListView, "Data File", mEmiFileData.);
            AddItem(SiteListView, "Create Time", Utility.GetEmiTime(mEmiFileData.Site_CreateTime));
            //AddItem(SiteListView, "Category", mEmiFileData.DataGroups);

            InstrumentsListView.Columns.Add("Attribute", 150, HorizontalAlignment.Left);
            InstrumentsListView.Columns.Add("Value", InstrumentsListView.Width - 150, HorizontalAlignment.Left);
            AddItem(InstrumentsListView, "Spectrum Model", mEmiFileData.SA_ID);
            AddItem(InstrumentsListView, "Antenna", mEmiFileData.Antenna_ID);
            AddItem(InstrumentsListView, "P&T", mEmiFileData.PT_ID);
            AddItem(InstrumentsListView, "Compass", mEmiFileData.Compass_ID);
            AddItem(InstrumentsListView, "GPS", mEmiFileData.GPS_ID);
            AddItem(InstrumentsListView, "Cable", mEmiFileData.Cable_ID);
            AddItem(InstrumentsListView, "LNA", mEmiFileData.LNA_ID);

            SpectrumListView.Columns.Add("Attribute", 150, HorizontalAlignment.Left);
            SpectrumListView.Columns.Add("Value", SpectrumListView.Width - 150, HorizontalAlignment.Left);
            AddItem(SpectrumListView, "Span", mEmiFileData.SA_Span + " MHz");
            AddItem(SpectrumListView, "Reference Level", mEmiFileData.SA_REF_LEVEL + " dBm");
            AddItem(SpectrumListView, "RBW", mEmiFileData.SA_RBW + " kHz");
            AddItem(SpectrumListView, "VBW", mEmiFileData.SA_VBW + " kHz");
            AddItem(SpectrumListView, "Detector", mEmiFileData.SA_Detector);
            AddItem(SpectrumListView, "Trace", mEmiFileData.SA_Trace);
            AddItem(SpectrumListView, "Trace Count", mEmiFileData.SA_Trace_Count.ToString());
            AddItem(SpectrumListView, "Filter", mEmiFileData.SA_Filter);
            AddItem(SpectrumListView, "Pre Amplify", mEmiFileData.SA_PreAmplify);
            AddItem(SpectrumListView, "Sweep Mode", mEmiFileData.SA_Sweep_Mode);

            MeasureListView.Columns.Add("Attribute", 150, HorizontalAlignment.Left);
            MeasureListView.Columns.Add("Value", MeasureListView.Width - 150, HorizontalAlignment.Left);
            
            ListViewGroup azimuthLvg = new ListViewGroup();
            azimuthLvg.Header = "Azimuth Information";
            
            ListViewGroup bandLvg = new ListViewGroup();
            bandLvg.Header = "Frequency Band";

            MeasureListView.Groups.Add(azimuthLvg);
            MeasureListView.Groups.Add(bandLvg);
            MeasureListView.ShowGroups = true;

            ListViewItem lvi = new ListViewItem(azimuthLvg);
            lvi.Text = "Azimuth Count";
            lvi.SubItems.Add(mEmiFileData.Azimuth_Item_Count.ToString());
            MeasureListView.Items.Add(lvi);
            for (int i = 0; i < mEmiFileData.Azimuth_Item_Count; i++)
            {
                lvi = new ListViewItem(azimuthLvg);
                lvi.Text = "Azimuth " + (i + 1).ToString();
                lvi.SubItems.Add("From "
                    + mEmiFileData.Azimuth_Data[i].Azimuth_Start
                    + " to "
                    + mEmiFileData.Azimuth_Data[i].Azimuth_End
                    + " step "
                    + mEmiFileData.Azimuth_Data[i].Azimuth_Step);
                MeasureListView.Items.Add(lvi);
            }

            lvi = new ListViewItem(bandLvg);
            lvi.Text = "Band Count";
            lvi.SubItems.Add(mEmiFileData.Freq_Item_Count.ToString());
            MeasureListView.Items.Add(lvi);
            for (int i = 0; i < mEmiFileData.Freq_Item_Count; i++)
            {
                lvi = new ListViewItem(bandLvg);
                lvi.Text = "Band " + (i + 1).ToString();
                lvi.SubItems.Add(mEmiFileData.Frequency_Data[i].Frequency_ID
                    + ", "
                    + mEmiFileData.Frequency_Data[i].Frequency_Start
                    + " ~ "
                    + mEmiFileData.Frequency_Data[i].Frequency_End
                    + " MHz");
                MeasureListView.Items.Add(lvi);
            }

            int rowIndex;
            DataGridViewRow row;
            for (int i = 0; i < mEmiFileData.DataGroups.Length; i++)
            {
                rowIndex = DataGridView.Rows.Add();
                row = DataGridView.Rows[rowIndex];
                row.Cells["NoColumn"].Value = (i + 1).ToString();
                row.Cells["PolarizationColumn"].Value
                    = (mEmiFileData.DataGroups[i].DB_FB_AntennaPolarization == 0 ? "Vertical" : "Horizontal");
                row.Cells["AzimuthColumn"].Value = mEmiFileData.DataGroups[i].DG_FB_Angle.ToString();
                row.Cells["FrequencyColumn"].Value
                    = mEmiFileData.DataGroups[i].DG_FB_Start + " ~ " + mEmiFileData.DataGroups[i].DG_FB_End + " MHz";
                row.Cells["SampleCountColumn"].Value = mEmiFileData.DataGroups[i].DG_Item_Count.ToString();
                row.Cells["TimeColumn"].Value = Utility.GetEmiTime(mEmiFileData.DataGroups[i].DB_FB_TestTime);
            }
        }

        private ListViewItem AddItem(ListView listView, string attribute, string value)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = attribute;
            lvi.SubItems.Add(value);

            return listView.Items.Add(lvi);
        }
    }
}
