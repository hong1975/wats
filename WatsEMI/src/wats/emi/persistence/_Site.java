package wats.emi.persistence;

import javax.persistence.*;

/**
 * Created with IntelliJ IDEA.
 * User: haoyu
 * Date: 14-3-30
 * Time: 上午12:01
 * To change this template use File | Settings | File Templates.
 */
@Entity
@Table(name = "SITE")
@SequenceGenerator(name="SITE_SEQ", sequenceName="SITE_SEQUENCE")
public class _Site {
    private long id;
    private String siteId;
    private String samId;
    private String siteName;
    private String siteType;
    private double longitude;
    private double latitude;
    private String bsc;
    private String rnc;
    private boolean deleted;

    @Id
    @GeneratedValue(strategy= GenerationType.SEQUENCE, generator="SITE_SEQ")
    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    @Column(name = "SITEID")
    public String getSiteId() {
        return siteId;
    }

    public void setSiteId(String siteId) {
        this.siteId = siteId;
    }

    @Column(name = "SAMID")
    public String getSamId() {
        return samId;
    }

    public void setSamId(String samId) {
        this.samId = samId;
    }

    @Column(name = "SITENAME")
    public String getSiteName() {
        return siteName;
    }

    public void setSiteName(String siteName) {
        this.siteName = siteName;
    }

    @Column(name = "SITETYPE")
    public String getSiteType() {
        return siteType;
    }

    public void setSiteType(String siteType) {
        this.siteType = siteType;
    }

    @Column(name = "LONGITUDE")
    public double getLongitude() {
        return longitude;
    }

    public void setLongitude(double longitude) {
        this.longitude = longitude;
    }

    @Column(name = "LATITUDE")
    public double getLatitude() {
        return latitude;
    }

    public void setLatitude(double latitude) {
        this.latitude = latitude;
    }

    @Column(name = "BSC")
    public String getBsc() {
        return bsc;
    }

    public void setBsc(String bsc) {
        this.bsc = bsc;
    }

    @Column(name = "RNC")
    public String getRnc() {
        return rnc;
    }

    public void setRnc(String rnc) {
        this.rnc = rnc;
    }

    @Column(name = "DELETED")
    public boolean isDeleted() {
        return deleted;
    }

    public void setDeleted(boolean deleted) {
        this.deleted = deleted;
    }
}
