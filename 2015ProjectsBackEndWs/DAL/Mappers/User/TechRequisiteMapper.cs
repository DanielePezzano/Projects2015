﻿using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Interfaces;
using DAL.Operations;
using DAL.Operations.BaseClasses;
using Models.Base;
using Models.Tech;
using Models.Tech.Enum;
using SharedDto.Interfaces;
using SharedDto.Universe.Technology;

namespace DAL.Mappers.User
{
    public class TechRequisiteMapper : BaseMapper,  IMapToDto,IMapToEntity
    {
        public TechRequisiteMapper(bool isTest, string connectionString, BaseOperations operations) : base(isTest, connectionString, operations)
        {
        }

        public override bool ExistsEntity()
        {
            var cacheKey = $"TECHREQ{Entity.Id}";
            return Operations.Any(MappedRepositories.TechNodesRepository, Entity.Id, cacheKey);
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            var reqDto = (TechnologyRequisiteDto) dto;
            Entity = new TechRequisiteNode()
            {
                RequisiteType = (RequisiteType)Enum.Parse(typeof(RequisiteType),reqDto.RequisiteType)
            };
            return Entity;
        }

        public IDto MapToDto(BaseEntity entity)
        {
            var reqEntity = (TechRequisiteNode) entity;
            return new TechnologyRequisiteDto()
            {
                RequisiteType = reqEntity.RequisiteType.ToString()
            };
        }

           public List<TechnologyRequisiteDto> EntityListToModel(ICollection<TechRequisiteNode> entityList)
        {
            return entityList.Select(MapToDto).Select(dto => dto).Cast<TechnologyRequisiteDto>().ToList();
        }

        
        public List<TechRequisiteNode> ModelListToEntity(List<TechnologyRequisiteDto> entityList)
        {
            return entityList.Select(MapToEntity).Select(dto => dto).Cast<TechRequisiteNode>().ToList();
        }
    }
}