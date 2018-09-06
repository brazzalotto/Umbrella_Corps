using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    [Serializable]
    public class MessageBase
    {
        internal MemoryStream InnerMessage { get; set; }
        public Guid CallbackID { get; set; }
        public bool HasError { get; set; }
        public Exception Exception { get; set; }

        public string message { get; set; }

        public MessageBase()
        {

            Exception = new Exception();
        }


    }

