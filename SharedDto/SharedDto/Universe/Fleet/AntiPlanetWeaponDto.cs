using System.Runtime.Serialization;


namespace SharedDto.Universe.Fleet
{
    [DataContract]
    public class AntiPlanetWeaponDto
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
        public int TechnologyId { get; set; }
    }
}
