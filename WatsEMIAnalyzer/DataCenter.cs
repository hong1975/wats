using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatsEMIAnalyzer.Bindings;
using WatsEmiReportTool;
using WatsEMIAnalyzer.EMI;
using System.IO;

namespace WatsEMIAnalyzer
{
    public class DataCenter
    {
        private const int MAX_READ_BUF = 500 * 1024;

        private static DataCenter mSingleton;

        private GlobalRegion mGlobalRegion;
        private Dictionary<string, User> mUsers = new Dictionary<string, User>();
        private Dictionary<string, Site> mSites = new Dictionary<string, Site>();
        private Dictionary<long, List<ChannelSetting>> mChannelSettings = new Dictionary<long, List<ChannelSetting>>();
        private Dictionary<long, Dictionary<string, EquipmentParameter>> mEquipmentParameters = new Dictionary<long, Dictionary<string, EquipmentParameter>>();
        private Dictionary<long, List<LinkConfiguration>> mLinkConfigurations = new Dictionary<long, List<LinkConfiguration>>();
        private Dictionary<long, EMIFileData> mEMIs = new Dictionary<long, EMIFileData>();
        private Dictionary<long, FileDescription> mChannelSettingDescriptions = new Dictionary<long, FileDescription>();
        private Dictionary<long, FileDescription> mEquipmentParameterDescriptions = new Dictionary<long, FileDescription>();
        private Dictionary<long, FileDescription> mLinkConfigurationDescriptions = new Dictionary<long, FileDescription>();
        private Dictionary<long, FileDescription> mEMIDescriptions = new Dictionary<long, FileDescription>();
        private List<string> mUploadFiles = new List<string>();
        private Dictionary<long, BindingTask> mTasks = new Dictionary<long, BindingTask>();

        public List<string> UploadFiles
        {
            get { return mUploadFiles; }
        }

        public Dictionary<long, BindingTask> Tasks
        {
            get { return mTasks; }
        }

        public GlobalRegion GlobalRegion
        {
            get { return mGlobalRegion; }
            set { mGlobalRegion = value; }
        }

        public Dictionary<string, User> Users
        {
            get { return mUsers; }
        }

        public Dictionary<string, Site> Sites
        {
            get { return mSites; }
        }

        public Dictionary<long, List<ChannelSetting>> ChannelSettings
        {
            get { return mChannelSettings; } 
        }

        public Dictionary<long, Dictionary<string, EquipmentParameter>> EquipmentParameters
        {
            get { return mEquipmentParameters; }
        }

        public Dictionary<long, List<LinkConfiguration>> LinkConfigurations
        {
            get { return mLinkConfigurations; }
        }

        public Dictionary<long, EMIFileData> EMIs
        {
            get { return mEMIs; }
        }

        public Dictionary<long, FileDescription> ChannelSettingDescriptions
        {
            get { return mChannelSettingDescriptions; }
        }

        public Dictionary<long, FileDescription> EquipmentParameterDescriptions
        {
            get { return mEquipmentParameterDescriptions; }
        }

        public Dictionary<long, FileDescription> LinkConfigurationDescriptions
        {
            get { return mLinkConfigurationDescriptions; }
        }

        public Dictionary<long, FileDescription> EMIDescriptions
        {
            get { return mEMIDescriptions; }
        }

        public static DataCenter Instance()
        {
            if (mSingleton == null)
                mSingleton = new DataCenter();

            return mSingleton;
        }

        public void LoadData()
        {
            try
            {
                if (File.Exists(".\\uploadfile"))
                {
                    byte[] uploadFileBytes;
                    byte[] tempBytes = new byte[MAX_READ_BUF];

                    using (FileStream fs = new FileStream(".\\uploadfile", FileMode.Open))
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            int len = br.Read(tempBytes, 0, MAX_READ_BUF);
                            if (len > 0)
                            {
                                uploadFileBytes = new byte[len];
                                Array.Copy(tempBytes, uploadFileBytes, len);
                                mUploadFiles = Utility.Deserailize<List<string>>(uploadFileBytes);
                            }
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
            	
            }
        }

        public void StoreData()
        {
            try
            {
                byte[] uploadFileBytes = Utility.Serialize<List<string>>(mUploadFiles);
                using (FileStream fs = new FileStream(".\\uploadfile", FileMode.OpenOrCreate))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        bw.Write(uploadFileBytes);
                    }
                }
            }
            catch (System.Exception e)
            {
            	
            }
            
        }

        public void PrepareSync()
        {

        }

        public DataCenter Reset()
        {
            mUsers.Clear();
            mSites.Clear();
            mChannelSettings.Clear();
            mEquipmentParameters.Clear();
            mLinkConfigurations.Clear();
            mEMIs.Clear();
            mChannelSettings.Clear();
            mChannelSettingDescriptions.Clear();
            mEquipmentParameterDescriptions.Clear();
            mLinkConfigurationDescriptions.Clear();
            mEMIDescriptions.Clear();
            mTasks.Clear();

            return this;
        }

        private DataCenter() 
        {
            LoadData();
        }
    }
}
