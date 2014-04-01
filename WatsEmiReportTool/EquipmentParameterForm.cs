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
    public partial class EquipmentParameterForm : Form
    {
        public EquipmentParameterForm(Dictionary<string, EquipmentParameter> equipmentParameters)
        {
            InitializeComponent();

            int rowIndex;
            foreach (KeyValuePair<string, EquipmentParameter> pair in equipmentParameters)
            {
                rowIndex = EquipmentParameterGrid.Rows.Add();
                EquipmentParameterGrid.Rows[rowIndex].Cells["TRSpacingColumn"].Value
                    = pair.Value.TRSpacing;
                EquipmentParameterGrid.Rows[rowIndex].Cells["SubBandColumn"].Value
                    = pair.Value.SubBand;
                EquipmentParameterGrid.Rows[rowIndex].Cells["LoStartColumn"].Value
                    = pair.Value.LoStart;
                EquipmentParameterGrid.Rows[rowIndex].Cells["LoStopColumn"].Value
                    = pair.Value.LoStop;
                EquipmentParameterGrid.Rows[rowIndex].Cells["HiStartColumn"].Value
                    = pair.Value.HiStart;
                EquipmentParameterGrid.Rows[rowIndex].Cells["HiStopColumn"].Value
                    = pair.Value.HiStop;
                EquipmentParameterGrid.Rows[rowIndex].Cells["LeftLowBandColumn"].Value
                    = pair.Value.LeftLowBand;
                EquipmentParameterGrid.Rows[rowIndex].Cells["RightLowBandColumn"].Value
                    = pair.Value.RightLowBand;
                EquipmentParameterGrid.Rows[rowIndex].Cells["LeftHighBandColumn"].Value
                    = pair.Value.LeftHighBand;
                EquipmentParameterGrid.Rows[rowIndex].Cells["RightHighBandColumn"].Value
                    = pair.Value.RightHighBand;
            }
        }
    }
}
