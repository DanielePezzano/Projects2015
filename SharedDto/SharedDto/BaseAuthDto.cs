using System;
using System.Runtime.Serialization;

namespace SharedDto
{
    [DataContract]
    public class BaseAuthDto
    {
        [DataMember]
        public DateTime GeneratedStamp { get; set; }
        [DataMember]
        public string AuthHash01 { get; set; } //sha256 hashed string username_password_DtoName_GeneratedStamp.ToUniversalTime()
        [DataMember]
        public string AuthHash02 { get; set; } //sha256 hashed string client_ServerGeneratedStamp.ToUniversalTime()_DtoName
    }
}
