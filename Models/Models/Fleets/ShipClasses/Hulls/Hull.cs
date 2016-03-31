using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using Models.Fleets.ShipClasses.Armors;
using Models.Fleets.ShipClasses.Base;
using Models.Fleets.ShipClasses.Engines;
using Models.Fleets.ShipClasses.Enums;
using Models.Fleets.ShipClasses.Shields;
using Models.Fleets.ShipClasses.System;
using Models.Fleets.ShipClasses.Weapons;

namespace Models.Fleets.ShipClasses.Hulls
{
    [DataContract(IsReference = true)]
    public class Hull : BaseShipEntity
    {
        [Required]
        [Display(Name = "TotalSpaces", Description = "TotalSpacesHint", ResourceType = typeof (Resources))]
        [DataMember]
        public int TotalSpaces { get; set; }

        [NotMapped]
        [Display(Name = "UsedSpaces", ResourceType = typeof (Resources))]
        [DataMember]
        public int UsedSpaces
        {
            get { return GetTotalUsedSpaces(); }
        }

        [Required]
        [Display(Name = "StructurePoints", Description = "StructurePointsHint", ResourceType = typeof (Resources))]
        [DataMember]
        public int StructurePoints { get; set; }

        [NotMapped]
        [Display(Name = "Armor", ResourceType = typeof (Resources))]
        [DataMember]
        public int TotalArmor
        {
            get { return GetTotalArmor(); }
        }

        [NotMapped]
        [Display(Name = "Shield", ResourceType = typeof (Resources))]
        [DataMember]
        public int TotalShields
        {
            get { return GetTotalShields(); }
        }

        [NotMapped]
        [Display(Name = "CombatSpeed", ResourceType = typeof (Resources))]
        [DataMember]
        public int CombatSpeed
        {
            get { return GetCombatSpeed(); }
        }

        [NotMapped]
        [Display(Name = "TravelSpeed", ResourceType = typeof (Resources))]
        [DataMember]
        public int TravelSpeed
        {
            get { return GetTravelSpeed(); }
        }

        [NotMapped]
        [Display(Name = "Range", Description = "RangeHint", ResourceType = typeof (Resources))]
        [DataMember]
        public int EngineRadius
        {
            get { return GetEngineRadius(); }
        }

        [NotMapped]
        [Display(Name = "BonusToHit", ResourceType = typeof (Resources))]
        [DataMember]
        public int ToHitBonus
        {
            get { return GetTotalToHitBonus(); }
        }

        [Display(Name = "MoneyCost", ResourceType = typeof (Resources))]
        [NotMapped]
        [DataMember]
        public int MoneyCost
        {
            get { return GetMoneyCost(); }
        }

        [Display(Name = "OreCost", ResourceType = typeof (Resources))]
        [NotMapped]
        [DataMember]
        public int OreCost
        {
            get { return GetOreCost(); }
        }

        [Display(Name = "MoneyMaintenanceCost", ResourceType = typeof (Resources))]
        [NotMapped]
        [DataMember]
        public int MoneyMaintenanceCost
        {
            get { return GetMoneyMaintCost(); }
        }

        [Display(Name = "OreMaintenanceCost", ResourceType = typeof (Resources))]
        [NotMapped]
        [DataMember]
        public int OreMaintenanceCost
        {
            get { return GetOreMaintCost(); }
        }

        [DataMember]
        [Required]
        [EnumDataType(typeof (HullType))]
        [Display(Name = "HullType", ResourceType = typeof (Resources))]
        public HullType HullType { get; set; }

        [DataMember]
        public virtual ICollection<Armor> Armors { get; set; }

        [DataMember]
        public virtual ICollection<Engine> Engines { get; set; }

        [DataMember]
        public virtual ICollection<AntiShipWeapon> AntiShipWeapons { get; set; }

        [DataMember]
        public virtual ICollection<AntiPlanetWeapon> AntiPlanetWeapons { get; set; }

