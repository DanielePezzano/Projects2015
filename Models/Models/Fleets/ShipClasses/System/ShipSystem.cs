using Models.Fleets.ShipClasses.Base;
using Models.Fleets.ShipClasses.Enums;
using Models.Fleets.ShipClasses.Hulls;
using Models.Tech;
using System.ComponentModel.DataAnnotations;

namespace Models.Fleets.ShipClasses.System
{
    public class ShipSystem : PartShipEntity
    {
        [Display(Name="EnergyBonus", ResourceType=typeof(Resources))]
        public int EnergyBonus { get; set; }
        [Display(Name = "BonusToHit", ResourceType = typeof(Resources))]
        public int ToHitBonus { get; set; }
        [Display(Name = "MalusToHit", ResourceType = typeof(Resources))]
        public int ToHitMalus { get; set; }
        [Display(Name = "TravelSpeedBonus", ResourceType = typeof(Resources))]
        public int TravelSpeedBonus { get; set; }
        [Display(Name = "CombatSpeedBonus", ResourceType = typeof(Resources))]
        public int CombatSpeedBonus { get; set; }
        [Display(Name = "ScannerRadius", ResourceType = typeof(Resources))]
        public int ScannerRadius { get; set; }
        [Display(Name = "Range", Description="RangeHint", ResourceType = typeof(Resources))]
        public int Range { get; set; } //usato per le celle di energia : quanto in là riesce a portare una nave?
        [Display(Name = "SpaceBonus", ResourceType = typeof(Resources))]
        public double SpaceBonus { get; set; }
        [Display(Name = "ScannerRelevationBonus", ResourceType = typeof(Resources))]
        public double ScannerRelevationBonus { get; set; }
        [Display(Name = "ScannerRelevationMalus", ResourceType = typeof(Resources))]
        public double ScannerRelevationMalus { get; set; }
        [Display(Name = "SystemType", ResourceType = typeof(Resources))]
        public SystemType SystemType { get; set; }

        public virtual Hull Hull { get; set; }
    }
}
