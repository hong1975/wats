package wats.emi.persistence;

import javax.persistence.*;

/**
 * Created with IntelliJ IDEA.
 * User: haoyu
 * Date: 14-3-30
 * Time: 上午12:34
 * To change this template use File | Settings | File Templates.
 */
@Entity
@Table(name = "ANALYSISTYPE")
@SequenceGenerator(name="ANALYSISTYPE_SEQ", sequenceName="ANALYSISTYPE_SEQUENCE")
public class _AnalysisType {
    private long id;
    private String name;

    @Id
    @GeneratedValue(strategy= GenerationType.SEQUENCE, generator="ANALYSISTYPE_SEQ")
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
}