        [DataMember]
        public virtual ICollection<ShipSystem> SubSystems { get; set; }

        [DataMember]
        public virtual ICollection<Shield> Shields { get; set; }

        private int GetTotalUsedSpaces()
        {
            var totalEngineSpaces = Engines != null && Engines.Count > 0 ? Engines.Sum(x => x.SpacesNeeded) : 0;
            var totalArmorSpaces = Armors != null && Armors.Count > 0 ? Armors.Sum(x => x.SpacesNeeded) : 0;
            var totalShieldSpaces = Shields != null && Shields.Count > 0 ? Shields.Sum(x => x.SpacesNeeded) : 0;
            var totalWeapons1 = AntiShipWeapons != null && AntiShipWeapons.Count > 0
                ? AntiShipWeapons.Sum(x => x.SpacesNeeded)
                : 0;
            var totalWeapons2 = AntiPlanetWeapons != null && AntiPlanetWeapons.Count > 0
                ? AntiPlanetWeapons.Sum(x => x.SpacesNeeded)
                : 0;
            var totalSystemSp = SubSystems != null && SubSystems.Count > 0 ? SubSystems.Sum(x => x.SpacesNeeded) : 0;

            return totalArmorSpaces + totalEngineSpaces + totalShieldSpaces + totalWeapons1 + totalWeapons2 +
                   totalSystemSp;
        }

        private int GetTotalToHitBonus()
        {
            return SubSystems != null && SubSystems.Count > 0
                ? SubSystems.Sum(x => x.ToHitBonus)
                : 0;
        }

        private int GetTotalArmor()
        {
            return Armors != null && Armors.Count > 0
                ? Armors.Sum(x => x.Protection)
                : 0;
        }

        private int GetTotalShields()
        {
            return Shields != null && Shields.Count > 0
                ? Shields.Sum(x => x.Protection)
                : 0;
        }

        private int GetCombatSpeed()
        {
            var engineBonus = Engines != null && Engines.Count > 0 ? Engines.Sum(x => x.CombatSpeed) : 0;
            var systemBonus = SubSystems != null && SubSystems.Count > 0 ? SubSystems.Sum(x => x.CombatSpeedBonus) : 0;

            return engineBonus + systemBonus;
        }

        private int GetTravelSpeed()
        {
            var engineBonus = Engines != null && Engines.Count > 0 ? Engines.Sum(x => x.TravelSpeed) : 0;
            var systemBonus = SubSystems != null && SubSystems.Count > 0 ? SubSystems.Sum(x => x.TravelSpeedBonus) : 0;

            return engineBonus + systemBonus;
        }

        private int GetEngineRadius()
        {
            return SubSystems != null && SubSystems.Count > 0 ? SubSystems.Sum(x => x.Range) : 0;
        }

        private int GetMoneyCost()
        {
            var totalEngineMoneyCost = Engines != null && Engines.Count > 0 ? Engines.Sum(x => x.MoneyCost) : 0;
            var totalArmorMoneyCost = Armors != null && Armors.Count > 0 ? Armors.Sum(x => x.MoneyCost) : 0;
            var totalShieldMoneyCost = Shields != null && Shields.Count > 0 ? Shields.Sum(x => x.MoneyCost) : 0;
            var totalWeapons1MoneyCost = AntiShipWeapons != null && AntiShipWeapons.Count > 0
                ? AntiShipWeapons.Sum(x => x.MoneyCost)
                : 0;
            var totalWeapons2MoneyCost = AntiPlanetWeapons != null && AntiPlanetWeapons.Count > 0
                ? AntiPlanetWeapons.Sum(x => x.MoneyCost)
                : 0;
            var totalSystemMoneyCost = SubSystems != null && SubSystems.Count > 0
                ? SubSystems.Sum(x => x.MoneyCost)
                : 0;

            return totalEngineMoneyCost + totalArmorMoneyCost + totalShieldMoneyCost + totalWeapons1MoneyCost +
                   totalWeapons2MoneyCost + totalSystemMoneyCost;
        }

