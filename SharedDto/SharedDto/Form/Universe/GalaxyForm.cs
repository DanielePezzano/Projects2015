using System.Runtime.Serialization;

namespace SharedDto.Form.Universe
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
