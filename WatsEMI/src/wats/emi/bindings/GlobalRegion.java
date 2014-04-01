package wats.emi.bindings;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.InputStream;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.io.Serializable;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.List;
import java.util.logging.Logger;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;
import wats.emi.db.ConnectionManager;
import wats.emi.utility.Utility;

@XmlRootElement(name="GlobalRegion")
@XmlType(name="globalRegion")
public class GlobalRegion implements Serializable {
	private static final long serialVersionUID = 1163376855319991715L;

	private static final Logger logger = Logger.getLogger(GlobalRegion.class.getName());
	private static GlobalRegion globalRegion = null;
	private int version = 0;
	private Region root;

	public static GlobalRegion instance() {
		if (globalRegion == null) {
			Connection conn = ConnectionManager.getInstance();
		    PreparedStatement pstmt = null;
		    if (conn != null) {
		    	try {
    				String sql = "SELECT * FROM (SELECT * FROM regions ORDER BY Version DESC) WHERE ROWNUM=1";
    				pstmt = conn.prepareStatement(sql, ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_READ_ONLY);

    				ResultSet rs = pstmt.executeQuery(sql);
    				int count = Utility.getRecordCount(rs);
    				logger.finest("Region count=" + count);
    				if (count == 0) {
    					pstmt.close();
    					pstmt = null;

    					globalRegion = new GlobalRegion();
    					globalRegion.setVersion(0);
    					Region root = new Region();
    					root.setId("0");
    					root.setName("Global");
    					globalRegion.setRoot(root);
    				
						sql = "INSERT INTO regions(Version, Data) VALUES(0, ?)";
						pstmt = conn.prepareStatement(sql);
						ByteArrayOutputStream baos = new ByteArrayOutputStream();
						ObjectOutputStream oos = new ObjectOutputStream(baos);
						oos.writeObject(globalRegion);

						ByteArrayInputStream bais = new ByteArrayInputStream(baos.toByteArray());
						pstmt.setBinaryStream(1, bais, baos.toByteArray().length);
						pstmt.executeUpdate();
    				} else {
    					rs.next();
    					InputStream is = rs.getBinaryStream("Data");
    					ObjectInputStream ois = new ObjectInputStream(is);
        				globalRegion = (GlobalRegion)ois.readObject();
    				}
		    	}
    			catch (Exception e) {
    				logger.warning(e.getMessage());
    				e.printStackTrace();
    				globalRegion = null;
		    	}
				finally {
					if (pstmt != null)
						try {
							pstmt.close();
						} catch (SQLException e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						}
    				}
		    	
		    		if (conn != null) {
		    			try {
							conn.close();
						} catch (SQLException e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						}
		    		}
				}
        }
		
		return globalRegion;
	}
	
	public synchronized static Region findRegionByID(Region startRegion, String regionID) {
		if (regionID.equals(startRegion.getId())) {
			return startRegion;
		}
		
		Region matchedRegion;
		for(Region subRegion : startRegion.getRegions()) {
			matchedRegion = findRegionByID(subRegion, regionID);
			if (null != matchedRegion) {
				return matchedRegion;
			}
		}
		
		return null;
	}
	
	public synchronized static Region findRegionByName(Region startRegion, String regionName) {
		if (regionName.equals(startRegion.getName())) {
			return startRegion;
		}
		
		Region matchedRegion;
		for(Region subRegion : startRegion.getRegions()) {
			matchedRegion = findRegionByName(subRegion, regionName);
			if (null != matchedRegion) {
				return matchedRegion;
			}
		}
		
		return null;
	}
	
	public synchronized List<String> getTaskManagers(Region startRegion, long taskID) {
		if (startRegion.getTasks().contains(taskID)) {
			return startRegion.getManagers();
		}
		
		List<String> managers;
		for(Region subRegion : startRegion.getRegions()) {
			managers = getTaskManagers(subRegion, taskID);
			if (null != managers) {
				return managers;
			}
		}
		
		return null;
	}

	public synchronized void storeRegion() throws Exception {
		Connection conn = ConnectionManager.getInstance();
		PreparedStatement pstmt = null;
		if (conn != null) {
			try {
				this.version += 1;

				String sql = "INSERT INTO regions(Version, Data) VALUES(" + this.version + ", ?)";
				pstmt = conn.prepareStatement(sql);
				ByteArrayOutputStream baos = new ByteArrayOutputStream();
				ObjectOutputStream oos = new ObjectOutputStream(baos);
				oos.writeObject(globalRegion);

				ByteArrayInputStream bais = new ByteArrayInputStream(baos.toByteArray());
				pstmt.setBinaryStream(1, bais, baos.toByteArray().length);
				pstmt.executeUpdate();

				return;
			}
			catch (Exception e) {
				this.version--;
				throw e;
			}
			finally {
				if (pstmt != null) {
					pstmt.close();
				}
		      
				if (conn != null) {
					conn.close();
				}
			}
		}
		  
		throw new Exception("No sql connection!");
	}

  	@XmlElement(name="Version")
  	public int getVersion() {
  		return this.version;
  	}

  	@XmlElement(name="Root")
  	public Region getRoot() {
  		return this.root;
  	}

  	public void setVersion(int version) {
  		this.version = version;
  	}

  	public void setRoot(Region root) {
  		this.root = root;
  	}
}