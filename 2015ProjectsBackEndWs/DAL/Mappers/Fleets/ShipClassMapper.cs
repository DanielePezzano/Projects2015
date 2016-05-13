using System.Collections.Generic;
using System.Linq;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Fleets.Enums;
using DAL.Mappers.Fleets.IstanceFactory;
using DAL.Mappers.Interfaces;
using DAL.Operations.Enums;
using DAL.Operations.IstanceFactory;
using Models.Base;
using Models.Fleets.ShipClasses;
using SharedDto.Interfaces;
using SharedDto.Universe.Fleet;

namespace DAL.Mappers.Fleets
{
    public class ShipClassMapper : BaseMapper,  IMapToDto,IMapToEntity
    {
        public ShipClassMapper( string connectionString, OpFactory operations) : base(connectionString, operations)
        {
        }

        public override bool ExistsEntity()
        {
            var cacheKey = $"SHIPCLASS{Entity.Id}";
             return
                Operations.SetOperation(MappedRepositories.ShipClassRepository, MappedOperations.Any, cacheKey,
                    Entity.Id).CheckResult;
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            var shipDto = (ShipClassDto) dto;
            Entity = new ShipClass()
            {
                Id = shipDto.Id,
                Name = shipDto.Name,
                Description = shipDto.Description,
                Hulls = ((HullMapper)FleetMapperFactory.RetrieveMapper(ConnectionString,Operations,FleetMapperTypes.Hull)).ModelListToEntity(shipDto.HullDtos)
            };
            return Entity;
        }

        public IDto MapToDto(BaseEntity entity)
        {
            var shipEntity = (ShipClass) entity;
            return new ShipClassDto()
            {
                Id = shipEntity.Id,
                Name = shipEntity.Name,
                OreCost = shipEntity.OreCost,
                Description = shipEntity.Description,
                MoneyCost = shipEntity.MoneyCost,
                MoneyMaintenanceCost = shipEntity.MoneyMaintenanceCost,
                OreMaintenanceCost = shipEntity.OreMaintenanceCost,
                ToHitBonus = shipEntity.ToHitBonus,
                CombatSpeed = shipEntity.CombatSpeed,
                StructurePoints = shipEntity.StructurePoints,
                TravelSpeed = shipEntity.TravelSpeed,
                HullDtos = ((HullMapper)FleetMapperFactory.RetrieveMapper(ConnectionString,Operations,FleetMapperTypes.Hull)).EntityListToModel(shipEntity.Hulls),
                EngineRadius = shipEntity.EngineRadius,
                TotalArmor = shipEntity.TotalArmor,
                TotalShields = shipEntity.TotalShields
            };
        }
          public List<ShipClassDto> EntityListToModel(ICollection<ShipClass> entityList)
        {
            return entityList.Select(MapToDto).Select(dto => dto).Cast<ShipClassDto>().ToList();
        }

        
        public List<ShipClass> ModelListToEntity(List<ShipClassDto> entityList)
        {
            return entityList.Select(MapToEntity).Select(dto => dto).Cast<ShipClass>().ToList();
        }

    }
}