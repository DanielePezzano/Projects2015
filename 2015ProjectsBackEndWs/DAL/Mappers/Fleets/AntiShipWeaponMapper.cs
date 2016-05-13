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
    public class AntiShipWeaponMapper : BaseMapper,  IMapToDto,IMapToEntity
    {
        public AntiShipWeaponMapper(string connectionString, OpFactory operations) : base(connectionString, operations)
        {
        }

        public override bool ExistsEntity()
        {
            var cacheKey = $"ANTISHIPWEAPON{Entity.Id}";
             return
                Operations.SetOperation(MappedRepositories.AntiShipWeaponRepository, MappedOperations.Any, cacheKey,
                    Entity.Id).CheckResult;
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            var asWeapon = (AntiShipWeaponDto) dto;
            Entity = new AntiShipWeapon()
            {
                Id = asWeapon.Id,
                OreCost = asWeapon.OreCost,
                Name = asWeapon.Name,
                UpdatedAt = DateTime.Now,
                MoneyCost = asWeapon.MoneyCost,
                MoneyMaintenanceCost =asWeapon.MoneyCost,
                OreMaintenanceCost = asWeapon.OreCost,
                Description = asWeapon.Description,
                BonusToHit = asWeapon.BonusToHit,
                Damage = asWeapon.Damage,
                IsBeamWeapon = asWeapon.IsBeamWeapon,
                RateOfFire = asWeapon.RateOfFire,
                SpacesNeeded = asWeapon.SpacesNeeded,
                RayOfFire = asWeapon.RayOfFire
            };
            return Entity;
        }

        public IDto MapToDto(BaseEntity entity)
        {
            var apEntity = (AntiShipWeapon) entity;
            return new AntiShipWeaponDto()
            {
                Id = apEntity.Id,
                OreCost = apEntity.OreCost,
                Name = apEntity.Name,
                MoneyCost = apEntity.MoneyCost,
                MoneyMaintenanceCost =apEntity.MoneyCost,
                OreMaintenanceCost = apEntity.OreCost,
                Description = apEntity.Description,
                BonusToHit = apEntity.BonusToHit,
                Damage = apEntity.Damage,
                IsBeamWeapon = apEntity.IsBeamWeapon,
                RateOfFire = apEntity.RateOfFire,
                SpacesNeeded = apEntity.SpacesNeeded,
                RayOfFire = apEntity.RayOfFire
            };
        }
        public List<AntiShipWeaponDto> EntityListToModel(ICollection<AntiShipWeapon> entityList)
        {
            return entityList.Select(MapToDto).Select(dto => dto).Cast<AntiShipWeaponDto>().ToList();
        }

        
        public List<AntiShipWeapon> ModelListToEntity(List<AntiShipWeaponDto> entityList)
        {
            return entityList.Select(MapToEntity).Select(dto => dto).Cast<AntiShipWeapon>().ToList();
        }
    }
}