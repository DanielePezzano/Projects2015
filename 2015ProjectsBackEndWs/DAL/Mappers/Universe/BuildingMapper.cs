using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Interfaces;
using DAL.Mappers.Universe.IstanceFactory;
using DAL.Operations;
using DAL.Operations.BaseClasses;
using Models.Base;
using Models.Buildings;
using SharedDto.Interfaces;
using SharedDto.Universe.Building;

namespace DAL.Mappers.Universe
{
    public class BuildingMapper : BaseMapper, IMapper
    {

        public BuildingMapper(string connectionString,BaseOperations operations,bool isTest=false):base(isTest,connectionString,operations)
        {
            
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            var buildingDto = (BuildingDto) dto;

            Entity= new Building()
            {
                //BuildingSpecs 
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
                Details = MapperFactory.RetrieveBuildingSpecsMapper(ConnectionString,Operations,IsTest).EntityListToModel(buildingEntity.BuildingSpecs),
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
            return Operations.Any(MappedRepositories.BuildingRepository, Entity.Id, cacheKey);
        }


        public List<BuildingDto> EntityListToModel(ICollection<Building> entityList)
        {
            return entityList.Select(MapToDto).Select(dto => dto).Cast<BuildingDto>().ToList();
        }

        public List<Building> ModelListToEntity(List<BuildingDto> entityList)
        {
            return entityList.Select(MapToEntity).Select(dto => dto).Cast<Building>().ToList();
        }
    }
}