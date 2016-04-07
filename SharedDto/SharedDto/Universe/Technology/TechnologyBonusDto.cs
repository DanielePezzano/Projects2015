using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Models.Tech.Enum;

namespace SharedDto.Universe.Technology
{
    [DataContract]
    public class TechnologyBonusDto
    {
        [DataMember]
        [EnumDataType(typeof(BonusType))]
        public BonusType Bonus { get; set; }
        [DataMember]
        public int Value { get; set; }
    }
}
