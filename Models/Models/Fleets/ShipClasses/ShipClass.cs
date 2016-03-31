using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using Models.Fleets.ShipClasses.Base;
using Models.Fleets.ShipClasses.Hulls;
using Models.Users;

namespace Models.Fleets.ShipClasses
{
    [DataContract(IsReference = true)]
    public class ShipClass : BaseShipEntity
    {
        [NotMapped]
        [Display(Name = "StructurePoints", Description = "StructurePointsHint", ResourceType = typeof (Resources))]
        [DataMember]
        public int StructurePoints
        {
            get { return GetStructurePoints(); }
        }

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
        public virtual ICollection<Hull> Hulls { get; set; }

        [DataMember]
        public virtual ICollection<Fleet> Fleets { get; set; }

        [DataMember]
        public virtual User User { get; set; }

        private int GetOreMaintCost()
        {
            return Hulls != null && Hulls.Count > 0
                ? Hulls.Sum(x => x.OreMaintenanceCost)
                : 0;
        }

        private int GetMoneyMaintCost()
        {
            return Hulls != null && Hulls.Count > 0
                ? Hulls.Sum(x => x.MoneyMaintenanceCost)
                : 0;
        }

        private int GetOreCost()
        {
            return Hulls != null && Hulls.Count > 0
                ? Hulls.Sum(x => x.OreCost)
                : 0;
        }

        private int GetMoneyCost()
        {
            return Hulls != null && Hulls.Count > 0
                ? Hulls.Sum(x => x.MoneyCost)
                : 0;
        }

        private int GetTotalToHitBonus()
        {
            return Hulls != null && Hulls.Count > 0
                ? Hulls.Sum(x => x.ToHitBonus)
                : 0;
        }

        private int GetEngineRadius()
        {
            return Hulls != null && Hulls.Count > 0
                ? Hulls.Sum(x => x.EngineRadius)
                : 0;
        }

        private int GetTravelSpeed()
        {
            return Hulls != null && Hulls.Count > 0
                ? Hulls.Sum(x => x.TravelSpeed)
                : 0;
        }

        private int GetCombatSpeed()
        {
            return Hulls != null && Hulls.Count > 0
                ? Hulls.Sum(x => x.CombatSpeed)
                : 0;
        }

        private int GetTotalShields()
        {
            return Hulls != null && Hulls.Count > 0
                ? Hulls.Sum(x => x.TotalShields)
                : 0;
        }

        private int GetTotalArmor()
        {
            return Hulls != null && Hulls.Count > 0
                ? Hulls.Sum(x => x.TotalArmor)
                : 0;
        }

        private int GetStructurePoints()
        {
            return Hulls != null && Hulls.Count > 0
                ? Hulls.Sum(x => x.StructurePoints)
                : 0;
        }
    }
}