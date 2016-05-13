using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Interfaces;
using DAL.Mappers.User.Enums;
using DAL.Mappers.User.IstanceFactory;
using DAL.Operations.Enums;
using DAL.Operations.IstanceFactory;
using Models.Base;
using Models.Tech;
using Models.Tech.Enum;
using SharedDto.Interfaces;
using SharedDto.Universe.Technology;

namespace DAL.Mappers.User
{
    public class TechMapper : BaseMapper,  IMapToDto,IMapToEntity
    {
        public TechMapper(string connectionString, OpFactory operations) : base(connectionString, operations)
        {
        }

        public override bool ExistsEntity()
        {
             var cacheKey = $"TECH{Entity.Id}";
             return
                Operations.SetOperation(MappedRepositories.TechnologyRepository, MappedOperations.Any, cacheKey,
                    Entity.Id).CheckResult;
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            var techDto = (TechnologyDto) dto;
            Entity = new Technology()
            {
                Id = techDto.Id,
                Name = techDto.Name,
                Description = techDto.Description,
                OreCost = techDto.OreCost,
                MoneyCost = techDto.MoneyCost,
                Field = (TechnologyField)Enum.Parse(typeof(TechnologyField),techDto.Field),
                ResearchPoints = techDto.ResearchPoints,
                SpaceNeeded = techDto.SpacesNeeded, 
                SubField = (TechnologySubField)Enum.Parse(typeof(TechnologySubField),techDto.SubField),
                TechBonuses = ((TechBonusMapper)UserMapperFactory.RetrieveMapper(ConnectionString,Operations,UserMapperTypes.TechBonus)).ModelListToEntity(techDto.TechnologyBonuses),
                TechRequisites = ((TechRequisiteMapper)UserMapperFactory.RetrieveMapper(ConnectionString,Operations,UserMapperTypes.TechRequisites)).ModelListToEntity(techDto.NeededTechnologies),
                ResearchQueues = ((ResearchQueueMapper)UserMapperFactory.RetrieveMapper(ConnectionString,Operations,UserMapperTypes.ResearchQueue)).ModelListToEntity(techDto.ResearchDtos)
            };
            return Entity;
        }

        public IDto MapToDto(BaseEntity entity)
        {
            var techEntity = (Technology) entity;
            return new TechnologyDto()
            {
                Id = techEntity.Id,
                Name = techEntity.Name,
                Description = techEntity.Description,
                OreCost = techEntity.OreCost,
                MoneyCost = techEntity.MoneyCost,
                SpacesNeeded = techEntity.SpaceNeeded,
                SubField = techEntity.SubField.ToString(),
                ResearchPoints = techEntity.ResearchPoints,
                TechnologyBonuses = ((TechBonusMapper)UserMapperFactory.RetrieveMapper(ConnectionString,Operations,UserMapperTypes.TechBonus)).EntityListToModel(techEntity.TechBonuses),
                Field = techEntity.Field.ToString(),
                NeededTechnologies = ((TechRequisiteMapper)UserMapperFactory.RetrieveMapper(ConnectionString,Operations,UserMapperTypes.TechRequisites)).EntityListToModel(techEntity.TechRequisites)
            };
        }

        public List<TechnologyDto> EntityListToModel(ICollection<Technology> entityList)
        {
            return entityList.Select(MapToDto).Select(dto => dto).Cast<TechnologyDto>().ToList();
        }

        
        public List<Technology> ModelListToEntity(List<TechnologyDto> entityList)
        {
            return entityList.Select(MapToEntity).Select(dto => dto).Cast<Technology>().ToList();
        }
    }
}