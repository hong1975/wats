package wats.emi.bindings;

import java.io.Serializable;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;

@XmlRootElement(name = "Report")
@XmlType(name = "report")
public class Report implements Serializable {
	private static final long serialVersionUID = -8852345917250497335L;
	
    private long id;
    private String reportTime;
    private String analyzer;
    private boolean pairReport;
    private long taskID;
    private long emiFileID;
    private long channelSettingID;
    private LimitSetting limitSetting;
    private double span;
    private double startFreq;
    private double endFreq;
    private boolean channelPreferred;
    private boolean displayChannel;
    
    @XmlElement(name = "ID")
	public long getId() {
		return id;
	}
	
    @XmlElement(name = "ReportTime")
	public String getReportTime() {
		return reportTime;
	}
	
    @XmlElement(name = "Analyzer")
	public String getAnalyzer() {
		return analyzer;
	}
	
    @XmlElement(name = "IsPairReport")
	public boolean isPairReport() {
		return pairReport;
	}
	
    @XmlElement(name = "TaskID")
	public long getTaskID() {
		return taskID;
	}
	
    @XmlElement(name = "EmiFileID")
	public long getEmiFileID() {
		return emiFileID;
	}
	
    @XmlElement(name = "ChannelSettingID")
	public long getChannelSettingID() {
		return channelSettingID;
	}
	
    @XmlElement(name = "LimitSetting")
	public LimitSetting getLimitSetting() {
		return limitSetting;
	}
	
    @XmlElement(name = "Span")
	public double getSpan() {
		return span;
	}
	
    @XmlElement(name = "StartFreq")
	public double getStartFreq() {
		return startFreq;
	}
	
    @XmlElement(name = "EndFreq")
	public double getEndFreq() {
		return endFreq;
	}
	
    @XmlElement(name = "IsChannelPreferred")
	public boolean isChannelPreferred() {
		return channelPreferred;
	}
	
    @XmlElement(name = "IsDisplayChannel")
	public boolean isDisplayChannel() {
		return displayChannel;
	}

	public void setId(long id) {
		this.id = id;
	}

	public void setReportTime(String reportTime) {
		this.reportTime = reportTime;
	}

	public void setAnalyzer(String analyzer) {
		this.analyzer = analyzer;
	}

	public void setPairReport(boolean pairReport) {
		this.pairReport = pairReport;
	}

	public void setTaskID(long taskID) {
		this.taskID = taskID;
	}

	public void setEmiFileID(long emiFileID) {
		this.emiFileID = emiFileID;
	}

	public void setChannelSettingID(long channelSettingID) {
		this.channelSettingID = channelSettingID;
	}

	public void setLimitSetting(LimitSetting limitSetting) {
		this.limitSetting = limitSetting;
	}

	public void setSpan(double span) {
		this.span = span;
	}

	public void setStartFreq(double startFreq) {
		this.startFreq = startFreq;
	}

	public void setEndFreq(double endFreq) {
		this.endFreq = endFreq;
	}

	public void setChannelPreferred(boolean channelPreferred) {
		this.channelPreferred = channelPreferred;
	}

	public void setDisplayChannel(boolean displayChannel) {
		this.displayChannel = displayChannel;
	}
}
