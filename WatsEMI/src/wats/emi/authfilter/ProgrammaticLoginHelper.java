package wats.emi.authfilter;

import java.io.IOException;
import java.security.AccessControlException;
import java.security.Principal;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.security.auth.Subject;
import javax.servlet.ServletRequest;
import javax.servlet.ServletRequestWrapper;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.apache.catalina.Context;
import org.apache.catalina.Manager;
import org.apache.catalina.Session;
import org.apache.catalina.connector.Request;
import org.apache.catalina.connector.RequestFacade;

import sun.security.acl.PrincipalImpl;

import com.sun.enterprise.security.SecurityContext;
import com.sun.enterprise.security.auth.login.common.PasswordCredential;
import com.sun.enterprise.security.web.integration.WebPrincipal;

public class ProgrammaticLoginHelper {
	private static final Logger logger = Logger
            .getLogger(ProgrammaticLoginHelper.class.getName());
    private static final String WEBAUTH_PROGRAMMATIC = "PROGRAMMATIC";

    /**
     * This method fakes a programmatic login which will make a UserPrincipal
     * available in the SecurityContext, Request and Session (if present).
     * 
     * @param user
     *            The user name
     * @param request
     * @param response
     * @return <code>true</code> on successful 'login', <code>false</code>
     *         otherwise.
     */
    public static Boolean doProgrammaticLogin(String user,
            HttpServletRequest request, HttpServletResponse response) {
        logger.fine("Enter doProgrammaticLogin with user " + user);
        Request req = getUnwrappedCoyoteRequest(request);

        if (req == null) {
            return Boolean.valueOf(false);
        }

        // This call will attempt to make a 'real' login towards the specified
        // realm, nothing we want...
        // LoginContextDriver.login(user, password, realm);

        Subject subject = new Subject();
        PasswordCredential pc = new PasswordCredential(user, null, null);
        subject.getPrivateCredentials().add(pc);
        subject.getPrincipals().add(new PrincipalImpl(user));
        SecurityContext securityContext = new SecurityContext(user, subject, null);
        SecurityContext.setCurrent(securityContext);
        SecurityContext secCtx = SecurityContext.getCurrent();
        WebPrincipal principal = new WebPrincipal(user, (char[])null, secCtx);

        req.setUserPrincipal((Principal) principal);
        req.setAuthType(WEBAUTH_PROGRAMMATIC);
        if (logger.isLoggable(Level.FINE)) {
            logger.log(
                    Level.FINE,
                    (new StringBuilder())
                            .append("Programmatic login set principal in http request to: ")
                            .append(user).toString());
        }
        Session realSession = getSession(req);
        if (realSession != null) {
            realSession.setPrincipal((Principal) principal);
            realSession.setAuthType(WEBAUTH_PROGRAMMATIC);
            if (logger.isLoggable(Level.FINE)) {
                logger.log(Level.FINE,"Programmatic login set principal in session.");
            }
        } else if (logger.isLoggable(Level.FINE)) {
            logger.log(Level.FINE, "Programmatic login: No session available.");
        }
        return Boolean.valueOf(true);
    }

    private static final Request getUnwrappedCoyoteRequest(
            HttpServletRequest request) {
        Request req = null;
        ServletRequest servletRequest = request;
        try {
            for (ServletRequest prevRequest = null; servletRequest != prevRequest
                    && (servletRequest instanceof ServletRequestWrapper); servletRequest = ((ServletRequestWrapper) servletRequest)
                    .getRequest())
                prevRequest = servletRequest;

            if (servletRequest instanceof RequestFacade) {
                req = ((RequestFacade) servletRequest).getUnwrappedCoyoteRequest();
            }
        } catch (AccessControlException ex) {
            logger.log(Level.FINE, "Programmatic login failed to get request");
        }
        return req;
    }

    public static final Boolean logout(HttpServletRequest request,
            HttpServletResponse response) throws Exception {
        Request req = getUnwrappedCoyoteRequest(request);
        if (req == null)
            return Boolean.valueOf(false);
        // LoginContextDriver.logout();
        req.setUserPrincipal(null);
        req.setAuthType(null);
        logger.log(Level.FINE,
                "Programmatic logout removed principal from request.");
        Session realSession = getSession(req);
        if (realSession != null) {
            realSession.setPrincipal(null);
            realSession.setAuthType(null);
            logger.log(Level.FINE,
                    "Programmatic logout removed principal from session.");
        }
        return Boolean.valueOf(true);
    }

    private static final Session getSession(Request request) {
        HttpSession session = request.getSession(false);
        if (session != null) {
            Context context = request.getContext();
            if (context != null) {
                Manager manager = context.getManager();
                if (manager != null) {
                    String sessionId = session.getId();
                    try {
                        Session realSession = manager.findSession(sessionId);
                        return realSession;
                    } catch (IOException e) {
                        return null;
                    }
                }
            }
        }
        return null;
    }

}
