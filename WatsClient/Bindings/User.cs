using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsClient.Bindings
{
    public class User
    {
        public long ID { get; set; }
        public string UserId { get; set; }
        public string HA1 { get; set; }
        public string Role { get; set; }
        public bool Locked { get; set; }
    }
}
