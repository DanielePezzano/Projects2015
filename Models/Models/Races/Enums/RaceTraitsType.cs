using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Models.Races.Enums
{
    [DataContract]
    public enum RaceTraitsType
    {
        [EnumMember]
        [Display(Name = "PhisycalTrait", ResourceType = typeof(Resources))]
        Phisycal,
        [EnumMember]
        [Display(Name = "CulturalTrait", ResourceType = typeof(Resources))]
        Cultural,
        [EnumMember]
        [Display(Name = "SocialTrait", ResourceType = typeof(Resources))]
        Social,
        [EnumMember]
        [Display(Name = "SpecialTrait", ResourceType = typeof(Resources))]
        Special
    }
}