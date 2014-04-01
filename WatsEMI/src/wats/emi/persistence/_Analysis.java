package wats.emi.persistence;

import javax.persistence.*;
import java.util.Date;

/**
 * Created with IntelliJ IDEA.
 * User: haoyu
 * Date: 14-3-30
 * Time: 上午1:03
 * To change this template use File | Settings | File Templates.
 */
@Entity
@Table(name = "ANALYSIS")
@SequenceGenerator(name="ANALYSIS_SEQ", sequenceName="ANALYSIS_SEQUENCE")
public class _Analysis {
    private long id;
    private _User analyzer;
    private _AnalysisType type;
    private _AnalysisSetting setting;
    private Date analyzeTime;

    @Id
    @GeneratedValue(strategy= GenerationType.SEQUENCE, generator="ANALYSIS_SEQ")
    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    @ManyToOne
    @JoinColumn(name = "ANALYZER")
    public _User getAnalyzer() {
        return analyzer;
    }

    public void setAnalyzer(_User analyzer) {
        this.analyzer = analyzer;
    }

    @ManyToOne
    @JoinColumn(name = "TYPE")
    public _AnalysisType getType() {
        return type;
    }

    public void setType(_AnalysisType type) {
        this.type = type;
    }

    @ManyToOne
    @JoinColumn(name = "SETTING")
    public _AnalysisSetting getSetting() {
        return setting;
    }

    public void setSetting(_AnalysisSetting setting) {
        this.setting = setting;
    }

    @Column(name = "ANALYZETIME")
    public Date getAnalyzeTime() {
        return analyzeTime;
    }

    public void setAnalyzeTime(Date analyzeTime) {
        this.analyzeTime = analyzeTime;
    }
}
