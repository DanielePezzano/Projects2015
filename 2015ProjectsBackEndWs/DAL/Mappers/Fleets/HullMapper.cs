using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Fleets.Enums;
using DAL.Mappers.Fleets.IstanceFactory;
using DAL.Mappers.Interfaces;
using DAL.Operations;
using DAL.Operations.BaseClasses;
using Models.Base;
using Models.Fleets.ShipClasses.Enums;
using Models.Fleets.ShipClasses.Hulls;
using SharedDto.Interfaces;
using SharedDto.Universe.Fleet;

namespace DAL.Mappers.Fleets
{
    public class HullMapper : BaseMapper, IMapper
    {
        public HullMapper(bool isTest, string connectionString, BaseOperations operations) : base(isTest, connectionString, operations)
        {
        }

        public override bool ExistsEntity()
        {
           var cacheKey = $"HULL{Entity.Id}";
            return Operations.Any(MappedRepositories.HullRepository, Entity.Id, cacheKey);
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            var hullDto = (HullDto) dto;
            Entity = new Hull()
            {
                Id = hullDto.Id,
                Name = hullDto.Name,
                Description = hullDto.Description,
                AntiPlanetWeapons = ((AntiPlanetWeaponMapper)FleetMapperFactory.RetrieveMapper(IsTest,ConnectionString,Operations,FleetMapperTypes.AntiplanetWeapons)).ModelListToEntity(hullDto.AntiPlanetWeaponDtos),
                AntiShipWeapons = ((AntiShipWeaponMapper)FleetMapperFactory.RetrieveMapper(IsTest,ConnectionString,Operations,FleetMapperTypes.AntishipWeapons)).ModelListToEntity(hullDto.AntishioShipWeaponDtos),
                Armors =((ArmorMapper)FleetMapperFactory.RetrieveMapper(IsTest,ConnectionString,Operations,FleetMapperTypes.Armor)).ModelListToEntity(hullDto.ArmorDtos),
                Engines = ((EngineMapper)FleetMapperFactory.RetrieveMapper(IsTest,ConnectionString,Operations,FleetMapperTypes.Engine)).ModelListToEntity(hullDto.EngineDtos),
                HullType = (HullType)Enum.Parse(typeof(HullType),hullDto.HullType),
                Shields = ((ShieldMapper)FleetMapperFactory.RetrieveMapper(IsTest,ConnectionString,Operations,FleetMapperTypes.Shield)).ModelListToEntity(hullDto.ShieldDtos),
                StructurePoints = hullDto.StructurePoints,
                SubSystems = ((SystemsMapper)FleetMapperFactory.RetrieveMapper(IsTest,ConnectionString,Operations,FleetMapperTypes.Systems)).ModelListToEntity(hullDto.SystemsDtos),
                TotalSpaces = hullDto.TotalSpaces
            };
            return Entity;
        }

        public IDto MapToDto(BaseEntity entity)
        {
            var hullEntity = (Hull) entity;
            return new HullDto()
            {
                Id = hullEntity.Id,
                Name = hullEntity.Name,
                Description = hullEntity.Description,
                AntiPlanetWeaponDtos = ((AntiPlanetWeaponMapper)FleetMapperFactory.RetrieveMapper(IsTest,ConnectionString,Operations,FleetMapperTypes.AntiplanetWeapons)).EntityListToModel(hullEntity.AntiPlanetWeapons),
                AntishioShipWeaponDtos = ((AntiShipWeaponMapper)FleetMapperFactory.RetrieveMapper(IsTest,ConnectionString,Operations,FleetMapperTypes.AntishipWeapons)).EntityListToModel(hullEntity.AntiShipWeapons),
                ArmorDtos = ((ArmorMapper)FleetMapperFactory.RetrieveMapper(IsTest,ConnectionString,Operations,FleetMapperTypes.Armor)).EntityListToModel(hullEntity.Armors),
                EngineDtos = ((EngineMapper)FleetMapperFactory.RetrieveMapper(IsTest,ConnectionString,Operations,FleetMapperTypes.Engine)).EntityListToModel(hullEntity.Engines),
                HullType = hullEntity.ToString(),
                ShieldDtos = ((ShieldMapper)FleetMapperFactory.RetrieveMapper(IsTest,ConnectionString,Operations,FleetMapperTypes.Shield)).EntityListToModel(hullEntity.Shields),
                StructurePoints = hullEntity.StructurePoints,
                SystemsDtos = ((SystemsMapper)FleetMapperFactory.RetrieveMapper(IsTest,ConnectionString,Operations,FleetMapperTypes.Systems)).EntityListToModel(hullEntity.SubSystems),
                TotalSpaces = hullEntity.TotalSpaces
            };
        }

        public List<HullDto> EntityListToModel(ICollection<Hull> entityList)
        {
            return entityList.Select(MapToDto).Select(dto => dto).Cast<HullDto>().ToList();
        }

        
        public List<Hull> ModelListToEntity(List<HullDto> entityList)
        {
            return entityList.Select(MapToEntity).Select(dto => dto).Cast<Hull>().ToList();
        }
    }
}