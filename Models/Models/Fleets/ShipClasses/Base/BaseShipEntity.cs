using Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Models.Fleets.ShipClasses.Base
{
    public class BaseShipEntity : BaseEntity
    {
        [Display(Name = "Name", ResourceType = typeof(Resources))]
        public string Name { get; set; }
        [Display(Name = "Description", ResourceType = typeof(Resources))]
        public string Description { get; set; }        
    }
}
