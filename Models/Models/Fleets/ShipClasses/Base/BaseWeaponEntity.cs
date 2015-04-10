
using System.ComponentModel.DataAnnotations;
namespace Models.Fleets.ShipClasses.Base
{
    public class BaseWeaponEntity : PartShipEntity
    {
        [Display(Name = "RateOfFire", Description = "RateOfFireHint", ResourceType = typeof(Resources))]
        public int RateOfFire { get; set; } //quante volte spara in un round
        [Display(Name = "Damage", ResourceType = typeof(Resources))]
        public int Damage { get; set; }
        [Display(Name = "BonusToHit", ResourceType = typeof(Resources))]
        public int BonusToHit { get; set; }
    }
}
