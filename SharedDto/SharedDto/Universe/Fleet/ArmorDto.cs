using System.Runtime.Serialization;

namespace SharedDto.Universe.Fleet
{
    [DataContract]
    public class ArmorDto
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int Protection { get; set; }
        [DataMember]
        public double PercCombatSpeedMalus { get; set; }
        [DataMember]
        public double PercTravelSpeedMalus { get; set; }
        [DataMember]
        public int TechnologyId { get; set; }
    }
}
