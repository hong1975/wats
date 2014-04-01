using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEMIAnalyzer.Model
{
    public class LinkModel:Model
    {
        private long mID;

        public long ID
        {
            get { return mID; }
            set { mID = value; }
        }

        public LinkModel(long id)
        {
            mID = id;
        }
    }
}
