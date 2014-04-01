package wats.emi.resource;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpSession;

import wats.emi.client.Client;
import wats.emi.constants.CONSTANTS;

public class Resource {
	protected HttpSession session;
	protected Client client;
	
	protected void validateClient(HttpServletRequest request) {
		session = request.getSession(false);
		if (session != null) {
			client = (Client)session.getAttribute(CONSTANTS.SESSION_KEY_CLIENT);
		}
	}
}
