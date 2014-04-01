package wats.emi.user;

import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.sql.*;
import java.util.List;
import java.util.logging.Logger;

import wats.emi.bindings.User;
import wats.emi.bindings.Users;
import wats.emi.constants.CONSTANTS;
import wats.emi.db.ConnectionManager;
import wats.emi.persistence._User;
import wats.emi.persistence.dao.UserDAO;
import wats.emi.utility.Utility;

public class UserManager {
	private static final Logger logger = Logger.getLogger(UserManager.class.getName());
	
	public enum UpdateType {
		ChangePassword,
		ChangeRole,
		LockUser
	} 
	
	public static Users getAllUsers() throws Exception {

        List<_User> _users = new UserDAO().findAll();
        Users users = new Users();
        User user;
        for (_User _user : _users) {
            user = new User();
            user.setId(_user.getId());
            user.setUserId(_user.getUserId());
            user.setHa1(_user.getPassword());
            user.setLocked(_user.isLocked());
            user.setRole(_user.getRole());

            users.addUser(user);
        }

		return users;
	}
	
	public static User getUser(String userId) throws Exception {
		User user = null;
		
		Connection conn = ConnectionManager.getInstance();
		Statement stmt = null;
		if (conn != null) {
			try {
				stmt = conn.createStatement();
				String sql = "SELECT * FROM users WHERE UserId='" + userId + "'";
	            ResultSet rs = stmt.executeQuery(sql);
				if(rs.next()) {
					user = new User();
					
					user.setUserId(rs.getString("UserId"));
					user.setHa1(rs.getString("Password"));
					user.setRole(rs.getString("Role"));
					user.setLocked(rs.getInt("Locked") == 1);
				}
			} catch (SQLException e) {
				// TODO Auto-generated catch block
				logger.warning(e.getMessage());
				user = null;
				throw e;
			} catch (Exception e) {
				// TODO Auto-generated catch block
				logger.warning(e.getMessage());
				user = null;
				throw e;
				
			} finally {
				if (stmt != null) {
					try {
						stmt.close();
					} catch (Exception e) { /* ignore close errors */
	                }
				}
	            if (conn != null) {
	                try {
	                    conn.close();
	                } catch (Exception e) { /* ignore close errors */
	                }
	            }
	        }
		}
		return user;
	}
	
	public static User createUser(String userId, String password, String role) throws Exception {
        String ha1 = hashIt(userId + ":" + CONSTANTS.REALM + ":" + password);
        if (ha1 == null) {
            logger.warning("Can't create password for user " + userId);
            return null;
        }

        _User _user = new _User();
        _user.setUserId(userId);
        _user.setPassword(ha1);
        _user.setRole(role);
        _user.setLocked(false);

        new UserDAO().createOrUpdate(_user, true);

        User user = new User(_user.getId(), userId, ha1, role, false);
		return user;
	}
	
	public static void updateUser(String userId, UpdateType type, String newValue) throws Exception {
		Connection conn = ConnectionManager.getInstance();
		Statement stmt = null;
		if (conn != null) {
			try {
				stmt = conn.createStatement();
				String sql = null;
				switch (type) {
				case ChangePassword:
					sql = "UPDATE users SET Password='" + newValue + "' WHERE UserID='" + userId + "'";
					break;
					
				case ChangeRole:
					sql = "UPDATE users SET Role='" + newValue + "' WHERE UserID='" + userId + "'";
					break;
					
				case LockUser:
					sql = "UPDATE users SET locked=" 
						+ (("1".equals(newValue) || "true".equalsIgnoreCase(newValue) || "yes".equalsIgnoreCase(newValue))?1:0) 
						+ " WHERE UserID='" + userId + "'";
					break;
				}
				
	            stmt.execute(sql);
	            if (stmt.getUpdateCount() == 1) {
				} else {
					logger.warning("Can't update user " + userId + "'s password");
					throw new Exception("Update " + userId + "'s " + type.toString() + " failed !");
				}
				
			} catch (SQLException e) {
				// TODO Auto-generated catch block
				logger.warning(e.getMessage());
				throw e;
				
			} catch (Exception e) {
				// TODO Auto-generated catch block
				logger.warning(e.getMessage());
				throw e;
				
			} finally {
				if (stmt != null) {
					try {
						stmt.close();
					} catch (Exception e) { /* ignore close errors */
	                }
				}
	            if (conn != null) {
	                try {
	                    conn.close();
	                } catch (Exception e) { /* ignore close errors */
	                }
	            }
	        }
		}
	}
	
	public static void removeUser(String userId) throws Exception {
		Connection conn = ConnectionManager.getInstance();
		Statement stmt = null;
		if (conn != null) {
			try {
				String sql = "DELETE FROM users WHERE UserID='" + userId + "'";
                stmt = conn.createStatement();
                stmt.execute(sql);
	            
                try {
                    sql = "DROP TABLE \"" + userId + "_TASK\"";
                    stmt.execute(sql);
                } catch (Exception e) {
                    logger.warning(e.getMessage());
                }

			} catch (SQLException e) {
				// TODO Auto-generated catch block
				logger.warning(e.getMessage());
				throw e;
				
			} catch (Exception e) {
				// TODO Auto-generated catch block
				logger.warning(e.getMessage());
				throw e;
				
			} finally {
                if (stmt != null) {
                    try {
                        stmt.close();
                    } catch (Exception e) { /* ignore close errors */
                    }
                }
	            if (conn != null) {
	                try {
	                    conn.close();
	                } catch (Exception e) { /* ignore close errors */
	                }
	            }
	        }
		}
	}
	
	
	private static String hashIt(String str) {
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
	
	public static void main(String[] args) throws Exception {
		UserManager.createUser("admin", "admin", "admin");

		UserManager.createUser("test1", "test1", "normal");
		UserManager.createUser("test2", "test2", "normal");
		UserManager.createUser("test3", "test3", "normal");
		UserManager.createUser("test4", "test4", "normal");
		UserManager.createUser("test5", "test5", "normal");
	}

}
