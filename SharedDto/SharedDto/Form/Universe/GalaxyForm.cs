using System.Runtime.Serialization;

namespace SharedDto.Form
{
    [DataContract]
    public class GalaxyForm
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int GalaxyId { get; set; }
    }
}
