using Models.Base;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Fleets.ShipClasses.Base
{
    [DataContract]
    public class BaseShipEntity : BaseEntity
    {
        [Display(Name = "Name", ResourceType = typeof(Resources))]
        [DataMember]
        public string Name { get; set; }
        [Display(Name = "Description", ResourceType = typeof(Resources))]
        [DataMember]
        public string Description { get; set; }
    }
}