using Models.Buildings;
using System.Collections.Generic;

namespace SharedDto.DataMapper
{
    public sealed class BuildingSpecEntityMapper
    {
        /// <summary>
        /// Map an entity to the correspondent DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BuildingSpecsDto EntityToModel(BuildingSpec entity)
        {
            if (entity == null) return null;
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
