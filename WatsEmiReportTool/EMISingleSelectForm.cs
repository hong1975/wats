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
    public partial class EMISingleSelectForm : Form
    {
        private List<EMIFileData> mEmiFileDatas;
        private EMIFileData mEmi;

        public EMIFileData Emi
        {
            get { return mEmi; }
        }

        public EMISingleSelectForm(List<string> emiFiles, List<EMIFileData> emiFileDatas)
        {
            InitializeComponent();

            mEmiFileDatas = emiFileDatas;
            for (int i = 0; i < emiFiles.Count; i++)
            {
                EMICombox.Items.Add("Site(" + mEmiFileDatas[i].Site_ID + ") - " + emiFiles[i]);
            }
        }

        private void EMISingleSelectForm_Load(object sender, EventArgs e)
        {
            if (EMICombox.Items.Count > 0)
                EMICombox.SelectedIndex = 0;
        }

        private void EMICombox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEmi = mEmiFileDatas[EMICombox.SelectedIndex];
        }
    }
}
