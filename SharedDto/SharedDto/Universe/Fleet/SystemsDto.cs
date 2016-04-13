using System.Runtime.Serialization;

namespace SharedDto.Universe.Fleet
{
    [DataContract]
    public class SystemsDto
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string SystemType { get; set; }
        [DataMember]
        public int EnergyBonus { get; set; }
        [DataMember]
        public int ToHitBonus { get; set; }
        [DataMember]
        public int ToHitMalus { get; set; }
        [DataMember]
        public int TravelSpeedBonus { get; set; }
        [DataMember]
        public int CombatSpeedBonus { get; set; }
        [DataMember]
        public int ScannerRadius { get; set; }
        [DataMember]
        public int Range { get; set; } //usato per le celle di energia : quanto in là riesce a portare una nave?
        [DataMember]
        public double SpaceBonus { get; set; }
        [DataMember]
        public double ScannerRelevationBonus { get; set; }
        [DataMember]
        public double ScannerRelevationMalus { get; set; }
        [DataMember]
        public int TechnologyId { get; set; }
    }
}
