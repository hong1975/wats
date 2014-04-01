package wats.emi.authfilter;

public class AuthorizationRequest {
	private String username;
	private String realm;
	private String nonce;
	private String uri;
	private String cnonce;
	private String opaque;
	private String qop;
	private String nc;
	private String response;

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getRealm() {
        return realm;
    }

    public void setRealm(String realm) {
        this.realm = realm;
    }

    public String getNonce() {
        return nonce;
    }

    public void setNonce(String nonce) {
        this.nonce = nonce;
    }

    public String getUri() {
        return uri;
    }

    public void setUri(String uri) {
        this.uri = uri;
    }

    public String getCnonce() {
        return cnonce;
    }

    public void setCnonce(String cnonce) {
        this.cnonce = cnonce;
    }

    public String getOpaque() {
        return opaque;
    }

    public void setOpaque(String opaque) {
        this.opaque = opaque;
    }

    public String getQop() {
        return qop;
    }

    public void setQop(String qop) {
        this.qop = qop;
    }

    public String getNc() {
        return nc;
    }

    public void setNc(String nc) {
        this.nc = nc;
    }

    public String getResponse() {
        return response;
    }

    public void setResponse(String response) {
        this.response = response;
    }

    @Override
    public String toString() {
        StringBuilder sb = new StringBuilder();
        sb.append("username=[" + username + "] ");
        sb.append("realm=[" + realm + "] ");
        sb.append("nonce=[" + nonce + "] ");
        sb.append("uri=[" + uri + "] ");
        sb.append("cnonce=[" + cnonce + "] ");
        sb.append("opaque=[" + opaque + "] ");
        sb.append("qop=[" + qop + "] ");
        sb.append("nc=[" + nc + "] ");
        sb.append("response=[" + response + "] ");

        return sb.toString();
    }

}
