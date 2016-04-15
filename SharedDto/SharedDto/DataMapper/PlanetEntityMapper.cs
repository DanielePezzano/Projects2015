using Models.Universe;
using System.Collections.Generic;
using System.Linq;
using SharedDto.Universe.Planets;

namespace SharedDto.DataMapper
{
    public static class PlanetEntityMapper
    {
        /// <summary>
        /// Map an entity to the correspondent DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static PlanetDto EntityToModel(Planet entity)
        {
            return new PlanetDto()
            {
                ActivePopOnFoodProduction = entity.SatelliteProduction.ActivePopOnFoodProduction,
                ActivePopOnOreProduction = entity.SatelliteProduction.ActivePopOnOreProduction,
                ActivePopOnResProduction = entity.SatelliteProduction.ActivePopOnResProduction,
                AtmospherePresent = entity.AtmospherePresent,
                Buildings = BuildingEntityMapper.EntityListToModel(entity.Buildings),
                DistanceR = entity.Orbit.DistanceR,
                Eccentricity = entity.Orbit.Eccentricity,
                FoodProduction = entity.SatelliteProduction.FoodProduction,
                GravityEarthCompared = entity.GravityEarthCompared,
                GroundRadiatedSpaces = entity.Spaces.GroundRadiatedSpaces,
                GroundSpaces = entity.Spaces.GroundSpaces,
                GroundSpacesLeft = entity.Spaces.GroundSpacesLeft,
                GroundUsedSpaces = entity.Spaces.GroundUsedSpaces,
                HabitableSpaces = entity.Spaces.HabitableSpaces,
                Id = entity.Id,
                Mass = entity.Mass,
                MaxPopulation = entity.MaxPopulation,
                Name = entity.Name,
                OreProduction = entity.SatelliteProduction.OreProduction,
                PeriodOfRevolution = entity.Orbit.PeriodOfRevolution,
                PeriodOfRotation = entity.Orbit.PeriodOfRotation,
                Population = entity.SatelliteSocial.Population,
                RadiationLevel = entity.RadiationLevel,
                Radius = entity.Radius,
                ResearchPointProduction = entity.SatelliteProduction.ResearchPointProduction,
                RingsPresent = entity.RingsPresent,
                Status = entity.SatelliteStatus,
                SurfaceTemp = entity.SurfaceTemp,
                TaxLevel = entity.SatelliteSocial.TaxLevel,
                TetaZero = entity.Orbit.TetaZero,
                Totalspaces = entity.Spaces.Totalspaces,
                WaterRadiatedSpaces = entity.Spaces.WaterRadiatedSpaces,
                WaterSpaces = entity.Spaces.WaterSpaces,
                WaterSpacesLeft = entity.Spaces.WaterSpacesLeft,
                WaterUsedSpaces = entity.Spaces.WaterUsedSpaces,
                UserId = entity.User?.Id,
                LastUpdateResearcDateTime = entity.SatelliteProduction.LastResearchUpdateTime,
                LastUpdateFoodProduction = entity.SatelliteProduction.LastFoodUpDateTime,
                LastUpdateOreProduction = entity.SatelliteProduction.LastOreUpdateTime,
                LastUpdatePopDateTime = entity.SatelliteSocial.LastPopulationUpdate,
                LastMaintenanceDateTime = entity.SatelliteProduction.LastMaintenanceUpdateTime,
                LastIncomeRevenueTime = entity.SatelliteProduction.LastIncomeRevenueTime,
                StoredFood = entity.SatelliteProduction.StoredFood,
                StoredOre = entity.SatelliteProduction.StoredOre,
                PlanetIncomeBalance = entity.SatelliteProduction.TotalIncome,
                ResearchPoints = entity.SatelliteProduction.ResearchPoints
            };
        }

        /// <summary>
        /// Map an entity List to the correspondent DTO List
        /// </summary>
        /// <returns></returns>
        public static List<PlanetDto> EntityListToModel(ICollection<Planet> items)
        {
            return items.Select(EntityToModel).ToList();
        }
    }
}
