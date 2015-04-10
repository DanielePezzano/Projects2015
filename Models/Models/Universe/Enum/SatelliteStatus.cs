using System;
using System.ComponentModel.DataAnnotations;
namespace Models.Universe.Enum
{
    [Flags]
    public enum SatelliteStatus
    {
        [Display(Name = "Uncolonizable", ResourceType = typeof(Resources))]
        Uncolonizable,
        [Display(Name = "Uncolonized", ResourceType = typeof(Resources))]
        Uncolonized,
        [Display(Name = "Colonized", ResourceType = typeof(Resources))]
        Colonized,
        [Display(Name = "Blocked", ResourceType = typeof(Resources))]
        Blocked,
        [Display(Name = "Starvation", ResourceType = typeof(Resources))]
        Starvation,
        [Display(Name = "Revolt", ResourceType = typeof(Resources))]
        Revolt,
        [Display(Name = "Optimum", ResourceType = typeof(Resources))]
        Optimum,
        [Display(Name = "Abandoned", ResourceType = typeof(Resources))]
        Abandoned
    }
}
