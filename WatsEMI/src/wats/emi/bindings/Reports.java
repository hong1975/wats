package wats.emi.bindings;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;

@XmlRootElement(name = "Reports")
@XmlType(name = "reports")
public class Reports implements Serializable {

	private static final long serialVersionUID = 2170404979938230278L;
	
	private List<Report> reports = new ArrayList<Report>();

	@XmlElement(name = "Report")
	public List<Report> getReports() {
		return reports;
	}

	public void setReports(List<Report> reports) {
		this.reports = reports;
	}
}
