using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using WatsClient.Utility;
using WatsClient.Bindings;

namespace WatsClient
{
    public class LocalStorage
    {
        public enum FileType
        {
            Color,
            Link,
            Channel,
            Equipment,
        }

        private static SQLiteConnection conn;
        
        static LocalStorage()
        {
            string path = "Data Source=" + SystemHelper.GetUserHomeDir() + "/wats.db";
            conn = new SQLiteConnection(path);  //创建数据库实例，指定文件位置  
            conn.Open();    //打开数据库，若文件不存在会自动创建  
        }

        public static Users QueryAllUsers()
        {
            Users users = new Users();
            string sql = "SELECT * FROM Users";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            User user;
            while (reader.Read())
            {
                user = new User();
                
                user.ID = reader.GetInt64(reader.GetOrdinal("id"));
                user.UserId = reader.GetString(reader.GetOrdinal("userId")); 
                user.HA1 = reader.GetString(reader.GetOrdinal("password"));
                user.Locked = reader.GetBoolean(reader.GetOrdinal("locked"));

                users.User.Add(user); 
            }

            return users;
        }

        public static List<long> GetFileList(FileType type)
        {
            List<long> ids = new List<long>();
            string sql = string.Format("SELECT id FROM Files WHERE type='{0}'", type.ToString());
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ids.Add(reader.GetInt64(reader.GetOrdinal("id")));
            }

            return ids;
        }

        public void StoreFile(FileType type, long id, string md5, byte[] data)
        {
            string sql = string.Format("REPLACE INTO Files(id, md5, data, type) VALUES({0}, '{1}', @Data ,'{2}')",
                id, md5, type.ToString());
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Data", data);

            cmd.ExecuteNonQuery();
        }

    }
}
