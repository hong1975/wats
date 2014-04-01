package wats.emi.resource;

import java.io.ByteArrayInputStream;
import java.io.InputStream;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.logging.Logger;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.ws.rs.Consumes;
import javax.ws.rs.DELETE;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.QueryParam;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.MultivaluedMap;
import javax.ws.rs.core.Response;
import javax.ws.rs.core.UriInfo;

import wats.emi.bindings.ErrorMessage;
import wats.emi.bindings.FileDescription;
import wats.emi.bindings.FileList;
import wats.emi.db.ConnectionManager;
import wats.emi.file.FileType;
import wats.emi.file.UploadFileData;
import wats.emi.utility.IDGenerator;
import wats.emi.utility.Utility;

@Path("/files")
public class FileResource extends Resource {
	private static final Logger logger = Logger.getLogger(FileResource.class.getName());
	
	@Context HttpServletRequest request;
	@Context HttpServletResponse response;
	
	@GET
	@Path("/{type}")
	@Produces({"application/json"})
	public Response getFileList(@PathParam("type") String type) throws Exception {
		FileList fileList = new FileList();
		
		Connection conn = ConnectionManager.getInstance();
		Statement stmt = null;
		String tableName = type + "file";
		String sql = "SELECT * from " + tableName;
		
		if (conn != null) {
			try {
				stmt = conn.createStatement();
	            ResultSet rs = stmt.executeQuery(sql);
	            FileDescription description;
				while (rs.next()) {
					description = new FileDescription();
					description.setID(rs.getLong("ID"));
					description.setTitle(rs.getString("Title"));
					description.setFileName(rs.getString("FileName"));
					description.setCreateTime(rs.getString("CreateTime"));
					description.setUploader(rs.getString("Uploader"));
					
					if ("emi".equals(type)) {
						description.setSiteId(rs.getString("SiteID"));
						description.setTester(rs.getString("Tester"));
						description.setTestTime(rs.getString("TestTime"));
					}
					
					fileList.addFile(description);
				}
				
			} catch (SQLException e) {
				// TODO Auto-generated catch block
				logger.warning(e.getMessage());
                e.printStackTrace();
				throw e;
				
			} catch (Exception e) {
				logger.warning(e.getMessage());
				throw e;
				
			} finally {
				if (stmt != null) {
					try {
						stmt.close();
					} catch (Exception e) { /* ignore close errors */
	                }
				}
	            if (conn != null) {
	                try {
	                    conn.close();
	                } catch (Exception e) { /* ignore close errors */
	                }
	            }
	        }

		}
			
		return Response.status(Response.Status.OK).entity(fileList).build();
	}
	
	@DELETE
	@Path("/{type}")
	public Response reomveFile(@PathParam("type") String type, @QueryParam("fid") long fid) throws Exception {
		
		Connection conn = ConnectionManager.getInstance();
		Statement stmt = null;
		String tableName = type.toUpperCase() + "FILE";
		String sql= "DELETE from " + tableName + " WHERE ID=" + fid;
		if (conn != null) {
			try {
				stmt = conn.createStatement();
	            stmt.executeUpdate(sql);
				
			} catch (SQLException e) {
				// TODO Auto-generated catch block
				logger.warning(e.getMessage());
                e.printStackTrace();
				throw e;
				
			} catch (Exception e) {
				logger.warning(e.getMessage());
				throw e;
				
			} finally {
				if (stmt != null) {
					try {
						stmt.close();
					} catch (Exception e) { /* ignore close errors */
	                }
				}
	            if (conn != null) {
	                try {
	                    conn.close();
	                } catch (Exception e) { /* ignore close errors */
	                }
	            }
	        }

		}
			
		return Response.status(Response.Status.NO_CONTENT).build();
	}
	
	@GET
	@Path("/{type}/{fieldtype}")
	@Produces({"application/octet-stream"})
	public String getFile(@PathParam("type") String type, @PathParam("fieldtype") String fieldtype,
		@QueryParam("fid") long fid,
		@Context HttpServletResponse aHttpResponse) throws Exception {
		
		Connection conn = ConnectionManager.getInstance();
		PreparedStatement pstmt = null;
		String tableName = type.toUpperCase() + "FILE";
		
		String field;
		if ("obj".equalsIgnoreCase(fieldtype))
			field = "ParseData";
		else if ("content".equalsIgnoreCase(fieldtype))
			field = "FileContent";
		else
			field = fieldtype;
		
		String sql= "SELECT " + field + " from " + tableName + " WHERE ID=" + fid;
		logger.finest(sql);
		if (conn != null) {
			try {
				pstmt = conn.prepareStatement(sql);
				ResultSet rs = pstmt.executeQuery();
				if (rs.next()) {
					InputStream is = rs.getBinaryStream("ParseData");
					int size = 0;
					byte[] buf = new byte[4096];
					while ((size = is.read(buf)) != -1) {
						aHttpResponse.getOutputStream().write(buf, 0, size);
					}
				}
				
			} catch (SQLException e) {
				// TODO Auto-generated catch block
				logger.warning(e.getMessage());
                e.printStackTrace();
				throw e;
				
			} catch (Exception e) {
				logger.warning(e.getMessage());
				throw e;
				
			} finally {
				if (pstmt != null) {
					try {
						pstmt.close();
					} catch (Exception e) { /* ignore close errors */
	                }
				}
	            if (conn != null) {
	                try {
	                    conn.close();
	                } catch (Exception e) { /* ignore close errors */
	                }
	            }
	        }

		}
			
		return "";
	}
	
