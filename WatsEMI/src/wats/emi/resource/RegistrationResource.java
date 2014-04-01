package wats.emi.resource;

import java.util.logging.Logger;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import javax.ws.rs.DELETE;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.Response;

import wats.emi.bindings.User;
import wats.emi.client.Client;
import wats.emi.constants.CONSTANTS;
import wats.emi.user.UserManager;

@Path("/registration")
public class RegistrationResource extends Resource {
	private static final Logger logger = Logger.getLogger(RegistrationResource.class.getName());
	
	@Context HttpServletRequest request;
	@Context HttpServletResponse response;
	
	@POST
    @Produces({"application/json"})
	public Response login() throws Exception {
		session = request.getSession(true);
		String userName = request.getUserPrincipal().getName();
		User user = UserManager.getUser(userName);
		logger.fine("User " + userName + " logged in");
		client = new Client(session);
		client.setUserId(user.getUserId());
		session.setAttribute(CONSTANTS.SESSION_KEY_CLIENT, client);		
		
		return Response.status(Response.Status.OK).entity(user).build();
	}
	
	@DELETE
	public Response logout() {
		validateClient(request);
		if (session != null) {
			session.invalidate();
		}
		
		return Response.status(Response.Status.NO_CONTENT).build();
	}

}
