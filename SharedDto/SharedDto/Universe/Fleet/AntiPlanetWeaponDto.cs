using System;
using System.Runtime.Serialization;
using SharedDto.BaseClasses;
using SharedDto.Interfaces;


namespace SharedDto.Universe.Fleet
{
    [DataContract]
    public class AntiPlanetWeaponDto : BaseDto, IDto, ICosts, ISpaces, IMaintenanceCost
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public bool RadiationHazard { get; set; }
        [DataMember]
        public int RateOfFire { get; set; } //quante volte spara in un round
        [DataMember]
        public int Damage { get; set; }
        [DataMember]
        public int BonusToHit { get; set; }
        [DataMember]
        public int OreCost { get; set; }
        [DataMember]
        public int MoneyCost { get; set; }
        [DataMember]
        public int OreMaintenanceCost { get; set; }
        [DataMember]
        public int MoneyMaintenanceCost { get; set; }
        [DataMember]
        public int SpacesNeeded { get; set; }
        [DataMember]
        public DateTime CreatedAt { get; set; }
    }
}
