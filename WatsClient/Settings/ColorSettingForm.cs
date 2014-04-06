using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WatsClient.Settings
{
    public partial class ColorSettingForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private static ColorSettingForm mColorSettingForm;

        public static ColorSettingForm Instance
        {
            get
            {
                if (mColorSettingForm == null)
                    mColorSettingForm = new ColorSettingForm();

                return mColorSettingForm;
            }
        }

        private ColorSettingForm()
        {
            InitializeComponent();
        }
    }
}
