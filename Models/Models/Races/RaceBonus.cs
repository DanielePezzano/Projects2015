using Models.Base;
using Models.Races.Enums;
using Models.Users;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Races
{
    [DataContract(IsReference=true)]
    public class RaceBonus : BaseEntity
    {
        [Display(Name = "RaceBonus", ResourceType = typeof(Resources))]
        [EnumDataType(typeof(RaceTraitsBonuses))]
        [DataMember]
        public RaceTraitsBonuses Bonus { get; set; }
        [Display(Name = "TraitType", ResourceType = typeof(Resources))]
        [EnumDataType(typeof(RaceTraitsType))]
        [DataMember]
        public RaceTraitsType TraitType { get; set; }
        [DataMember]
        public int Value { get; set; }
        [DataMember]
        public virtual User Race { get; set; }
    }
}