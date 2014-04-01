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
    public partial class EMIPairSelectForm : Form
    {
        class AngleDiff
        {
            public double angle;
            public double diff;
            public AngleDiff(double angle, double diff)
            {
                this.angle = angle;
                this.diff = diff;
            }

            public static int SortRountine(AngleDiff diff1, AngleDiff diff2)
            {
                return diff1.diff.CompareTo(diff2.diff);
            }
        }

        private bool mIsOnlySitesSelectable = false;
        private bool mOKInvoked = false;
        private List<EMIFileData> mEmiFileDatas;
        private EMIFileData mEmiA;
        private EMIFileData mEmiB;

        private List<double> mAngleAs = new List<double>();
        private List<double> mAngleBs = new List<double>();

        public bool IsOnlySitesSelectable
        {
            set { mIsOnlySitesSelectable = value; }
        }

        public EMIFileData EmiA
        {
            get { return mEmiA; }
        }

        public EMIFileData EmiB
        {
            get { return mEmiB; }
        }

        public double AzimuthA
        {
            get 
            {
                return double.Parse(AzimuthComboxA.SelectedItem.ToString());
            }
        }

        public double AzimuthB
        {
            get
            {
                return double.Parse(AzimuthComboxB.SelectedItem.ToString());
            }
        }

        public EMIPairSelectForm(List<string> emiFiles, List<EMIFileData> emiFileDatas)
        {
            InitializeComponent();

            mEmiFileDatas = emiFileDatas;
            for (int i = 0; i < emiFiles.Count; i++)
            {
                EMIComboxA.Items.Add("Site(" + mEmiFileDatas[i].Site_ID + ") - " + emiFiles[i]);
                EMIComboxB.Items.Add("Site(" + mEmiFileDatas[i].Site_ID + ") - " + emiFiles[i]);
            }
        }

        private void EMIPairSelectForm_Load(object sender, EventArgs e)
        {
            if (mIsOnlySitesSelectable)
            {
                AngleLabel.Visible = false;
                AzimuthALabel.Visible = false;
                AzimuthBLabel.Visible = false;
                AzimuthComboxA.Visible = false;
                AzimuthComboxB.Visible = false;

                CancelButton.Top -= 60;
                OKButton.Top -= 60;
                this.Height -= 60;
            }
        }

        private void CalculateAngle()
        {
            EMIFileData emiA = mEmiFileDatas[EMIComboxA.SelectedIndex];
            EMIFileData emiB = mEmiFileDatas[EMIComboxB.SelectedIndex];
            double angle = JWD.angle(new JWD(emiA.Site_Longitude, emiA.Site_Latitude),
                new JWD(emiB.Site_Longitude, emiB.Site_Latitude));

            double pairAngle;
            if (angle <= 180)
                pairAngle = 180 + angle;
            else
                pairAngle = angle - 180;
            AngleLabel.Text = "Azimuth: A->B " + angle.ToString("f3") + "\x00B0, "
                + "B->A " + pairAngle.ToString("f3") + "\x00B0";

            double closestAzimuthA = FindClosestAzimuths(angle, emiA);
            double closestAzimuthB = FindClosestAzimuths(pairAngle, emiB);

            mAngleAs.Clear();
            mAngleBs.Clear();

            HashSet<double> availableAnglesA = new HashSet<double>();
            foreach (DG_Type dataGroup in emiA.DataGroups)
                availableAnglesA.Add(dataGroup.DG_FB_Angle);
            foreach (double azimuthA in availableAnglesA)
            {
                AzimuthComboxA.Items.Add(azimuthA.ToString());
                mAngleAs.Add(azimuthA);
            }
            AzimuthComboxA.SelectedIndex = FindIndex(AzimuthComboxA.Items, closestAzimuthA);

            HashSet<double> availableAnglesB = new HashSet<double>();
            foreach (DG_Type dataGroup in emiB.DataGroups)
                availableAnglesB.Add(dataGroup.DG_FB_Angle);
            foreach (double azimuthB in availableAnglesB)
            {
                AzimuthComboxB.Items.Add(azimuthB.ToString());
            }
            AzimuthComboxB.SelectedIndex = FindIndex(AzimuthComboxB.Items, closestAzimuthB);
        }

        private void EMIComboxA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EMIComboxA.SelectedIndex == -1
                || EMIComboxB.SelectedIndex == -1)
                return;

            AzimuthComboxA.Items.Clear();
            AzimuthComboxB.Items.Clear();

            if (EMIComboxA.SelectedIndex >= 0 && EMIComboxA.SelectedIndex == EMIComboxB.SelectedIndex)
            {
                MessageBox.Show("Can't select same EMI file !");
                EMIComboxA.SelectedIndex = -1;
                EMIComboxB.SelectedIndex = -1;
                return;
            }

            if (mEmiFileDatas[EMIComboxA.SelectedIndex].Site_ID.
                Equals(mEmiFileDatas[EMIComboxB.SelectedIndex].Site_ID, StringComparison.CurrentCultureIgnoreCase))
            {
                MessageBox.Show("Can't select EMI files of same site !");
                EMIComboxA.SelectedIndex = -1;
                EMIComboxB.SelectedIndex = -1;
                return;
            }

            mEmiA = mEmiFileDatas[EMIComboxA.SelectedIndex];
            mEmiB = mEmiFileDatas[EMIComboxB.SelectedIndex];

            if (!mIsOnlySitesSelectable)
                CalculateAngle();
        }

        private void EMIComboxB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EMIComboxA.SelectedIndex == -1
                || EMIComboxB.SelectedIndex == -1)
                return;

            AzimuthComboxA.Items.Clear();
            AzimuthComboxB.Items.Clear();

            if (EMIComboxA.SelectedIndex >= 0 && EMIComboxA.SelectedIndex == EMIComboxB.SelectedIndex)
            {
                MessageBox.Show("Can't select same EMI file !");
                EMIComboxA.SelectedIndex = -1;
                EMIComboxB.SelectedIndex = -1;
                return;
            }

            mEmiA = mEmiFileDatas[EMIComboxA.SelectedIndex];
            mEmiB = mEmiFileDatas[EMIComboxB.SelectedIndex];

            if (!mIsOnlySitesSelectable)
                CalculateAngle();
        }

        private static double FindClosestAzimuths(double angle, EMIFileData emiFileData)
        {
             List<AngleDiff> diffs = new List<AngleDiff>();
            double diff;

            HashSet<double> availableAngles = new HashSet<double>();
            foreach (DG_Type dataGroup in emiFileData.DataGroups)
                availableAngles.Add(dataGroup.DG_FB_Angle);

            foreach (double availableAngle in availableAngles)
            {
                diff = Math.Min(Math.Abs(availableAngle - angle),
                    360 - Math.Abs(availableAngle - angle));

                diffs.Add(new AngleDiff(availableAngle, diff));
            }
            diffs.Sort(AngleDiff.SortRountine);

            return diffs[0].angle;
        }

        private int FindIndex(ComboBox.ObjectCollection items, double azimuth)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (double.Parse(items[i].ToString()) == azimuth)
                    return i;
            }

            return -1;
        }

        private void AzimuthComboxA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AzimuthComboxA.SelectedIndex == -1)
                return;
            if (AzimuthComboxB.Items.Count == 0)
                return;
            else if (AzimuthComboxB.Items.Count == 1)
                AzimuthComboxB.SelectedIndex = 0;
            else //AzimuthComboxB.Items.Count == 2
            {
                double angleA = double.Parse(AzimuthComboxA.SelectedItem.ToString());
                double pairAngle;
                if (angleA <= 180)
                    pairAngle = 180 + angleA;
                else
                    pairAngle = angleA - 180;
                double angleB = FindClosestAzimuths(pairAngle, mEmiB);
                AzimuthComboxB.SelectedIndex = FindIndex(AzimuthComboxB.Items, angleB);
            }
        }

        private void EMIPairSelectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mOKInvoked)
            {
                mOKInvoked = false;
                if (EMIComboxA.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select EMI for Site A !");
                    e.Cancel = true;
                    return;
                }
                if (EMIComboxB.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select EMI for Site B !");
                    e.Cancel = true;
                    return;
                }
                
                if (!mIsOnlySitesSelectable)
                {
                    if (AzimuthComboxA.SelectedIndex == -1)
                    {
                        MessageBox.Show("Please select Azimuth for Site A !");
                        e.Cancel = true;
                        return;
                    }
                    if (AzimuthComboxB.SelectedIndex == -1)
                    {
                        MessageBox.Show("Please select Azimuth for Site B !");
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            mOKInvoked = true;
        }

        private void EMIPairSelectForm_Shown(object sender, EventArgs e)
        {
            if (mEmiFileDatas.Count >= 2)
            {
                EMIComboxA.SelectedIndex = 0;
                EMIComboxB.SelectedIndex = 1;
            }
        }

        
    }
}
