using System;
using System.Collections.Generic;
using System.Text;

namespace PartageTCP.Messages
{
    [System.Serializable]
    public class AdnLinePackage
    {
        public int code { get; set; }
        public GenericAdnList adnList{ get; set; }

        public AdnLinePackage()
        {
            code = 0;
          
        }
    }
}
