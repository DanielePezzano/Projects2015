using System.Runtime.Serialization;
using Models.Tech.Enum;

namespace SharedDto.Universe.Building
{
    [DataContract]
    public class BuildingSpecsDto
    {
        [DataMember]
        public BonusType Bonus { get; set; }
        [DataMember]
        public int Value { get; set; }

    }
}
