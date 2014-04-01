package wats.emi;

public class Notes {
	/*
	 * com.mysql.jdbc.PacketTooBigException: Packet for query is too large (1638914 > 1048576). You can change this value on the server by setting the max_allowed_packet' variable.
		at com.mysql.jdbc.MysqlIO.send(MysqlIO.java:2691)
		at com.mysql.jdbc.MysqlIO.sendCommand(MysqlIO.java:1612)
		at com.mysql.jdbc.MysqlIO.sqlQueryDirect(MysqlIO.java:1723)
		at com.mysql.jdbc.Connection.execSQL(Connection.java:3283)
		at com.mysql.jdbc.PreparedStatement.executeInternal(PreparedStatement.java:1332)
		at com.mysql.jdbc.PreparedStatement.executeUpdate(PreparedStatement.java:1604)
		at com.mysql.jdbc.PreparedStatement.executeUpdate(PreparedStatement.java:1519)
		从网上找到的解决方法，在my.ini里的[mysqld]增加如下部分
		[mysqld]
		max_allowed_packet=64M 
	 */

}
