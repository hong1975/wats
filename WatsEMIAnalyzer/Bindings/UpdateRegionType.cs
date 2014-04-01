using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WatsEMIAnalyzer.Bindings
{
    public enum UpdateRegionType
    {
        AddRegion,
        RenameRegion,
        RemoveRegion,

        AddSite,
        RemoveSite,

        AddManager,
        RemoveManager,

        AddAnalyzer,
        RemoveAnalyzer,

        UpdateLinkConfigID,
        UpdateEquipParamID,
        UpdateChanSettingID,

        MoveTasksToNewRegion
    }
}
