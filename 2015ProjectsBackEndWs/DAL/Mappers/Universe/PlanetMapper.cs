using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Interfaces;
using DAL.Mappers.Universe.Enums;
using DAL.Mappers.Universe.IstanceFactory;
using DAL.Operations.Enums;
using DAL.Operations.IstanceFactory;
using Models.Base;
using Models.Universe;
using SharedDto.Interfaces;
using SharedDto.Universe.Planets;

namespace DAL.Mappers.Universe
{
    public class PlanetMapper : BaseMapper,  IMapToDto,IMapToEntity
    {
        public PlanetMapper(OpFactory operations):base(operations)
        {
            
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            var planetDto = (PlanetDto)dto;
            Entity = new Planet()
            {
                SatelliteProduction = new Production()
                {
                    StoredFood = planetDto.StoredFood,
                    StoredOre = planetDto.StoredOre,
                    FoodProduction = planetDto.FoodProduction,
                    LastOreUpdateTime = planetDto.LastUpdateOreProduction,
                    OreProduction = planetDto.OreProduction,
                    LastFoodUpDateTime = planetDto.LastUpdateFoodProduction,
                    LastMaintenanceUpdateTime = planetDto.LastMaintenanceDateTime,
                    ActivePopOnFoodProduction = planetDto.ActivePopOnFoodProduction,
                    ActivePopOnOreProduction = planetDto.ActivePopOnOreProduction,
                    ActivePopOnResProduction = planetDto.ActivePopOnResProduction,
                    LastIncomeRevenueTime = planetDto.LastIncomeRevenueTime,
                    ResearchPoints = planetDto.ResearchPoints,
                    LastResearchUpdateTime = planetDto.LastUpdateResearcDateTime,
                    TotalIncome = planetDto.PlanetIncomeBalance,
                    ResearchPointProduction = planetDto.ResearchPointProduction
                },
                AtmospherePresent = planetDto.AtmospherePresent,
                IsGaseous = planetDto.IsGaseous,
                IsHabitable = planetDto.IsHabitable,
                IsHomePlanet = planetDto.IsHomePlanet,
                Mass = planetDto.Mass,
                Name = planetDto.Name,
                Orbit = new OrbitDetail()
                {
                    TetaZero = planetDto.TetaZero,
                    PeriodOfRotation = planetDto.PeriodOfRotation,
                    Eccentricity = planetDto.Eccentricity,
                    PeriodOfRevolution = planetDto.PeriodOfRevolution,
                    DistanceR = planetDto.DistanceR
                },
                RadiationLevel = planetDto.RadiationLevel,
                Id = planetDto.Id,
                Radius = planetDto.Radius,
                RingsPresent = planetDto.RingsPresent,
                SatelliteSocial = new SatelliteSocials()
                {
                    Population = planetDto.Population,
                    TaxLevel = planetDto.TaxLevel,
                    LastPopulationUpdate = planetDto.LastUpdatePopDateTime
                },
                Satellites = ModelListToSatellites(planetDto.Satellites),
                Spaces = new Spaces()
                {
                    WaterRadiatedSpaces = planetDto.WaterRadiatedSpaces,
                    WaterSpaces = planetDto.WaterSpaces,
                    GroundSpaces = planetDto.GroundSpaces,
                    WaterUsedSpaces = planetDto.WaterUsedSpaces,
                    GroundUsedSpaces = planetDto.GroundUsedSpaces,
                    GroundRadiatedSpaces = planetDto.GroundRadiatedSpaces
                    
                },
                UpdatedAt = DateTime.Now,
                CreatedAt = planetDto.CreatedAt,
                SurfaceTemp = planetDto.SurfaceTemp,
                SatelliteStatus = planetDto.Status,
                Buildings = ((BuildingMapper)MapperFactory.RetrieveMapper(Operations,UniverseMapperTypes.Buildings)).ModelListToEntity(planetDto.Buildings),
            };

            return Entity;
        }

