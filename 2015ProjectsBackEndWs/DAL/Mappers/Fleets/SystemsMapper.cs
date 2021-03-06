﻿using System;
using System.Collections.Generic;
using System.Linq;
using BaseModels;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Interfaces;
using DAL.Operations.Enums;
using DAL.Operations.IstanceFactory;
using Models.Fleets.ShipClasses.Enums;
using Models.Fleets.ShipClasses.System;
using SharedDto.Interfaces;
using SharedDto.Universe.Fleet;

namespace DAL.Mappers.Fleets
{
    public class SystemsMapper : BaseMapper,  IMapToDto,IMapToEntity
    {
        public SystemsMapper(OpFactory operations) : base(operations)
        {
        }

        public override bool ExistsEntity()
        {
           var cacheKey = $"SHIPSYSTEMS{Entity.Id}";
           return
                Operations.SetOperation(MappedRepositories.ShipSystemRepository, MappedOperations.Any, cacheKey,
                    Entity.Id).CheckResult;
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            var sysDto = (SystemsDto) dto;
            Entity = new ShipSystem()
            {
                Id = sysDto.Id,
                Name = sysDto.Name,
                UpdatedAt = DateTime.Now,
                OreCost = sysDto.OreCost,
                MoneyCost = sysDto.MoneyCost,
                Description = sysDto.Description,
                OreMaintenanceCost = sysDto.OreMaintenanceCost,
                MoneyMaintenanceCost = sysDto.MoneyMaintenanceCost,
                SpacesNeeded = sysDto.SpacesNeeded,
                CombatSpeedBonus = sysDto.CombatSpeedBonus,
                EnergyBonus = sysDto.EnergyBonus,
                Range = sysDto.Range,
                ScannerRadius = sysDto.ScannerRadius,
                ScannerRelevationBonus = sysDto.ScannerRelevationBonus,
                SpaceBonus = sysDto.SpaceBonus,
                SystemType = (SystemType)Enum.Parse(typeof(SystemType),sysDto.SystemType),
                ToHitBonus = sysDto.ToHitBonus,
                TravelSpeedBonus = sysDto.TravelSpeedBonus,
                CreatedAt = sysDto.CreatedAt

            };
            return Entity;
        }

        public IDto MapToDto(BaseEntity entity)
        {
            var systemEntity = (ShipSystem) entity;
            return new SystemsDto()
            {
             Id = systemEntity.Id,
             Name = systemEntity.Name,
             OreCost = systemEntity.OreCost,
             MoneyCost = systemEntity.MoneyCost,
             Description = systemEntity.Description,
             OreMaintenanceCost = systemEntity.OreMaintenanceCost,
             MoneyMaintenanceCost = systemEntity.MoneyMaintenanceCost,
             SystemType = systemEntity.SystemType.ToString(),
             SpacesNeeded = systemEntity.SpacesNeeded,
             CombatSpeedBonus = systemEntity.CombatSpeedBonus,
             EnergyBonus = systemEntity.EnergyBonus,
             Range = systemEntity.Range,
             ScannerRadius = systemEntity.ScannerRadius,
             ScannerRelevationBonus = systemEntity.ScannerRelevationBonus,
             SpaceBonus = systemEntity.ScannerRelevationBonus,
             ToHitBonus = systemEntity.ToHitBonus,
             TravelSpeedBonus = systemEntity.TravelSpeedBonus,
                CreatedAt = systemEntity.CreatedAt
            };
        }

        public List<SystemsDto> EntityListToModel(ICollection<ShipSystem> entityList)
        {
            return entityList?.Select(MapToDto).Select(dto => dto).Cast<SystemsDto>().ToList() ?? new List<SystemsDto>();
        }

        
        public List<ShipSystem> ModelListToEntity(List<SystemsDto> entityList)
        {
            return entityList?.Select(MapToEntity).Select(dto => dto).Cast<ShipSystem>().ToList() ?? new List<ShipSystem>();
        }
    }
}