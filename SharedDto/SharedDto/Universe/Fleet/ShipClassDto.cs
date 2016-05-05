using System.Collections.Generic;
using System.Runtime.Serialization;
using SharedDto.BaseClasses;
using SharedDto.Interfaces;

namespace SharedDto.Universe.Fleet
{
    [DataContract]
    public class ShipClassDto : BaseDto, IDto, ICosts
    {
        [DataMember]
        public int StructurePoints { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int TotalArmor { get; set; }
        [DataMember]
        public int TotalShields { get; set; }
        [DataMember]
        public int CombatSpeed { get; set; }
        [DataMember]
        public int TravelSpeed { get; set; }
        [DataMember]
        public int EngineRadius { get; set; }
        [DataMember]
        public int ToHitBonus { get; set; }
        [DataMember]
        public int MoneyCost { get; set; }
        [DataMember]
        public int OreCost { get; set; }
        [DataMember]
        public int MoneyMaintenanceCost { get; set; }
        [DataMember]
        public int OreMaintenanceCost { get; set; }
        [DataMember]
        public List<HullDto> HullDtos { get; set; }
    }
}