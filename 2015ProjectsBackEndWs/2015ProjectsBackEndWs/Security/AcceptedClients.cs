using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace _2015ProjectsBackEndWs.Security
{
    public static class AcceptedClients
    {
        public static readonly ReadOnlyCollection<string> WhiteList = new ReadOnlyCollection<string>(new List<string>() { 
            "WcfTester",
        });
    }
}