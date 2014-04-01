using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Reflection;
using System.Xml;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using WatsEMIAnalyzer.Bindings;
using WatsEMIAnalyzer.EMI;

namespace WatsEMIAnalyzer
{
    public class Utility
    {
        public static void FillGlobalRegion(GlobalRegion globalRegion)
        {
            if (globalRegion == null)
                return;
            SubRegion subRegion = globalRegion.Root;
            FillSubRegion(subRegion);
        }

        public static void FillSubRegion(SubRegion subRegion)
        {
            if (subRegion == null)
                return;

            if (subRegion.Task == null)
                subRegion.Task = new List<long>();
            if (subRegion.Site == null)
                subRegion.Site = new List<string>();
            if (subRegion.Manager == null)
                subRegion.Manager = new List<string>();
            if (subRegion.Sub == null)
            {
                subRegion.Sub = new List<SubRegion>();
            }
            else
            {
                foreach (SubRegion region in subRegion.Sub)
                {
                    FillSubRegion(region);
                }
            }
        }

        public static void FillTasks(Tasks tasks)
        {
            if (tasks == null)
                return;
            foreach (BindingTask task in tasks.Task)
            {
                FillTask(task);
            }
        }

        public static void FillTask(BindingTask task)
        {
            if (task == null)
                return;

            if (task.Analyzer == null)
                task.Analyzer = new List<string>();
            if (task.Site == null)
                task.Site = new List<string>();
            if (task.UnassignedSite == null)
                task.UnassignedSite = new List<string>();
        }

        public static void FillSites(Sites sites)
        {
            if (sites == null)
                return;
            if (sites.Site == null)
                sites.Site = new List<Site>();
        }

        public static void FillUsers(Users users)
        {
            if (users == null)
                return;
            if (users.User == null)
                users.User = new List<User>();
        }

        public static void FillUpdateRegionResult(UpdateRegionResult result)
        {
            if (result == null)
                return;
            FillSubRegion(result.Region);
        }

        public static void FillReports(Reports reports)
        {
            if (reports == null)
                return;
            if (reports.Report == null)
                reports.Report = new List<Report>();
        }

        public static void FillFileList(FileList fileList)
        {
            if (fileList == null)
                return;
            if (fileList.Description == null)
                fileList.Description = new List<FileDescription>();
        }

        public static List<EMIFileData> GetTaskAvailableEmiFiles(BindingTask task)
        {
            List<EMIFileData> emiFileDatas = new List<EMIFileData>();
            Site site;
            EMIFileData emiFileData;
            foreach (string siteID in task.Site)
            {
                site = DataCenter.Instance().Sites[siteID];
                foreach (KeyValuePair<long, FileDescription> pair in DataCenter.Instance().EMIDescriptions)
                {
                    if (siteID.Equals(pair.Value.SiteID))
                    {
                        emiFileData = DataCenter.Instance().EMIs[pair.Key];
                        emiFileData.UID = pair.Key;
                        emiFileDatas.Add(emiFileData);
                    }
                }
            }

            foreach (string siteID in task.UnassignedSite)
            {
                site = DataCenter.Instance().Sites[siteID];
                foreach (KeyValuePair<long, FileDescription> pair in DataCenter.Instance().EMIDescriptions)
                {
                    if (siteID.Equals(pair.Value.SiteID))
                    {
                        emiFileData = DataCenter.Instance().EMIs[pair.Key];
                        emiFileData.UID = pair.Key;
                        emiFileDatas.Add(emiFileData);
                    }
                }
            }
            

            return emiFileDatas;
        }

        public static string ConvertMagDeclination(double declination)
        {
            long totalSeconds = (long)Math.Abs(declination * 3600);
            int degree = (int)(totalSeconds / 3600);
            int cent = (int)((totalSeconds - degree * 3600) / 60);
            float seconds = (float)(Math.Abs(declination * 3600) - degree * 3600 - cent * 60);

            return degree.ToString() + "\x00B0"
                + cent.ToString() + "'"
                + seconds.ToString() + "\""
                + " "
                + (declination >= 0 ? "E" : "W");
        }

