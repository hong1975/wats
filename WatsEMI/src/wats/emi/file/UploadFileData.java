package wats.emi.file;

import java.io.ByteArrayInputStream;
import java.io.DataInputStream;
import java.io.IOException;

public class UploadFileData {
	
	private String shortName;
    private String uploader;
    private String md5;
    private byte[] fileContent;
    private byte[] parseData;
    
    public static UploadFileData deserialize(byte[] rawData) throws IOException {
    	UploadFileData uploadFileData = new UploadFileData();
    	
    	ByteArrayInputStream bsIs = new ByteArrayInputStream(rawData);
    	DataInputStream dis = new DataInputStream(bsIs);
    	
    	int shortNameLen = dis.readInt();
    	byte[] shortNameBytes = new byte[shortNameLen];
    	dis.read(shortNameBytes, 0, shortNameLen);
    	uploadFileData.shortName = new String(shortNameBytes, "UTF-8");
    	
    	int uploaderLen = dis.readInt();
    	byte[] uploaderBytes = new byte[uploaderLen];
    	dis.read(uploaderBytes, 0, uploaderLen);
    	uploadFileData.uploader = new String(uploaderBytes, "UTF-8");
    	
    	int md5Len = dis.readInt();
    	byte[] md5Bytes = new byte[md5Len];
    	dis.read(md5Bytes, 0, md5Len);
    	uploadFileData.md5 = new String(md5Bytes, "UTF-8");
    	
    	int fileContentLen = dis.readInt();
    	uploadFileData.fileContent = new byte[fileContentLen];
    	dis.read(uploadFileData.fileContent, 0, fileContentLen);
    	
    	int parseDataLen = dis.readInt();
    	uploadFileData.parseData = new byte[parseDataLen];
    	dis.read(uploadFileData.parseData, 0, parseDataLen);
    	
    	return uploadFileData;  	
    }
    
    private UploadFileData() {
    	
    }
    
	public String getShortName() {
		return shortName;
	}
	public void setShortName(String shortName) {
		this.shortName = shortName;
	}
	public String getUploader() {
		return uploader;
	}
	public void setUploader(String uploader) {
		this.uploader = uploader;
	}
	public String getMd5() {
		return md5;
	}
	public void setMd5(String md5) {
		this.md5 = md5;
	}
	public byte[] getFileContent() {
		return fileContent;
	}
	public void setFileContent(byte[] fileContent) {
		this.fileContent = fileContent;
	}
	public byte[] getParseData() {
		return parseData;
	}
	public void setParseData(byte[] parseData) {
		this.parseData = parseData;
	}
    
    

}
