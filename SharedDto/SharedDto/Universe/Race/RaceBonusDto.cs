using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Models.Races.Enums;

namespace SharedDto.Universe.Race
{
    [DataContract]
    public class RaceBonusDto
    {
        [EnumDataType(typeof(RaceTraitsBonuses))]
        [DataMember]
        public RaceTraitsBonuses Bonus { get; set; }
        [EnumDataType(typeof(RaceTraitsType))]
        [DataMember]
        public RaceTraitsType TraitType { get; set; }
        [DataMember]
        public int Value { get; set; }
    }
}