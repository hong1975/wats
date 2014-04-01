using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEMIAnalyzer.Model
{
    public class SettingsModel:Model
    {
        private long mChannelId = -1;
        private long mEquipmentId = -1;
        private long mLinkId = -1;

        public long ChannelID
        {
            get { return mChannelId; }
            set { mChannelId = value; }
        }

        public long EquipmentID
        {
            get { return mEquipmentId; }
            set { mEquipmentId = value; }
        }

        public long LinkID
        {
            get { return mLinkId; }
            set { mLinkId = value; }
        }


        public SettingsModel()
        {
            
        }
    }
}
