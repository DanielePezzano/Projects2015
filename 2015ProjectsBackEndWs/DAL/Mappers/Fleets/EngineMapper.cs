using System.Collections.Generic;
using System.Linq;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Interfaces;
using DAL.Operations;
using DAL.Operations.BaseClasses;
using Models.Base;
using Models.Fleets.ShipClasses.Engines;
using SharedDto.Interfaces;
using SharedDto.Universe.Fleet;

namespace DAL.Mappers.Fleets
{
    public class EngineMapper : BaseMapper,  IMapToDto,IMapToEntity
    {
        public EngineMapper(bool isTest, string connectionString, BaseOperations operations) : base(isTest, connectionString, operations)
        {
        }

        public override bool ExistsEntity()
        {
            var cacheKey = $"ENGINE{Entity.Id}";
            return Operations.Any(MappedRepositories.EngineRepository, Entity.Id, cacheKey);
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            var engineDto = (EngineDto) dto;
            Entity = new Engine()
            {
                Id = engineDto.Id,
                Name = engineDto.Name,
                OreCost = engineDto.OreCost,
                MoneyCost = engineDto.MoneyCost,
                CombatSpeed = engineDto.CombatSpeed,
                Description = engineDto.Description,
                GeneratedEnergy = engineDto.GeneratedEnergy,
                MoneyMaintenanceCost = engineDto.MoneyMaintenanceCost,
                OreMaintenanceCost = engineDto.OreMaintenanceCost,
                SpacesNeeded = engineDto.SpacesNeeded,
                TravelSpeed = engineDto.TravelSpeed
            };
            return Entity;
        }

        public IDto MapToDto(BaseEntity entity)
        {
            var engineEntity = (Engine) entity;
            return new EngineDto()
            {
                Id = engineEntity.Id,
                Name = engineEntity.Name,
                OreCost = engineEntity.OreCost,
                MoneyCost = engineEntity.MoneyCost,
                CombatSpeed = engineEntity.CombatSpeed,
                Description = engineEntity.Description,
                GeneratedEnergy = engineEntity.GeneratedEnergy,
                MoneyMaintenanceCost = engineEntity.MoneyMaintenanceCost,
                OreMaintenanceCost = engineEntity.OreMaintenanceCost,
                SpacesNeeded = engineEntity.SpacesNeeded,
                TravelSpeed = engineEntity.TravelSpeed
            };
        }

        
        public List<EngineDto> EntityListToModel(ICollection<Engine> entityList)
        {
            return entityList.Select(MapToDto).Select(dto => dto).Cast<EngineDto>().ToList();
        }

        
        public List<Engine> ModelListToEntity(List<EngineDto> entityList)
        {
            return entityList.Select(MapToEntity).Select(dto => dto).Cast<Engine>().ToList();
        }
    }
}