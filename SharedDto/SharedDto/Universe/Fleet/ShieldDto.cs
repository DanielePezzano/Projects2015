using System.Runtime.Serialization;

namespace SharedDto.Universe.Fleet
{
    [DataContract]
    public class ShieldDto
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int Protection { get; set; }
        [DataMember]
        public int RequiredEnergy { get; set; }
        [DataMember]
        public int TechnologyId { get; set; }
    }
}
