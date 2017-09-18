using System.Collections.Generic;
using System.Linq;
using BaseModels;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Interfaces;
using DAL.Operations.Enums;
using DAL.Operations.IstanceFactory;
using Models.Tech;
using SharedDto.Interfaces;
using SharedDto.Universe.Technology;

namespace DAL.Mappers.User
{
    public class TechBonusMapper : BaseMapper,  IMapToDto,IMapToEntity
    {
        public TechBonusMapper(OpFactory operations) : base(operations)
        {
        }

        public override bool ExistsEntity()
        {
             var cacheKey = $"TECHBONUS{Entity.Id}";
             return
                Operations.SetOperation(MappedRepositories.TechBonusRepository, MappedOperations.Any, cacheKey,
                    Entity.Id).CheckResult;
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            var bonusDto = (TechnologyBonusDto) dto;
            Entity = new TechBonus()
            {
                Id = bonusDto.Id,
                Bonus = bonusDto.Bonus,
                Value = bonusDto.Value
            };
            return Entity;
        }

        public IDto MapToDto(BaseEntity entity)
        {
            var bonusEntity = (TechBonus) entity;
            return new TechnologyBonusDto()
            {
                Bonus = bonusEntity.Bonus,
                Id = bonusEntity.Id,
                Value = bonusEntity.Value
            };
        }

        public List<TechnologyBonusDto> EntityListToModel(ICollection<TechBonus> entityList)
        {
            return entityList.Select(MapToDto).Select(dto => dto).Cast<TechnologyBonusDto>().ToList();
        }

        
        public List<TechBonus> ModelListToEntity(List<TechnologyBonusDto> entityList)
        {
            return entityList.Select(MapToEntity).Select(dto => dto).Cast<TechBonus>().ToList();
        }
    }
}