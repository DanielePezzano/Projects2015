using Models.Fleets.ShipClasses.Base;
using Models.Fleets.ShipClasses.Enums;
using Models.Fleets.ShipClasses.Hulls;
using Models.Tech;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Fleets.ShipClasses.System
{
    [DataContract]
    public class ShipSystem : PartShipEntity
    {
        [Display(Name="EnergyBonus", ResourceType=typeof(Resources))]
        [DataMember]
        public int EnergyBonus { get; set; }
        [Display(Name = "BonusToHit", ResourceType = typeof(Resources))]
        [DataMember]
        public int ToHitBonus { get; set; }
        [Display(Name = "MalusToHit", ResourceType = typeof(Resources))]
        [DataMember]
        public int ToHitMalus { get; set; }
        [Display(Name = "TravelSpeedBonus", ResourceType = typeof(Resources))]
        [DataMember]
        public int TravelSpeedBonus { get; set; }
        [Display(Name = "CombatSpeedBonus", ResourceType = typeof(Resources))]
        [DataMember]
        public int CombatSpeedBonus { get; set; }
        [Display(Name = "ScannerRadius", ResourceType = typeof(Resources))]
        [DataMember]
        public int ScannerRadius { get; set; }
        [Display(Name = "Range", Description="RangeHint", ResourceType = typeof(Resources))]
        [DataMember]
        public int Range { get; set; } //usato per le celle di energia : quanto in là riesce a portare una nave?
        [Display(Name = "SpaceBonus", ResourceType = typeof(Resources))]
        [DataMember]
        public double SpaceBonus { get; set; }
        [Display(Name = "ScannerRelevationBonus", ResourceType = typeof(Resources))]
        [DataMember]
        public double ScannerRelevationBonus { get; set; }
        [Display(Name = "ScannerRelevationMalus", ResourceType = typeof(Resources))]
        [DataMember]
        public double ScannerRelevationMalus { get; set; }
        [Display(Name = "SystemType", ResourceType = typeof(Resources))]
        [DataMember]
        public SystemType SystemType { get; set; }
        [DataMember]
        public virtual Hull Hull { get; set; }
    }
}
