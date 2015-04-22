using _2015ProjectsBackEndWs.DataMapper;
using Models.Buildings;
using Models.Buildings.Enums;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace _2015ProjectsBackEndWs.DTO.Universe
{
    [DataContract]
    public class BuildingDto : BaseDto<Building>
    {        
        [DataMember]
        public int Id { get { return Model.Id; } set { Model.Id = value; } }
        [DataMember]
        public BuildingType BuildingType { get { return Model.BuildingType; } set { Model.BuildingType = value; } }
        [DataMember]
        public string Description { get { return Model.Description; } set { Model.Description = value; } }
        [DataMember]
        public int MoneyCost { get { return Model.MoneyCost; } set { Model.MoneyCost = value; } }
        [DataMember]
        public int MoneyMaintenanceCost { get { return Model.MoneyMaintenanceCost; } set { Model.MoneyMaintenanceCost = value; } }
        [DataMember]
        public string Name { get { return Model.Name; } set { Model.Name = value; } }
        [DataMember]
        public int Number { get { return Model.Number; } set { Model.Number = value; } }
        [DataMember]
        public int OreCost { get { return Model.OreCost; } set { Model.OreCost = value; } }
        [DataMember]
        public int OreMaintenanceCost { get { return Model.OreMaintenanceCost; } set { Model.OreMaintenanceCost = value; } }
        [DataMember]
        public int SpaceNeeded { get { return Model.SpaceNeeded; } set { Model.SpaceNeeded = value; } }
        [DataMember]
        public int UsedSpaces { get { return Model.UsedSpaces; } set { Model.UsedSpaces = value; } }
        [DataMember]
        public List<BuildingSpecsDto> Details { get; set; }

        public BuildingDto(Building model)
            : base(model)
        {
            BuildingSpecEntityMapper mapper = new BuildingSpecEntityMapper();
            this.Details = mapper.EntityListToModel((List<BuildingSpec>)model.BuildingSpecs);
        }
    }
}
