using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Interfaces;
using DAL.Operations;
using DAL.Operations.BaseClasses;
using Models.Base;
using Models.Buildings;
using SharedDto.Interfaces;
using SharedDto.Universe.Building;
namespace DAL.Mappers.Universe
{
    public class BuildingSpecsMapper : BaseMapper, IMapper
    {
        public BuildingSpecsMapper(string connectionString,BaseOperations operations,bool isTest=false):base(isTest,connectionString,operations)
        {
        }

        public override bool ExistsEntity()
        {
            var cacheKey = $"COUNTBUILDINGSPECS{Entity.Id}";
            return Operations.Any(MappedRepositories.BuildingSpecRepository, Entity.Id, cacheKey);
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            var specsDto = (BuildingSpecsDto) dto;

            Entity = new BuildingSpec()
            {
                Bonus = specsDto.Bonus,
                Value = specsDto.Value,
                UpdatedAt = DateTime.Now
            };

            return Entity;
        }

        public IDto MapToDto(BaseEntity entity)
        {
            var specsEntity = (BuildingSpec) entity;
            return new BuildingSpecsDto()
            {
                Bonus = specsEntity.Bonus,
                Value = specsEntity.Value
            };
        }

        public List<BuildingSpecsDto> EntityListToModel(ICollection<BuildingSpec> entityList)
        {
            return entityList.Select(MapToDto).Select(dto => dto).Cast<BuildingSpecsDto>().ToList();
        }

        public List<BuildingSpec> ModelListToEntity(List<BuildingSpecsDto> entityList)
        {
            return entityList.Select(MapToEntity).Select(dto => dto).Cast<BuildingSpec>().ToList();
        }
    }
}