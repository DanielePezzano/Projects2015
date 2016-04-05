using Models.Universe;
using System.Collections.Generic;
using System.Linq;
using SharedDto.Universe.Planets;

namespace SharedDto.DataMapper
{
    public sealed class PlanetEntityMapper
    {
        /// <summary>
        /// Map an entity to the correspondent DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public PlanetDto EntityToModel(Planet entity)
        {
            if (entity == null) return null;
            var planet = new PlanetDto();
            var buildingMapper = new BuildingEntityMapper();
            planet.ActivePopOnFoodProduction = entity.SatelliteProduction.ActivePopOnFoodProduction;
            planet.ActivePopOnOreProduction = entity.SatelliteProduction.ActivePopOnOreProduction;
            planet.ActivePopOnResProduction = entity.SatelliteProduction.ActivePopOnResProduction;
            planet.AtmospherePresent = entity.AtmospherePresent;
            planet.Buildings = buildingMapper.EntityListToModel(entity.Buildings);
            planet.DistanceR = entity.Orbit.DistanceR;
            planet.Eccentricity = entity.Orbit.Eccentricity;
            planet.FoodProduction = entity.SatelliteProduction.FoodProduction;
            planet.GravityEarthCompared = entity.GravityEarthCompared;
            planet.GroundRadiatedSpaces = entity.Spaces.GroundRadiatedSpaces;
            planet.GroundSpaces = entity.Spaces.GroundSpaces;
            planet.GroundSpacesLeft = entity.Spaces.GroudSpacesLeft;
            planet.GroundUsedSpaces = entity.Spaces.GroudUsedSpaces;
            planet.HabitableSpaces = entity.Spaces.HabitableSpaces;
            planet.Id = entity.Id;
            planet.Mass = entity.Mass;
            planet.MaxPopulation = entity.MaxPopulation;
            planet.Name = entity.Name;
            planet.OreProduction = entity.SatelliteProduction.OreProduction;
            planet.PeriodOfRevolution = entity.Orbit.PeriodOfRevolution;
            planet.PeriodOfRotation = entity.Orbit.PeriodOfRotation;
            planet.Population = entity.SatelliteSocial.Population;
            planet.RadiationLevel = entity.RadiationLevel;
            planet.Radius = entity.Radius;
            planet.ResearchPointProduction = entity.SatelliteProduction.ResearchPointProduction;
            planet.RingsPresent = entity.RingsPresent;
            planet.Status = entity.SatelliteStatus;
            planet.SurfaceTemp = entity.SurfaceTemp;
            planet.TaxLevel = entity.SatelliteSocial.TaxLevel;
            planet.TetaZero = entity.Orbit.TetaZero;
            planet.Totalspaces = entity.Spaces.Totalspaces;
            planet.WaterRadiatedSpaces = entity.Spaces.WaterRadiatedSpaces;
            planet.WaterSpaces = entity.Spaces.WaterSpaces;
            planet.WaterSpacesLeft = entity.Spaces.WaterSpacesLeft;
            planet.WaterUsedSpaces = entity.Spaces.WaterUsedSpaces;
            planet.UserId = entity.User?.Id;
            planet.LastUpdateResearcDateTime = entity.SatelliteProduction.LastResearchUpdateTime;
            planet.LastUpdateFoodProduction = entity.SatelliteProduction.LastFoodUpDateTime;
            planet.LastUpdateOreProduction = entity.SatelliteProduction.LastOreUpdateTime;
            planet.LastUpdatePopDateTime = entity.SatelliteSocial.LastPopulationUpdate;
            return planet;
        }

        /// <summary>
        /// Map an entity List to the correspondent DTO List
        /// </summary>
        /// <returns></returns>
        public List<PlanetDto> EntityListToModel(ICollection<Planet> items)
        {
            return items.Select(EntityToModel).ToList();
        }
    }
}
