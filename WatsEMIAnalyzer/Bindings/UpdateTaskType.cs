using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEMIAnalyzer.Bindings
{
    public enum UpdateTaskType
    {
        AddSite,
        RemoveSite,

        AddUnassignedSite,
        RemoveUnassignedSite,
        AcceptUnAssignedSite,

        AddAnalyzer,
        RemoveAnalyzer,

        RenameTask,
    }
}
