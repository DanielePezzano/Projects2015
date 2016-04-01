using Models.Buildings;
using Models.Buildings.Enums;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Base
{
    [DataContract(IsReference=true)]
    [KnownType(typeof(Building))]
    public class BaseBuildingEntity : BaseEntity
    {
        [Display(Name = "BuildingType", ResourceType = typeof(Resources))]
        [DataMember]
        public BuildingType BuildingType { get; set; }
        [Display(Name = "Name", ResourceType = typeof(Resources))]
        [Required]
        [DataMember]
        public string Name { get; set; }
        [Display(Name = "Description", ResourceType = typeof(Resources))]
        [Required]
        [DataMember]
        public string Description { get; set; }
        [Display(Name = "SpacesNeeded", ResourceType = typeof(Resources))]
        [Required]
        [DataMember]
        public int SpaceNeeded { get; set; }
        [Display(Name = "UsedSpaces", ResourceType = typeof(Resources))]
        [DataMember]
        public int UsedSpaces { get; set; }
        [Display(Name = "Number", ResourceType = typeof(Resources))]
        [Required]
        [DataMember]
        public int Number { get; set; }
        [Display(Name = "OreCost", ResourceType = typeof(Resources))]
        [Required]
        [DataMember]
        public int OreCost { get; set; }
        [Display(Name = "MoneyCost", ResourceType = typeof(Resources))]
        [Required]
        [DataMember]
        public int MoneyCost { get; set; }
        [Display(Name = "OreMaintenanceCost", ResourceType = typeof(Resources))]
        [DataMember]
        public int OreMaintenanceCost { get; set; }
        [Display(Name = "MoneyMaintenanceCost", ResourceType = typeof(Resources))]
        [DataMember]
        public int MoneyMaintenanceCost { get; set; }
    }
}
