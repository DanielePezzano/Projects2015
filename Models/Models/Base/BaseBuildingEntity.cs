using Models.Buildings.Enums;
using System.ComponentModel.DataAnnotations;

namespace Models.Base
{
    public class BaseBuildingEntity:BaseEntity
    {
        [Display(Name = "BuildingType", ResourceType = typeof(Resources))]
        public BuildingType BuildingType { get; set; }
        [Display(Name = "Name", ResourceType = typeof(Resources))]
        [Required()]
        public string Name { get; set; }
        [Display(Name = "Description", ResourceType = typeof(Resources))]
        [Required()]
        public string Description { get; set; }
        [Display(Name="SpacesNeeded",ResourceType=typeof(Resources))]
        [Required()]
        public int SpaceNeeded { get; set; }
        [Display(Name = "UsedSpaces", ResourceType = typeof(Resources))]
        public int UsedSpaces { get; set; }
        [Display(Name = "Number", ResourceType = typeof(Resources))]
        [Required()]
        public int Number { get; set; }
        [Display(Name = "OreCost", ResourceType = typeof(Resources))]
        [Required()]
        public int OreCost { get; set; }
        [Display(Name = "MoneyCost", ResourceType = typeof(Resources))]
        [Required()]
        public int MoneyCost { get; set; }
        [Display(Name = "OreMaintenanceCost", ResourceType = typeof(Resources))]
        public int OreMaintenanceCost { get; set; }
        [Display(Name = "MoneyMaintenanceCost", ResourceType = typeof(Resources))]
        public int MoneyMaintenanceCost { get; set; }
    }
}
