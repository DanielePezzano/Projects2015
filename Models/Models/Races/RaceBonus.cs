using Models.Base;
using Models.Races.Enums;
using Models.Users;
using System.ComponentModel.DataAnnotations;

namespace Models.Races
{
    public class RaceBonus : BaseEntity
    {
        [Display(Name = "RaceBonus", ResourceType = typeof(Resources))]
        [EnumDataType(typeof(RaceTraitsBonuses))]
        public RaceTraitsBonuses Bonus { get; set; }
        [Display(Name = "TraitType", ResourceType = typeof(Resources))]
        [EnumDataType(typeof(RaceTraitsType))]
        public RaceTraitsType TraitType { get; set; }

        public virtual User Race { get; set; }
    }
}
