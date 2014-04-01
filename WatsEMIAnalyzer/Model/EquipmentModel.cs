using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEMIAnalyzer.Model
{
    public class EquipmentModel: Model
    {
        private long mID;

        public long ID
        {
            get { return mID; }
            set { mID = value; }
        }

        public EquipmentModel(long id)
        {
            mID = id;
        }
    }
}
