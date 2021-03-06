﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using Models.Fleets.ShipClasses;
using Models.Fleets.ShipClasses.Base;
using Models.Universe.Strcut;
using Models.Users;

namespace Models.Fleets
{
    [DataContract(IsReference = true)]
    public class Fleet : BaseShipEntity
    {
        [NotMapped]
        [Display(Name = "TravelSpeed", ResourceType = typeof (Resources))]
        [DataMember]
        public int TravelSpeed => GetTravelSpeed();

        [DataMember]
        [NotMapped]
        [Display(Name = "Range", Description = "RangeHint", ResourceType = typeof (Resources))]
        public int Range => GetRange();

        [Required]
        [Display(Name = "AtBay", Description = "AtBayHint", ResourceType = typeof (Resources))]
        [DataMember]
        public bool AtBay { get; set; }

        [DataMember]
        public int AtBayPlanetId { get; set; }

        [DataMember]
        [Display(Name = "MoneyCost", ResourceType = typeof (Resources))]
        [NotMapped]
        public int MoneyCost => GetMoneyCost();

        [Display(Name = "OreCost", ResourceType = typeof (Resources))]
        [NotMapped]
        [DataMember]
        public int OreCost => GetOreCost();

        [Display(Name = "MoneyMaintenanceCost", ResourceType = typeof (Resources))]
        [NotMapped]
        [DataMember]
        public int MoneyMaintenanceCost => GetMoneyMaintCost();

        [Display(Name = "OreMaintenanceCost", ResourceType = typeof (Resources))]
        [NotMapped]
        [DataMember]
        public int OreMaintenanceCost => GetOreMaintCost();

        [Required]
        [Display(Name = "Position", ResourceType = typeof (Resources))]
        [DataMember]
        public Coordinates Position { get; set; }

        [DataMember]
        public virtual ICollection<ShipClass> ShipClasses { get; set; }

        [DataMember]
        public virtual User User { get; set; }

        private int GetTravelSpeed()
        {
            return ShipClasses != null && ShipClasses.Count > 0
                ? ShipClasses.OrderBy(x => x.TravelSpeed).Select(x => x.TravelSpeed).First()
                : 0;
        }

        private int GetRange()
        {
            return ShipClasses != null && ShipClasses.Count > 0
                ? ShipClasses.OrderBy(x => x.EngineRadius).Select(x => x.EngineRadius).First()
                : 0;
        }

        private int GetMoneyCost()
        {
            return ShipClasses != null && ShipClasses.Count > 0
                ? ShipClasses.Sum(x => x.MoneyCost)
                : 0;
        }

        private int GetMoneyMaintCost()
        {
            return ShipClasses != null && ShipClasses.Count > 0
                ? ShipClasses.Sum(x => x.MoneyMaintenanceCost)
                : 0;
        }

        private int GetOreCost()
        {
            return ShipClasses != null && ShipClasses.Count > 0
                ? ShipClasses.Sum(x => x.OreCost)
                : 0;
        }

        private int GetOreMaintCost()
        {
            return ShipClasses != null && ShipClasses.Count > 0
                ? ShipClasses.Sum(x => x.OreMaintenanceCost)
                : 0;
        }
    }
}