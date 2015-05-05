using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace _2015ProjectsBackEndWs.Security
{
    public static class AcceptedClients
    {
        public static readonly ReadOnlyCollection<string> WhiteList = new ReadOnlyCollection<string>(new List<string>
        {
            "WcfTester"
        });
    }
}