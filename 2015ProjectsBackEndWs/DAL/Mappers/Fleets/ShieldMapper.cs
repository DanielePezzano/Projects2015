using System.Collections.Generic;
using System.Linq;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Interfaces;
using DAL.Operations.Enums;
using DAL.Operations.IstanceFactory;
using Models.Base;
using Models.Fleets.ShipClasses.Shields;
using SharedDto.Interfaces;
using SharedDto.Universe.Fleet;

namespace DAL.Mappers.Fleets
{
    public class ShieldMapper : BaseMapper,  IMapToDto,IMapToEntity
    {
        public ShieldMapper(OpFactory operations) : base(operations)
        {
        }

        public override bool ExistsEntity()
        {
            var cacheKey = $"SHIELD{Entity.Id}";
             return
                Operations.SetOperation(MappedRepositories.ShieldRepository, MappedOperations.Any, cacheKey,
                    Entity.Id).CheckResult;
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            var shieldDto = (ShieldDto) dto;
            Entity = new Shield()
            {
                Id = shieldDto.Id,
                Name = shieldDto.Name,
                OreCost = shieldDto.OreCost,
                MoneyCost = shieldDto.MoneyCost,
                Description = shieldDto.Description,
                MoneyMaintenanceCost = shieldDto.MoneyMaintenanceCost,
                OreMaintenanceCost = shieldDto.OreMaintenanceCost,
                Protection = shieldDto.Protection,
                RequiredEnergy = shieldDto.RequiredEnergy,
                SpacesNeeded = shieldDto.SpacesNeeded,
                CreatedAt = shieldDto.CreatedAt
            };
            return Entity;
        }

        public IDto MapToDto(BaseEntity entity)
        {
            var shieldEntity = (Shield) entity;
            return new ShieldDto()
            {
                Id = shieldEntity.Id,
                Name = shieldEntity.Name,
                OreCost = shieldEntity.OreCost,
                MoneyCost = shieldEntity.MoneyCost,
                Description = shieldEntity.Description,
                MoneyMaintenanceCost = shieldEntity.MoneyMaintenanceCost,
                OreMaintenanceCost = shieldEntity.OreMaintenanceCost,
                Protection = shieldEntity.Protection,
                RequiredEnergy = shieldEntity.RequiredEnergy,
                SpacesNeeded = shieldEntity.SpacesNeeded,
                CreatedAt = shieldEntity.CreatedAt
            };
        }

        public List<ShieldDto> EntityListToModel(ICollection<Shield> entityList)
        {
            return entityList.Select(MapToDto).Select(dto => dto).Cast<ShieldDto>().ToList();
        }

        
        public List<Shield> ModelListToEntity(List<ShieldDto> entityList)
        {
            return entityList.Select(MapToEntity).Select(dto => dto).Cast<Shield>().ToList();
        }
    }
}