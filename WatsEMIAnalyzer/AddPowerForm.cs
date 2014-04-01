using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WatsEMIAnalyzer
{
    public partial class AddPowerForm : Form
    {
        private bool mInvokeOK = false;
        public Color PowerColor
        {
            get { return PowerColorPicker.Color; }
        }

        public int PowerValue
        {
            get { return Int32.Parse(PowerEditor.Text); }
        }

        public AddPowerForm()
        {
            InitializeComponent();
            PowerColorPicker.Color = Color.Black;
        }

        private void MoreColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.ShowDialog();
        }

        private void AddPowerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mInvokeOK)
            {
                PowerEditor.Text = PowerEditor.Text.Trim();
                if (PowerEditor.Text.Length == 0)
                {
                    e.Cancel = true;
                    MessageBox.Show("Power can not be empty !");
                    PowerEditor.Focus();
                }
                else if (!Regex.IsMatch(PowerEditor.Text, @"^[+-]?\d*$"))
                {
                    e.Cancel = true;
                    MessageBox.Show("Invalid power value !");
                    PowerEditor.SelectAll();
                    PowerEditor.Focus();
                }
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            mInvokeOK = true;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            mInvokeOK = false;
        }

        private void AddPowerForm_Load(object sender, EventArgs e)
        {
            PowerEditor.Focus();
        }
    }
}
