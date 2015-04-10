using Models.Fleets.ShipClasses.Base;
using Models.Fleets.ShipClasses.Hulls;
using Models.Tech;
using System.ComponentModel.DataAnnotations;

namespace Models.Fleets.ShipClasses.Armors
{
    public class Armor : PartShipEntity
    {
        [Display(Name="Protection", ResourceType=typeof(Resources))]
        public int Protection { get; set; }
        [Display(Name = "PercCombatSpeedMalus", ResourceType = typeof(Resources))]
        public double PercCombatSpeedMalus { get; set; }
        [Display(Name = "PercTravelSpeedMalus", ResourceType = typeof(Resources))]
        public double PercTravelSpeedMalus { get; set; }

        public virtual Hull Hull { get; set; }
    }
}
