using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2015ProjectsBackEndWs.Utility.Struct
{
    public struct MessageData
    {
        public string Hash;
        public DateTime Sent;

        public MessageData(string hash, DateTime sent)
        {
            this.Hash = hash;
            this.Sent = sent;
        }
    }
}