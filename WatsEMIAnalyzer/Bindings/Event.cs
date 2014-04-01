using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WatsEMIAnalyzer.Bindings
{
    [DataContract]
    public class Event
    {
        /*
        public const string registerEvtType = "registrationChangedEvent";
        public const string groupEvtType = "groupChangedEvent";
        public const string presenceEvtType = "presenceChangedEvent";
        public const string contactEvtType = "contactChangedEvent";
        public const string configurationEvtType = "configurationChangedEvent";
        public const string pollingPresenceEvtType = "pollingPresenceEvent";
        public const string imChatEvtType = "imChatEvent";
        public const string imMessageEvtType = "imMessageEvent";
        public const string imFileMediaEvtType = "imFileMediaEvent";
        public const string p2pSessionEvtType = "p2pSessionEvent";
        */

        [DataMember]
        public int eventId;

        [DataMember]
        public string timestamp;
    }
}
