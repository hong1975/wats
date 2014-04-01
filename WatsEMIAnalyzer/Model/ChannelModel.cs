using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEMIAnalyzer.Model
{
    class ChannelModel:Model
    {
        private long mID;

        public long ID
        {
            get { return mID; }
            set { mID = value; }
        }

        public ChannelModel(long id)
        {
            mID = id;
        }
    }
}
