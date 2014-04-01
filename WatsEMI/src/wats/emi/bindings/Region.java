package wats.emi.bindings;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

import javax.xml.bind.annotation.*;

@XmlRootElement(name = "Region")
@XmlType(name = "region")
public class Region  implements Serializable {
	private static final long serialVersionUID = 6466995339607680875L;
	
	private String parentId;
	private String id;
	private String name;
	
	private long channelSettingId = -1;
	private boolean validChannelSetting = false;
	
	private long linkConfigurationID = -1;
	private boolean validLinkConfiguration = false;
	
	private long equipmentParameterID = -1;
	private boolean validEquipmentParameter = false;
	
	private String owner;
	private List<String> sites = new ArrayList<String>(); 
	private List<String> managers = new ArrayList<String>();
	private List<Region> regions = new ArrayList<Region>();
	private List<Long> tasks = new ArrayList<Long>();
	private int version;
	
	private Region parent = null;
	
	@XmlElement(name = "Version")
	public int getVersion() {
		return version;
	}

	@XmlElement(name = "Owner")
	public String getOwner() {
		return owner;
	}
	
	@XmlElement(name = "ParentID")
	public String getParentId() {
		return parentId;
	}

	@XmlElement(name = "ID")
	public String getId() {
		return id;
	}
	
	@XmlElement(name = "Name")
	public String getName() {
		return name;
	}
	
	@XmlElement(name = "ChannelSettingID")
	public long getChannelSettingId() {
		return channelSettingId;
	}
	
	@XmlElement(name = "LinkConfigurationID")
	public long getLinkConfigurationID() {
		return linkConfigurationID;
	}
	
	@XmlElement(name = "EquipmentParameterID")
	public long getEquipmentParameterID() {
		return equipmentParameterID;
	}
	
	@XmlElement(name = "Site")
	public List<String> getSites() {
		return sites;
	}

    @XmlElement(name = "Manager")
	public List<String> getManagers() {
		return managers;
	}
	
	//@JsonManagedReference
	@XmlElement(name = "Sub")
	public List<Region> getRegions() {
		return regions;
	}
	
	@XmlElement(name = "Task")
	public List<Long> getTasks() {
		return tasks;
	}
	
	@XmlElement(name = "ValidChannelSetting")
	public boolean isValidChannelSetting() {
		return validChannelSetting;
	}

	@XmlElement(name = "ValidLinkConfiguration")
	public boolean isValidLinkConfiguration() {
		return validLinkConfiguration;
	}

	@XmlElement(name = "ValidEquipmentParameter")
	public boolean isValidEquipmentParameter() {
		return validEquipmentParameter;
	}
	
	@XmlTransient
	public Region getParent() {
		return parent;
	}
	
	public void setVersion(int version) {
		this.version = version;
	}
	
	public void setOwner(String owner) {
		this.owner = owner;
	}
	
	public void setParentId(String parentId) {
		this.parentId = parentId;
	}

	public void setId(String id) {
		this.id = id;
	}
	
	public void setName(String name) {
		this.name = name;
	}
	
	public void setChannelSettingId(long channelSettingId) {
		this.channelSettingId = channelSettingId;
	}
	
	public void setLinkConfigurationID(long linkConfigurationID) {
		this.linkConfigurationID = linkConfigurationID;
	}
	
	public void setEquipmentParameterID(long equipmentParameterID) {
		this.equipmentParameterID = equipmentParameterID;
	}
	
	public void setSites(List<String> sites) {
		this.sites = sites;
	}
	
	public void setManagers(List<String> managers) {
		this.managers = managers;
	}
	
	public void setRegions(List<Region> regions) {
		this.regions = regions;
	}
	
	public void setTasks(List<Long> tasks) {
		this.tasks = tasks;
		//this.tasks = tasks;
	}

	public void setValidChannelSetting(boolean validChannelSetting) {
		this.validChannelSetting = validChannelSetting;
	}

	public void setValidLinkConfiguration(boolean validLinkConfiguration) {
		this.validLinkConfiguration = validLinkConfiguration;
	}

	public void setValidEquipmentParameter(boolean validEquipmentParameter) {
		this.validEquipmentParameter = validEquipmentParameter;
	}
	
	public void setParent(Region parent) {
		this.parent = parent;
	}
	
	public void addSite(String site) {
		sites.add(site);
	}
	
	public void addManager(String manager) {
		managers.add(manager);
	}
	
	public void addTask(Long taskID) {
		tasks.add(taskID);
	}
	
	public void addRegion(Region region) {
		regions.add(region);
	}

}
