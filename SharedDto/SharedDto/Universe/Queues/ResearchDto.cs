using System.Runtime.Serialization;
using SharedDto.Interfaces;
using SharedDto.Universe.Technology;

namespace SharedDto.Universe.Queues
{
    [DataContract]
    public class ResearchDto:IDto
    {
        [DataMember]
        public TechnologyDto TechnologyUnderResearch { get; set; }
        [DataMember]
        public int? PlanetId { get; set; }
        [DataMember]
        public int? SatelliteId { get; set; }
    }
}