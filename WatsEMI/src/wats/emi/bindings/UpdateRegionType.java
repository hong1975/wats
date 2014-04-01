package wats.emi.bindings;

import javax.xml.bind.annotation.XmlEnum;

@XmlEnum(String.class)
public enum UpdateRegionType {
	AddRegion,
    RenameRegion,
    RemoveRegion,

    AddSite,
    RemoveSite,
    
    AddManager,
    RemoveManager,
    
    UpdateLinkConfigID,
    UpdateEquipParamID,
    UpdateChanSettingID,

    MoveTaskToNewRegion
}
