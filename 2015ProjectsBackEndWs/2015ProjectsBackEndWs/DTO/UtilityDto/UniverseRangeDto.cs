using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace _2015ProjectsBackEndWs.DTO.UtilityDto
{
    [DataContract]
    public class UniverseRangeDto
    {
        [DataMember]
        public int MinX { get; set; }
        [DataMember]
        public int MaxX { get; set; }
        [DataMember]
        public int MinY { get; set; }
        [DataMember]
        public int MaxY { get; set; }
        [DataMember]
        public BaseAuthDto Auth { get; set; }
    }
}