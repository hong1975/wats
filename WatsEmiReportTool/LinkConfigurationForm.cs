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
    public partial class LinkConfigurationForm : Form
    {
        public LinkConfigurationForm(List<LinkConfiguration> linkConfigurations)
        {
            InitializeComponent();

            int rowIndex;
            foreach (LinkConfiguration linkConfiguration in linkConfigurations)
            {
                rowIndex = LinkConfigurationGrid.Rows.Add();
                LinkConfigurationGrid.Rows[rowIndex].Cells["LinkNameColumn"].Value 
                    = linkConfiguration.LinkName;
                LinkConfigurationGrid.Rows[rowIndex].Cells["IsParallelLinkColumn"].Value 
                    = linkConfiguration.IsParallel ? "Yes":"No";
                LinkConfigurationGrid.Rows[rowIndex].Cells["RequiredConfigurationColumn"].Value 
                    = linkConfiguration.RequiredConfiguration;
            }
        }
    }
}
