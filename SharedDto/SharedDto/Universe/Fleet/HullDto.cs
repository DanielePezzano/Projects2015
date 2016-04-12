
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SharedDto.Universe.Fleet
{
    [DataContract]
    public class HullDto
    {
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
