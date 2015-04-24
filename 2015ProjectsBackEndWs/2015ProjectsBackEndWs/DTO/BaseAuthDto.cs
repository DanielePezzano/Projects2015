using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace _2015ProjectsBackEndWs.DTO
{
    [DataContract]
    public class BaseAuthDto
    {
        [DataMember]
        public DateTime GeneratedStamp { get; set; }
        [DataMember]
        public string AuthHash_01 { get; set; } //sha256 hashed string username_password_DtoName_GeneratedStamp.ToUniversalTime()
        [DataMember]
        public string AuthHash_02 { get; set; } //sha256 hashed string client_ServerGeneratedStamp.ToUniversalTime()_DtoName
    }
}