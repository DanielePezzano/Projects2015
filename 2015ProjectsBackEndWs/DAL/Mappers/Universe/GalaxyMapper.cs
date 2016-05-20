using System;
using System.Runtime.InteropServices;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Interfaces;
using DAL.Operations.Enums;
using DAL.Operations.IstanceFactory;
using Models.Base;
using Models.Universe;
using SharedDto.Interfaces;
using SharedDto.Universe;

namespace DAL.Mappers.Universe
{
    public class GalaxyMapper : BaseMapper, IMapToDto, IMapToEntity
    {
        public GalaxyMapper(OpFactory operations) : base(operations)
        {
        }

        public override bool ExistsEntity()
        {
            var cacheKey = $"COUNTGALAXY{Entity.Id}";
            return
               Operations.SetOperation(MappedRepositories.GalaxyRepository, MappedOperations.Any, cacheKey,
                   Entity.Id).CheckResult;
        }

        public IDto MapToDto(BaseEntity entity)
        {
            var galaxyEntity = (Galaxy) entity;
            return new GalaxyDto
            {
                CreatedAt = galaxyEntity.CreatedAt,
                Id = galaxyEntity.Id,
                Name = galaxyEntity.Name
            };
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            var galaxyDto = (GalaxyDto) dto;
            Entity = new Galaxy()
            {
                CreatedAt = galaxyDto.CreatedAt,
                Name = galaxyDto.Name,
                Id = galaxyDto.Id,
                UpdatedAt = DateTime.Now
            };
            return Entity;
        }
    }
}