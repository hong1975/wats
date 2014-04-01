package wats.emi.bindings;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;

@XmlRootElement(name = "Task")
@XmlType(name = "task")
public class Task implements Serializable {

	private static final long serialVersionUID = -3415581716002642193L;
	
    private long id;
	private String name;
	private String description;
	private String regionID;
	private String creator;
	private String createTime;
	private long channelSettingID;
	private long linkConfigurationID;
	private long equipmentParameterID;
	private List<String> sites = new ArrayList<String>();
	private List<String> unAssignedSites = new ArrayList<String>();
	private List<String> analyzers = new ArrayList<String>();

	@XmlElement(name = "ID")
	public long getId() {
		return id;
	}
	
	@XmlElement(name = "Name")
	public String getName() {
		return name;
	}
	
	@XmlElement(name = "Description")
	public String getDescription() {
		return description;
	}
	
	@XmlElement(name="RegionID")
	public String getRegionID() {
		return regionID;
	}
	
	@XmlElement(name = "Creator")
	public String getCreator() {
		return creator;
	}
	
	@XmlElement(name = "CreateTime")
	public String getCreateTime() {
		return createTime;
	}
	
	@XmlElement(name = "Site")
	public List<String> getSites() {
		return sites;
	}
	
	@XmlElement(name = "UnassignedSite")
	public List<String> getUnassignedSites() {
		return unAssignedSites;
	}
	
	@XmlElement(name = "ChannelSettingID")
	public long getChannelSettingID() {
		return channelSettingID;
	}
	
	@XmlElement(name = "LinkConfigurationID")
	public long getLinkConfigurationID() {
		return linkConfigurationID;
	}
	
	@XmlElement(name = "EquipmentParameterID")
	public long getEquipmentParameterID() {
		return equipmentParameterID;
	}
	
	@XmlElement(name = "Analyzer")
	public List<String> getAnalyzers() {
		return analyzers;
	}
	
	public void setId(long id) {
		this.id = id;
	}
	
	public void setName(String name) {
		this.name = name;
	}
	
	public void setDescription(String description) {
		this.description = description;
	}
	
	public void setRegionID(String regionID) {
		this.regionID = regionID;
	}
	
	public void setCreator(String creator) {
		this.creator = creator;
	}
	
	public void setCreateTime(String createTime) {
		this.createTime = createTime;
	}
	
	public void addSite(String site) {
		this.sites.add(site);
	}
	
	public void setChannelSettingID(long channelSettingID) {
		this.channelSettingID = channelSettingID;
	}
	
	public void setLinkConfigurationID(long linkConfigurationID) {
		this.linkConfigurationID = linkConfigurationID;
	}
	
	public void setEquipmentParameterID(long equipmentParameterID) {
		this.equipmentParameterID = equipmentParameterID;
	}
	
	public void setAnalyzers(List<String> analyzers) {
		this.analyzers = analyzers;
	}
	
	public void setSites(List<String> sites) {
		this.sites = sites;
	}
	
	public void setUnAssignedSites(List<String> unAssignedSites) {
		this.unAssignedSites = unAssignedSites;
	}
}
