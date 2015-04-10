
using System;
using System.ComponentModel.DataAnnotations;
namespace Models.Fleets.ShipClasses.Enums
{
    [Flags]
    public enum SystemType
    {
        [Display(Name="ECM",ResourceType= typeof(Resources))]
        ECM = 0,
        [Display(Name = "ECCM", ResourceType = typeof(Resources))]
        ECCM = 1,
        [Display(Name = "EnergyCell", ResourceType = typeof(Resources))]
        EnergyCell = 2,
        [Display(Name = "AimingSystem", ResourceType = typeof(Resources))]
        AimingSystem = 3,
        [Display(Name = "FlightComputer", ResourceType = typeof(Resources))]
        FlightComputer = 4,
        [Display(Name = "AimingComputer", ResourceType = typeof(Resources))]
        AimingComputer = 5,
        [Display(Name = "Generic", ResourceType = typeof(Resources))]
        Generic = 6,
        [Display(Name = "Scanners", ResourceType = typeof(Resources))]
        Scanners = 7
    }
}
