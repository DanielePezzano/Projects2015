using _2015ProjectsBackEndWs.DTO;
using _2015ProjectsBackEndWs.DTO.Universe;
using Models.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2015ProjectsBackEndWs.DataMapper
{
    public class BuildingEntityMapper
    {
        /// <summary>
        /// Map an entity to the correspondent DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BuildingDto EntityToModel(Building entity)
        {
            BuildingDto result = new BuildingDto();
            BuildingSpecEntityMapper mapper = new BuildingSpecEntityMapper();
            result.BuildingType = entity.BuildingType;
            result.Description = entity.Description;
            result.Details = mapper.EntityListToModel(entity.BuildingSpecs);
            result.Id = entity.Id;
            result.MoneyCost = entity.MoneyCost;
            result.MoneyMaintenanceCost = entity.MoneyMaintenanceCost;
            result.Name = entity.Name;
            result.Number = entity.Number;
            result.OreCost = entity.OreCost;
            result.OreMaintenanceCost = entity.OreMaintenanceCost;
            result.SpaceNeeded = entity.SpaceNeeded;
            result.UsedSpaces = entity.UsedSpaces;            
            return result;
        }

        /// <summary>
        /// Map an entity List to the correspondent DTO List
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<BuildingDto> EntityListToModel(ICollection<Building> items)
        {
            List<BuildingDto> result = new List<BuildingDto>();
            foreach (Building item in items)
            {
                result.Add(EntityToModel(item));
            }
            return result;
        }
    }
}