        public static string GetEmiPolarization(byte polarization)
        {
            switch(polarization)
            {
                case 0:
                    return "Vertical only";

                case 1:
                    return "Horizotal only";

                case 2:
                    return "Vertical first, horizotal follow";

                case 3:
                    return "Horizotal first, vertical follow";
            }

            return "Unknown";
        }
        public static string GetEmiTime(string emiTimeStr)
        {
            if (emiTimeStr.Length != 14)
                return emiTimeStr;

            return emiTimeStr.Substring(0, 4)
                + "-"
                + emiTimeStr.Substring(4, 2)
                + "-"
                + emiTimeStr.Substring(6, 2)
                + " "
                + emiTimeStr.Substring(8, 2)
                + ":"
                + emiTimeStr.Substring(10, 2)
                + ":"
                + emiTimeStr.Substring(12, 2);
        }

        public static BindingTask FindTaskByName(SubRegion parentRegion, string taskName)
        {
            if (parentRegion == null)
                return null;

            BindingTask task;
            foreach (long taskID in parentRegion.Task)
            {
                if (!DataCenter.Instance().Tasks.ContainsKey(taskID))
                    continue;

                task = DataCenter.Instance().Tasks[taskID];
                if (taskName.Equals(task.Name))
                    return task;
            }

            BindingTask matchedTask;
            foreach (SubRegion region in parentRegion.Sub)
            {
                matchedTask = FindTaskByName(region, taskName);
                if (matchedTask != null)
                    return matchedTask;
            }

            return null;
        }

        public static BindingTask FindTaskByID(SubRegion parentRegion, int findTaskID)
        {
            if (parentRegion == null)
                return null;

            foreach (long taskID in parentRegion.Task)
            {
                if (findTaskID == taskID && DataCenter.Instance().Tasks.ContainsKey(taskID))
                {
                    return DataCenter.Instance().Tasks[taskID];
                }
            }

            BindingTask matchedTask;
            foreach (SubRegion region in parentRegion.Sub)
            {
                matchedTask = FindTaskByID(region, findTaskID);
                if (matchedTask != null)
                    return matchedTask;
            }

            return null;
        }

        public static SubRegion FindRegionByName(SubRegion parentRegion, string regionName)
        {
            if (parentRegion == null)
                return null;

            if (parentRegion.Name.Equals(regionName))
                return parentRegion;

            SubRegion matchedRegion;
            foreach (SubRegion subRegion in parentRegion.Sub)
            {
                matchedRegion = FindRegionByName(subRegion, regionName);
                if (matchedRegion != null)
                    return matchedRegion;
            }

            return null; 
        }

        public static SubRegion FindRegionByID(SubRegion parentRegion, string regionID)
        {
            if (parentRegion == null)
                return null;

            if (parentRegion.ID.Equals(regionID))
                return parentRegion;

            SubRegion matchedRegion;
            foreach (SubRegion subRegion in parentRegion.Sub)
            {
                matchedRegion = FindRegionByID(subRegion, regionID);
                if (matchedRegion != null)
                    return matchedRegion;
            }

            return null;        
        }

        public static List<Site> GetAvailableSites(SubRegion region)
        {
            List<Site> sites = new List<Site>();
            while (!"0".Equals(region.ID))
            {
                if (region.Site != null && region.Site.Count > 0)
                {
                    foreach (string siteID in region.Site)
                        sites.Add(DataCenter.Instance().Sites[siteID]);
                    break;
                }
                region = region.Parent;

                if (region == null)
                    break;
            }

            return sites;
        }

        public static long GetChannelSettingID(SubRegion region)
        {
            long channelSettingID = -1;
            while (!"0".Equals(region.ID))
            {
                channelSettingID = region.ChannelSettingID;
                if (channelSettingID != -1)
                    return channelSettingID;

                region = region.Parent;
                if (region == null)
                    break;
            }

            return channelSettingID;
        }

        public static long GetLinkConfigurationID(SubRegion region)
        {
            long linkConfigurationID = -1;
            while (!"0".Equals(region.ID))
            {
                linkConfigurationID = region.LinkConfigurationID;
                if (linkConfigurationID != -1)
                    return linkConfigurationID;

                region = region.Parent;
                if (region == null)
                    break;
            }

            return linkConfigurationID;
        }

