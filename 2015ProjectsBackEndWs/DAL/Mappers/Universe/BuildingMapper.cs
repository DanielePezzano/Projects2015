using System;
using System.Collections.Generic;
using System.Linq;
using BaseModels;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Interfaces;
using DAL.Mappers.Universe.Enums;
using DAL.Mappers.Universe.IstanceFactory;
using DAL.Operations.Enums;
using DAL.Operations.IstanceFactory;
using Models.Buildings;
using SharedDto.Interfaces;
using SharedDto.Universe.Building;

namespace DAL.Mappers.Universe
{
    public class BuildingMapper : BaseMapper,  IMapToDto,IMapToEntity
    {

        public BuildingMapper(OpFactory operations):base(operations)
        {
            
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            var buildingDto = (BuildingDto) dto;

            Entity= new Building()
            {
                BuildingSpecs =((BuildingSpecsMapper)MapperFactory.RetrieveMapper(Operations,UniverseMapperTypes.BuildingSpecs)).ModelListToEntity(buildingDto.Details),
                BuildingType = buildingDto.BuildingType,
                Description = buildingDto.Description,
                Id = buildingDto.Id,
                MoneyCost = buildingDto.MoneyCost,
                MoneyMaintenanceCost = buildingDto.MoneyMaintenanceCost,
                Name = buildingDto.Name,
                Number = buildingDto.Number,
                OreCost = buildingDto.OreCost,
                OreMaintenanceCost = buildingDto.OreMaintenanceCost,
                SpaceNeeded =buildingDto.SpaceNeeded,
                UpdatedAt = DateTime.Now,
                UsedSpaces = buildingDto.UsedSpaces
            };

            return Entity;
        }

        public IDto MapToDto(BaseEntity entity)
        {
            var buildingEntity = (Building) entity;
            return new BuildingDto()
            {
                BuildingType = buildingEntity.BuildingType,
                Description = buildingEntity.Description,
                Details = ((BuildingSpecsMapper)MapperFactory.RetrieveMapper(Operations,UniverseMapperTypes.BuildingSpecs)).EntityListToModel(buildingEntity.BuildingSpecs),
                Id = buildingEntity.Id,
                MoneyCost = buildingEntity.MoneyCost,
                MoneyMaintenanceCost = buildingEntity.MoneyMaintenanceCost,
                Name = buildingEntity.Name,
                Number = buildingEntity.Number,
                OreCost = buildingEntity.OreCost,
                OreMaintenanceCost = buildingEntity.OreMaintenanceCost,
                SpaceNeeded = buildingEntity.SpaceNeeded,
                UsedSpaces = buildingEntity.UsedSpaces,
                PlanetId = buildingEntity.Planet.Id
            };
        }

        public override bool ExistsEntity()
        {
            var cacheKey = $"COUNTBUILDING{Entity.Id}";
             return
                Operations.SetOperation(MappedRepositories.BuildingRepository, MappedOperations.Any, cacheKey,
                    Entity.Id).CheckResult;
        }


        public List<BuildingDto> EntityListToModel(ICollection<Building> entityList)
        {
            return entityList?.Select(MapToDto).Select(dto => dto).Cast<BuildingDto>().ToList() ?? new List<BuildingDto>();
        }

        public List<Building> ModelListToEntity(List<BuildingDto> entityList)
        {
            return entityList?.Select(MapToEntity).Select(dto => dto).Cast<Building>().ToList() ?? new List<Building>();
        }
    }
}