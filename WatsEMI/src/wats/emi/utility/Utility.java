package wats.emi.utility;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.logging.Logger;

public class Utility {
    private static final Logger logger = Logger.getLogger(Utility.class.getName());

    public static String getCurDateTimeStr() {
        Date now = new Date();
        SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy/MM/dd HH:mm:ss");
        return dateFormat.format(now);
    }

    public static int getRecordCount(ResultSet rs) {
        int count = 0;
        try {
            rs.last();
            count = rs.getRow();
            logger.warning("count=" + count);
            rs.beforeFirst();
        } catch (SQLException e) {
            e.printStackTrace();
        }

        return count;
    }

    public static String[] createUserTableSqls(String userId) {
      /*MySQL
	  String[] sqls = {
		  "SET SQL_MODE=\"NO_AUTO_VALUE_ON_ZERO\"",
			  
		  "CREATE TABLE IF NOT EXISTS `" + userId + "_project` ("
		  + "`ID` int(11) NOT NULL AUTO_INCREMENT,"
		  + "`ProjectID` int(11) NOT NULL,"
		  + "`Role` int(11) NOT NULL,"
		  + "`Status` int(11) NOT NULL,"
		  + "`ProjectReportID` int(11) NOT NULL DEFAULT '-1',"
		  + "PRIMARY KEY (`ID`),"
		  + "UNIQUE KEY `ProjectID_2` (`ProjectID`),"
		  + "KEY `ProjectID` (`ProjectID`),"
		  + "KEY `ProjectReportID` (`ProjectReportID`)"
		  + ") ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin AUTO_INCREMENT=1",
		  
		  "ALTER TABLE `"+ userId +"_project`"
		  + "ADD CONSTRAINT `" + userId + "_fk1` FOREIGN KEY (`ProjectID`) REFERENCES `project` (`ID`) ON DELETE CASCADE"//,"
		  //+ "ADD CONSTRAINT `" + userId + "_fk2` FOREIGN KEY (`ProjectReportID`) REFERENCES `projectreport` (`ID`)"
	  };
	  */

        String[] sqls = {
                "CREATE TABLE \"EMI\".\"" + userId + "_TASK\"\r\n"
                        + "(\"ID\" NUMBER,\r\n"
                        + "\"TASKID\" NUMBER,\r\n"
                        + "\"ROLE\" NUMBER,\r\n"
                        + "\"STATUS\" NUMBER,\r\n"
                        + "\"REPORTID\" NUMBER DEFAULT -1\r\n"
                        + ") SEGMENT CREATION DEFERRED\r\n"
                        + "PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING\r\n"
                        + "TABLESPACE \"USERS\"",

                "CREATE UNIQUE INDEX \"EMI\".\"" + userId + "_TASK\" ON \"EMI\".\"" + userId + "_TASK\" (\"ID\")\r\n"
                        + "PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS NOCOMPRESS LOGGING\r\n"
                        + "TABLESPACE \"USERS\"\r\n",

                "ALTER TABLE \"EMI\".\"" + userId + "_TASK\" MODIFY (\"ID\" NOT NULL ENABLE)",

                "ALTER TABLE \"EMI\".\"" + userId + "_TASK\" MODIFY (\"TASKID\" NOT NULL ENABLE)",

                "ALTER TABLE \"EMI\".\"" + userId + "_TASK\" MODIFY (\"ROLE\" NOT NULL ENABLE)",

                "ALTER TABLE \"EMI\".\"" + userId + "_TASK\" MODIFY (\"STATUS\" NOT NULL ENABLE)",

                "ALTER TABLE \"EMI\".\"" + userId + "_TASK\" MODIFY (\"REPORTID\" NOT NULL ENABLE)",

                "ALTER TABLE \"EMI\".\"" + userId + "_TASK\" ADD CONSTRAINT \"" + userId + "_TASK\" PRIMARY KEY (\"ID\")\r\n"
                        + "USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS NOCOMPRESS LOGGING\r\n"
                        + "TABLESPACE \"USERS\"  ENABLE",

                "ALTER TABLE \"EMI\".\"" + userId + "_TASK\" ADD CONSTRAINT \"" + userId + "_TASK_PRO_FK1\" FOREIGN KEY (\"TASKID\")"
                        + "REFERENCES \"EMI\".\"TASK\" (\"ID\") ENABLE"
        };

        return sqls;
    }

}