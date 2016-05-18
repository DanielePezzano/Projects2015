using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SharedDto.BaseClasses;
using SharedDto.Interfaces;
using SharedDto.Universe.Queues;

namespace SharedDto.Universe.Technology
{
    [DataContract]
    public class TechnologyDto : BaseDto, IDto, ICosts, ISpaces
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Field { get; set; }
        [DataMember]
        public string SubField { get; set; }
        [DataMember]
        public int OreCost { get; set; }
        [DataMember]
        public int MoneyCost { get; set; }
        [DataMember]
        public int ResearchPoints { get; set; } //punti ricerca da investire
        [DataMember]
        public List<TechnologyRequisiteDto> NeededTechnologies { get; set; }
        [DataMember]
        public List<TechnologyBonusDto> TechnologyBonuses { get; set; }
        [DataMember]
        public List<ResearchDto> ResearchDtos { get; set; }
        [DataMember]
        public int SpacesNeeded { get; set; }
        [DataMember]
        public DateTime CreatedAt { get; set; } 

    }
}