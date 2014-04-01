package wats.emi.authfilter;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

import wats.emi.exception.HeaderException;

public class AuthorizationRequestParser {
	static final Pattern USERNAME_PATTERN = Pattern.compile("username=\\\"(.*?)\\\"");
    static final Pattern REALM_PATTERN = Pattern.compile("realm=\\\"(.*?)\\\"");
    static final Pattern NONCE_PATTERN = Pattern.compile("nonce=\\\"(.*?)\\\"");
    static final Pattern URI_PATTERN = Pattern.compile("uri=\\\"(.*?)\\\"");
    static final Pattern CNONCE_PATTERN = Pattern.compile("cnonce=\\\"(.*?)\\\"");
    static final Pattern OPAQUE_PATTERN = Pattern.compile("opaque=\\\"(.*?)\\\"");
    static final Pattern QOP_PATTERN = Pattern.compile("qop=\\\"?([a-z]+)\\\"?");
    static final Pattern NC_PATTERN = Pattern.compile("nc=(\\p{XDigit}{8}?)");
    static final Pattern RESPONSE_PATTERN = Pattern.compile("response=\\\"(.*?)\\\"");

    protected AuthorizationRequestParser(){   
    }

    protected static String getAuthHeaderParameter(String parameter,
            String authHeader, Pattern p) throws HeaderException {
        Matcher m = p.matcher(authHeader);
        if (m.find()) {
            return m.group(1);
        } else {
            throw new HeaderException("Missing header parameter " + parameter + " from Authorization header: " + authHeader);
        }
    }
        
    public static AuthorizationRequest parseAuthorizationHeader(String authheader) throws HeaderException {
        AuthorizationRequest auth = new AuthorizationRequest();
        auth.setUsername(getAuthHeaderParameter("username", authheader, USERNAME_PATTERN));
        auth.setRealm(getAuthHeaderParameter("realm", authheader, REALM_PATTERN));
        auth.setNonce(getAuthHeaderParameter("nonce", authheader, NONCE_PATTERN));
        auth.setUri(getAuthHeaderParameter("uri", authheader, URI_PATTERN));
        auth.setCnonce(getAuthHeaderParameter("cnonce", authheader, CNONCE_PATTERN));
        auth.setOpaque(getAuthHeaderParameter("opaque", authheader, OPAQUE_PATTERN));
        auth.setQop(getAuthHeaderParameter("qop", authheader, QOP_PATTERN));
        auth.setNc(getAuthHeaderParameter("nc", authheader, NC_PATTERN));
        auth.setResponse(getAuthHeaderParameter("response", authheader, RESPONSE_PATTERN));
        
        return auth;        
    }
}
