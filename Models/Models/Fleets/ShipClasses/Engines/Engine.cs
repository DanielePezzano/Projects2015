using Models.Fleets.ShipClasses.Base;
using Models.Fleets.ShipClasses.Hulls;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Fleets.ShipClasses.Engines
{
    [DataContract(IsReference=true)]
    public class Engine : PartShipEntity
    {
        [Display(Name = "CombatSpeed", ResourceType = typeof(Resources))]
        [DataMember]
        public int CombatSpeed { get; set; }
        [Display(Name = "TravelSpeed", ResourceType = typeof(Resources))]
        [DataMember]
        public int TravelSpeed { get; set; }
        [Display(Name = "GeneratedEnergy", ResourceType = typeof(Resources))]
        [DataMember]
        public int GeneratedEnergy { get; set; }
        [DataMember]
        public virtual Hull Hull { get; set; }
    }
}