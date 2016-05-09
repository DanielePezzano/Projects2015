using System;
using System.Runtime.Serialization;
using SharedDto.BaseClasses;
using SharedDto.Interfaces;
using SharedDto.Universe.Technology;

namespace SharedDto.Universe.Queues
{
    [DataContract]
    public class ResearchDto : BaseDto, IDto, IQueue
    {
        [DataMember]
        public TechnologyDto TechnologyUnderResearch { get; set; }
        [DataMember]
        public int? PlanetId { get; set; }
        [DataMember]
        public int? SatelliteId { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public DateTime FinishDateTime { get; set; }
    }
}