using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Models.Fleets.ShipClasses.Base
{
    [DataContract]
    public class BaseWeaponEntity : PartShipEntity
    {
        [Display(Name = "RateOfFire", Description = "RateOfFireHint", ResourceType = typeof(Resources))]
        [DataMember]
        public int RateOfFire { get; set; } //quante volte spara in un round
        [Display(Name = "Damage", ResourceType = typeof(Resources))]
        [DataMember]
        public int Damage { get; set; }
        [Display(Name = "BonusToHit", ResourceType = typeof(Resources))]
        [DataMember]
        public int BonusToHit { get; set; }
    }
}