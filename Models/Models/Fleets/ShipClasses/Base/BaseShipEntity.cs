using Models.Fleets.ShipClasses.Hulls;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using BaseModels;

namespace Models.Fleets.ShipClasses.Base
{
    [DataContract(IsReference=true)]
    [KnownType(typeof(Fleet))]
    [KnownType(typeof(PartShipEntity))]
    [KnownType(typeof(Hull))]
    [KnownType(typeof(ShipClass))]
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