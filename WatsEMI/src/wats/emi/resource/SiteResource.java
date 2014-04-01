package wats.emi.resource;

import java.sql.*;
import java.util.logging.Logger;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.ws.rs.Consumes;
import javax.ws.rs.DELETE;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.PUT;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.Response;

import wats.emi.bindings.Site;
import wats.emi.bindings.Sites;
import wats.emi.bindings.User;
import wats.emi.db.ConnectionManager;
import wats.emi.user.UserManager;

@Path("/sites")
public class SiteResource extends Resource {
	private static final Logger logger = Logger.getLogger(SiteResource.class.getName());
	
	@Context HttpServletRequest request;
	@Context HttpServletResponse response;
	
	@GET
	@Produces({"application/json"})
	public Response getSites() {
		validateClient(request);
		
		Sites sites = new Sites();
		Site site;
		Connection conn = ConnectionManager.getInstance();
		Statement stmt = null;
		if (conn != null) {
			try {
				stmt = conn.createStatement();
				String sql = "SELECT * FROM site";
	            ResultSet rs = stmt.executeQuery(sql);
				while (rs.next()) {
					site = new Site();
					
					site.setSamId(rs.getString("SamID"));
					site.setSiteId(rs.getString("SiteID"));
					site.setSiteName(rs.getString("SiteName"));
					site.setSiteType(rs.getString("SiteType"));
					site.setLongitude(rs.getDouble("Longtitude"));
					site.setLatitude(rs.getDouble("Latitude"));
					site.setBsc(rs.getString("BSC"));
					site.setRnc(rs.getString("RNC"));
					
					sites.addSite(site);
				}
				
			} catch (SQLException e) {
				// TODO Auto-generated catch block
                e.printStackTrace();
                logger.warning(e.getMessage());

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
		
		if (sites.getSites().size() == 0)
			return Response.status(Response.Status.NO_CONTENT).build();
		else
			return Response.status(Response.Status.OK).entity(sites).build();
	}
	
	@POST
	@Consumes({"application/json"})
	@Produces({"application/json"})
	public Response addSites(Sites sites) {
		validateClient(request);
		Sites addedSites = new Sites();
		
		if (sites.getSites().size() == 0)
			return Response.status(Response.Status.NO_CONTENT).build();
		
		Connection conn = ConnectionManager.getInstance();
		PreparedStatement updatePstmt = null;
        PreparedStatement insertPstmt = null;
        Statement stmt = null;
		if (conn != null) {
			try {
                updatePstmt = conn.prepareStatement("UPDATE SITE SET SAMID=?,SITENAME=?,SITETYPE=?,LONGTITUDE=?,LATITUDE=?,BSC=?,RNC=? WHERE SITEID=?");
                insertPstmt = conn.prepareStatement("INSERT INTO SITE(SITEID,SAMID,SITENAME,SITETYPE,LONGTITUDE,LATITUDE,BSC,RNC) VALUES(?,?,?,?,?,?,?,?)");
				for (Site site: sites.getSites()) {
                    updatePstmt.setString(1, site.getSamId());
                    updatePstmt.setString(2, site.getSiteName());
                    updatePstmt.setString(3, site.getSiteType());
                    updatePstmt.setDouble(4, site.getLongitude());
                    updatePstmt.setDouble(5, site.getLatitude());
                    updatePstmt.setString(6, site.getBsc());
                    updatePstmt.setString(7, site.getRnc());
                    updatePstmt.setString(8, site.getSiteId());

					int affectCount = updatePstmt.executeUpdate();
					logger.finest("addSites affectCount == " + affectCount);
                    if (affectCount == 0) {
                        insertPstmt.setString(1, site.getSiteId());
                        insertPstmt.setString(2, site.getSamId());
                        insertPstmt.setString(3, site.getSiteName());
                        insertPstmt.setString(4, site.getSiteType());
                        insertPstmt.setDouble(5, site.getLongitude());
                        insertPstmt.setDouble(6, site.getLatitude());
                        insertPstmt.setString(7, site.getBsc());
                        insertPstmt.setString(8, site.getRnc());
                        affectCount = insertPstmt.executeUpdate();
                    }
					if (affectCount > 0)
						addedSites.addSite(site);
				}
			} catch (SQLException e) {
				// TODO Auto-generated catch block
                e.printStackTrace();
				logger.warning(e.getMessage());
				
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
		
		if (addedSites.getSites().size() == 0)
			return Response.status(Response.Status.NO_CONTENT).build();
		else
			return Response.status(Response.Status.OK).entity(addedSites).build();
	}
	
	@DELETE
	@Path("/{siteId}")
	public Response removeSites(@PathParam("siteId") String siteId) throws SQLException {
		validateClient(request);

		Connection conn = ConnectionManager.getInstance();
		Statement stmt = null;
		if (conn != null) {
			try {
				stmt = conn.createStatement();
				String sql;
				sql = "DELETE FROM sites WHERE SiteID='" + siteId + "'";
					
				int affectCount = stmt.executeUpdate(sql);
				logger.finest("removeSites affectCount == " + affectCount);
				if (affectCount > 0)
					return Response.status(Response.Status.NO_CONTENT).build();
				else
					return Response.status(Response.Status.FORBIDDEN).build();
				
			} catch (SQLException e) {
				// TODO Auto-generated catch block
				logger.warning(e.getMessage());
                e.printStackTrace();
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
		return Response.status(Response.Status.FORBIDDEN).build();
	}
}
