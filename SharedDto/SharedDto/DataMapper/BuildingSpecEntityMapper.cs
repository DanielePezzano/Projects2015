using Models.Buildings;
using System.Collections.Generic;
using System.Linq;
using SharedDto.Universe.Building;

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
            var result = new BuildingSpecsDto
            {
                Bonus = entity.Bonus,
                Value = entity.Value
            };
            return result;
        }

        /// <summary>
        /// Map an entity List to the correspondent DTO List
        /// </summary>
        /// <returns></returns>
        public List<BuildingSpecsDto> EntityListToModel(ICollection<BuildingSpec> items)
        {
            return items.Select(EntityToModel).ToList();
        }
    }
}
