
using System.ComponentModel.DataAnnotations;
namespace Models.Races.Enums
{
    public enum RaceTraitsType
    {
        [Display(Name="PhisycalTrait", ResourceType = typeof(Resources))]
        Phisycal,
        [Display(Name = "CulturalTrait", ResourceType = typeof(Resources))]
        Cultural,
        [Display(Name = "SocialTrait", ResourceType = typeof(Resources))]
        Social,
        [Display(Name = "SpecialTrait", ResourceType = typeof(Resources))]
        Special
    }
}
