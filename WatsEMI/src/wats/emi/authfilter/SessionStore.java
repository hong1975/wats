package wats.emi.authfilter;

public interface SessionStore {
	void addSession(String nonce);
	Boolean checkExistSession(String nonce);
}
