using System;
using System.Collections.Generic;
using System.Text;

namespace PartageTCP.Messages
{
    [System.Serializable]
    public class GenericAdnList: List<AdnLine>
    {
        public GenericAdnList(IEnumerable<AdnLine> collection) : base(collection)
        {
        }

        public GenericAdnList(int capacity) : base(capacity)
        {
        }

        public GenericAdnList()
        {
            
        }
    }
}

