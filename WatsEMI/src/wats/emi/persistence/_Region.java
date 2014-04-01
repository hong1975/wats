package wats.emi.persistence;

import javax.persistence.*;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

/**
 * Created with IntelliJ IDEA.
 * User: haoyu
 * Date: 14-3-30
 * Time: 上午12:06
 * To change this template use File | Settings | File Templates.
 */
@Entity
@Table(name = "REGION")
@SequenceGenerator(name="REGION_SEQ", sequenceName="REGION_SEQUENCE")
public class _Region {
    private long id;
    private String name;
    private _User creator;
    private Date createTime;
    private boolean deleted;

    private List<_Region> subRegions = new ArrayList<_Region>();
    private List<_Task> tasks = new ArrayList<_Task>();
    private List<_Site> sites = new ArrayList<_Site>();
    private List<_ChannelSettingFile> channelSettings = new ArrayList<_ChannelSettingFile>();
    private List<_LinkConfigurationFile> lingConfigurationFiles = new ArrayList<_LinkConfigurationFile>();
    private List<_EquipmentParameterFile> equipmentParameterFiles = new ArrayList<_EquipmentParameterFile>();
    private List<_User> managers = new ArrayList<_User>();

    @Id
    @GeneratedValue(strategy= GenerationType.SEQUENCE, generator="REGION_SEQ")
    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    @Column(name = "NAME")
    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    @ManyToOne
    @JoinColumn(name = "CREATOR")
    public _User getCreator() {
        return creator;
    }

    public void setCreator(_User creator) {
        this.creator = creator;
    }

    @Column(name = "CREATETIME")
    public Date getCreateTime() {
        return createTime;
    }

    public void setCreateTime(Date createTime) {
        this.createTime = createTime;
    }

    @Column(name = "DELETED")
    public boolean isDeleted() {
        return deleted;
    }

    public void setDeleted(boolean deleted) {
        this.deleted = deleted;
    }

    @OneToMany(cascade = CascadeType.ALL)
    @JoinTable(
            name="REGION_STRUCTURE",
            joinColumns = @JoinColumn(name="REGION_ID"),
            inverseJoinColumns = @JoinColumn(name="SUB_REGION_ID")
    )
    @OrderBy("name")
    public List<_Region> getSubRegions() {
        return subRegions;
    }

    protected void setSubRegions(List<_Region> subRegions) {
        this.subRegions = subRegions;
    }

    @OneToMany(mappedBy="region",
        cascade={CascadeType.ALL}
    )
    @OrderBy("createTime desc, name asc")
    public List<_Task> getTasks() {
        return tasks;
    }

    protected void setTasks(List<_Task> tasks) {
        this.tasks = tasks;
    }

    @ManyToMany(cascade = CascadeType.ALL)
    @JoinTable(
        name="REGION_SITE",
        joinColumns = @JoinColumn(name="REGION_ID"),
        inverseJoinColumns = @JoinColumn(name="SITE_ID")
    )
    @OrderBy("siteId, siteName")
    public List<_Site> getSites() {
        return sites;
    }

    protected void setSites(List<_Site> sites) {
        this.sites = sites;
    }

    @ManyToMany(cascade = CascadeType.ALL)
    @JoinTable(
        name="REGION_CHANNELSETTINGFILE",
        joinColumns = @JoinColumn(name="REGION_ID"),
        inverseJoinColumns = @JoinColumn(name="CHANNELSETTINGFILE_ID")
    )
    @OrderBy("createTime desc")
    public List<_ChannelSettingFile> getChannelSettings() {
        return channelSettings;
    }

    protected void setChannelSettings(List<_ChannelSettingFile> channelSettings) {
        this.channelSettings = channelSettings;
    }

    @ManyToMany(cascade = CascadeType.ALL)
    @JoinTable(
        name="REGION_LINKCONFIGURATIONFILE",
        joinColumns = @JoinColumn(name="REGION_ID"),
        inverseJoinColumns = @JoinColumn(name="LINKCONFIGURATIONFILE_ID")
    )
    @OrderBy("createTime desc")
    public List<_LinkConfigurationFile> getLingConfigurationFiles() {
        return lingConfigurationFiles;
    }

    protected void setLingConfigurationFiles(List<_LinkConfigurationFile> lingConfigurationFiles) {
        this.lingConfigurationFiles = lingConfigurationFiles;
    }

    @ManyToMany(cascade = CascadeType.ALL)
    @JoinTable(
        name="REGION_EQUIPMENTPARAMETER",
        joinColumns = @JoinColumn(name="REGION_ID"),
        inverseJoinColumns = @JoinColumn(name="EQUIPMENTPARAMETER_ID")
    )
    @OrderBy("createTime desc")
    public List<_EquipmentParameterFile> getEquipmentParameterFiles() {
        return equipmentParameterFiles;
    }

    protected void setEquipmentParameterFiles(List<_EquipmentParameterFile> equipmentParameterFiles) {
        this.equipmentParameterFiles = equipmentParameterFiles;
    }

    @ManyToMany(cascade = CascadeType.ALL)
    @JoinTable(
        name="REGION_MANAGER",
        joinColumns = @JoinColumn(name="REGION_ID"),
        inverseJoinColumns = @JoinColumn(name="MANAGER_ID")
    )
    @OrderBy("userId")
    public List<_User> getManagers() {
        return managers;
    }

    protected void setManagers(List<_User> managers) {
        this.managers = managers;
    }
}
