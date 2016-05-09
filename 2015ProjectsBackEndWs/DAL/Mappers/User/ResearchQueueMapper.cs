using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Interfaces;
using DAL.Operations;
using DAL.Operations.BaseClasses;
using Models.Base;
using Models.Queues;
using Models.Queues.Enum;
using SharedDto.Interfaces;
using SharedDto.Universe.Queues;

namespace DAL.Mappers.User
{
    public class ResearchQueueMapper : BaseMapper,  IMapToDto,IMapToEntity
    {
        public ResearchQueueMapper(bool isTest, string connectionString, BaseOperations operations) : base(isTest, connectionString, operations)
        {
        }

        public override bool ExistsEntity()
        {
             var cacheKey = $"ResearchQueue{Entity.Id}";
            return Operations.Any(MappedRepositories.ResearchQueueRepository, Entity.Id, cacheKey);
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            var queueDto = (ResearchDto) dto;
            Entity = new ResearchQueue()
            {
                Id = queueDto.Id,
                FinishAt = queueDto.FinishDateTime,
                PlanetId = queueDto.PlanetId,
                SatelliteId = queueDto.SatelliteId,
                Status = (QueueStatus)(Enum.Parse(typeof(QueueStatus),queueDto.Status))
            };
            return Entity;
        }

        public IDto MapToDto(BaseEntity entity)
        {
            var queueEntity = (ResearchQueue) entity;
            return new ResearchDto()
            {
                Id = queueEntity.Id,
                Status = queueEntity.Status.ToString(),
                FinishDateTime = queueEntity.FinishAt,
                PlanetId = queueEntity.PlanetId,
                SatelliteId = queueEntity.SatelliteId
            };
        }

        public List<ResearchDto> EntityListToModel(ICollection<ResearchQueue> entityList)
        {
            return entityList.Select(MapToDto).Select(dto => dto).Cast<ResearchDto>().ToList();
        }

        
        public List<ResearchQueue> ModelListToEntity(List<ResearchDto> entityList)
        {
            return entityList.Select(MapToEntity).Select(dto => dto).Cast<ResearchQueue>().ToList();
        }
    }
}