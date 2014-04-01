using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsEMIAnalyzer.EMI;
using System.IO;

namespace WatsEMIAnalyzer
{
    public partial class TestEMIParseForm : Form
    {
        EMIFileParser mParser = new EMIFileParser();

        public TestEMIParseForm()
        {
            InitializeComponent();

            mParser.AttachedForm = this;
            mParser.onParseFailed += new EMIFileParser.parseFailed(TestEMIParseForm_onParseFailed);
            mParser.onParseSuccessfully += new EMIFileParser.parseSuccessfully(TestEMIParseForm_onParseSuccessfully);
        }

        void TestEMIParseForm_onParseSuccessfully(string emiName, EMIFileData emiFileData, object context)
        {
            MessageBox.Show(emiName + " parse successfully");
        }

        void TestEMIParseForm_onParseFailed(string emiName, string errorMessage, object context)
        {
            MessageBox.Show(emiName + " parse failed, error=" + errorMessage);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (DialogResult.OK != dialog.ShowDialog())
                return;

            mParser.Parse(dialog.FileName, null);

        }
    }
}
