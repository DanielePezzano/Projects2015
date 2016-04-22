using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Interfaces;
using DAL.Mappers.Universe.IstanceFactory;
using Models.Base;
using Models.Universe;
using Models.Universe.Enum;
using SharedDto.Interfaces;
using SharedDto.Universe.Stars;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Interfaces.UnitOfWork;

namespace DAL.Mappers.Universe
{
    public class StarMapper : BaseMapper, IMapper
    {

        public StarMapper(IUnitOfWork uow,bool isTest=false):base(uow,isTest)
        {
            
        }

        public override bool ExistsEntity()
        {
            return (IsTest)
                ? ((TestUow) UnitOfWork)?.StarRepository.Count(c => c.Id == Entity.Id,"COUNTSTAR"+Entity.Id) > 0
                : ((ProductionUow) UnitOfWork)?.StarRepository.Count(c => c.Id == Entity.Id,"COUNTSTAR"+Entity.Id) > 0;
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            var starDto = (StarDto) dto;
            Entity =new Star()
            {
                Id = starDto.StarId,
                //Galaxy = 
                Planets = MapperFactory.RetrievePlanetMapper(UnitOfWork, IsTest).ModelListToEntity(starDto.Planets),
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
                Planets = MapperFactory.RetrievePlanetMapper(UnitOfWork, IsTest).EntityListToModel(starEntity.Planets),
                PositionY = starEntity.CoordinateY,
                PositionX = starEntity.CoordinateX,
                RadiationLevel = starEntity.RadiationLevel,
                Radius = starEntity.Radius,
                StarColor = starEntity.StarColor.ToString(),
                StarType = starEntity.StarType.ToString(),
                SurfaceTemp = starEntity.SurfaceTemp,
                StarId = entity.Id
            };
        }

        public List<StarDto> EntityListToModel(ICollection<Star> entityList)
        {
            return entityList.Select(MapToDto).Select(dto => dto).Cast<StarDto>().ToList();
        }
    }
}
