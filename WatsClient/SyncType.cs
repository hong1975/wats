using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsClient
{
    public class SyncType
    {
        public const int ALL        = 0x0FFFFFFF;
        public const int USER       = 1;
        public const int COLOR      = 1 << 1;
        public const int SITE       = 1 << 2;
        public const int EMI        = 1 << 3;
        public const int CHANNEL    = 1 << 4;
        public const int LINK       = 1 << 5;
        public const int EQUIPMENT  = 1 << 6;
        public const int REGION     = 1 << 7;
        public const int TASK       = 1 << 8;
        public const int ANALYSIS   = 1 << 9;
    }
}
