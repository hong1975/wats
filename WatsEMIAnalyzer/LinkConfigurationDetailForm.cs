using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WatsEmiReportTool;

namespace WatsEMIAnalyzer
{
    public partial class LinkConfigurationDetailForm : Form
    {
        public LinkConfigurationDetailForm(List<LinkConfiguration> linkConfigurations)
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