	@POST
	@Path("/{type}")
	@Consumes({"application/octet-stream"})
	@Produces({"application/json"})
	public Response uploadFile(@PathParam("type") String type, byte[] data, @Context UriInfo uri) throws Exception {
		
		UploadFileData uploadFileData = UploadFileData.deserialize(data); 
		if (FileType.channelSetting.toString().equals(type)
			|| FileType.colorSetting.toString().equals(type)
			|| FileType.equipmentParameter.toString().equals(type)
			|| FileType.linkconfiguration.toString().equals(type)
			|| FileType.emi.toString().equals(type)) {
			
			String tableName = type.toUpperCase() + "FILE";
			Connection conn = ConnectionManager.getInstance();
			
			String createTime = Utility.getCurDateTimeStr();
			String title = uploadFileData.getShortName() + " (" + uploadFileData.getUploader() + ", " + createTime + ")";
			
			PreparedStatement pstmt = null;
			String sql;
			String tester = "";
			String siteId = "";
			String testTime = "";
            long id =  IDGenerator.getID();

			if (FileType.emi.toString().equals(type)) {
				MultivaluedMap<String, String> queryParams = uri.getQueryParameters();
				tester = queryParams.getFirst("Tester");
				siteId = queryParams.getFirst("SiteID");
				testTime = queryParams.getFirst("TestTime");

				sql = "INSERT INTO " + tableName + "(ID,Title,FileName,FileContent,MD5,ParseData,Uploader,CreateTime, SiteID, Tester, TestTime)" 
					+ " VALUES(" + id + ",'" + title + "','" + uploadFileData.getShortName() + "',?,'"
					+ uploadFileData.getMd5() + "',?,'" + uploadFileData.getUploader() + "', '" + createTime + "','" 
					+ siteId + "', '" + tester + "', '" + testTime + "')";
			} else {
				sql = "INSERT INTO " + tableName + "(ID,Title,FileName,FileContent,MD5,ParseData,Uploader,CreateTime)" 
					+ " VALUES(" + id + ",'" + title + "','" + uploadFileData.getShortName() + "',?,'"
					+ uploadFileData.getMd5() + "',?,'" + uploadFileData.getUploader() + "', '" + createTime + "')";
			}
            logger.warning(sql);
			if (conn != null) {
				try {
					logger.finest(sql);
					
					ByteArrayInputStream fileBaIs = new ByteArrayInputStream(uploadFileData.getFileContent());
					ByteArrayInputStream parseDataBaIs = new ByteArrayInputStream(uploadFileData.getParseData());
					
					pstmt = conn.prepareStatement(sql, Statement.RETURN_GENERATED_KEYS);
					pstmt.setBinaryStream(1, fileBaIs, uploadFileData.getFileContent().length);
					pstmt.setBinaryStream(2, parseDataBaIs, uploadFileData.getParseData().length);
					
					pstmt.executeUpdate();
                    FileDescription description = new FileDescription();
                    description.setID(id);
                    description.setTitle(title);
                    description.setFileName(uploadFileData.getShortName());
                    description.setUploader(uploadFileData.getUploader());
                    description.setCreateTime(createTime);

                    if (FileType.emi.toString().equals(type)) {
                        description.setSiteId(siteId);
                        description.setTester(tester);
                        description.setTestTime(testTime);
                    }
                    return Response.status(Response.Status.ACCEPTED).entity(description).build();

				} catch (SQLException e) {
					// TODO Auto-generated catch block
					logger.warning(e.getMessage());
                    e.printStackTrace();
					
					ErrorMessage errMsg = new ErrorMessage();
					errMsg.setName(e.getClass().getName());
					errMsg.setDescription(e.getMessage());
					return Response.status(Response.Status.INTERNAL_SERVER_ERROR).entity(errMsg).build();
					
				} catch (Exception e) {
					logger.warning(e.getMessage());
					
					ErrorMessage errMsg = new ErrorMessage();
					errMsg.setName(e.getClass().getName());
					errMsg.setDescription(e.getMessage());
					return Response.status(Response.Status.INTERNAL_SERVER_ERROR).entity(errMsg).build();
					
				} finally {
					if (pstmt != null) {
						try {
							pstmt.close();
						} catch (Exception e) { /* ignore close errors */
		                }
					}
		            if (conn != null) {
		                try {
		                    conn.close();
		                } catch (Exception e) { /* ignore close errors */
		                }
		            }
		        }
			}
		} else {
			
		}
		return Response.status(Response.Status.FORBIDDEN).build();
	}
}
