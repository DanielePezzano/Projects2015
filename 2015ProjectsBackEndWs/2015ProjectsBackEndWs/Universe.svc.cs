using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace _2015ProjectsBackEndWs
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Universe" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Universe.svc or Universe.svc.cs at the Solution Explorer and start debugging.
    public class Universe : IUniverse
    {
        public string GetUniversePortion(int minX, int minY, int maxX, int maxY)
        {
            throw new NotImplementedException();
        }

        public void GeneratePortion(int minX, int minY, int maxX, int maxY)
        {
            throw new NotImplementedException();
        }
    }
}
