package wats.emi.bindings;

import java.io.Serializable;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;

@XmlRootElement(name="FileDescription")
@XmlType(name="fileDescription")
public class FileDescription implements Serializable {
	private static final long serialVersionUID = -1635462907882789146L;

	private long ID;
	private String Title;
	private String FileName;
	private String Uploader;
	private String CreateTime;
	
	private String tester = "";
	private String siteId = "";
	private String testTime = "";
	
	@XmlElement(name = "ID")
	public long getID() {
		return ID;
	}
	
	@XmlElement(name = "Title")
	public String getTitle() {
		return Title;
	}
	
	@XmlElement(name = "FileName")
	public String getFileName() {
		return FileName;
	}
	
	@XmlElement(name = "Uploader")
	public String getUploader() {
		return Uploader;
	}
	
	@XmlElement(name = "CreateTime")
	public String getCreateTime() {
		return CreateTime;
	}
	
	@XmlElement(name = "Tester")
	public String getTester() {
		return tester;
	}

	@XmlElement(name = "SiteID")
	public String getSiteId() {
		return siteId;
	}

	@XmlElement(name = "TestTime")
	public String getTestTime() {
		return testTime;
	}

	public void setID(long id) {
		ID = id;
	}
	
	public void setTitle(String title) {
		Title = title;
	}
	
	public void setFileName(String fileName) {
		FileName = fileName;
	}
	
	public void setUploader(String uploader) {
		Uploader = uploader;
	}
	
	public void setCreateTime(String createTime) {
		CreateTime = createTime;
	}
	
	public void setTester(String tester) {
		this.tester = tester;
	}

	public void setSiteId(String siteId) {
		this.siteId = siteId;
	}
	
	public void setTestTime(String testTime) {
		this.testTime = testTime;
	}
}
