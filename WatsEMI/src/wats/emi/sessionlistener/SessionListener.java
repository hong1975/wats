package wats.emi.sessionlistener;

import java.util.logging.Logger;

import javax.servlet.http.HttpSessionEvent;
import javax.servlet.http.HttpSessionListener;

import wats.emi.constants.CONSTANTS;

public class SessionListener implements HttpSessionListener {
	private static final Logger logger = Logger.getLogger(SessionListener.class.getName());
	
	@Override
	public void sessionCreated(HttpSessionEvent se) {
		se.getSession().setMaxInactiveInterval(3600*24);
		logger.finest("Session<" + se.getSession().getId() + "> was created");
	}

	@Override	
	public void sessionDestroyed(HttpSessionEvent se) {
		logger.finest("Session<" + se.getSession().getId() +"> for " + (String)se.getSession().getAttribute(CONSTANTS.SESSION_KEY_USER) +" was destoried");
	}
}
