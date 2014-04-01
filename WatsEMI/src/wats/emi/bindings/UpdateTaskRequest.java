package wats.emi.bindings;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;

@XmlRootElement(name = "UpdateTaskRequest")
@XmlType(name = "updateTaskRequest")
public class UpdateTaskRequest implements Serializable{
	private static final long serialVersionUID = -1000530597373312992L;
	
	private String type;
	private String name;
	private List<String> sites = new ArrayList<String>();
	private List<String> analyzers = new ArrayList<String>();
	
	@XmlElement(name="Type")
	public String getType() {
		return type;
	}
	
	@XmlElement(name="Site")
	public List<String> getSites() {
		return sites;
	}
	
	@XmlElement(name="Analyzer")
	public List<String> getAnalyzers() {
		return analyzers;
	}
	
	@XmlElement(name="NewName")
	public String getName() {
		return name;
	}

	public void setType(String type) {
		this.type = type;
	}
	
	public void setSites(List<String> sites) {
		this.sites = sites;
	}
	
	public void setAnalyzers(List<String> analyzers) {
		this.analyzers = analyzers;
	}
	
	public void setName(String name) {
		this.name = name;
	}	
}
