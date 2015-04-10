
using System.ComponentModel.DataAnnotations;
namespace Models.Queues.Enum
{
    public enum MissionType
    {
        [Display(Name = "Movement", ResourceType = typeof(Resources))]
        Movement,
        [Display(Name = "Colonization", ResourceType = typeof(Resources))]
        Colonization,
        [Display(Name = "Invasion", ResourceType = typeof(Resources))]
        Invasion,
        [Display(Name = "Raid", ResourceType = typeof(Resources))]
        Raid,
        [Display(Name = "Transport", ResourceType = typeof(Resources))]
        Transport,
        [Display(Name = "Bombing", ResourceType = typeof(Resources))]
        Bombing,
        [Display(Name = "Spy", ResourceType = typeof(Resources))]
        Spy,
        [Display(Name = "Attack", ResourceType = typeof(Resources))]
        Attack
    }
}
