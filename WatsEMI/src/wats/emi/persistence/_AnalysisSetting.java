package wats.emi.persistence;

import javax.persistence.*;

/**
 * Created with IntelliJ IDEA.
 * User: haoyu
 * Date: 14-3-30
 * Time: 上午12:36
 * To change this template use File | Settings | File Templates.
 */
@Entity
@Table(name = "ANALYSISSETTING")
@SequenceGenerator(name="ANALYSISSETTING_SEQ", sequenceName="ANALYSISSETTING_SEQUENCE")
public class _AnalysisSetting {
    private long id;
    private _EMIFile primaryEmiFile;
    private _EMIFile secondaryEmiFile;
    private _ChannelSettingFile channelSettingFile;
    private _LinkConfigurationFile lingConfigurationFile;
    private boolean useManualLinkConfiguration;
    private int manualLinkConfiguration;
    private _EquipmentParameterFile equipmentParameterFile;
    private double startFreq;
    private double endFreq;
    private double deltaPowerLimit;
    private double channelPowerLimit;
    private boolean useDeltaPowerLimit;
    private boolean useChannelPowerLimit;
    private boolean channelPreferred;
    private boolean displayChannel;

    @Id
    @GeneratedValue(strategy= GenerationType.SEQUENCE, generator="ANALYSISSETTING_SEQ")
    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    @ManyToOne
    @JoinColumn(name = "PRIMARYEMIFILE")
    public _EMIFile getPrimaryEmiFile() {
        return primaryEmiFile;
    }

    public void setPrimaryEmiFile(_EMIFile primaryEmiFile) {
        this.primaryEmiFile = primaryEmiFile;
    }

    @ManyToOne
    @JoinColumn(name = "SECONDARYEMIFILE")
    public _EMIFile getSecondaryEmiFile() {
        return secondaryEmiFile;
    }

    public void setSecondaryEmiFile(_EMIFile secondaryEmiFile) {
        this.secondaryEmiFile = secondaryEmiFile;
    }

    @ManyToOne
    @JoinColumn(name = "CHANNELSETTINGFILE")
    public _ChannelSettingFile getChannelSettingFile() {
        return channelSettingFile;
    }

    public void setChannelSettingFile(_ChannelSettingFile channelSettingFile) {
        this.channelSettingFile = channelSettingFile;
    }

    @ManyToOne
    @JoinColumn(name = "LINGCONFIGURATIONFILE")
    public _LinkConfigurationFile getLingConfigurationFile() {
        return lingConfigurationFile;
    }

    public void setLingConfigurationFile(_LinkConfigurationFile lingConfigurationFile) {
        this.lingConfigurationFile = lingConfigurationFile;
    }

    @Column(name = "USEMANUALLINKCONFIGURATION")
    public boolean isUseManualLinkConfiguration() {
        return useManualLinkConfiguration;
    }

    public void setUseManualLinkConfiguration(boolean useManualLinkConfiguration) {
        this.useManualLinkConfiguration = useManualLinkConfiguration;
    }

    @Column(name = "MANUALLINKCONFIGURATION")
    public int getManualLinkConfiguration() {
        return manualLinkConfiguration;
    }

    public void setManualLinkConfiguration(int manualLinkConfiguration) {
        this.manualLinkConfiguration = manualLinkConfiguration;
    }

    @ManyToOne
    @JoinColumn(name = "EQUIPMENTPARAMETERFILE")
    public _EquipmentParameterFile getEquipmentParameterFile() {
        return equipmentParameterFile;
    }

    public void setEquipmentParameterFile(_EquipmentParameterFile equipmentParameterFile) {
        this.equipmentParameterFile = equipmentParameterFile;
    }

    @Column(name = "STARTFREQ")
    public double getStartFreq() {
        return startFreq;
    }

    public void setStartFreq(double startFreq) {
        this.startFreq = startFreq;
    }

    @Column(name = "ENDFREQ")
    public double getEndFreq() {
        return endFreq;
    }

    public void setEndFreq(double endFreq) {
        this.endFreq = endFreq;
    }

    @Column(name = "DELTAPOWERLIMIT")
    public double getDeltaPowerLimit() {
        return deltaPowerLimit;
    }

    public void setDeltaPowerLimit(double deltaPowerLimit) {
        this.deltaPowerLimit = deltaPowerLimit;
    }

    @Column(name = "CHANNELPOWERLIMIT")
    public double getChannelPowerLimit() {
        return channelPowerLimit;
    }

    public void setChannelPowerLimit(double channelPowerLimit) {
        this.channelPowerLimit = channelPowerLimit;
    }

    @Column(name = "USEDELTAPOWERLIMIT")
    public boolean isUseDeltaPowerLimit() {
        return useDeltaPowerLimit;
    }

    public void setUseDeltaPowerLimit(boolean useDeltaPowerLimit) {
        this.useDeltaPowerLimit = useDeltaPowerLimit;
    }

    @Column(name = "USECHANNELPOWERLIMIT")
    public boolean isUseChannelPowerLimit() {
        return useChannelPowerLimit;
    }

    public void setUseChannelPowerLimit(boolean useChannelPowerLimit) {
        this.useChannelPowerLimit = useChannelPowerLimit;
    }

    @Column(name = "CHANNELPREFERRED")
    public boolean isChannelPreferred() {
        return channelPreferred;
    }

    public void setChannelPreferred(boolean channelPreferred) {
        this.channelPreferred = channelPreferred;
    }

    @Column(name = "DISPLAYCHANNEL")
    public boolean isDisplayChannel() {
        return displayChannel;
    }

    public void setDisplayChannel(boolean displayChannel) {
        this.displayChannel = displayChannel;
    }
}
