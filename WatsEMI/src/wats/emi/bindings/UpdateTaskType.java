package wats.emi.bindings;

import javax.xml.bind.annotation.XmlEnum;

@XmlEnum(String.class)
public enum UpdateTaskType {
	AddSite,
    RemoveSite,
    
    AddUnassignedSite,
    RemoveUnassignedSite,
    AcceptUnAssignedSite,

    AddAnalyzer,
    RemoveAnalyzer,

    RenameTask
}
