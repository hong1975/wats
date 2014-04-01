using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEMIAnalyzer.Model
{
    [Serializable]
    public abstract class Model
    {

        protected string mId;
        protected int mVersion;

        [NonSerialized]
        protected object mAttachement;

        public int Version
        {
            set { mVersion = value; }
            get { return mVersion; }
        }

        public string ID
        {
            get { return mId; }
        }

        public object Attachment
        {
            set { mAttachement = value; }
            get { return mAttachement; }
        }

        public Model()
        {
            mId = Utility.GetTimeAsId();
        }

        
    }
}
