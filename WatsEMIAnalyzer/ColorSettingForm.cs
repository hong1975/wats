using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WatsEMIAnalyzer
{
    public partial class ColorSettingForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public ColorSettingForm()
        {
            InitializeComponent();
            HideOnClose = true;
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;

            //ColorSettingGridView.Controls.Add(new ColorComboBox());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddPowerForm addPowerForm = new AddPowerForm();
            addPowerForm.ShowDialog();
        }

        private void ColorSettingGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //ColorSettingGridView.Columns[0].
        }

        private void ColorSettingForm_Load(object sender, EventArgs e)
        {
        }
    }
}
