using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Interfaces;
using DAL.Operations.Enums;
using DAL.Operations.IstanceFactory;
using Models.Base;
using Models.Races;
using SharedDto.Interfaces;
using SharedDto.Universe.Race;

namespace DAL.Mappers.User
{
    public class RaceBonusMapper : BaseMapper,  IMapToDto,IMapToEntity
    {
        public RaceBonusMapper(string connectionString, OpFactory operations) : base(connectionString, operations)
        {
        }

        public override bool ExistsEntity()
        {
            var cacheKey =  $"RACEBONUS{Entity.Id}";
             return
                Operations.SetOperation(MappedRepositories.RaceBonusRepository, MappedOperations.Any, cacheKey,
                    Entity.Id).CheckResult;
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            var raceDto = (RaceBonusDto) dto;
            Entity = new RaceBonus()
            {
                Bonus = raceDto.Bonus,
                Id = raceDto.Id,
                TraitType = raceDto.TraitType,
                UpdatedAt = DateTime.Now,
                Value = raceDto.Value
            };
            return Entity;
        }

        public IDto MapToDto(BaseEntity entity)
        {
            var raceEntity = (RaceBonus) entity;
            return new RaceBonusDto()
            {
                Id = raceEntity.Id,
                Value = raceEntity.Value,
                Bonus = raceEntity.Bonus,
                TraitType = raceEntity.TraitType
            };
        }

        public List<RaceBonusDto> EntityListToModel(ICollection<RaceBonus> entityList)
        {
            return entityList.Select(MapToDto).Select(dto => dto).Cast<RaceBonusDto>().ToList();
        }

        
        public List<RaceBonus> ModelListToEntity(List<RaceBonusDto> entityList)
        {
            return entityList.Select(MapToEntity).Select(dto => dto).Cast<RaceBonus>().ToList();
        }
    }
}