using System;
using System.Collections.Generic;
using System.Text;

namespace PartageTCP.Messages
{
    [Serializable]
    public class AdnLinePackage
    {
        public int code { get; set; }
        public List<AdnLine> adnList{ get; set; }

        public AdnLinePackage()
        {
            code = 0;
            adnList = new List<AdnLine>();
        }
    }
}
