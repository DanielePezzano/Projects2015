using System;
using System.Collections.Generic;
using System.Linq;
using BaseModels;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Fleets.Enums;
using DAL.Mappers.Fleets.IstanceFactory;
using DAL.Mappers.Interfaces;
using DAL.Operations.Enums;
using DAL.Operations.IstanceFactory;
using Models.Fleets.ShipClasses.Enums;
using Models.Fleets.ShipClasses.Hulls;
using SharedDto.Interfaces;
using SharedDto.Universe.Fleet;

namespace DAL.Mappers.Fleets
{
    public class HullMapper : BaseMapper,  IMapToDto,IMapToEntity
    {
        public HullMapper(OpFactory operations) : base(operations)
        {
        }

        public override bool ExistsEntity()
        {
           var cacheKey = $"HULL{Entity.Id}";
           return
                Operations.SetOperation(MappedRepositories.HullRepository, MappedOperations.Any, cacheKey,
                    Entity.Id).CheckResult;
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            var hullDto = (HullDto) dto;
            Entity = new Hull()
            {
                Id = hullDto.Id,
                Name = hullDto.Name,
                Description = hullDto.Description,
                AntiPlanetWeapons = ((AntiPlanetWeaponMapper)FleetMapperFactory.RetrieveMapper(Operations,FleetMapperTypes.AntiplanetWeapons)).ModelListToEntity(hullDto.AntiPlanetWeaponDtos),
                AntiShipWeapons = ((AntiShipWeaponMapper)FleetMapperFactory.RetrieveMapper(Operations,FleetMapperTypes.AntishipWeapons)).ModelListToEntity(hullDto.AntishioShipWeaponDtos),
                Armors =((ArmorMapper)FleetMapperFactory.RetrieveMapper(Operations,FleetMapperTypes.Armor)).ModelListToEntity(hullDto.ArmorDtos),
                Engines = ((EngineMapper)FleetMapperFactory.RetrieveMapper(Operations,FleetMapperTypes.Engine)).ModelListToEntity(hullDto.EngineDtos),
                HullType = (HullType)Enum.Parse(typeof(HullType),hullDto.HullType),
                Shields = ((ShieldMapper)FleetMapperFactory.RetrieveMapper(Operations,FleetMapperTypes.Shield)).ModelListToEntity(hullDto.ShieldDtos),
                StructurePoints = hullDto.StructurePoints,
                SubSystems = ((SystemsMapper)FleetMapperFactory.RetrieveMapper(Operations,FleetMapperTypes.Systems)).ModelListToEntity(hullDto.SystemsDtos),
                TotalSpaces = hullDto.TotalSpaces,
                CreatedAt = hullDto.CreatedAt
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
                AntiPlanetWeaponDtos = ((AntiPlanetWeaponMapper)FleetMapperFactory.RetrieveMapper(Operations,FleetMapperTypes.AntiplanetWeapons)).EntityListToModel(hullEntity.AntiPlanetWeapons),
                AntishioShipWeaponDtos = ((AntiShipWeaponMapper)FleetMapperFactory.RetrieveMapper(Operations,FleetMapperTypes.AntishipWeapons)).EntityListToModel(hullEntity.AntiShipWeapons),
                ArmorDtos = ((ArmorMapper)FleetMapperFactory.RetrieveMapper(Operations,FleetMapperTypes.Armor)).EntityListToModel(hullEntity.Armors),
                EngineDtos = ((EngineMapper)FleetMapperFactory.RetrieveMapper(Operations,FleetMapperTypes.Engine)).EntityListToModel(hullEntity.Engines),
                HullType = hullEntity.ToString(),
                ShieldDtos = ((ShieldMapper)FleetMapperFactory.RetrieveMapper(Operations,FleetMapperTypes.Shield)).EntityListToModel(hullEntity.Shields),
                StructurePoints = hullEntity.StructurePoints,
                SystemsDtos = ((SystemsMapper)FleetMapperFactory.RetrieveMapper(Operations,FleetMapperTypes.Systems)).EntityListToModel(hullEntity.SubSystems),
                TotalSpaces = hullEntity.TotalSpaces,
                CreatedAt = hullEntity.CreatedAt
            };
        }

        public List<HullDto> EntityListToModel(ICollection<Hull> entityList)
        {
            return entityList?.Select(MapToDto).Select(dto => dto).Cast<HullDto>().ToList() ?? new List<HullDto>();
        }

        
        public List<Hull> ModelListToEntity(List<HullDto> entityList)
        {
            return entityList?.Select(MapToEntity).Select(dto => dto).Cast<Hull>().ToList() ?? new List<Hull>();
        }
    }
}