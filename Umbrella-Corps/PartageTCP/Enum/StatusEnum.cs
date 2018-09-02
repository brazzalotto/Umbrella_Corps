using System;
using System.Collections.Generic;
using System.Text;

namespace PartageTCP.Enum
{

        [Serializable]
        public enum StatusEnum
        {
            Connected,
            Disconnected,
            Validated,
            InSession
        }

}