        public static long GetEquipmentParameterID(SubRegion region)
        {
            long equipmentParameterID = -1;
            while (!"0".Equals(region.ID))
            {
                equipmentParameterID = region.EquipmentParameterID;
                if (equipmentParameterID != -1)
                    return equipmentParameterID;

                region = region.Parent;
                if (region == null)
                    break;
            }

            return equipmentParameterID;
        }

        public static byte[] GetFileContent(string fileName)
        {
            byte[] fileContent = null;
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    int nBytes = (int)fs.Length;
                    fileContent = new byte[nBytes];
                    int nBytesRead = fs.Read(fileContent, 0, nBytes);
                    if (nBytesRead != nBytes)
                        return null;
                }
            }
            catch (System.Exception e)
            {
                fileContent = null;
            }
            
            return fileContent;
        }

        public static string JsonSerialize<T>(T aObject)
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
            string json = "";
            using (MemoryStream stream = new MemoryStream())
            {
                jsonSerializer.WriteObject(stream, aObject);
                byte[] dataBytes = new byte[stream.Length];
                stream.Position = 0;
                stream.Read(dataBytes, 0, (int)stream.Length);
                json = Encoding.UTF8.GetString(dataBytes);
            }

            return json;
        }

        public static T JsonDeserialize<T>(string aJsonString)
        {
            object obj = null;

            try
            {
                if (string.IsNullOrEmpty(aJsonString))
                    return (T)obj;
                else if (aJsonString.Trim().Length == 0)
                    return (T)obj;

                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));

                using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(aJsonString)))
                {
                    obj = jsonSerializer.ReadObject(ms);
                }
            }
            catch (System.Exception e)
            {
                obj = null;
            }

            return (T)obj;
        }

        public static byte[] Serialize<T> (T obj)
        {
            byte[] bytes;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                bytes = ms.GetBuffer();
            }

            return bytes;            
        }

        public static T Deserailize<T>(byte[] data)
        {
            T obj;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                obj = (T)bf.Deserialize(ms);
            }

            return obj;
        }
    
        public static string GetDateTime()
        {
            DateTime now = DateTime.Now;
            return now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetTimeStr()
        {
            DateTime now = DateTime.Now;
            return now.ToString("yyyy-MM-dd-HH:mm:ss.ffff");
        }

        public static string GetTimeAsId()
        {
            DateTime now = DateTime.Now;
            return now.ToString("yyyyMMddHHmmssffff");
        }

        public static string MD5File(string fileName)
        {
            return HashFile(fileName, "md5");
        }

        public static string GetShortFileName(string filePath)
        {
            int pos = filePath.LastIndexOf("\\");
            if (pos == -1)
                return filePath;

            string shortFileName = filePath.Substring(pos + 1);
            return shortFileName;
        }
        
        private static string HashFile(string fileName, string algName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            byte[] hashBytes = HashData(fs, algName);
            fs.Close();
            return ByteArrayToHexString(hashBytes);
        }

        private static byte[] HashData(Stream stream, string algName)
        {
            HashAlgorithm algorithm;
            if (algName == null)
            {
                throw new ArgumentNullException("algName can't be null");
            }
            if (string.Compare(algName, "sha1", true) == 0)
            {
                algorithm = SHA1.Create();
            }
            else
            {
                if (string.Compare(algName, "md5", true) != 0)
                {
                    throw new Exception("algName must be sha1 or md5");
                }
                algorithm = MD5.Create();
            }
            return algorithm.ComputeHash(stream);
        }

        private static string ByteArrayToHexString(byte[] buf)
        {
            int iLen = 0;

            Type type = typeof(System.Web.Configuration.MachineKeySection);
            MethodInfo byteArrayToHexString = type.GetMethod("ByteArrayToHexString", BindingFlags.Static | BindingFlags.NonPublic);

            return (string)byteArrayToHexString.Invoke(null, new object[] { buf, iLen });
        }

    }
}
