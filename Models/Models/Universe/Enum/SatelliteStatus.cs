using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Models.Universe.Enum
{
    [Flags]
    [DataContract]
    public enum SatelliteStatus
    {
        [Display(Name = "Uncolonizable", ResourceType = typeof(Resources))]
        [EnumMember]
        Uncolonizable,
        [Display(Name = "Uncolonized", ResourceType = typeof(Resources))]
        [EnumMember]
        Uncolonized,
        [Display(Name = "Colonized", ResourceType = typeof(Resources))]
        [EnumMember]
        Colonized,
        [Display(Name = "Blocked", ResourceType = typeof(Resources))]
        [EnumMember]
        Blocked,
        [Display(Name = "Starvation", ResourceType = typeof(Resources))]
        [EnumMember]
        Starvation,
        [Display(Name = "Revolt", ResourceType = typeof(Resources))]
        [EnumMember]
        Revolt,
        [Display(Name = "Optimum", ResourceType = typeof(Resources))]
        [EnumMember]
        Optimum,
        [Display(Name = "Abandoned", ResourceType = typeof(Resources))]
        [EnumMember]
        Abandoned
    }
}