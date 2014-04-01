package wats.emi.bindings;

import java.io.Serializable;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;

@XmlRootElement(name = "ID")
@XmlType(name = "id")
public class ID implements Serializable {
	private static final long serialVersionUID = -4809711110590727904L;
	private long id;

	@XmlElement(name = "long")
	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}
}
