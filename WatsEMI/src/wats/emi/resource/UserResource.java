package wats.emi.resource;

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

import wats.emi.bindings.User;
import wats.emi.bindings.Users;
import wats.emi.user.UserManager;

@Path("/users")
public class UserResource extends Resource {
	@Context HttpServletRequest request;
	@Context HttpServletResponse response;
	private static final Logger logger = Logger.getLogger(UserResource.class.getName());
	
	@GET
	@Produces({"application/json"})
	public Response getAllUsers() throws Exception {
		validateClient(request);

		Users users = UserManager.getAllUsers();
		if (users.getUsers().size() == 0)
			return Response.status(Response.Status.NO_CONTENT).build();
		else
			return Response.status(Response.Status.OK).entity(users).build();
	}
	
	@POST
	@Consumes({"application/json"})
	public Response addUser(User user) throws Exception {
		validateClient(request);
		UserManager.createUser(user.getUserId(), user.getUserId(), user.getRole()); //initial password is userId
		return Response.status(Response.Status.ACCEPTED).build();
		
	}

	@PUT
	@Path("/{id}/{type}/{newValue}")
	public Response updateUser(@PathParam("id") String userId, @PathParam("type") String type,  @PathParam("newValue")String newValue) throws Exception {
		validateClient(request);
		UserManager.UpdateType updateType = UserManager.UpdateType.valueOf(type);
		
		UserManager.updateUser(userId, updateType, newValue);
		return Response.status(Response.Status.ACCEPTED).build();
	}
	
	@DELETE
	@Path("/{id}")
	public Response deleteUser(@PathParam("id") String userId) throws Exception {
		validateClient(request);
		UserManager.removeUser(userId);
		return Response.status(Response.Status.NO_CONTENT).build();
	}
	
	
}
