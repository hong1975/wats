package wats.emi.bindings;

import java.io.Serializable;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;

@XmlRootElement(name = "UpdateRegionRequest")
@XmlType(name = "updateRegionRequest")
public class ErrorMessage implements Serializable {
	
	private static final long serialVersionUID = -167690037377591759L;
	private String name;
	private String description;
	
	@XmlElement(name="Name")
	public String getName() {
		return name;
	}
	
	@XmlElement(name="Description")
	public String getDescription() {
		return description;
	}

	public void setName(String name) {
		this.name = name;
	}
	
	public void setDescription(String description) {
		this.description = description;
	}
}
