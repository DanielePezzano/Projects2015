using System.Runtime.Serialization;

namespace SharedDto
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
