using Models.Fleets.ShipClasses;
using Models.Fleets.ShipClasses.Base;
using Models.Universe.Strcut;
using Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Fleets
{
    public class Fleet : BaseShipEntity
    {
        [NotMapped()]
        [Display(Name = "TravelSpeed", ResourceType = typeof(Resources))]
        public int TravelSpeed { get { return GetTravelSpeed(); } }
        
        [NotMapped()]
        [Display(Name = "Range", Description = "RangeHint", ResourceType = typeof(Resources))]
        public int Range { get { return GetRange(); } }
        [Required()]
        [Display(Name = "AtBay", Description = "AtBayHint", ResourceType = typeof(Resources))]
        public bool AtBay { get; set; }

        [Display(Name = "MoneyCost", ResourceType = typeof(Resources))]
        [NotMapped()]
        public int MoneyCost { get { return GetMoneyCost(); } }
        [Display(Name = "OreCost", ResourceType = typeof(Resources))]
        [NotMapped()]
        public int OreCost { get { return GetOreCost(); } }
        [Display(Name = "MoneyMaintenanceCost", ResourceType = typeof(Resources))]
        [NotMapped()]
        public int MoneyMaintenanceCost { get { return GetMoneyMaintCost(); } }
        [Display(Name = "OreMaintenanceCost", ResourceType = typeof(Resources))]
        [NotMapped()]
        public int OreMaintenanceCost { get { return GetOreMaintCost(); } }
        [Required()]
        [Display(Name = "Position", ResourceType = typeof(Resources))]
        public Coordinates Position { get; set; }

        public virtual ICollection<ShipClass> ShipClasses { get; set; }
        public virtual User User { get; set; }

        private int GetTravelSpeed()
        {
            return (this.ShipClasses != null && this.ShipClasses.Count > 0) ?
                this.ShipClasses.OrderBy(x => x.TravelSpeed).Select(x => x.TravelSpeed).First() : 0;
        }

        private int GetRange()
        {
            return (this.ShipClasses != null && this.ShipClasses.Count > 0) ?
                this.ShipClasses.OrderBy(x => x.EngineRadius).Select(x => x.EngineRadius).First() : 0;
        }

        private int GetMoneyCost()
        {
            return (this.ShipClasses != null && this.ShipClasses.Count > 0) ?
                this.ShipClasses.Sum(x => x.MoneyCost) : 0;
        }

        private int GetMoneyMaintCost()
        {
            return (this.ShipClasses != null && this.ShipClasses.Count > 0) ?
                this.ShipClasses.Sum(x => x.MoneyMaintenanceCost) : 0;
        }

        private int GetOreCost()
        {
            return (this.ShipClasses != null && this.ShipClasses.Count > 0) ?
                this.ShipClasses.Sum(x => x.OreCost) : 0;
        }

        private int GetOreMaintCost()
        {
            return (this.ShipClasses != null && this.ShipClasses.Count > 0) ?
                this.ShipClasses.Sum(x => x.OreMaintenanceCost) : 0;
        }
    }
}
