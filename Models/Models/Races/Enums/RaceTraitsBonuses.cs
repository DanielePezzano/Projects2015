using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Models.Races.Enums
{
    [DataContract]
    public enum RaceTraitsBonuses
    {
        [Display(Name = "Armor", ResourceType = typeof(Resources))]
        [EnumMember]
        Armor,
        [Display(Name = "Shield", ResourceType = typeof(Resources))]
        [EnumMember]
        Shield,
        [Display(Name = "Structure", ResourceType = typeof(Resources))]
        [EnumMember]
        Structure,
        [Display(Name = "Attack", ResourceType = typeof(Resources))]
        [EnumMember]
        Attack,
        [Display(Name = "BonusToHit", ResourceType = typeof(Resources))]
        [EnumMember]
        ToHit,
        [Display(Name = "RateOfFire", ResourceType = typeof(Resources))]
        [EnumMember]
        Rof,
        [Display(Name = "MoneyMaintenanceCost", ResourceType = typeof(Resources))]
        [EnumMember]
        MaintenanceMoney,
        [Display(Name = "OreMaintenanceCost", ResourceType = typeof(Resources))]
        [EnumMember]
        MaintenanceOre,
        [Display(Name = "OreProduction", ResourceType = typeof(Resources))]
        [EnumMember]
        OreProduction,
        [Display(Name = "MoneyTax", ResourceType = typeof(Resources))]
        [EnumMember]
        MoneyTax,
        [Display(Name = "Social", ResourceType = typeof(Resources))]
        [EnumMember]
        Social,
        [Display(Name = "PopulationGrowth", ResourceType = typeof(Resources))]
        [EnumMember]
        Growth,
        [Display(Name = "Military", ResourceType = typeof(Resources))]
        [EnumMember]
        Military,
        [Display(Name = "FoodProduction", ResourceType = typeof(Resources))]
        [EnumMember]
        FoodProduction,
        [Display(Name = "FoodConsumption", ResourceType = typeof(Resources))]
        [EnumMember]
        FoodConsumption,
        [Display(Name = "TimeToComplete", ResourceType = typeof(Resources))]
        [EnumMember]
        TimeToBuild,
        [Display(Name = "ResearchProduction", ResourceType = typeof(Resources))]
        [EnumMember]
        Research,
        [Display(Name = "Spy", ResourceType = typeof(Resources))]
        [EnumMember]
        Spying
    }
}