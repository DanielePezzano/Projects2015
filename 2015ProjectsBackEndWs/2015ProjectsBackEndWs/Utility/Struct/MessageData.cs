using System;

namespace _2015ProjectsBackEndWs.Utility.Struct
{
    public struct MessageData
    {
        public string Hash;
        public DateTime Sent;

        public MessageData(string hash, DateTime sent)
        {
            Hash = hash;
            Sent = sent;
        }
    }
}