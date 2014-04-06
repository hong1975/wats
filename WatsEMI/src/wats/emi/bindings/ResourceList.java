package wats.emi.bindings;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;
import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

/**
 * Created with IntelliJ IDEA.
 * User: haoyu
 * Date: 14-4-6
 * Time: 下午4:15
 * To change this template use File | Settings | File Templates.
 */
@XmlRootElement(name = "Reports")
@XmlType(name = "reports")
public class ResourceList  implements Serializable {
    private static final long serialVersionUID = -7248371329878611769L;

    private String colorMd5;
    private List<Long> sites = new ArrayList<Long>();
    private List<Long> emis = new ArrayList<Long>();
    private List<Long> channels = new ArrayList<Long>();
    private List<Long> links = new ArrayList<Long>();
    private List<Long> equipments = new ArrayList<Long>();
    private List<Long> regions = new ArrayList<Long>();
    private List<Long> tasks = new ArrayList<Long>();
    private List<Long> analysis = new ArrayList<Long>();

    @XmlElement(name = "ColorMd5")
    public String getColorMd5() {
        return colorMd5;
    }

    public void setColorMd5(String colorMd5) {
        this.colorMd5 = colorMd5;
    }

    @XmlElement(name = "Site")
    public List<Long> getSites() {
        return sites;
    }

    public void setSites(List<Long> sites) {
        this.sites = sites;
    }

    public void addSite(long site) {
        sites.add(site);
    }

    @XmlElement(name = "Emi")
    public List<Long> getEmis() {
        return emis;
    }

    public void setEmis(List<Long> emis) {
        this.emis = emis;
    }

    public void addEmi(long emi) {
        emis.add(emi);
    }

    @XmlElement(name = "Channel")
    public List<Long> getChannels() {
        return channels;
    }

    public void setChannels(List<Long> channels) {
        this.channels = channels;
    }

    public void addChannel(long channel) {
        channels.add(channel);
    }

    @XmlElement(name = "Link")
    public List<Long> getLinks() {
        return links;
    }

    public void setLinks(List<Long> links) {
        this.links = links;
    }

    public void addLink(long link) {
        links.add(link);
    }

    @XmlElement(name = "Equipment")
    public List<Long> getEquipments() {
        return equipments;
    }

    public void setEquipments(List<Long> equipments) {
        this.equipments = equipments;
    }

    public void addEquipment(long equipment) {
        equipments.add(equipment);
    }

    @XmlElement(name = "Region")
    public List<Long> getRegions() {
        return regions;
    }

    public void setRegions(List<Long> regions) {
        this.regions = regions;
    }

    public void addRegion(long region) {
        regions.add(region);
    }

    @XmlElement(name = "Task")
    public List<Long> getTasks() {
        return tasks;
    }

    public void setTasks(List<Long> tasks) {
        this.tasks = tasks;
    }

    public void addTask(long task) {
        tasks.add(task);
    }

    @XmlElement(name = "Analysis")
    public List<Long> getAnalysis() {
        return analysis;
    }

    public void setAnalysis(List<Long> analysis) {
        this.analysis = analysis;
    }

    public void addAnalysis(long analysis) {
        this.analysis.add(analysis);
    }
}
