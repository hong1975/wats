package wats.emi.bindings;

import java.util.ArrayList;
import java.util.List;
import java.io.Serializable;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;

@XmlRootElement(name = "Sites")
@XmlType(name="sites")
public class Sites implements Serializable {

	private static final long serialVersionUID = 8416399499771708875L;
	
    private List<Site> sites = new ArrayList<Site>();
	
	public void addSite(Site site) {
		sites.add(site);
    }

	@XmlElement(name = "Site")
    public List<Site> getSites() {
        return sites;
    }

}
