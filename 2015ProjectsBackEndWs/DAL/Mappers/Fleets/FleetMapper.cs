using System.Collections.Generic;
using System.Linq;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Fleets.Enums;
using DAL.Mappers.Fleets.IstanceFactory;
using DAL.Mappers.Interfaces;
using DAL.Operations.Enums;
using DAL.Operations.IstanceFactory;
using Models.Base;
using Models.Fleets;
using SharedDto.Interfaces;
using SharedDto.Universe.Fleet;

namespace DAL.Mappers.Fleets
{
    public class FleetMapper : BaseMapper,  IMapToDto,IMapToEntity
    {
        public FleetMapper(string connectionString, OpFactory operations) : base(connectionString, operations)
        {
        }

        public override bool ExistsEntity()
        {
            var cacheKey = $"Fleet{Entity.Id}";
             return
                Operations.SetOperation(MappedRepositories.FleetRepository, MappedOperations.Any, cacheKey,
                    Entity.Id).CheckResult;
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            var fleetDto = (FleetDto) dto;
            Entity = new Fleet()
            {
                Id = fleetDto.Id,
                Name = fleetDto.Name,
                Description = fleetDto.Description,
                AtBay = fleetDto.AtBay,
                Position = fleetDto.Position,
                AtBayPlanetId = fleetDto.AtBayPlanetId,
                ShipClasses = ((ShipClassMapper)FleetMapperFactory.RetrieveMapper(ConnectionString,Operations,FleetMapperTypes.Ship)).ModelListToEntity(fleetDto.ShipClassDtos)
            };
            return Entity;
        }

        public IDto MapToDto(BaseEntity entity)
        {
            var fleetEntity = (Fleet) entity;
            return new FleetDto()
            {
                Id = fleetEntity.Id,
                Name = fleetEntity.Name,
                Description = fleetEntity.Description,
                OreCost = fleetEntity.OreCost,
                MoneyCost = fleetEntity.MoneyCost,
                AtBayPlanetId = fleetEntity.AtBayPlanetId,
                OreMaintenanceCost = fleetEntity.OreMaintenanceCost,
                MoneyMaintenanceCost = fleetEntity.MoneyMaintenanceCost,
                Range = fleetEntity.Range,
                TravelSpeed = fleetEntity.TravelSpeed,
                Position = fleetEntity.Position,
                AtBay = fleetEntity.AtBay,
                ShipClassDtos = ((ShipClassMapper)FleetMapperFactory.RetrieveMapper(ConnectionString,Operations,FleetMapperTypes.Ship)).EntityListToModel(fleetEntity.ShipClasses)
            };
        }

        public List<FleetDto> EntityListToModel(ICollection<Fleet> entityList)
        {
            return entityList.Select(MapToDto).Select(dto => dto).Cast<FleetDto>().ToList();
        }

        
        public List<Fleet> ModelListToEntity(List<FleetDto> entityList)
        {
            return entityList.Select(MapToEntity).Select(dto => dto).Cast<Fleet>().ToList();
        }
    }
}
