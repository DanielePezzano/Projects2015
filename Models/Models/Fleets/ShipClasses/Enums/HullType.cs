
using System;
using System.ComponentModel.DataAnnotations;
namespace Models.Fleets.ShipClasses.Enums
{
    [Flags]
    public enum HullType
    {
        [Display(Name = "Bulk", ResourceType = typeof(Resources))]
        Bulk = 0,   //per trasporto merci
        [Display(Name = "Rooms", ResourceType = typeof(Resources))]
        Rooms = 1, //per trasporto persone/truppe
        [Display(Name = "Command", ResourceType = typeof(Resources))]
        Command = 2,
        [Display(Name = "EngineRoom", ResourceType = typeof(Resources))]
        EngineRoom = 3,
        [Display(Name = "Special", ResourceType = typeof(Resources))]
        Special = 4,
        [Display(Name = "Generic", ResourceType = typeof(Resources))]
        Generic = 5
    }
}
