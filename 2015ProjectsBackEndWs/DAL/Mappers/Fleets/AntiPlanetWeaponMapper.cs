using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Interfaces;
using DAL.Operations.Enums;
using DAL.Operations.IstanceFactory;
using Models.Base;
using Models.Fleets.ShipClasses.Weapons;
using SharedDto.Interfaces;
using SharedDto.Universe.Fleet;

namespace DAL.Mappers.Fleets
{
    public class AntiPlanetWeaponMapper : BaseMapper, IMapToDto,IMapToEntity
    {
        public AntiPlanetWeaponMapper(OpFactory operations) : base(operations)
        {
        }

        public override bool ExistsEntity()
        {
            var cacheKey = $"ANTIPLANETWEAPON{Entity.Id}";
             return
                Operations.SetOperation(MappedRepositories.AntiPlanetWeaponRepository, MappedOperations.Any, cacheKey,
                    Entity.Id).CheckResult;
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            var apWeapon = (AntiPlanetWeaponDto) dto;
            Entity = new AntiPlanetWeapon()
            {
                Id = apWeapon.Id,
                OreCost = apWeapon.OreCost,
                Name = apWeapon.Name,
                UpdatedAt = DateTime.Now,
                MoneyCost = apWeapon.MoneyCost,
                MoneyMaintenanceCost =apWeapon.MoneyCost,
                OreMaintenanceCost = apWeapon.OreCost,
                Description = apWeapon.Description,
                BonusToHit = apWeapon.BonusToHit,
                Damage = apWeapon.Damage,
                RadiationHazard = apWeapon.RadiationHazard,
                RateOfFire = apWeapon.RateOfFire,
                SpacesNeeded = apWeapon.SpacesNeeded,
                CreatedAt = apWeapon.CreatedAt
            };
            return Entity;
        }

        public IDto MapToDto(BaseEntity entity)
        {
            var apEntity = (AntiPlanetWeapon) entity;
            return new AntiPlanetWeaponDto()
            {
                RadiationHazard = apEntity.RadiationHazard,
                Id = apEntity.Id,
                Name = apEntity.Name,
                Description = apEntity.Description,
                BonusToHit = apEntity.BonusToHit,
                Damage = apEntity.Damage,
                RateOfFire = apEntity.RateOfFire,
                OreCost = apEntity.OreCost,
                MoneyCost = apEntity.MoneyCost,
                MoneyMaintenanceCost = apEntity.MoneyMaintenanceCost,
                OreMaintenanceCost = apEntity.OreMaintenanceCost,
                SpacesNeeded = apEntity.SpacesNeeded,
                CreatedAt = apEntity.CreatedAt
            };
        }
        public List<AntiPlanetWeaponDto> EntityListToModel(ICollection<AntiPlanetWeapon> entityList)
        {
            return entityList.Select(MapToDto).Select(dto => dto).Cast<AntiPlanetWeaponDto>().ToList();
        }

        
        public List<AntiPlanetWeapon> ModelListToEntity(List<AntiPlanetWeaponDto> entityList)
        {
            return entityList.Select(MapToEntity).Select(dto => dto).Cast<AntiPlanetWeapon>().ToList();
        }

    }
}