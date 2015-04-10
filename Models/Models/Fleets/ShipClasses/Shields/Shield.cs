using Models.Fleets.ShipClasses.Base;
using Models.Fleets.ShipClasses.Hulls;
using Models.Tech;
using System.ComponentModel.DataAnnotations;

namespace Models.Fleets.ShipClasses.Shields
{
    public class Shield: PartShipEntity
    {
        [Display(Name = "Protection", ResourceType = typeof(Resources))]
        public int Protection { get; set; }
        [Display(Name = "RequiredEnergy", ResourceType = typeof(Resources))]
        public int RequiredEnergy { get; set; }

        public virtual Hull Hull { get; set; }
    }
}
