using Models.Base;
using Models.Tech;
using System.ComponentModel.DataAnnotations;

namespace Models.Fleets.ShipClasses.Base
{
    public class PartShipEntity : BaseShipEntity
    {
        [Display(Name = "OreCost", ResourceType = typeof(Resources))]
        public int OreCost { get; set; }
        [Display(Name = "MoneyCost", ResourceType = typeof(Resources))]
        public int MoneyCost { get; set; }
        [Display(Name = "OreMaintenanceCost", ResourceType = typeof(Resources))]
        public int OreMaintenanceCost { get; set; }
        [Display(Name = "MoneyMaintenanceCost", ResourceType = typeof(Resources))]
        public int MoneyMaintenanceCost { get; set; }

        [Display(Name = "SpacesNeeded", ResourceType = typeof(Resources))]
        public int SpacesNeeded { get; set; }

        public virtual Technology Techonology { get; set; }
    }
}
