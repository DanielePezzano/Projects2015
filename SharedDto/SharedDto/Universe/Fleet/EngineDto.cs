
using System.Runtime.Serialization;

namespace SharedDto.Universe.Fleet
{
    [DataContract]
    public class EngineDto
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int CombatSpeed { get; set; }
        [DataMember]
        public int TravelSpeed { get; set; }
        [DataMember]
        public int GeneratedEnergy { get; set; }
        [DataMember]
        public int TechnologyId { get; set; }
    }
}
