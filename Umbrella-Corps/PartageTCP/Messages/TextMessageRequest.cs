﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    [Serializable]
    public class TextMessageRequest : RequestMessageBase
    {
        public String Message { get; set; }

        public int Kilos { get; set; }

}

