package wats.emi.authfilter;

import java.io.IOException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.Random;
import java.util.logging.Logger;

import javax.servlet.Filter;
import javax.servlet.FilterChain;
import javax.servlet.FilterConfig;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import wats.emi.bindings.User;
import wats.emi.constants.CONSTANTS;
import wats.emi.exception.AuthenticationException;
import wats.emi.exception.ChallengeException;
import wats.emi.exception.HeaderException;
import wats.emi.exception.NonceAuthenticationException;
import wats.emi.user.UserManager;

public class AuthenticationFilter implements Filter {
	
	private static final Logger logger = Logger.getLogger(AuthenticationFilter.class.getName());
	private SessionStore sessionStore = new StaticSessionStore();
	private final Random rnd = new Random(System.nanoTime());

	@Override
	public void destroy() {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void doFilter(ServletRequest req, ServletResponse resp,
			FilterChain chain) throws IOException, ServletException {
		// TODO Auto-generated method stub
		HttpServletRequest request = (HttpServletRequest) req;
        HttpServletResponse response = (HttpServletResponse) resp;
        HttpSession session = request.getSession(false);
        
        String username = null;
    	try {
			String auth = getAuthorizationHeader(request);
			AuthorizationRequest authRequest = AuthorizationRequestParser.parseAuthorizationHeader(auth);
			logger.finest(authRequest.toString());
			
			username = authRequest.getUsername();
			checkMatchingNonce(authRequest);
            String password = getPassword(authRequest.getUsername());
            authenticateRequest(request.getMethod(), authRequest, password);
            
            logger.finest("User is successfully authenticated");
            ProgrammaticLoginHelper.doProgrammaticLogin(
            		username, request, response);
            chain.doFilter(request, response);
            
            session = request.getSession(false);
            if (session != null) {
                session.setAttribute(CONSTANTS.SESSION_KEY_USER, username);
            }
			
		} catch (ChallengeException e) {
			doFailedAuthentication(session, response, true);
		} catch (HeaderException e) {
			logger.warning(e.getMessage());
			doFailedAuthentication(session, response, true);
			
		} catch (NonceAuthenticationException e) {
			logger.warning("Nonce not match");
			doFailedAuthentication(session, response, true);
			
		} catch (AuthenticationException e) {
			logger.warning("User unauthorized");
			doFailedAuthentication(session, response, false);
		} catch (Exception e) {
			logger.severe("Unexpected Exception: " + e.getMessage());
            doInteralError(response);
		}
	}

	@Override
	public void init(FilterConfig arg0) throws ServletException {
		// TODO Auto-generated method stub
		
	}
	
	private String getAuthorizationHeader(HttpServletRequest request) throws ChallengeException {
        String auth = request.getHeader("Authorization");
        if (auth == null) {
            throw new ChallengeException();
        } else {
            return auth;
        }
    }
	
	private void checkMatchingNonce(AuthorizationRequest authRequest)
            throws NonceAuthenticationException {
        if (!sessionStore.checkExistSession(authRequest.getNonce())) {
            logger.warning("Client provided nonce doesn't match generated nonce");
            throw new NonceAuthenticationException();
        }
    }
	
	private String getPassword(String username) {
        return "";
    }
	
	private void authenticateRequest(String method,
            AuthorizationRequest authRequest, String password)
            throws AuthenticationException {

        String expectedResponse = computeExpectedAuthResponse(authRequest,
                method, password);

        if (!expectedResponse.equals(authRequest.getResponse())) {
        	throw new AuthenticationException();
        } 
    }
	
	private String computeExpectedAuthResponse(
            AuthorizationRequest authRequest, String method, String password) {

        String ha1 = getUserHA1(authRequest.getUsername());
        String ha2 = computeHa2(method, authRequest.getUri());

        String response = computeAuthResponse(ha1, authRequest.getNonce(),
                authRequest.getNc(), authRequest.getCnonce(),
                authRequest.getQop(), ha2);

        return response;
    }
	
	private String getUserHA1(String username) {
        User user = null;
		try {
			user = UserManager.getUser(username);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
        if (user != null) {
        	return user.getHa1();
        } else {
        	return null;
        }
    }
	
	private String computeHa2(String httpMethod, String uri) {
        String str = httpMethod + ":" + uri;
        return hashIt(str);
    }
	
	private String hashIt(String str) {
        MessageDigest algorithm;
        try {
            algorithm = MessageDigest.getInstance("MD5");
            algorithm.reset();
            algorithm.update(str.getBytes());
            byte digest[] = algorithm.digest();

            StringBuffer hexString = new StringBuffer();
            for (byte element : digest) {
                String hex = Integer.toHexString(0xFF & element);
                if (hex.length() == 1) {
                    hexString.append('0');
                }
                hexString.append(hex);
            }

            return hexString.toString();
        } catch (NoSuchAlgorithmException e) {
            logger.severe("No MD5 support from platform!");
            return null;
        }
    }
	
	/**
     * Compute the RFC2617 response string
     * 
     * @param ha1
     *            HA1 part of response hash
     * @param nonce
     *            Nonce value
     * @param nonceCount
     *            Nonce count
     * @param clientNonce
     *            Client-supplied nonce (cnonce)
     * @param qop
     *            QOP value (only "auth" supported)
     * @param ha2
     *            HA2 part of response hash
     * @return RFC2617 response string
     */
    private String computeAuthResponse(String ha1, String nonce,
            String nonceCount, String clientNonce, String qop, String ha2) {
        String str = ha1 + ":" + nonce + ":" + nonceCount + ":" + clientNonce
                + ":" + qop + ":" + ha2;
        return hashIt(str);
    }
    
    private void doFailedAuthentication(HttpSession session,
            HttpServletResponse response, boolean returnNonceHeader)
            throws IOException {
        if (returnNonceHeader) {
            String nonce = generateRandomString();
            String opaque = generateRandomString();
            sessionStore.addSession(nonce);
            response.setHeader("WWW-Authenticate", "Digest realm=\"" + CONSTANTS.REALM
                    + "\", qop=\"auth\", nonce=\"" + nonce + "\", opaque=\""
                    + opaque + "\"");
        }
        response.sendError(HttpServletResponse.SC_UNAUTHORIZED);

    }
    
    private String generateRandomString() {
        String seed = "sdlafj" + System.nanoTime() + "43ln" + rnd.nextLong()
                + "345k34nk";

        return hashIt(seed);
    }
    
    private void doInteralError(HttpServletResponse response)
            throws IOException {

        response.sendError(HttpServletResponse.SC_INTERNAL_SERVER_ERROR);
    }

}
