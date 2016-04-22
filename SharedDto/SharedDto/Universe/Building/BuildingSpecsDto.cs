using System.Runtime.Serialization;
using Models.Tech.Enum;
using SharedDto.Interfaces;

namespace SharedDto.Universe.Building
{
    [DataContract]
    public class BuildingSpecsDto:IDto
    {
        [DataMember]
        public BonusType Bonus { get; set; }
        [DataMember]
        public int Value { get; set; }

    }
}
