using System.Collections.Generic;
using System.Linq;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Interfaces;
using DAL.Operations;
using DAL.Operations.BaseClasses;
using Models.Base;
using Models.Fleets.ShipClasses.Armors;
using SharedDto.Interfaces;
using SharedDto.Universe.Fleet;

namespace DAL.Mappers.Fleets
{
    public class ArmorMapper : BaseMapper,  IMapToDto,IMapToEntity
    {
        public ArmorMapper(bool isTest, string connectionString, BaseOperations operations) : base(isTest, connectionString, operations)
        {
        }

        public override bool ExistsEntity()
        {
            var cacheKey = $"ARMOR{Entity.Id}";
            return Operations.Any(MappedRepositories.ArmorRepository, Entity.Id, cacheKey);
        }


        public BaseEntity MapToEntity(IDto dto)
        {
            var armorDto = (ArmorDto) dto;
            Entity = new Armor()
            {
                Id = armorDto.Id,
                Name = armorDto.Name,
                OreCost = armorDto.OreCost,
                Description = armorDto.Description,
                MoneyCost = armorDto.MoneyCost,
                MoneyMaintenanceCost = armorDto.MoneyMaintenanceCost,
                OreMaintenanceCost = armorDto.OreMaintenanceCost,
                PercCombatSpeedMalus = armorDto.PercCombatSpeedMalus,
                PercTravelSpeedMalus = armorDto.PercTravelSpeedMalus,
                Protection = armorDto.Protection,
                SpacesNeeded = armorDto.SpacesNeeded
            };
            return Entity;
        }

        public IDto MapToDto(BaseEntity entity)
        {
            var armorEntity = (Armor) entity;
            return new ArmorDto()
            {
                 Id = armorEntity.Id,
                Name = armorEntity.Name,
                OreCost = armorEntity.OreCost,
                Description = armorEntity.Description,
                MoneyCost = armorEntity.MoneyCost,
                MoneyMaintenanceCost = armorEntity.MoneyMaintenanceCost,
                OreMaintenanceCost = armorEntity.OreMaintenanceCost,
                PercCombatSpeedMalus = armorEntity.PercCombatSpeedMalus,
                PercTravelSpeedMalus = armorEntity.PercTravelSpeedMalus,
                Protection = armorEntity.Protection,
                SpacesNeeded = armorEntity.SpacesNeeded
            };
        }

        
        public List<ArmorDto> EntityListToModel(ICollection<Armor> entityList)
        {
            return entityList.Select(MapToDto).Select(dto => dto).Cast<ArmorDto>().ToList();
        }

        
        public List<Armor> ModelListToEntity(List<ArmorDto> entityList)
        {
            return entityList.Select(MapToEntity).Select(dto => dto).Cast<Armor>().ToList();
        }
    }
}