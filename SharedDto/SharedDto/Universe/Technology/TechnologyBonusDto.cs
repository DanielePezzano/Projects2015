using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Models.Tech.Enum;
using SharedDto.BaseClasses;
using SharedDto.Interfaces;

namespace SharedDto.Universe.Technology
{
    [DataContract]
    public class TechnologyBonusDto : BaseDto,IDto
    {
        [DataMember]
        [EnumDataType(typeof(BonusType))]
        public BonusType Bonus { get; set; }
        [DataMember]
        public int Value { get; set; }
    }
}
