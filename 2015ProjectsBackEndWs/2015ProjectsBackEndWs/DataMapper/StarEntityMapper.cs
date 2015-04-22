using _2015ProjectsBackEndWs.DataMapper.Interface;
using _2015ProjectsBackEndWs.DTO;
using _2015ProjectsBackEndWs.DTO.Universe;
using Models.Universe;
using System;
using System.Collections.Generic;

namespace _2015ProjectsBackEndWs.DataMapper
{
    public class StarEntityMapper : IDataMapper<Star>
    {
        /// <summary>
        /// Map an entity to the correspondent DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BaseDto<Star> EntityToModel(Star entity)
        {
            return new StarDto(entity);
        }
        /// <summary>
        /// Map an entity List to the correspondent DTO List
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<StarDto> EntityListToModel(List<Star> stars)
        {
            List<StarDto> result = new List<StarDto>();
            foreach (Star star in stars)
            {
                result.Add((StarDto)EntityToModel(star));
            }
            return result;
        }
    }
}