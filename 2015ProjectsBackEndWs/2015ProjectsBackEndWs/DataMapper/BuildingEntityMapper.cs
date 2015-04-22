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
    public class BuildingEntityMapper : IDataMapper<Building>
    {
        /// <summary>
        /// Map an entity to the correspondent DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BaseDto<Building> EntityToModel(Building entity)
        {
            return new BuildingDto(entity);
        }

        /// <summary>
        /// Map an entity List to the correspondent DTO List
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<BuildingDto> EntityListToModel(List<Building> items)
        {
            List<BuildingDto> result = new List<BuildingDto>();
            foreach (Building item in items)
            {
                result.Add((BuildingDto)EntityToModel(item));
            }
            return result;
        }
    }
}