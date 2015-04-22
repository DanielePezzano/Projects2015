using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Models.Fleets.ShipClasses.Enums
{
    [Flags]
    [DataContract]
    public enum SystemType
    {
        [EnumMember]
        [Display(Name = "ECM", ResourceType = typeof(Resources))]
        ECM = 0,
        [Display(Name = "ECCM", ResourceType = typeof(Resources))]
        [EnumMember]
        ECCM = 1,
        [Display(Name = "EnergyCell", ResourceType = typeof(Resources))]
        [EnumMember]
        EnergyCell = 2,
        [Display(Name = "AimingSystem", ResourceType = typeof(Resources))]
        [EnumMember]
        AimingSystem = 3,
        [Display(Name = "FlightComputer", ResourceType = typeof(Resources))]
        [EnumMember]
        FlightComputer = 4,
        [Display(Name = "AimingComputer", ResourceType = typeof(Resources))]
        [EnumMember]
        AimingComputer = 5,
        [Display(Name = "Generic", ResourceType = typeof(Resources))]
        [EnumMember]
        Generic = 6,
        [Display(Name = "Scanners", ResourceType = typeof(Resources))]
        [EnumMember]
        Scanners = 7
    }
}