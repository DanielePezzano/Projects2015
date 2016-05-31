using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Interfaces;
using DAL.Mappers.Universe.Enums;
using DAL.Mappers.Universe.IstanceFactory;
using DAL.Operations.Enums;
using DAL.Operations.IstanceFactory;
using Models.Base;
using Models.Universe;
using Models.Universe.Enum;
using SharedDto.Interfaces;
using SharedDto.Universe.Stars;

namespace DAL.Mappers.Universe
{
    public class StarMapper : BaseMapper,  IMapToDto,IMapToEntity
    {

        public StarMapper(OpFactory operations):base(operations)
        {
            
        }

        public override bool ExistsEntity()
        {
            var cacheKey = $"COUNTSTAR{Entity.Id}";
             return
                Operations.SetOperation(MappedRepositories.StarRepository, MappedOperations.Any, cacheKey,
                    Entity.Id).CheckResult;
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            var starDto = (StarDto) dto;
            Entity =new Star()
            {
                Id = starDto.Id,
                Planets = ((PlanetMapper)MapperFactory.RetrieveMapper(Operations,UniverseMapperTypes.Planets)).ModelListToEntity(starDto.Planets),
                CoordinateX = starDto.PositionX,
                CoordinateY = starDto.PositionY,
                Mass = starDto.Mass,
                Name = starDto.Name,
                RadiationLevel = starDto.RadiationLevel,
                Radius = starDto.Radius,
                StarColor = (StarColor)Enum.Parse(typeof(StarColor), starDto.StarColor),
                StarType = (StarType)Enum.Parse(typeof(StarType), starDto.StarType),
                SurfaceTemp = starDto.SurfaceTemp,
                UpdatedAt = DateTime.Now,
                CreatedAt = starDto.CreatedAt,
                GalaxyID = starDto.GalaxyId
            };
            return Entity;
        }

        public IDto MapToDto(BaseEntity entity)
        {
            var starEntity = (Star) entity;
            return new StarDto()
            {
                GalaxyId = starEntity.GalaxyID,
                Mass = starEntity.Mass,
                Name = starEntity.Name,
                Planets = ((PlanetMapper)MapperFactory.RetrieveMapper(Operations,UniverseMapperTypes.Planets)).EntityListToModel(starEntity.Planets),
                PositionY = starEntity.CoordinateY,
                PositionX = starEntity.CoordinateX,
                RadiationLevel = starEntity.RadiationLevel,
                Radius = starEntity.Radius,
                StarColor = starEntity.StarColor.ToString(),
                StarType = starEntity.StarType.ToString(),
                SurfaceTemp = starEntity.SurfaceTemp,
                Id = entity.Id,
                CreatedAt = starEntity.CreatedAt
            };
        }

        public List<StarDto> EntityListToModel(ICollection<Star> entityList)
        {
            return entityList?.Select(MapToDto).Select(dto => dto).Cast<StarDto>().ToList() ?? new List<StarDto>();
        }

        
        public List<Star> ModelListToEntity(List<StarDto> entityList)
        {
            return entityList?.Select(MapToEntity).Select(dto => dto).Cast<Star>().ToList() ?? new List<Star>();
        }
    }
}
