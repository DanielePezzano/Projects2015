using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Models.Queues.Enum
{
    [DataContract]
    public enum MissionType
    {
        [Display(Name = "Movement", ResourceType = typeof(Resources))]
        [EnumMember]
        Movement,
        [Display(Name = "Colonization", ResourceType = typeof(Resources))]
        [EnumMember]
        Colonization,
        [Display(Name = "Invasion", ResourceType = typeof(Resources))]
        [EnumMember]
        Invasion,
        [Display(Name = "Raid", ResourceType = typeof(Resources))]
        [EnumMember]
        Raid,
        [Display(Name = "Transport", ResourceType = typeof(Resources))]
        [EnumMember]
        Transport,
        [Display(Name = "Bombing", ResourceType = typeof(Resources))]
        [EnumMember]
        Bombing,
        [Display(Name = "Spy", ResourceType = typeof(Resources))]
        [EnumMember]
        Spy,
        [Display(Name = "Attack", ResourceType = typeof(Resources))]
        [EnumMember]
        Attack
    }
}