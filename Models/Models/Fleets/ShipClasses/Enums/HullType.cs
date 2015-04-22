using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Models.Fleets.ShipClasses.Enums
{
    [Flags]
    [DataContract]
    public enum HullType
    {
        [Display(Name = "Bulk", ResourceType = typeof(Resources))]
        [EnumMember]
        Bulk = 0,   //per trasporto merci
        [Display(Name = "Rooms", ResourceType = typeof(Resources))]
        [EnumMember]
        Rooms = 1, //per trasporto persone/truppe
        [Display(Name = "Command", ResourceType = typeof(Resources))]
        [EnumMember]
        Command = 2,
        [Display(Name = "EngineRoom", ResourceType = typeof(Resources))]
        [EnumMember]
        EngineRoom = 3,
        [Display(Name = "Special", ResourceType = typeof(Resources))]
        [EnumMember]
        Special = 4,
        [Display(Name = "Generic", ResourceType = typeof(Resources))]
        [EnumMember]
        Generic = 5
    }
}