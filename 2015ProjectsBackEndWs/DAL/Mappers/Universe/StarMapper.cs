using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Interfaces;
using DAL.Mappers.Universe.Enums;
using DAL.Mappers.Universe.IstanceFactory;
using DAL.Operations;
using DAL.Operations.BaseClasses;
using Models.Base;
using Models.Universe;
using Models.Universe.Enum;
using SharedDto.Interfaces;
using SharedDto.Universe.Stars;

namespace DAL.Mappers.Universe
{
    public class StarMapper : BaseMapper,  IMapToDto,IMapToEntity
    {

        public StarMapper(string connectionString,BaseOperations operations,bool isTest=false):base(isTest,connectionString,operations)
        {
            
        }

        public override bool ExistsEntity()
        {
            var cacheKey = $"COUNTSTAR{Entity.Id}";
            return Operations.Any(MappedRepositories.StarRepository, Entity.Id, cacheKey);
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            var starDto = (StarDto) dto;
            Entity =new Star()
            {
                Id = starDto.Id,
                Planets = ((PlanetMapper)MapperFactory.RetrieveMapper(IsTest,ConnectionString,Operations,UniverseMapperTypes.Planets)).ModelListToEntity(starDto.Planets),
                CoordinateX = starDto.PositionX,
                CoordinateY = starDto.PositionY,
                Mass = starDto.Mass,
                Name = starDto.Name,
                RadiationLevel = starDto.RadiationLevel,
                Radius = starDto.Radius,
                StarColor = (StarColor)Enum.Parse(typeof(StarColor), starDto.StarColor),
                StarType = (StarType)Enum.Parse(typeof(StarType), starDto.StarType),
                SurfaceTemp = starDto.SurfaceTemp,
                UpdatedAt = DateTime.Now
            };
            return Entity;
        }

        public IDto MapToDto(BaseEntity entity)
        {
            var starEntity = (Star) entity;
            return new StarDto()
            {
                GalaxyId = starEntity.Galaxy.Id,
                Mass = starEntity.Mass,
                Name = starEntity.Name,
                Planets = ((PlanetMapper)MapperFactory.RetrieveMapper(IsTest,ConnectionString,Operations,UniverseMapperTypes.Planets)).EntityListToModel(starEntity.Planets),
                PositionY = starEntity.CoordinateY,
                PositionX = starEntity.CoordinateX,
                RadiationLevel = starEntity.RadiationLevel,
                Radius = starEntity.Radius,
                StarColor = starEntity.StarColor.ToString(),
                StarType = starEntity.StarType.ToString(),
                SurfaceTemp = starEntity.SurfaceTemp,
                Id = entity.Id
            };
        }

        public List<StarDto> EntityListToModel(ICollection<Star> entityList)
        {
            return entityList.Select(MapToDto).Select(dto => dto).Cast<StarDto>().ToList();
        }

        
        public List<Star> ModelListToEntity(List<StarDto> entityList)
        {
            return entityList.Select(MapToEntity).Select(dto => dto).Cast<Star>().ToList();
        }
    }
}
