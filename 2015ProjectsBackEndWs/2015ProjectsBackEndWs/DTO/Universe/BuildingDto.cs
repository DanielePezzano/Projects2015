using Models.Buildings.Enums;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace _2015ProjectsBackEndWs.DTO.Universe
{
    [DataContract]
    public class BuildingDto
    {        
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public BuildingType BuildingType { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int MoneyCost { get; set; }
        [DataMember]
        public int MoneyMaintenanceCost { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Number { get; set; }
        [DataMember]
        public int OreCost { get; set; }
        [DataMember]
        public int OreMaintenanceCost { get; set; }
        [DataMember]
        public int SpaceNeeded { get; set; }
        [DataMember]
        public int UsedSpaces { get; set; }
        [DataMember]
        public List<BuildingSpecsDto> Details { get; set; }
    }
}
