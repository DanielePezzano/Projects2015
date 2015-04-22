using _2015ProjectsBackEndWs.DataMapper.Interface;
using _2015ProjectsBackEndWs.DTO;
using _2015ProjectsBackEndWs.DTO.Universe;
using Models.Universe;
using System.Collections.Generic;

namespace _2015ProjectsBackEndWs.DataMapper
{
    public class PlanetEntityMapper : IDataMapper<Planet>
    {
        /// <summary>
        /// Map an entity to the correspondent DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BaseDto<Planet> EntityToModel(Planet entity)
        {
            return new PlanetDto(entity);
        }

        /// <summary>
        /// Map an entity List to the correspondent DTO List
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<PlanetDto> EntityListToModel(List<Planet> items)
        {
            List<PlanetDto> result = new List<PlanetDto>();
            foreach (Planet item in items)
            {
                result.Add((PlanetDto)EntityToModel(item));
            }
            return result;
        }
    }
}