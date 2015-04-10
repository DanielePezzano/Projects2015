using Models.Fleets.ShipClasses.Base;
using Models.Fleets.ShipClasses.Hulls;
using System.ComponentModel.DataAnnotations;

namespace Models.Fleets.ShipClasses.Engines
{
    public class Engine : PartShipEntity
    {
        [Display(Name = "CombatSpeed", ResourceType = typeof(Resources))]
        public int CombatSpeed { get; set; }
        [Display(Name = "TravelSpeed", ResourceType = typeof(Resources))]
        public int TravelSpeed { get; set; }
        [Display(Name = "GeneratedEnergy", ResourceType = typeof(Resources))]
        public int GeneratedEnergy { get; set; }

        public virtual Hull Hull { get; set; }
    }
}
