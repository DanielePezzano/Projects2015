using Models.Buildings;
using Models.Tech.Enum;
using System.Runtime.Serialization;

namespace _2015ProjectsBackEndWs.DTO.Universe
{
    [DataContract]
    public class BuildingSpecsDto : BaseDto<BuildingSpec>
    {
        [DataMember]
        public BonusType Bonus { get { return Model.Bonus; } set { Model.Bonus = value; } }
        [DataMember]
        public int Value { get { return Model.Value; } set { Model.Value = value; } }

        public BuildingSpecsDto(BuildingSpec model):base(model)
        {

        }
    }
}
