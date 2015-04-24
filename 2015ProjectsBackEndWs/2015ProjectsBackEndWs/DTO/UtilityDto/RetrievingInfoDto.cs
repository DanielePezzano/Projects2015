using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace _2015ProjectsBackEndWs.DTO.UtilityDto
{
    [DataContract]
    public class RetrievingInfoDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public BaseAuthDto Auth { get; set; }
    }
}