        private int GetOreCost()
        {
            var totalEngineOreCost = Engines != null && Engines.Count > 0 ? Engines.Sum(x => x.OreCost) : 0;
            var totalArmorOreCost = Armors != null && Armors.Count > 0 ? Armors.Sum(x => x.OreCost) : 0;
            var totalShieldOreCost = Shields != null && Shields.Count > 0 ? Shields.Sum(x => x.OreCost) : 0;
            var totalWeapons1OreCost = AntiShipWeapons != null && AntiShipWeapons.Count > 0
                ? AntiShipWeapons.Sum(x => x.OreCost)
                : 0;
            var totalWeapons2OreCost = AntiPlanetWeapons != null && AntiPlanetWeapons.Count > 0
                ? AntiPlanetWeapons.Sum(x => x.OreCost)
                : 0;
            var totalSystemOreCost = SubSystems != null && SubSystems.Count > 0 ? SubSystems.Sum(x => x.OreCost) : 0;

            return totalEngineOreCost + totalArmorOreCost + totalShieldOreCost + totalWeapons1OreCost +
                   totalWeapons2OreCost + totalSystemOreCost;
        }

        private int GetOreMaintCost()
        {
            var totalEngineOreCost = Engines != null && Engines.Count > 0 ? Engines.Sum(x => x.OreMaintenanceCost) : 0;
            var totalArmorOreCost = Armors != null && Armors.Count > 0 ? Armors.Sum(x => x.OreMaintenanceCost) : 0;
            var totalShieldOreCost = Shields != null && Shields.Count > 0 ? Shields.Sum(x => x.OreMaintenanceCost) : 0;
            var totalWeapons1OreCost = AntiShipWeapons != null && AntiShipWeapons.Count > 0
                ? AntiShipWeapons.Sum(x => x.OreMaintenanceCost)
                : 0;
            var totalWeapons2OreCost = AntiPlanetWeapons != null && AntiPlanetWeapons.Count > 0
                ? AntiPlanetWeapons.Sum(x => x.OreMaintenanceCost)
                : 0;
            var totalSystemOreCost = SubSystems != null && SubSystems.Count > 0
                ? SubSystems.Sum(x => x.OreMaintenanceCost)
                : 0;

            return totalEngineOreCost + totalArmorOreCost + totalShieldOreCost + totalWeapons1OreCost +
                   totalWeapons2OreCost + totalSystemOreCost;
        }

        private int GetMoneyMaintCost()
        {
            var totalEngineMoneyCost = Engines != null && Engines.Count > 0
                ? Engines.Sum(x => x.MoneyMaintenanceCost)
                : 0;
            var totalArmorMoneyCost = Armors != null && Armors.Count > 0 ? Armors.Sum(x => x.MoneyMaintenanceCost) : 0;
            var totalShieldMoneyCost = Shields != null && Shields.Count > 0
                ? Shields.Sum(x => x.MoneyMaintenanceCost)
                : 0;
            var totalWeapons1MoneyCost = AntiShipWeapons != null && AntiShipWeapons.Count > 0
                ? AntiShipWeapons.Sum(x => x.MoneyMaintenanceCost)
                : 0;
            var totalWeapons2MoneyCost = AntiPlanetWeapons != null && AntiPlanetWeapons.Count > 0
                ? AntiPlanetWeapons.Sum(x => x.MoneyMaintenanceCost)
                : 0;
            var totalSystemMoneyCost = SubSystems != null && SubSystems.Count > 0
                ? SubSystems.Sum(x => x.MoneyMaintenanceCost)
                : 0;

            return totalEngineMoneyCost + totalArmorMoneyCost + totalShieldMoneyCost + totalWeapons1MoneyCost +
                   totalWeapons2MoneyCost + totalSystemMoneyCost;
        }
    }
}