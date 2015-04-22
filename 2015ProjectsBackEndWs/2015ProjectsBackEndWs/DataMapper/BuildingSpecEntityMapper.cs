using _2015ProjectsBackEndWs.DataMapper.Interface;
using _2015ProjectsBackEndWs.DTO;
using _2015ProjectsBackEndWs.DTO.Universe;
using Models.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2015ProjectsBackEndWs.DataMapper
{
    public class BuildingSpecEntityMapper : IDataMapper<BuildingSpec>
    {
        /// <summary>
        /// Map an entity to the correspondent DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BaseDto<BuildingSpec> EntityToModel(BuildingSpec entity)
        {
            return new BuildingSpecsDto(entity);
        }

        /// <summary>
        /// Map an entity List to the correspondent DTO List
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<BuildingSpecsDto> EntityListToModel(List<BuildingSpec> items)
        {
            List<BuildingSpecsDto> result = new List<BuildingSpecsDto>();
            foreach (BuildingSpec item in items)
            {
                result.Add((BuildingSpecsDto)EntityToModel(item));
            }
            return result;
        }
    }
}