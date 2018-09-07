using System;
using System.Collections.Generic;
using System.Text;

namespace PartageTCP.Messages
{
    [System.Serializable]
    public class AdnLine
    {
        public string rsId { get; set; }

        public string chromosome { get; set; }

        public string position { get; set; }

        public string genotype { get; set;}
        
        public AdnLine() { }
    }
}
