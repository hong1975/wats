using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using System.Text.RegularExpressions;

namespace WatsEmiReportTool
{
    public partial class LimitSettingForm : Form
    {
        private IniFile mIniFile = new IniFile(".\\WatsEmiReportTool.ini");
        private bool mOKInvoked = false;

        public bool UseChannelPowerLimit
        {
            get { return ChannelPowerCheckBox.Checked; }
        }

        public int ChannelPowerLimit
        {
            get { return Int32.Parse(ChannelPowerLimitEditor.Text); }
        }

        public bool UseDeltaPowerLimit
        {
            get { return DeltaPowerCheckBox.Checked; }
        }

        public int DeltaPowerLimit
        {
            get { return Int32.Parse(DeltaPowerLimitEditor.Text); }
        }

        public LimitSettingForm()
        {
            InitializeComponent();

            ChannelPowerCheckBox.Checked = mIniFile.ReadInt("General", "UseChannelPowerLimit", 0) == 1;
            ChannelPowerLimitEditor.Text = mIniFile.ReadString("General", "ChannelPowerLimit", "-85").Trim();
            DeltaPowerCheckBox.Checked = mIniFile.ReadInt("General", "UseDeltaPowerLimit", 0) == 1;
            DeltaPowerLimitEditor.Text = mIniFile.ReadString("General", "DeltaPowerLimit", "-3").Trim();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            mOKInvoked = true;
        }

        private void LimitSettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mOKInvoked)
            {
                mOKInvoked = false;
                ChannelPowerLimitEditor.Text = ChannelPowerLimitEditor.Text.Trim();
                if (ChannelPowerLimitEditor.Text.Length == 0)
                {
                    MessageBox.Show("Please input channel power limit !", "Warning");
                    ChannelPowerLimitEditor.Focus();
                    e.Cancel = true;
                    return;
                }
                if (!Regex.IsMatch(ChannelPowerLimitEditor.Text, @"^-?[1-9]\d*$"))
                {
                    MessageBox.Show(ChannelPowerLimitEditor.Text + " is not a valid power limit !", "Warning");
                    ChannelPowerLimitEditor.SelectAll();
                    ChannelPowerLimitEditor.Focus();
                    e.Cancel = true;
                    return;
                }

                DeltaPowerLimitEditor.Text = DeltaPowerLimitEditor.Text.Trim();
                if (DeltaPowerLimitEditor.Text.Length == 0)
                {
                    MessageBox.Show("Please input delta power limit !", "Warning");
                    DeltaPowerLimitEditor.Focus();
                    e.Cancel = true;
                    return;
                }
                if (!Regex.IsMatch(DeltaPowerLimitEditor.Text, @"^-?[1-9]\d*$"))
                {
                    MessageBox.Show(DeltaPowerLimitEditor.Text + " is not a valid delta limit !", "Warning");
                    DeltaPowerLimitEditor.SelectAll();
                    DeltaPowerLimitEditor.Focus();
                    e.Cancel = true;
                    return;
                }

                mIniFile.WriteInt("General", "UseChannelPowerLimit", ChannelPowerCheckBox.Checked ? 1 : 0);
                mIniFile.WriteString("General", "ChannelPowerLimit", ChannelPowerLimitEditor.Text.Trim());
                mIniFile.WriteInt("General", "UseDeltaPowerLimit", DeltaPowerCheckBox.Checked ? 1 : 0);
                mIniFile.WriteString("General", "DeltaPowerLimit", DeltaPowerLimitEditor.Text.Trim());
            }
        }




    }
}
