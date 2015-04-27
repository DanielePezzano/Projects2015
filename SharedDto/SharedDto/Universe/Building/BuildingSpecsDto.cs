using Models.Tech.Enum;
using System.Runtime.Serialization;

namespace SharedDto
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
