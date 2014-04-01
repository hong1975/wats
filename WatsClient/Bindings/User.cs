using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsClient.Bindings
{
    public class User
    {
        public string userId { get; set; }
        
        public string ha1 { get; set; }
        
        public string role { get; set; }
        
        public bool locked { get; set; }
    }
}
