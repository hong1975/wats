package wats.emi.db;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.logging.Logger;

import javax.naming.InitialContext;
import javax.naming.NamingException;
import javax.sql.DataSource;

import wats.emi.constants.CONSTANTS;

public class ConnectionManager {
	private static final Logger logger = Logger.getLogger(ConnectionManager.class.getName());
	public static Connection getInstance() {
		Connection conn = null;
		InitialContext ctx;
		try {
			ctx = new InitialContext();
			DataSource ds = (DataSource) ctx.lookup(CONSTANTS.JDBC_NAME);
			conn = ds.getConnection();
		} catch (NamingException e) {
			// TODO Auto-generated catch block
			logger.severe(e.getMessage());
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			logger.severe(e.getMessage());
		}
		
		return conn;
	}
	
	public static Connection getConsoleInstance() {
		String userName = "emi";
	    //String password = "emi";
		String password = "weitech";
	    //String url = "jdbc:mysql://localhost:3306/emi?useUnicode=true&characterEncoding=UTF-8";
	    String url = "jdbc:oracle:thin:@localhost:1521:emi";
	    
	    Connection conn = null;
	    try {
			//Class.forName("com.mysql.jdbc.Driver").newInstance();
	    	Class.forName("oracle.jdbc.driver.OracleDriver").newInstance();
			conn = DriverManager.getConnection(url, userName, password);
		} catch (InstantiationException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IllegalAccessException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (ClassNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
        
        
        return conn;
	}
}

