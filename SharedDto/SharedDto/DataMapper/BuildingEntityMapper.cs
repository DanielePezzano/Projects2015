using System.Collections.Generic;
using System.Linq;
using Models.Buildings;
using SharedDto.Universe.Building;

namespace SharedDto.DataMapper
{
    public static class BuildingEntityMapper
    {
        /// <summary>
        ///     Map an entity to the correspondent DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static BuildingDto EntityToModel(Building entity)
        {
            
            return new BuildingDto()
            {
                BuildingType = entity.BuildingType,
                Description = entity.Description,
                Details = BuildingSpecEntityMapper.EntityListToModel(entity.BuildingSpecs),
                Id = entity.Id,
                MoneyCost = entity.MoneyCost,
                MoneyMaintenanceCost = entity.MoneyMaintenanceCost,
                Name = entity.Name,
                Number = entity.Number,
                OreCost = entity.OreCost,
                OreMaintenanceCost = entity.OreMaintenanceCost,
                SpaceNeeded = entity.SpaceNeeded,
                UsedSpaces = entity.UsedSpaces,
                PlanetId = entity.Planet.Id
            };
        }

        /// <summary>
        ///     Map an entity List to the correspondent DTO List
        /// </summary>
        /// <returns></returns>
        public static List<BuildingDto> EntityListToModel(ICollection<Building> items)
        {
            return items.Select(EntityToModel).ToList();
        }
    }
}