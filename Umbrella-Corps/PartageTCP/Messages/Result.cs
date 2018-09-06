using System;
using System.Collections.Generic;
using System.Text;

namespace PartageTCP.Messages
{
    [System.Serializable]
    public class Result
    {
        public int aNumber { get; set; }
        public int tNumber { get; set; }
        public int cNumber { get; set; }
        public int gNumber { get; set; }
        public int unknownNumber { get; set; }
        public int pairNumber { get; set; }

        public Result() { }
    }
}
