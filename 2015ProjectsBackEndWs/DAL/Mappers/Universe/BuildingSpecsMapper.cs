using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Interfaces;
using Models.Base;
using Models.Buildings;
using SharedDto.Interfaces;
using SharedDto.Universe.Building;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Interfaces.UnitOfWork;
namespace DAL.Mappers.Universe
{
    public class BuildingSpecsMapper : BaseMapper, IMapper
    {
        public BuildingSpecsMapper(IUnitOfWork uow, bool isTest) : base(uow, isTest)
        {
        }

        public override bool ExistsEntity()
        {
            return (IsTest)
                ? ((TestUow) UnitOfWork)?.BuildingSpecRepository.Count(c => c.Id == Entity.Id,"COUNTBUILDINGSPECS"+Entity.Id) > 0
                : ((ProductionUow) UnitOfWork)?.BuildingSpecRepository.Count(c => c.Id == Entity.Id,"COUNTBUILDINGSPECS"+Entity.Id) > 0;
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            var specsDto = (BuildingSpecsDto) dto;

            Entity = new BuildingSpec()
            {
                Bonus = specsDto.Bonus,
                Value = specsDto.Value,
                UpdatedAt = DateTime.Now
            };

            return Entity;
        }

        public IDto MapToDto(BaseEntity entity)
        {
            var specsEntity = (BuildingSpec) entity;
            return new BuildingSpecsDto()
            {
                Bonus = specsEntity.Bonus,
                Value = specsEntity.Value
            };
        }

        public List<BuildingSpecsDto> EntityListToModel(ICollection<BuildingSpec> entityList)
        {
            return entityList.Select(MapToDto).Select(dto => dto).Cast<BuildingSpecsDto>().ToList();
        }

        public List<BuildingSpec> ModelListToEntity(List<BuildingSpecsDto> entityList)
        {
            return entityList.Select(MapToEntity).Select(dto => dto).Cast<BuildingSpec>().ToList();
        }
    }
}