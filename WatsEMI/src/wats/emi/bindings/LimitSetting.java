package wats.emi.bindings;

import java.io.Serializable;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;

@XmlRootElement(name = "LimitSetting")
@XmlType(name = "limitSetting")
public class LimitSetting implements Serializable {

	private static final long serialVersionUID = 2885758023096687735L;
	
	private boolean useChannelPowerLimit;
	private boolean useDeltaPowerLimit;
	private int channelPowerLimit;
	private int deltaPowerLimit;
	
	@XmlElement(name = "UseChannelPowerLimit")
	public boolean isUseChannelPowerLimit() {
		return useChannelPowerLimit;
	}
	
	@XmlElement(name = "UseDeltaPowerLimit")
	public boolean isUseDeltaPowerLimit() {
		return useDeltaPowerLimit;
	}
	
	@XmlElement(name = "DeltaPowerLimit")
	public int getDeltaPowerLimit() {
		return deltaPowerLimit;
	}
	
	@XmlElement(name = "ChannelPowerLimit")
	public int getChannelPowerLimit() {
		return channelPowerLimit;
	}
	
	public void setUseChannelPowerLimit(boolean mUseChannelPowerLimit) {
		this.useChannelPowerLimit = mUseChannelPowerLimit;
	}
	
	public void setUseDeltaPowerLimit(boolean mUseDeltaPowerLimit) {
		this.useDeltaPowerLimit = mUseDeltaPowerLimit;
	}
	
	public void setChannelPowerLimit(int mChannelPowerLimit) {
		this.channelPowerLimit = mChannelPowerLimit;
	}
	
	public void setDeltaPowerLimit(int mDeltaPowerLimit) {
		this.deltaPowerLimit = mDeltaPowerLimit;
	}
}
