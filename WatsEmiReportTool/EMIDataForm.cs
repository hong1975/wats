using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsEMIAnalyzer.EMI;

namespace WatsEmiReportTool
{
    public partial class EMIDataForm : Form
    {
        public EMIDataForm(EMIFileData data)
        {
            InitializeComponent();
            SiteIDEditor.Text = data.Site_ID;
            SiteNameEditor.Text = data.Site_ID;
            AddressEditor.Text = data.Site_Address;
            EngineerEditor.Text = data.PA_UserName;
            DateEditor.Text = data.PA_TestTime;
            LongtitudeEditor.Text = Utility.ConvertLongtitude(data.Site_Longitude);
            LatitudeEditor.Text = Utility.ConvertLatitude(data.Site_Latitude);
        }
    }
}
