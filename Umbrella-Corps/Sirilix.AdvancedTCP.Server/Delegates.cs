using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirilix.AdvancedTCP.Shared.Messages;

namespace Sirilix.AdvancedTCP.Server
{
    public class Delegates
    {
        public delegate void ClientValidatingDelegate(EventArguments.ClientValidatingEventArgs args);
        public delegate void ClientBasicDelegate(Receiver receiver);
    }
}
