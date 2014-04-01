package wats.emi.resource;

import java.io.IOException;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.logging.Logger;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.ws.rs.Consumes;
import javax.ws.rs.GET;
import javax.ws.rs.PUT;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.Response;

import wats.emi.bindings.GlobalRegion;
import wats.emi.bindings.Region;
import wats.emi.bindings.UpdateRegionRequest;
import wats.emi.bindings.UpdateRegionResult;
import wats.emi.bindings.UpdateRegionType;
import wats.emi.db.ConnectionManager;
import wats.emi.utility.TokenGenerator;

@Path("/region")
public class RegionResource extends Resource
{
	private static final Logger logger = Logger.getLogger(RegionResource.class.getName());

	@Context
	HttpServletRequest request;

	@Context
	HttpServletResponse response;

	@GET
	@Produces({"application/json"})
	public Response getGlobalRegion(@Context HttpServletResponse aHttpResponse) throws IOException {
		return Response.ok().entity(GlobalRegion.instance()).build();
	}

	@PUT
	@Path("{version}")
	@Consumes({"application/json"})
	@Produces({"application/json"})
	public Response updateRegion(@PathParam("version") int version, UpdateRegionRequest request) throws Exception {
		logger.finest("request version=" + version);
		logger.finest("request type=" + request.getType().toString());
		logger.finest("request region name=" + request.getRegion().getName());
		
		boolean isLatestVersion = (version == GlobalRegion.instance().getVersion());
		/*
		if (version != GlobalRegion.instance().getVersion()) {
			logger.finest("Request ver=" + version + ", latest ver=" + GlobalRegion.instance().getVersion());
			return Response.status(Response.Status.CONFLICT).build();
		}*/

		UpdateRegionResult result = new UpdateRegionResult();
		if (request.getType() == UpdateRegionType.AddRegion) {
			logger.finest("Add region " + request.getRegion().getName() + " ...");
			Region parent = GlobalRegion.findRegionByID(GlobalRegion.instance().getRoot(), request.getRegion().getParentId());
			//findRegion(GlobalRegion.instance().getRoot(), request.getRegion().getParentId());
			if (parent == null)
				return Response.status(Response.Status.NOT_FOUND).build();

            logger.info(request.getRegion().getManagers() == null ? "null":"not null");

			String regionId = TokenGenerator.generateToken(8);
			request.getRegion().setId(regionId);
			request.getRegion().setVersion(1);
			request.getRegion().setParentId(parent.getId());
			request.getRegion().setOwner(this.request.getUserPrincipal().getName());
			parent.addRegion(request.getRegion());
			
		} else if (request.getType() == UpdateRegionType.RemoveRegion) {
			Region parentRegion = GlobalRegion.findRegionByID(GlobalRegion.instance().getRoot(), request.getRegion().getParentId());
			//findRegion(GlobalRegion.instance().getRoot(), request.getRegion().getParentId());
			Region region = GlobalRegion.findRegionByID(GlobalRegion.instance().getRoot(), request.getRegion().getId());
			//findRegion(GlobalRegion.instance().getRoot(), request.getRegion().getId());
			if ( parentRegion == null || region == null)
				return Response.status(Response.Status.NOT_FOUND).build();
			if (region == GlobalRegion.instance().getRoot())
				return Response.status(Response.Status.FORBIDDEN).build();

			parentRegion.getRegions().remove(region);
			
		} else if (request.getType() == UpdateRegionType.RenameRegion) {
			Region region = GlobalRegion.findRegionByID(GlobalRegion.instance().getRoot(), request.getRegion().getId());
			//findRegion(GlobalRegion.instance().getRoot(), request.getRegion().getId());
			if (region == null)
				return Response.status(Response.Status.NOT_FOUND).build();
			if (region == GlobalRegion.instance().getRoot())
				return Response.status(Response.Status.FORBIDDEN).build();
			
			if (GlobalRegion.findRegionByName(GlobalRegion.instance().getRoot(), request.getRegion().getName()) != null)
				return Response.status(Response.Status.CONFLICT).build();

			if (region.getName().equals(request.getRegion().getName())) {
				return Response.status(Response.Status.NOT_MODIFIED).build();
			}

			region.setName(request.getRegion().getName());
				
		} else if (request.getType() == UpdateRegionType.AddSite) {
			Region region = GlobalRegion.findRegionByID(GlobalRegion.instance().getRoot(), request.getRegion().getId()); 
			//findRegion(GlobalRegion.instance().getRoot(), request.getRegion().getId());
			if (region == null)
				return Response.status(Response.Status.NOT_FOUND).build();
			if (region == GlobalRegion.instance().getRoot()) {
				return Response.status(Response.Status.FORBIDDEN).build();
			}
			
			List<String> addedSites = new ArrayList<String>();
			for (String site : request.getRegion().getSites()) {
				if (region.getSites().indexOf(site) >= 0)
					continue;
				
				addedSites.add(site);
				region.getSites().add(site);
			}
			
			request.getRegion().setSites(addedSites);
					
		} else if (request.getType() == UpdateRegionType.RemoveSite) {
			Region region = GlobalRegion.findRegionByID(GlobalRegion.instance().getRoot(), request.getRegion().getId()); 
			//findRegion(GlobalRegion.instance().getRoot(), request.getRegion().getId());
			if (region == null)
				return Response.status(Response.Status.NOT_FOUND).build();
			if (region == GlobalRegion.instance().getRoot())
				return Response.status(Response.Status.FORBIDDEN).build();
			
			List<String> removedSites = new ArrayList<String>();
			for (String site : request.getRegion().getSites()) {
				if (region.getSites().indexOf(site) == -1)
					continue;
				
				removedSites.add(site);
				region.getSites().remove(site);
			}
			
			request.getRegion().setSites(removedSites);
			
		} else if (request.getType() == UpdateRegionType.AddManager) {
			Region region = GlobalRegion.findRegionByID(GlobalRegion.instance().getRoot(), request.getRegion().getId()); 
			//findRegion(GlobalRegion.instance().getRoot(), request.getRegion().getId());
            if (region == null)
              return Response.status(Response.Status.NOT_FOUND).build();
            if (region == GlobalRegion.instance().getRoot())
              return Response.status(Response.Status.FORBIDDEN).build();
            
            List<String> addedManagers = new ArrayList<String>();
            for (String manager : request.getRegion().getManagers()) {
            	if (region.getManagers().indexOf(manager) >= 0)
            		continue;
            	
            	addedManagers.add(manager);
				region.getManagers().add(manager);
			}
            request.getRegion().setManagers(addedManagers);
            
		} else if (request.getType() == UpdateRegionType.RemoveManager) {
			Region region = GlobalRegion.findRegionByID(GlobalRegion.instance().getRoot(), request.getRegion().getId());
			//findRegion(GlobalRegion.instance().getRoot(), request.getRegion().getId());
            if (region == null)
              return Response.status(Response.Status.NOT_FOUND).build();
            if (region == GlobalRegion.instance().getRoot())
              return Response.status(Response.Status.FORBIDDEN).build();

            List<String> removedManagers = new ArrayList<String>();
            for (String manager : request.getRegion().getManagers()) {
            	if (region.getManagers().indexOf(manager) == -1)
            		continue;
            	
            	removedManagers.add(manager);
				region.getManagers().remove(manager);
			}
            request.getRegion().setManagers(removedManagers);
            
		} else if (request.getType() == UpdateRegionType.UpdateChanSettingID) {
			Region region = GlobalRegion.findRegionByID(GlobalRegion.instance().getRoot(), request.getRegion().getId());
			//findRegion(GlobalRegion.instance().getRoot(), request.getRegion().getId());
            if (region == null)
              return Response.status(Response.Status.NOT_FOUND).build();
            if (region == GlobalRegion.instance().getRoot())
              return Response.status(Response.Status.FORBIDDEN).build();
            if (region.getChannelSettingId() == request.getRegion().getChannelSettingId())
              return Response.status(Response.Status.NOT_MODIFIED).build();

            region.setChannelSettingId(request.getRegion().getChannelSettingId());
            
		} else if (request.getType() == UpdateRegionType.UpdateLinkConfigID) {
			Region region = GlobalRegion.findRegionByID(GlobalRegion.instance().getRoot(), request.getRegion().getId());
			//findRegion(GlobalRegion.instance().getRoot(), request.getRegion().getId());
            if (region == null)
              return Response.status(Response.Status.NOT_FOUND).build();
            if (region == GlobalRegion.instance().getRoot())
              return Response.status(Response.Status.FORBIDDEN).build();
            if (region.getLinkConfigurationID() == request.getRegion().getLinkConfigurationID())
              return Response.status(Response.Status.NOT_MODIFIED).build();

            region.setLinkConfigurationID(request.getRegion().getLinkConfigurationID());
            
		} else if (request.getType() == UpdateRegionType.UpdateEquipParamID) {
			Region region = GlobalRegion.findRegionByID(GlobalRegion.instance().getRoot(), request.getRegion().getId());
			//findRegion(GlobalRegion.instance().getRoot(), request.getRegion().getId());
            if (region == null)
              return Response.status(Response.Status.NOT_FOUND).build();
            if (region == GlobalRegion.instance().getRoot())
              return Response.status(Response.Status.FORBIDDEN).build();
            if (region.getEquipmentParameterID() == request.getRegion().getEquipmentParameterID())
              return Response.status(Response.Status.NOT_MODIFIED).build();

            region.setEquipmentParameterID(request.getRegion().getEquipmentParameterID());
            
		} else if (request.getType() == UpdateRegionType.MoveTaskToNewRegion) {
			Region region = GlobalRegion.findRegionByID(GlobalRegion.instance().getRoot(), request.getRegion().getId());
			//findRegion(GlobalRegion.instance().getRoot(), request.getRegion().getId());
            if (region == null)
              return Response.status(Response.Status.NOT_FOUND).build();
            logger.finest("request region is '" + region.getName() + "'");
            
            if (GlobalRegion.findRegionByName(GlobalRegion.instance().getRoot(), request.getRegionName()) != null)
				return Response.status(Response.Status.CONFLICT).build();
            
            Connection conn = ConnectionManager.getInstance();
            PreparedStatement pstmt = null;
            String regionId = TokenGenerator.generateToken(8);
            String sql = "UPDATE task SET RegionID='" + regionId + "' WHERE RegionID='" + region.getId() + "'";
            try {
				conn.setAutoCommit(false);
				
				pstmt = conn.prepareStatement(sql);
				pstmt.executeUpdate();
				
				Region newRegion = new Region();
				newRegion.setId(regionId);
				newRegion.setChannelSettingId(region.getChannelSettingId());
				newRegion.setLinkConfigurationID(region.getLinkConfigurationID());
				newRegion.setEquipmentParameterID(region.getEquipmentParameterID());
				for (String manager : region.getManagers()) {
					newRegion.getManagers().add(manager);
				}
				newRegion.setName(request.getRegionName());
				newRegion.setOwner(region.getOwner());
				newRegion.setParent(region);
				newRegion.setParentId(region.getId());
				for (long taskID : region.getTasks()) {
					newRegion.getTasks().add(taskID);
				}
				region.getTasks().clear();
				for (String site : region.getSites()) {				
					newRegion.getSites().add(site);
				}
				region.addRegion(newRegion);
				
				logger.finest("Add new region '" + newRegion.getName() + "' to region '" + region.getName() + "'" );
				
    			GlobalRegion.instance().storeRegion();
    			conn.commit();
    			
    			result.setNewVer(GlobalRegion.instance().getVersion());
    		    result.setRegion(newRegion);
    		    result.setType(request.getType());

    		    return Response.ok().entity(result).build();
				
			} catch (SQLException e) {
				conn.rollback();
				// TODO Auto-generated catch block
				logger.warning(e.getMessage());
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
            
		} else {
            return Response.status(Response.Status.FORBIDDEN).build();
      	}
		
		GlobalRegion.instance().storeRegion();

	    result.setNewVer(GlobalRegion.instance().getVersion());
	    result.setRegion(request.getRegion());
	    result.setType(request.getType());

	    return Response.ok().entity(result).build();
    }

	private Region findRegion(Region rootRegion, String regionId) {
		
		if (regionId.equals(rootRegion.getId())) {
			return rootRegion;
		}

		Region matchedRegion;
		for (Region region : rootRegion.getRegions()) {
			matchedRegion = findRegion(region, regionId); 
			if (matchedRegion != null) 
				return matchedRegion;
		}
		return null;
	}
	
	public static void main(String[] argc) {
		RegionResource resource = new RegionResource();
		
		Region r1 = new Region();
		r1.setId("1");
		
		Region r11 = new Region();
		r11.setId("11");
		
		Region r111 = new Region();
		r111.setId("111");
		
		r1.addRegion(r11);
		r11.addRegion(r111);
		
		Region x = resource.findRegion(r1, "111");
		System.out.println("x's id = " + x.getId());
		
	}
}