        public BaseEntity MapSatelliteToEntity(IDto dto)
        {
            var planetDto = (PlanetDto)dto;
            return new Satellite()
            {
                SatelliteProduction = new Production()
                {
                    StoredFood = planetDto.StoredFood,
                    StoredOre = planetDto.StoredOre,
                    FoodProduction = planetDto.FoodProduction,
                    LastOreUpdateTime = planetDto.LastUpdateOreProduction,
                    OreProduction = planetDto.OreProduction,
                    LastFoodUpDateTime = planetDto.LastUpdateFoodProduction,
                    LastMaintenanceUpdateTime = planetDto.LastMaintenanceDateTime,
                    ActivePopOnFoodProduction = planetDto.ActivePopOnFoodProduction,
                    ActivePopOnOreProduction = planetDto.ActivePopOnOreProduction,
                    ActivePopOnResProduction = planetDto.ActivePopOnResProduction,
                    LastIncomeRevenueTime = planetDto.LastIncomeRevenueTime,
                    ResearchPoints = planetDto.ResearchPoints,
                    LastResearchUpdateTime = planetDto.LastUpdateResearcDateTime,
                    TotalIncome = planetDto.PlanetIncomeBalance,
                    ResearchPointProduction = planetDto.ResearchPointProduction
                },
                AtmospherePresent = planetDto.AtmospherePresent,
                IsGaseous = planetDto.IsGaseous,
                IsHabitable = planetDto.IsHabitable,
                Mass = planetDto.Mass,
                Name = planetDto.Name,
                Orbit = new OrbitDetail()
                {
                    TetaZero = planetDto.TetaZero,
                    PeriodOfRotation = planetDto.PeriodOfRotation,
                    Eccentricity = planetDto.Eccentricity,
                    PeriodOfRevolution = planetDto.PeriodOfRevolution,
                    DistanceR = planetDto.DistanceR
                },
                RadiationLevel = planetDto.RadiationLevel,
                Id = planetDto.Id,
                Radius = planetDto.Radius,
                RingsPresent = planetDto.RingsPresent,
                SatelliteSocial = new SatelliteSocials()
                {
                    Population = planetDto.Population,
                    TaxLevel = planetDto.TaxLevel,
                    LastPopulationUpdate = planetDto.LastUpdatePopDateTime
                },
                Spaces = new Spaces()
                {
                    WaterRadiatedSpaces = planetDto.WaterRadiatedSpaces,
                    WaterSpaces = planetDto.WaterSpaces,
                    GroundSpaces = planetDto.GroundSpaces,
                    WaterUsedSpaces = planetDto.WaterUsedSpaces,
                    GroundUsedSpaces = planetDto.GroundUsedSpaces,
                    GroundRadiatedSpaces = planetDto.GroundRadiatedSpaces

                },
                UpdatedAt = DateTime.Now,
                CreatedAt = planetDto.CreatedAt,
                SurfaceTemp = planetDto.SurfaceTemp,
                SatelliteStatus = planetDto.Status,
                Buildings = ((BuildingMapper)MapperFactory.RetrieveMapper(Operations, UniverseMapperTypes.Buildings)).ModelListToEntity(planetDto.Buildings),
            };
        }

