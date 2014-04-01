using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WatsEmiReportTool
{
    public partial class PictureForm : Form
    {
        private string mImageLocation;
        private string mTitle;

        public string ImageLocation
        {
            set { mImageLocation = value; }
        }

        public string Title
        {
            set { mTitle = value; }
        }

        public PictureForm()
        {
            InitializeComponent();
        }

        private void PictureForm_Load(object sender, EventArgs e)
        {
            PictureBox.ImageLocation = mImageLocation;
            this.Text = mTitle;
        }

        private void PictureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            PictureBox.ImageLocation = null;
        }
    }
}
