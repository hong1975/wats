package wats.emi.bindings;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;

@XmlRootElement(name = "UpdateRegionRequest")
@XmlType(name = "updateRegionRequest")
public class UpdateRegionRequest implements Serializable {
	private static final long serialVersionUID = -1578681951224319515L;
	
	private int newVer;
	private UpdateRegionType type;
	private Region region;
	private List<String> sites = new ArrayList<String>();
	
	private long linkConfigID = -1;
	private long equipParamID = -1;
	private long chanSettingID = -1;
	
	private String regionName;
	
	@XmlElement(name="NewVer")
	public int getNewVer() {
		return newVer;
	}
	
	@XmlElement(name="Type")
	public UpdateRegionType getType() {
		return type;
	}
	
	@XmlElement(name="Region")
	public Region getRegion() {
		return region;
	}
	
	@XmlElement(name="Site")
	public List<String> getSites() {
		return sites;
	}
	
	@XmlElement(name="LinkConfigurationID")
	public long getLinkConfigID() {
		return linkConfigID;
	}


	@XmlElement(name="EquipmentParameterID")
	public long getEquipParamID() {
		return equipParamID;
	}


	@XmlElement(name="ChannelSettingID")
	public long getChanSettingID() {
		return chanSettingID;
	}
	
	@XmlElement(name="NewRegionName")
	public String getRegionName() {
		return regionName;
	}

	public void setNewVer(int newVer) {
		this.newVer = newVer;
	}
	
	public void setType(UpdateRegionType type) {
		this.type = type;
	}
	
	public void setRegion(Region region) {
		this.region = region;
	}
	
	public void setSites(List<String> sites) {
		this.sites = sites;
	}
	
	public void setLinkConfigID(long linkConfigID) {
		this.linkConfigID = linkConfigID;
	}
	
	public void setEquipParamID(long equipParamID) {
		this.equipParamID = equipParamID;
	}
	
	public void setChanSettingID(long chanSettingID) {
		this.chanSettingID = chanSettingID;
	}
	
	public void setRegionName(String regionName) {
		this.regionName = regionName;
	}

}
