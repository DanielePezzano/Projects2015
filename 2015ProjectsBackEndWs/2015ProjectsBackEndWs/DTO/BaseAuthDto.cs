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
        public string AuthHash { get; set; } //sha256 hashed string username_password_DtoName
    }
}