﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WatsEMIAnalyzer.Bindings
{
    [DataContract]
    public class AddTaskResult
    {
        [DataMember]
        public int RegionVersion;
        [DataMember]
        public long TaskID;
    }
}
