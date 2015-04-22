using _2015ProjectsBackEndWs.DTO;
using _2015ProjectsBackEndWs.DTO.Universe;
using Models.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2015ProjectsBackEndWs.DataMapper
{
    public class BuildingSpecEntityMapper 
    {
        /// <summary>
        /// Map an entity to the correspondent DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BuildingSpecsDto EntityToModel(BuildingSpec entity)
        {
            BuildingSpecsDto result = new BuildingSpecsDto();
            result.Bonus = entity.Bonus;
            result.Value = entity.Value;
            return result;
        }

        /// <summary>
        /// Map an entity List to the correspondent DTO List
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<BuildingSpecsDto> EntityListToModel(ICollection<BuildingSpec> items)
        {
            List<BuildingSpecsDto> result = new List<BuildingSpecsDto>();
            foreach (BuildingSpec item in items)
            {
                result.Add(EntityToModel(item));
            }
            return result;
        }
    }
}