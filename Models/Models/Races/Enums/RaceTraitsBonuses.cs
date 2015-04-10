
using System.ComponentModel.DataAnnotations;
namespace Models.Races.Enums
{
    public enum RaceTraitsBonuses
    {
        [Display(Name="Armor", ResourceType = typeof(Resources))]
        Armor,
        [Display(Name = "Shield", ResourceType = typeof(Resources))]
        Shield,
        [Display(Name = "Structure", ResourceType = typeof(Resources))]
        Structure,
        [Display(Name = "Attack", ResourceType = typeof(Resources))]
        Attack,
        [Display(Name = "BonusToHit", ResourceType = typeof(Resources))]
        ToHit,
        [Display(Name = "RateOfFire", ResourceType = typeof(Resources))]
        Rof,
        [Display(Name = "MoneyMaintenanceCost", ResourceType = typeof(Resources))]
        MaintenanceMoney,
        [Display(Name = "OreMaintenanceCost", ResourceType = typeof(Resources))]
        MaintenanceOre,
        [Display(Name = "OreProduction", ResourceType = typeof(Resources))]
        OreProduction,
        [Display(Name = "MoneyTax", ResourceType = typeof(Resources))]
        MoneyTax,
        [Display(Name = "Social", ResourceType = typeof(Resources))]
        Social,
        [Display(Name = "PopulationGrowth", ResourceType = typeof(Resources))]
        Growth,
        [Display(Name = "Military", ResourceType = typeof(Resources))]
        Military,
        [Display(Name = "FoodProduction", ResourceType = typeof(Resources))]
        FoodProduction,
        [Display(Name = "FoodConsumption", ResourceType = typeof(Resources))]
        FoodConsumption,
        [Display(Name = "TimeToComplete", ResourceType = typeof(Resources))]
        TimeToBuild,
        [Display(Name = "ResearchProduction", ResourceType = typeof(Resources))]
        Research,
        [Display(Name = "Spy", ResourceType = typeof(Resources))]
        Spying
    }
}
