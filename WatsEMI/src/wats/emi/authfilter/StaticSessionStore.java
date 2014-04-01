package wats.emi.authfilter;

import java.util.Iterator;
import java.util.Map;
import java.util.Map.Entry;
import java.util.concurrent.ConcurrentHashMap;
import java.util.logging.Logger;

public class StaticSessionStore implements SessionStore {
    private static final Logger logger = Logger.getLogger(StaticSessionStore.class.getName());
    private static Map<String, Long> sessions = new ConcurrentHashMap<String, Long>();
    private static final int MAX_NONCE_TIME = Integer.parseInt(System.getProperty("authFilterExpireTime", "30000"));

    public int getMaxNonceTime() {
        return MAX_NONCE_TIME;
    }

    @Override
    public void addSession(String nonce) {
        sessions.put(nonce, System.currentTimeMillis());
        logger.finest("AddSession current session size: " + sessions.size());
    }

    @Override
    public Boolean checkExistSession(String nonce) {
        Boolean exists = false;
        if (sessions.containsKey(nonce)) {
            if ((System.currentTimeMillis() - sessions.get(nonce)) <= MAX_NONCE_TIME) {
                exists = true;
            } else {
                logger.fine("Nonce in session store too old");
            }
            sessions.remove(nonce);
        }
        cleanMap();
        return exists;
    }

    private synchronized void cleanMap() {
        if (sessions.size() > 1000) {
            logger.fine("Cleaning the Authentication Session Store because its size is > 1000");
            Iterator<Entry<String, Long>> iterator = sessions.entrySet()
                    .iterator();
            while (iterator.hasNext()) {
                Entry<String, Long> entry = iterator.next();
                if ((System.currentTimeMillis() - entry.getValue()) > MAX_NONCE_TIME) {
                    iterator.remove();
                }
            }
            logger.finest("After cleanMap session size: " + sessions.size());
        }
    }

    public void setHashMap(ConcurrentHashMap<String, Long> map) {
        sessions = map;
    }
}