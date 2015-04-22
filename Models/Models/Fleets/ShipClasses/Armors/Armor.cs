using Models.Fleets.ShipClasses.Base;
using Models.Fleets.ShipClasses.Hulls;
using Models.Tech;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Fleets.ShipClasses.Armors
{
    [DataContract(IsReference=true)]
    public class Armor : PartShipEntity
    {
        [Display(Name = "Protection", ResourceType = typeof(Resources))]
        [DataMember]
        public int Protection { get; set; }
        [Display(Name = "PercCombatSpeedMalus", ResourceType = typeof(Resources))]
        [DataMember]
        public double PercCombatSpeedMalus { get; set; }
        [Display(Name = "PercTravelSpeedMalus", ResourceType = typeof(Resources))]
        [DataMember]
        public double PercTravelSpeedMalus { get; set; }
        [DataMember]
        public virtual Hull Hull { get; set; }
    }
}