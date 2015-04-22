
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Models.Base.Enum
{
    [DataContract]
    public enum TaxLevel
    {
        [Display(Name = "GreatDonations", ResourceType = typeof(Resources))]
        [EnumMember]
        GreatDonations,
        [Display(Name = "SmallDonations", ResourceType = typeof(Resources))]
        [EnumMember]
        SmallDonations,
        [Display(Name = "Low", ResourceType = typeof(Resources))]
        [EnumMember]
        Low,
        [Display(Name = "Normal", ResourceType = typeof(Resources))]
        [EnumMember]
        Normal,
        [Display(Name = "High", ResourceType = typeof(Resources))]
        [EnumMember]
        Heavy,
        [Display(Name = "Super", ResourceType = typeof(Resources))]
        [EnumMember]
        SuperHeavy
    }
}