        public IDto MapSatelliteToDto(BaseEntity entity)
        {
            var planetEntity = (Satellite)entity;
            return new PlanetDto()
            {
                ActivePopOnFoodProduction = planetEntity.SatelliteProduction.ActivePopOnFoodProduction,
                ActivePopOnOreProduction = planetEntity.SatelliteProduction.ActivePopOnOreProduction,
                ActivePopOnResProduction = planetEntity.SatelliteProduction.ActivePopOnResProduction,
                AtmospherePresent = planetEntity.AtmospherePresent,
                Buildings = ((BuildingMapper)MapperFactory.RetrieveMapper(Operations, UniverseMapperTypes.Buildings)).EntityListToModel(planetEntity.Buildings),
                DistanceR = planetEntity.Orbit.DistanceR,
                Eccentricity = planetEntity.Orbit.Eccentricity,
                FoodProduction = planetEntity.SatelliteProduction.FoodProduction,
                GravityEarthCompared = planetEntity.GravityEarthCompared,
                GroundRadiatedSpaces = planetEntity.Spaces.GroundRadiatedSpaces,
                GroundSpaces = planetEntity.Spaces.GroundSpaces,
                GroundSpacesLeft = planetEntity.Spaces.GroundSpacesLeft,
                GroundUsedSpaces = planetEntity.Spaces.GroundUsedSpaces,
                HabitableSpaces = planetEntity.Spaces.HabitableSpaces,
                Id = planetEntity.Id,
                Mass = planetEntity.Mass,
                MaxPopulation = planetEntity.MaxPopulation,
                Name = planetEntity.Name,
                OreProduction = planetEntity.SatelliteProduction.OreProduction,
                PeriodOfRevolution = planetEntity.Orbit.PeriodOfRevolution,
                PeriodOfRotation = planetEntity.Orbit.PeriodOfRotation,
                Population = planetEntity.SatelliteSocial.Population,
                RadiationLevel = planetEntity.RadiationLevel,
                Radius = planetEntity.Radius,
                ResearchPointProduction = planetEntity.SatelliteProduction.ResearchPointProduction,
                RingsPresent = planetEntity.RingsPresent,
                Status = planetEntity.SatelliteStatus,
                SurfaceTemp = planetEntity.SurfaceTemp,
                TaxLevel = planetEntity.SatelliteSocial.TaxLevel,
                TetaZero = planetEntity.Orbit.TetaZero,
                Totalspaces = planetEntity.Spaces.Totalspaces,
                WaterRadiatedSpaces = planetEntity.Spaces.WaterRadiatedSpaces,
                WaterSpaces = planetEntity.Spaces.WaterSpaces,
                WaterSpacesLeft = planetEntity.Spaces.WaterSpacesLeft,
                WaterUsedSpaces = planetEntity.Spaces.WaterUsedSpaces,
                UserId = planetEntity.User?.Id,
                LastUpdateResearcDateTime = planetEntity.SatelliteProduction.LastResearchUpdateTime,
                LastUpdateFoodProduction = planetEntity.SatelliteProduction.LastFoodUpDateTime,
                LastUpdateOreProduction = planetEntity.SatelliteProduction.LastOreUpdateTime,
                LastUpdatePopDateTime = planetEntity.SatelliteSocial.LastPopulationUpdate,
                LastMaintenanceDateTime = planetEntity.SatelliteProduction.LastMaintenanceUpdateTime,
                LastIncomeRevenueTime = planetEntity.SatelliteProduction.LastIncomeRevenueTime,
                StoredFood = planetEntity.SatelliteProduction.StoredFood,
                StoredOre = planetEntity.SatelliteProduction.StoredOre,
                PlanetIncomeBalance = planetEntity.SatelliteProduction.TotalIncome,
                ResearchPoints = planetEntity.SatelliteProduction.ResearchPoints,
                IsGaseous = planetEntity.IsGaseous,
                IsHabitable = planetEntity.IsHabitable,
                CreatedAt = planetEntity.CreatedAt
            };
        }

