package wats.emi.bindings;

import java.io.Serializable;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;

@XmlRootElement(name = "AddTaskRequest")
@XmlType(name = "addTaskRequest")
public class AddTaskResult implements Serializable {
	private static final long serialVersionUID = -620216399582085909L;
	
	private int regionVersion;
	private long taskID;
	
	@XmlElement(name="RegionVersion")
	public int getRegionVersion() {
		return regionVersion;
	}
	
	@XmlElement(name="TaskID")
	public long getTaskID() {
		return taskID;
	}
	
	public void setRegionVersion(int regionVersion) {
		this.regionVersion = regionVersion;
	}
	
	public void setTaskID(long taskID) {
		this.taskID = taskID;
	}
}
