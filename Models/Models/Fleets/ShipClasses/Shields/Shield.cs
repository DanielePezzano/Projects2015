using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Models.Fleets.ShipClasses.Base;
using Models.Fleets.ShipClasses.Hulls;

namespace Models.Fleets.ShipClasses.Shields
{
    [DataContract(IsReference = true)]
    public class Shield : PartShipEntity
    {
        [Display(Name = "Protection", ResourceType = typeof (Resources))]
        [DataMember]
        public int Protection { get; set; }

        [Display(Name = "RequiredEnergy", ResourceType = typeof (Resources))]
        [DataMember]
        public int RequiredEnergy { get; set; }

        [DataMember]
        public virtual Hull Hull { get; set; }
    }
}