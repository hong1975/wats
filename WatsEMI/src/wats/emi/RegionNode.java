package wats.emi;

import java.util.ArrayList;
import java.util.List;

public class RegionNode {
	
	private List<String> managers = new ArrayList<String>();
	private List<RegionNode> subRegions = new ArrayList<RegionNode>();
	private List<TaskNode> taskNodes = new ArrayList<TaskNode>();
	
	private int linkConfigurationID = -1;
	private int equipmentParameterID = -1; 
	private int channelSettingID = -1;

	private String name;
	private int id;
	
	
	public String getName() {
		return name;
	}
	
	public void setName(String name) {
		this.name = name;
	}
	
	public int getId() {
		return id;
	}
	
	public void setId(int id) {
		this.id = id;
	}
	
	public void addManager(String manager) {
		managers.add(manager);
	}
	
	public void removeManager(String manager) {
		managers.remove(manager);
	}
	
	public List<String> getManagers() {
		return managers;
	}
	
	public void addRegion(RegionNode regionNode) {
		subRegions.add(regionNode);
	}
	
	public void removeRegion(RegionNode regionNode) {
		subRegions.remove(regionNode);
	}
	
	public List<RegionNode> getSubRegions() {
		return subRegions; 
	}
	
	public void addTask(TaskNode taskNode) {
		taskNodes.add(taskNode);
	}
	
	public void removeTask(TaskNode taskNode) {
		taskNodes.remove(taskNode);
	}
	
	public List<TaskNode> getTasks() {
		return taskNodes;
	}
}
