using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEMIAnalyzer.Model
{
    [Serializable]
    public class MapModel: Model
    {
        private string mName;

        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        public MapModel()
        {
        }
    }
}
