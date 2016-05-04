
using System.Collections.Generic;
using System.Runtime.Serialization;
using SharedDto.BaseClasses;
using SharedDto.Interfaces;

namespace SharedDto.Universe.Fleet
{
    [DataContract]
    public class HullDto :BaseDto, IDto
    {
        [DataMember]
        public int TotalSpaces { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int UsedSpaces { get; set; }
        [DataMember]
        public int StructurePoints { get; set; }
        [DataMember]
        public string HullType { get; set; }
        [DataMember]
        public List<AntiPlanetWeaponDto> AntiPlanetWeaponDtos { get; set; }
        [DataMember]
        public List<AntiShipWeaponDto> AntishioShipWeaponDtos { get; set; }
        [DataMember]
        public List<EngineDto> EngineDtos { get; set; }
        [DataMember]
        public List<ArmorDto> ArmorDtos { get; set; }
        [DataMember]
        public List<ShieldDto> ShieldDtos { get; set; }
        [DataMember]
        public List<SystemsDto> SystemsDtos { get; set; }
    }
}
