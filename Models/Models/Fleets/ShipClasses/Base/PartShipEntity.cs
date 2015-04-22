using Models.Base;
using Models.Tech;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Fleets.ShipClasses.Base
{
    [DataContract]
    public class PartShipEntity : BaseShipEntity
    {
        [Display(Name = "OreCost", ResourceType = typeof(Resources))]
        [DataMember]
        public int OreCost { get; set; }
        [Display(Name = "MoneyCost", ResourceType = typeof(Resources))]
        [DataMember]
        public int MoneyCost { get; set; }
        [Display(Name = "OreMaintenanceCost", ResourceType = typeof(Resources))]
        [DataMember]
        public int OreMaintenanceCost { get; set; }
        [Display(Name = "MoneyMaintenanceCost", ResourceType = typeof(Resources))]
        [DataMember]
        public int MoneyMaintenanceCost { get; set; }
        [DataMember]
        [Display(Name = "SpacesNeeded", ResourceType = typeof(Resources))]
        public int SpacesNeeded { get; set; }
        [DataMember]
        public virtual Technology Techonology { get; set; }
    }
}