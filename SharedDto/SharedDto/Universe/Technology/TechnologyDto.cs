using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SharedDto.Universe.Technology
{
    [DataContract]
    public class TechnologyDto
    {
        [DataMember]
        public int Id { get; set; }
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
        public List<int> NeededTechnologies { get; set; }
        [DataMember]
        public List<TechnologyBonusDto> TechnologyBonuses { get; set; }
    }
}