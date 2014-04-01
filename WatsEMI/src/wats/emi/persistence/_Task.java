package wats.emi.persistence;

import javax.persistence.*;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

/**
 * Created with IntelliJ IDEA.
 * User: haoyu
 * Date: 14-3-29
 * Time: 下午11:57
 * To change this template use File | Settings | File Templates.
 */
@Entity
@Table(name = "TASK")
@SequenceGenerator(name="TASK_SEQ", sequenceName="TASK_SEQUENCE")
public class _Task {
    private long id;
    private String name;
    private String description;
    private Date createTime;
    private boolean deleted;

    private _Region region;
    private _User creator;

    private List<_Analysis> analysis = new ArrayList<_Analysis>();

    @Id
    @GeneratedValue(strategy= GenerationType.SEQUENCE, generator="TASK_SEQ")
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

    @Column(name = "DESCRIPTION")
    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
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

    @ManyToOne(cascade = {CascadeType.ALL})
    //@JoinColumn(name = "REGIONID")
    public _Region getRegion() {
        return region;
    }

    public void setRegion(_Region region) {
        this.region = region;
    }

    @ManyToOne
    @JoinColumn(name = "CREATOR")
    public _User getCreator() {
        return creator;
    }

    public void setCreator(_User creator) {
        this.creator = creator;
    }

    @OneToMany(cascade = CascadeType.ALL)
    @JoinTable(
            name="TASK_ANALYSIS",
            joinColumns = @JoinColumn(name="TASK_ID"),
            inverseJoinColumns = @JoinColumn(name="ANALYSIS_ID")
    )
    public List<_Analysis> getAnalysis() {
        return analysis;
    }

    protected void setAnalysis(List<_Analysis> analysis) {
        this.analysis = analysis;
    }
}
