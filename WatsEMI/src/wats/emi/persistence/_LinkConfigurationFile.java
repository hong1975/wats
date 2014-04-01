package wats.emi.persistence;

import javax.persistence.*;

/**
 * Created with IntelliJ IDEA.
 * User: haoyu
 * Date: 14-3-30
 * Time: 上午12:18
 * To change this template use File | Settings | File Templates.
 */
@Entity
@Table(name = "LINKCONFIGURATIONFILE")
@SequenceGenerator(name="LINKCONFIGURATION_SEQ", sequenceName="LINKCONFIGURATION_SEQUENCE")
public class _LinkConfigurationFile {
    private long id;
    private String title;
    private String fileName;
    private byte[] content;
    private String md5;
    private byte[] parseData;
    private String uploader;
    private String createTime;
    private boolean deleted;

    @Id
    @GeneratedValue(strategy= GenerationType.SEQUENCE, generator="LINKCONFIGURATION_SEQ")
    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    @Column(name = "TITLE")
    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    @Column(name = "FILENAME")
    public String getFileName() {
        return fileName;
    }

    public void setFileName(String fileName) {
        this.fileName = fileName;
    }

    @Lob
    @Column(name = "FILECONTENT")
    public byte[] getContent() {
        return content;
    }

    public void setContent(byte[] content) {
        this.content = content;
    }

    @Column(name = "MD5")
    public String getMd5() {
        return md5;
    }

    public void setMd5(String md5) {
        this.md5 = md5;
    }

    @Column(name = "PARSEDATA")
    public byte[] getParseData() {
        return parseData;
    }

    public void setParseData(byte[] parseData) {
        this.parseData = parseData;
    }

    @Column(name = "UPLOADER")
    public String getUploader() {
        return uploader;
    }

    public void setUploader(String uploader) {
        this.uploader = uploader;
    }

    @Column(name = "CREATETIME")
    public String getCreateTime() {
        return createTime;
    }

    public void setCreateTime(String createTime) {
        this.createTime = createTime;
    }

    @Column(name = "DELETED")
    public boolean isDeleted() {
        return deleted;
    }

    public void setDeleted(boolean deleted) {
        this.deleted = deleted;
    }
}
