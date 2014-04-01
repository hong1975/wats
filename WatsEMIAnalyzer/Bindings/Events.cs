using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WatsEMIAnalyzer.Bindings
{
    [DataContract]
    public class Events
    {
        [DataMember]
        public List<Event> Event = new List<Event>();

        [DataMember]
        public bool moreEventsAvailable;
    }
}
