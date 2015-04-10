
using System.ComponentModel.DataAnnotations;
namespace Models.Base.Enum
{
    public enum TaxLevel
    {
        [Display(Name = "GreatDonations", ResourceType = typeof(Resources))]
        GreatDonations,
        [Display(Name = "SmallDonations", ResourceType = typeof(Resources))]
        SmallDonations,
        [Display(Name = "Low", ResourceType = typeof(Resources))]
        Low,
        [Display(Name = "Normal", ResourceType = typeof(Resources))]
        Normal,
        [Display(Name = "High", ResourceType = typeof(Resources))]
        Heavy,
        [Display(Name = "Super", ResourceType = typeof(Resources))]
        SuperHeavy
    }
}
