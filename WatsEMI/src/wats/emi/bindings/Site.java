package wats.emi.bindings;

import java.io.Serializable;

import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlType;

@XmlType(name="site")
public class Site implements Serializable {
	
	private static final long serialVersionUID = -3197299430787279655L;
	
	private String samId;
    private String siteId;
    private String siteName;
    private String siteType;
    private double longitude;
    private double latitude;
    private String bsc;
    private String rnc;
    
    @XmlElement(name = "SamID")
    public String getSamId() {
		return samId;
	}
	
    @XmlElement(name = "SiteID")
	public String getSiteId() {
		return siteId;
	}
	
    @XmlElement(name = "SiteName")
	public String getSiteName() {
		return siteName;
	}
	
    @XmlElement(name = "SiteType")
	public String getSiteType() {
		return siteType;
	}
	
    @XmlElement(name = "Longitude")
	public double getLongitude() {
		return longitude;
	}
	
    @XmlElement(name = "Latitude")
	public double getLatitude() {
		return latitude;
	}
	
    @XmlElement(name = "BSC")
	public String getBsc() {
		return bsc;
	}
	
    @XmlElement(name = "RNC")
	public String getRnc() {
		return rnc;
	}
	
	public void setSamId(String samId) {
		this.samId = samId;
	}
	
	public void setSiteId(String siteId) {
		this.siteId = siteId;
	}
	
	public void setSiteName(String siteName) {
		this.siteName = siteName;
	}
	
	public void setSiteType(String siteType) {
		this.siteType = siteType;
	}
	
	public void setLongitude(double longitude) {
		this.longitude = longitude;
	}
	
	public void setLatitude(double latitude) {
		this.latitude = latitude;
	}
	
	public void setBsc(String bsc) {
		this.bsc = bsc;
	}
	
	public void setRnc(String rnc) {
		this.rnc = rnc;
	}
	

}
