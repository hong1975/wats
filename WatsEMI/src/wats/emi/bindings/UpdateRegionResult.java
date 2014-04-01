package wats.emi.bindings;

import java.io.Serializable;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;

@XmlRootElement(name = "UpdateRegionResult")
@XmlType(name = "updateRegionResult")
public class UpdateRegionResult implements Serializable {
	private static final long serialVersionUID = -7036821556597880057L;
	
	private UpdateRegionType type;
    private int newVer;
    private Region region;
    
    @XmlElement(name="Type")
	public UpdateRegionType getType() {
		return type;
	}
	
    @XmlElement(name="NewVer")
	public int getNewVer() {
		return newVer;
	}
	
    @XmlElement(name="Region")
	public Region getRegion() {
		return region;
	}
	
	public void setType(UpdateRegionType type) {
		this.type = type;
	}
	
	public void setNewVer(int newVer) {
		this.newVer = newVer;
	}
	
	public void setRegion(Region region) {
		this.region = region;
	}
}