        public IDto MapToDto(BaseEntity entity)
        {
            var planetEntity = (Planet) entity;
            return new PlanetDto()
            {
               ActivePopOnFoodProduction = planetEntity.SatelliteProduction.ActivePopOnFoodProduction,
                ActivePopOnOreProduction = planetEntity.SatelliteProduction.ActivePopOnOreProduction,
                ActivePopOnResProduction = planetEntity.SatelliteProduction.ActivePopOnResProduction,
                AtmospherePresent = planetEntity.AtmospherePresent,
                Buildings = ((BuildingMapper)MapperFactory.RetrieveMapper(Operations,UniverseMapperTypes.Buildings)).EntityListToModel(planetEntity.Buildings),
                DistanceR = planetEntity.Orbit.DistanceR,
                Eccentricity = planetEntity.Orbit.Eccentricity,
                FoodProduction = planetEntity.SatelliteProduction.FoodProduction,
                GravityEarthCompared = planetEntity.GravityEarthCompared,
                GroundRadiatedSpaces = planetEntity.Spaces.GroundRadiatedSpaces,
                GroundSpaces = planetEntity.Spaces.GroundSpaces,
                GroundSpacesLeft = planetEntity.Spaces.GroundSpacesLeft,
                GroundUsedSpaces = planetEntity.Spaces.GroundUsedSpaces,
                HabitableSpaces = planetEntity.Spaces.HabitableSpaces,
                Id = planetEntity.Id,
                Mass = planetEntity.Mass,
                MaxPopulation = planetEntity.MaxPopulation,
                Name = planetEntity.Name,
                OreProduction = planetEntity.SatelliteProduction.OreProduction,
                PeriodOfRevolution = planetEntity.Orbit.PeriodOfRevolution,
                PeriodOfRotation = planetEntity.Orbit.PeriodOfRotation,
                Population = planetEntity.SatelliteSocial.Population,
                RadiationLevel = planetEntity.RadiationLevel,
                Radius = planetEntity.Radius,
                ResearchPointProduction = planetEntity.SatelliteProduction.ResearchPointProduction,
                RingsPresent = planetEntity.RingsPresent,
                Status = planetEntity.SatelliteStatus,
                SurfaceTemp = planetEntity.SurfaceTemp,
                TaxLevel = planetEntity.SatelliteSocial.TaxLevel,
                TetaZero = planetEntity.Orbit.TetaZero,
                Totalspaces = planetEntity.Spaces.Totalspaces,
                WaterRadiatedSpaces = planetEntity.Spaces.WaterRadiatedSpaces,
                WaterSpaces = planetEntity.Spaces.WaterSpaces,
                WaterSpacesLeft = planetEntity.Spaces.WaterSpacesLeft,
                WaterUsedSpaces = planetEntity.Spaces.WaterUsedSpaces,
                UserId = planetEntity.User?.Id,
                LastUpdateResearcDateTime = planetEntity.SatelliteProduction.LastResearchUpdateTime,
                LastUpdateFoodProduction = planetEntity.SatelliteProduction.LastFoodUpDateTime,
                LastUpdateOreProduction = planetEntity.SatelliteProduction.LastOreUpdateTime,
                LastUpdatePopDateTime = planetEntity.SatelliteSocial.LastPopulationUpdate,
                LastMaintenanceDateTime = planetEntity.SatelliteProduction.LastMaintenanceUpdateTime,
                LastIncomeRevenueTime = planetEntity.SatelliteProduction.LastIncomeRevenueTime,
                StoredFood = planetEntity.SatelliteProduction.StoredFood,
                StoredOre = planetEntity.SatelliteProduction.StoredOre,
                PlanetIncomeBalance = planetEntity.SatelliteProduction.TotalIncome,
                ResearchPoints = planetEntity.SatelliteProduction.ResearchPoints,
                IsGaseous = planetEntity.IsGaseous,
                IsHabitable = planetEntity.IsHabitable,
                IsHomePlanet = planetEntity.IsHomePlanet,
                Satellites = EntityListToSatelliteModel(planetEntity.Satellites),
                CreatedAt = planetEntity.CreatedAt
            };
        }

        public override bool ExistsEntity()
        {
            var cacheKey = $"COUNTPLANET{Entity.Id}";
             return
                Operations.SetOperation(MappedRepositories.PlanetRepository, MappedOperations.Any, cacheKey,
                    Entity.Id).CheckResult;
        }

        public List<PlanetDto> EntityListToModel(ICollection<Planet> entityList)
        {
            return entityList.Select(MapToDto).Select(dto => dto).Cast<PlanetDto>().ToList();
        }

        public List<Planet> ModelListToEntity(List<PlanetDto> entityList)
        {
            return entityList.Select(MapToEntity).Select(dto => dto).Cast<Planet>().ToList();
        }

        public List<Satellite> ModelListToSatellites(List<PlanetDto> entityList)
        {
            return entityList.Select(MapSatelliteToEntity).Select(dto => dto).Cast<Satellite>().ToList();
        }

        public List<PlanetDto> EntityListToSatelliteModel(ICollection<Satellite> entityList)
        {
            return entityList.Select(MapSatelliteToDto).Select(dto => dto).Cast<PlanetDto>().ToList();
        }
    }
}