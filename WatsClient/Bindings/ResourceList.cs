using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsClient.Bindings
{
    public class ResourceList
    {
        public string ColorMd5 { get; set; }
        public List<long> Site { get; set; }
        public List<long> Emi { get; set; }
        public List<long> Channel { get; set; }
        public List<long> Link { get; set; }
        public List<long> Equipment { get; set; }
        public List<long> Region { get; set; }
        public List<long> Task { get; set; }
        public List<long> Analysis { get; set; }
    }
}
