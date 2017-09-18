using System;
using System.Collections.Generic;
using System.Linq;
using BaseModels;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Interfaces;
using DAL.Operations.Enums;
using DAL.Operations.IstanceFactory;
using Models.Buildings;
using SharedDto.Interfaces;
using SharedDto.Universe.Building;
namespace DAL.Mappers.Universe
{
    public class BuildingSpecsMapper : BaseMapper,  IMapToDto,IMapToEntity
    {
        public BuildingSpecsMapper(OpFactory operations):base(operations)
        {
        }

        public override bool ExistsEntity()
        {
            var cacheKey = $"COUNTBUILDINGSPECS{Entity.Id}";
            return
                Operations.SetOperation(MappedRepositories.BuildingSpecRepository, MappedOperations.Any, cacheKey,
                    Entity.Id).CheckResult;
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
            return entityList?.Select(MapToDto).Select(dto => dto).Cast<BuildingSpecsDto>().ToList() ?? new List<BuildingSpecsDto>();
        }

        public List<BuildingSpec> ModelListToEntity(List<BuildingSpecsDto> entityList)
        {
            return entityList?.Select(MapToEntity).Select(dto => dto).Cast<BuildingSpec>().ToList() ?? new List<BuildingSpec>();
        }
    }
}