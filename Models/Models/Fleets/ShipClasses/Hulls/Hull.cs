using Models.Fleets.ShipClasses.Armors;
using Models.Fleets.ShipClasses.Base;
using Models.Fleets.ShipClasses.Engines;
using Models.Fleets.ShipClasses.Enums;
using Models.Fleets.ShipClasses.Shields;
using Models.Fleets.ShipClasses.System;
using Models.Fleets.ShipClasses.Weapons;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Models.Fleets.ShipClasses.Hulls
{
    public class Hull : BaseShipEntity
    {
        [Required()]
        [Display(Name = "TotalSpaces", Description = "TotalSpacesHint", ResourceType = typeof(Resources))]
        public int TotalSpaces { get; set; }
        [NotMapped()]
        [Display(Name = "UsedSpaces", ResourceType = typeof(Resources))]
        public int UsedSpaces { get { return GetTotalUsedSpaces(); } }
        [Required()]
        [Display(Name = "StructurePoints", Description = "StructurePointsHint", ResourceType = typeof(Resources))]
        public int StructurePoints { get; set; }
        [NotMapped()]
        [Display(Name = "Armor", ResourceType = typeof(Resources))]
        public int TotalArmor { get { return GetTotalArmor(); } }
        [NotMapped()]
        [Display(Name = "Shield", ResourceType = typeof(Resources))]
        public int TotalShields { get { return GetTotalShields(); } }
        [NotMapped()]
        [Display(Name = "CombatSpeed", ResourceType = typeof(Resources))]
        public int CombatSpeed { get { return GetCombatSpeed(); } }
        [NotMapped()]
        [Display(Name = "TravelSpeed", ResourceType = typeof(Resources))]
        public int TravelSpeed { get { return GetTravelSpeed(); } }
        [NotMapped()]
        [Display(Name = "Range", Description = "RangeHint", ResourceType = typeof(Resources))]
        public int EngineRadius { get { return GetEngineRadius(); } }
        [NotMapped()]
        [Display(Name = "BonusToHit", ResourceType = typeof(Resources))]
        public int ToHitBonus { get { return GetTotalToHitBonus(); } }

        
        [Display(Name = "MoneyCost", ResourceType = typeof(Resources))]
        [NotMapped()]
        public int MoneyCost { get { return GetMoneyCost(); } }    
        [Display(Name = "OreCost", ResourceType = typeof(Resources))]
        [NotMapped()]
        public int OreCost { get { return GetOreCost(); }}       
        [Display(Name = "MoneyMaintenanceCost", ResourceType = typeof(Resources))]
        [NotMapped()]
        public int MoneyMaintenanceCost { get { return GetMoneyMaintCost(); }  }       
        [Display(Name = "OreMaintenanceCost", ResourceType = typeof(Resources))]
        [NotMapped()]
        public int OreMaintenanceCost { get { return GetOreMaintCost(); } }

        [Required()]
        [EnumDataType(typeof(HullType))]
        [Display(Name = "HullType", ResourceType = typeof(Resources))]
        public HullType HullType { get; set; }
        public virtual ICollection<Armor> Armors { get; set; }
        public virtual ICollection<Engine> Engines { get; set; }
        public virtual ICollection<AntiShipWeapon> AntiShipWeapons { get; set; }
        public virtual ICollection<AntiPlanetWeapon> AntiPlanetWeapons { get; set; }
        public virtual ICollection<ShipSystem> SubSystems { get; set; }
        public virtual ICollection<Shield> Shields { get; set; }


        private int GetTotalUsedSpaces()
        {
            int totalEngineSpaces = (this.Engines != null && this.Engines.Count > 0) ? this.Engines.Sum(x => x.SpacesNeeded) : 0;
            int totalArmorSpaces = (this.Armors != null && this.Armors.Count > 0) ? this.Armors.Sum(x => x.SpacesNeeded) : 0;
            int totalShieldSpaces = (this.Shields != null && this.Shields.Count > 0) ? this.Shields.Sum(x => x.SpacesNeeded) : 0;
            int totalWeapons1 = (this.AntiShipWeapons != null && this.AntiShipWeapons.Count > 0) ? this.AntiShipWeapons.Sum(x => x.SpacesNeeded) : 0;
            int totalWeapons2 = (this.AntiPlanetWeapons != null && this.AntiPlanetWeapons.Count > 0) ? this.AntiPlanetWeapons.Sum(x => x.SpacesNeeded) : 0;
            int totalSystemSp = (this.SubSystems != null && this.SubSystems.Count > 0) ? this.SubSystems.Sum(x => x.SpacesNeeded) : 0;

            return totalArmorSpaces + totalEngineSpaces + totalShieldSpaces + totalWeapons1 + totalWeapons2 + totalSystemSp;
        }

        private int GetTotalToHitBonus()
        {
            return (this.SubSystems != null && this.SubSystems.Count > 0) ?
                this.SubSystems.Sum(x => x.ToHitBonus) : 0;
        }

        private int GetTotalArmor()
        {
            return (this.Armors != null && this.Armors.Count > 0) ?
                this.Armors.Sum(x => x.Protection) : 0;
        }

        private int GetTotalShields()
        {
            return (this.Shields != null && this.Shields.Count > 0) ?
                this.Shields.Sum(x => x.Protection) : 0;
        }

        private int GetCombatSpeed()
        {
            int engineBonus = (this.Engines != null && this.Engines.Count > 0) ? this.Engines.Sum(x => x.CombatSpeed) : 0;
            int systemBonus = (this.SubSystems != null && this.SubSystems.Count > 0) ? this.SubSystems.Sum(x => x.CombatSpeedBonus) : 0;

            return engineBonus + systemBonus;
        }

        private int GetTravelSpeed()
        {
            int engineBonus = (this.Engines != null && this.Engines.Count > 0) ? this.Engines.Sum(x => x.TravelSpeed) : 0;
            int systemBonus = (this.SubSystems != null && this.SubSystems.Count > 0) ? this.SubSystems.Sum(x => x.TravelSpeedBonus) : 0;

            return engineBonus + systemBonus;
        }

        private int GetEngineRadius()
        {
            return (this.SubSystems != null && this.SubSystems.Count > 0) ? this.SubSystems.Sum(x => x.Range) : 0;
        }

        private int GetMoneyCost()
        {
            int totalEngineMoneyCost = (this.Engines != null && this.Engines.Count > 0) ? this.Engines.Sum(x => x.MoneyCost) : 0;
            int totalArmorMoneyCost = (this.Armors != null && this.Armors.Count > 0) ? this.Armors.Sum(x => x.MoneyCost) : 0;
            int totalShieldMoneyCost = (this.Shields != null && this.Shields.Count > 0) ? this.Shields.Sum(x => x.MoneyCost) : 0;
            int totalWeapons1MoneyCost = (this.AntiShipWeapons != null && this.AntiShipWeapons.Count > 0) ? this.AntiShipWeapons.Sum(x => x.MoneyCost) : 0;
            int totalWeapons2MoneyCost = (this.AntiPlanetWeapons != null && this.AntiPlanetWeapons.Count > 0) ? this.AntiPlanetWeapons.Sum(x => x.MoneyCost) : 0;
            int totalSystemMoneyCost = (this.SubSystems != null && this.SubSystems.Count > 0) ? this.SubSystems.Sum(x => x.MoneyCost) : 0;

            return totalEngineMoneyCost + totalArmorMoneyCost + totalShieldMoneyCost + totalWeapons1MoneyCost + totalWeapons2MoneyCost + totalSystemMoneyCost;
        }

        private int GetOreCost()
        {
            int totalEngineOreCost = (this.Engines != null && this.Engines.Count > 0) ? this.Engines.Sum(x => x.OreCost) : 0;
            int totalArmorOreCost = (this.Armors != null && this.Armors.Count > 0) ? this.Armors.Sum(x => x.OreCost) : 0;
            int totalShieldOreCost = (this.Shields != null && this.Shields.Count > 0) ? this.Shields.Sum(x => x.OreCost) : 0;
            int totalWeapons1OreCost = (this.AntiShipWeapons != null && this.AntiShipWeapons.Count > 0) ? this.AntiShipWeapons.Sum(x => x.OreCost) : 0;
            int totalWeapons2OreCost = (this.AntiPlanetWeapons != null && this.AntiPlanetWeapons.Count > 0) ? this.AntiPlanetWeapons.Sum(x => x.OreCost) : 0;
            int totalSystemOreCost = (this.SubSystems != null && this.SubSystems.Count > 0) ? this.SubSystems.Sum(x => x.OreCost) : 0;

            return totalEngineOreCost + totalArmorOreCost + totalShieldOreCost + totalWeapons1OreCost + totalWeapons2OreCost + totalSystemOreCost;
        }

        private int GetOreMaintCost()
        {
            int totalEngineOreCost = (this.Engines != null && this.Engines.Count > 0) ? this.Engines.Sum(x => x.OreMaintenanceCost) : 0;
            int totalArmorOreCost = (this.Armors != null && this.Armors.Count > 0) ? this.Armors.Sum(x => x.OreMaintenanceCost) : 0;
            int totalShieldOreCost = (this.Shields != null && this.Shields.Count > 0) ? this.Shields.Sum(x => x.OreMaintenanceCost) : 0;
            int totalWeapons1OreCost = (this.AntiShipWeapons != null && this.AntiShipWeapons.Count > 0) ? this.AntiShipWeapons.Sum(x => x.OreMaintenanceCost) : 0;
            int totalWeapons2OreCost = (this.AntiPlanetWeapons != null && this.AntiPlanetWeapons.Count > 0) ? this.AntiPlanetWeapons.Sum(x => x.OreMaintenanceCost) : 0;
            int totalSystemOreCost = (this.SubSystems != null && this.SubSystems.Count > 0) ? this.SubSystems.Sum(x => x.OreMaintenanceCost) : 0;

            return totalEngineOreCost + totalArmorOreCost + totalShieldOreCost + totalWeapons1OreCost + totalWeapons2OreCost + totalSystemOreCost;
        }

        private int GetMoneyMaintCost()
        {
            int totalEngineMoneyCost = (this.Engines != null && this.Engines.Count > 0) ? this.Engines.Sum(x => x.MoneyMaintenanceCost) : 0;
            int totalArmorMoneyCost = (this.Armors != null && this.Armors.Count > 0) ? this.Armors.Sum(x => x.MoneyMaintenanceCost) : 0;
            int totalShieldMoneyCost = (this.Shields != null && this.Shields.Count > 0) ? this.Shields.Sum(x => x.MoneyMaintenanceCost) : 0;
            int totalWeapons1MoneyCost = (this.AntiShipWeapons != null && this.AntiShipWeapons.Count > 0) ? this.AntiShipWeapons.Sum(x => x.MoneyMaintenanceCost) : 0;
            int totalWeapons2MoneyCost = (this.AntiPlanetWeapons != null && this.AntiPlanetWeapons.Count > 0) ? this.AntiPlanetWeapons.Sum(x => x.MoneyMaintenanceCost) : 0;
            int totalSystemMoneyCost = (this.SubSystems != null && this.SubSystems.Count > 0) ? this.SubSystems.Sum(x => x.MoneyMaintenanceCost) : 0;

            return totalEngineMoneyCost + totalArmorMoneyCost + totalShieldMoneyCost + totalWeapons1MoneyCost + totalWeapons2MoneyCost + totalSystemMoneyCost;
        }
    }
}
