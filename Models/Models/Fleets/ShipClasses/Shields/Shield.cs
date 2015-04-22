using Models.Fleets.ShipClasses.Base;
using Models.Fleets.ShipClasses.Hulls;
using Models.Tech;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Fleets.ShipClasses.Shields
{
    [DataContract]
    public class Shield : PartShipEntity
    {
        [Display(Name = "Protection", ResourceType = typeof(Resources))]
        [DataMember]
        public int Protection { get; set; }
        [Display(Name = "RequiredEnergy", ResourceType = typeof(Resources))]
        [DataMember]
        public int RequiredEnergy { get; set; }
        [DataMember]
        public virtual Hull Hull { get; set; }
    }
}