using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;

namespace WatsEmiReportTool
{
    public partial class ExcelExportSettingForm : Form
    {
        private bool mExportOffice2003;

        public bool ExportOffice2003
        {
            get { return mExportOffice2003; }
        }

        public ExcelExportSettingForm()
        {
            InitializeComponent();

            if (Utility.GetExcelVersion() > 11.5)
            {
                Office2007RadioButton.Checked = true;
                mExportOffice2003 = false;
            }
            else
            {
                Office2003RadioButton.Checked = true;
                Office2007RadioButton.Enabled = false;

                mExportOffice2003 = true;
            }
        }

        private void Office2003RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            mExportOffice2003 = Office2003RadioButton.Checked;
        }
    }
}
