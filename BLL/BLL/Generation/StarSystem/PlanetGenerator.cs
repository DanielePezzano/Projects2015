using System;
using System.Collections.Generic;
using BLL.Utilities;
using BLL.Utilities.Structs;
using Models.Base;
using Models.Base.Enum;
using Models.Buildings;
using Models.Universe;
using Models.Universe.Enum;

namespace BLL.Generation.StarSystem
{
    public sealed class PlanetGenerator : IDisposable
    {
        private readonly PlanetCustomConditions _conditions;

        private double _mediumDensity = 5.5;
            //densità media terrestre --> se densità calcolata <=3 probabilmente è gassoso        

        private DoubleRange _satelliteCloseRange = new DoubleRange(PlanetProperties.MinSatelliteDistance,
            PlanetProperties.MaxSatelliteDistance);

        private Star _star;
        private bool _disposed;

        public PlanetGenerator(Star star, PlanetCustomConditions conditions)
        {
            _star = star;
            _conditions = conditions;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                _satelliteCloseRange = new DoubleRange();
                _star = null;
            }
            //GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Calculate Radiation Level
        /// </summary>
        /// <param name="atmospherePresent"></param>
        /// <param name="starRadiation"></param>
        /// <param name="distance"></param>
        /// <param name="forceLiving"></param>
        /// <param name="fixedDistance"></param>
        /// <returns></returns>
        private static int CalculateRadiationLevel(bool atmospherePresent, int starRadiation, double distance, bool forceLiving,
            double fixedDistance)
        {
            if (forceLiving) return 1;
            var result = 1;
            if (distance <= fixedDistance)
            {
                result += starRadiation - (int) (starRadiation*(fixedDistance - distance));
            }
            else
            {
                var temp = starRadiation - (int) (starRadiation*(distance - fixedDistance));
                result += (temp > 0) ? temp : 3;
            }
            return (atmospherePresent) ? result/2 : result;
        }

        /// <summary>
        ///     Determine if an atmosphere is present
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="rnd"></param>
        /// <returns></returns>
        private bool IsAtmospherePresent(double distance, Random rnd)
        {
            if (_conditions.ForceLiving) return true;
            if (distance >= BasicConstants.MinAtmosphereDistance)
            {
                return (RandomNumbers.RandomInt(0, 100, rnd) <= 30);
            }
            return false;
        }

        /// <summary>
        ///     Determine Water Presence
        /// </summary>
        /// <param name="hasAtmosphere"></param>
        /// <param name="forcewater"></param>
        /// <param name="isSatellite"></param>
        /// <param name="rnd"></param>
        /// <returns></returns>
        private bool IsWaterPresent(bool hasAtmosphere, bool forcewater, bool isSatellite, Random rnd)
        {
            if (forcewater) return true;
            if (hasAtmosphere)
            {
                if (!isSatellite) return (RandomNumbers.RandomInt(0, 10, rnd) <= 1);
                return (RandomNumbers.RandomInt(0, 100, rnd) <= 1);
            }
            return false;
        }

        /// <summary>
        ///     It Calculate the Planet Mass: the closer to sun, the higher prob of lesser mass
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="closeRange"></param>
        /// <param name="forceliving"></param>
        /// <param name="scaleMaxClose"></param>
        /// <param name="scaleMaxMed"></param>
        /// <param name="scaleMaxGreatest"></param>
        /// <param name="rnd"></param>
        /// <returns></returns>
        private double CalculateMass(double distance, DoubleRange closeRange, bool forceliving, double scaleMaxClose,
            double scaleMaxMed, double scaleMaxGreatest, Random rnd)
        {
            if (forceliving) return 0.8 + RandomNumbers.RandomDouble(0.1, 1, rnd);
            double result;
            var convertScaleMax = scaleMaxClose;
            if (distance > closeRange.Max)
            {
                if (distance > closeRange.Max && distance <= (BasicConstants.EarthDistance + 2))
                    convertScaleMax = scaleMaxMed;
                else
                    convertScaleMax = scaleMaxGreatest;
            }

            using (var scale = new ScaleConversion(10, convertScaleMax))
            {
                result = scale.Convert(RandomNumbers.RandomInt(1, 10, rnd));
            }

            result = Math.Truncate(result*100)/100;
            if (Math.Abs(result) < 0.001) result = scaleMaxClose;
            return result;
        }

        /// <summary>
        ///     Calculate total Spaces for the planet
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="density"></param>
        /// <param name="isGasseous"></param>
        /// <returns></returns>
        private static int AssignTotalSpaces(double mass, double density, bool isGasseous)
        {
            if (isGasseous) return 0;
            double result;
            if (density >= BasicConstants.MinDensityForGas)
                result = 100*(mass*density)/BasicConstants.EarthDensity;
            else
            {
                result = (density >= 1)
                    ? 10*mass/(Math.Pow(BasicConstants.EarthDensity*density, 2))
                    : 10*mass*Math.Pow(density, 10)/BasicConstants.EarthDensity;
            }
            if ((int) result > 250) return 250;
            return (int) result;
        }

        /// <summary>
        ///     if density is lesser than a minimum there's a high probability of gas planet
        /// </summary>
        /// <param name="density"></param>
        /// <param name="isSatellite"></param>
        /// <param name="rnd"></param>
        /// <param name="atmospherePresent"></param>
        /// <returns></returns>
        private bool IsGasseous(double density, bool isSatellite, Random rnd, bool atmospherePresent)
        {
            if (isSatellite || !atmospherePresent) return false;
            if (density <= 1) return true;
            var result = false;
            var perc = 0;
            if (density > 2 && density <= BasicConstants.MinDensityForGas) perc = 20;
            if (density > 1 && density <= 2) perc = 70;
            if (RandomNumbers.RandomInt(0, 100, rnd) <= perc) result = true;
            return result;
        }

        /// <summary>
        ///     set the medium density
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="rnd"></param>
        /// <returns></returns>
        private static double SetMediumDensity(double mass, Random rnd)
        {
            double res;
            if (mass >= BasicConstants.EarthMass)
                res = RandomNumbers.RandomDouble(BasicConstants.MinDensity, BasicConstants.MaxDensity, rnd);
            else
                res = RandomNumbers.RandomDouble(BasicConstants.MinDensityForGas, (BasicConstants.EarthDensity + 1),
                    rnd);
            return res;
        }

        /// <summary>
        ///     DEtermine number of satellites
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="rnd"></param>
        /// <param name="satelliteFactor"></param>
        /// <returns></returns>
        private int NumberOfSatellite(double mass, Random rnd, double satelliteFactor = 0.1)
        {
            var maxNumb = 0;
            if (mass < BasicConstants.MinMassForSatellite) return maxNumb;

            if (mass >= BasicConstants.MinMassForSatellite && mass <= (2*BasicConstants.EarthMass)) maxNumb = 2;
            if (mass > (2*BasicConstants.EarthMass)) maxNumb = (int) (satelliteFactor*mass);
            return RandomNumbers.RandomInt(0, maxNumb, rnd);
        }

        /// <summary>
        ///     This will calculate the radius based on mass
        ///     Radius is Moon-compared
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="density"></param>
        /// <param name="isSatellite"></param>
        /// <returns></returns>
        private static double CalculateRadius(double mass, double density, bool isSatellite = false)
        {
            if (!isSatellite) return Math.Truncate((mass/density)*100)/100;

            return Math.Truncate((density/(mass*BasicConstants.LunarVolumeFactor))*100)/100;
        }

        /// <summary>
        ///     Generate N satellites for this planet
        /// </summary>
        /// <param name="planet"></param>
        /// <param name="generator"></param>
        /// <param name="satellites"></param>
        /// <param name="rnd"></param>
        private void GenerateSatellites(Planet planet, OrbitGenerator generator, int satellites, Random rnd)
        {
            for (var x = 0; x < satellites; x++)
            {
                var toAdd = CreateSatellite(planet);
                if (toAdd != null)
                {
                    CompleteSatelliteGeneration(toAdd, generator, planet.Orbit.DistanceR, rnd);
                    planet.Satellites.Add(toAdd);
                }
            }
        }

        /// <summary>
        ///     Determine the surface temperature
        /// </summary>
        /// <returns></returns>
        private static int AssignSurfaceTemperature(double distance, bool atmpspherePresent, int starTemperature, Random rnd)
        {
            int result;
            var temp = (starTemperature - (starTemperature*distance))/7.095;
            if (atmpspherePresent) result = (int) (temp - (temp*0.5));
            else result = (int) temp;

            if (result >= 100) return result;
            result = RandomNumbers.RandomInt(100, !atmpspherePresent ? 273 : 500, rnd);

            return result;
        }

        /// <summary>
        ///     This generate a completly new planet
        /// </summary>
        /// <returns></returns>
        public Planet CreateBrandNewPlanet(Random rnd)
        {
            var planet = new Planet
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Star = _star,
                Name = "PL - " + DateTime.Now.ToFileTimeUtc()
            };
            _mediumDensity = (_conditions.ForceLiving)
                ? BasicConstants.EarthDensity + RandomNumbers.RandomDouble(-0.5, 0.5, rnd)
                : RandomNumbers.RandomDouble(BasicConstants.MinDensity, BasicConstants.MaxDensity, rnd);
            planet.Buildings = new List<Building>();
            planet.SatelliteSocial = new SatelliteSocials {Population = 0, TaxLevel = TaxLevel.Normal};
            planet.User = null;
            planet.RingsPresent = (RandomNumbers.RandomInt(0, 100, rnd) == 0);
            planet.Satellites = new List<Satellite>();
            return planet;
        }

        /// <summary>
        ///     this will generate a satellite for this planet
        /// </summary>
        /// <param name="planet"></param>
        /// <returns></returns>
        public Satellite CreateSatellite(Planet planet)
        {
            var satellite = new Satellite
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Planet = planet,
                Name = "STL - " + DateTime.Now.ToFileTimeUtc(),
                Buildings = new List<Building>(),
                SatelliteSocial = new SatelliteSocials {Population = 0, TaxLevel = TaxLevel.Normal},
                User = null,
                RingsPresent = false
            };

            return satellite;
        }

        /// <summary>
        ///     Complete Satellite's generation process
        /// </summary>
        /// <param name="satellite"></param>
        /// <param name="generator"></param>
        /// <param name="planetDistance"></param>
        /// <param name="rnd"></param>
        public void CompleteSatelliteGeneration(Satellite satellite, OrbitGenerator generator, double planetDistance,
            Random rnd)
        {
            var satelliteConditions = new PlanetCustomConditions();

            satellite.Orbit = generator.GenerateSatellite(rnd);
            satellite.AtmospherePresent = IsAtmospherePresent(satellite.Orbit.DistanceR, rnd);
            satellite.RadiationLevel = CalculateRadiationLevel(satellite.AtmospherePresent, _star.RadiationLevel,
                planetDistance, false, BasicConstants.EarthDistance);
            satellite.Mass = CalculateMass(satellite.Orbit.DistanceR, _satelliteCloseRange,
                satelliteConditions.ForceLiving, BasicConstants.SatelliteMinCloseScale,
                BasicConstants.SatelliteMedCloseScale, BasicConstants.SatelliteMaxCloseScale, rnd);
            _mediumDensity = RandomNumbers.RandomDouble(BasicConstants.MinDensityForGas,
                (BasicConstants.EarthDensity + 1), rnd);
            satellite.SurfaceTemp = AssignSurfaceTemperature(planetDistance, satellite.AtmospherePresent,
                _star.SurfaceTemp, rnd);
            satellite.Radius = CalculateRadius(satellite.Mass, _mediumDensity, true);
            satellite.Orbit.PeriodOfRotation = generator.CalculatePeriodOfRotation(satellite.Orbit.DistanceR,
                satellite.Orbit.PeriodOfRevolution, _mediumDensity, rnd);

            var totalSpaces = AssignTotalSpaces(satellite.Mass, _mediumDensity, false);
            satellite.Spaces = PlanetProperties.CalculateSpaces(
                totalSpaces,
                satellite.RadiationLevel,
                satelliteConditions,
                IsWaterPresent(satellite.AtmospherePresent, satelliteConditions.ForceWater, true, rnd),
                satellite.AtmospherePresent,
                rnd);

            satelliteConditions.MineralRich = (RandomNumbers.RandomInt(0, 100, rnd) == 0);
            satelliteConditions.MineralPoor = (!satelliteConditions.MineralRich &&
                                               RandomNumbers.RandomInt(0, 100, rnd) <= 20);

            satellite.SatelliteProduction = PlanetProperties.CalculateProduction(satellite.Spaces, _mediumDensity,
                BasicConstants.EarthDensity, satelliteConditions);
            satellite.SatelliteStatus = (totalSpaces > 0) ? SatelliteStatus.Uncolonized : SatelliteStatus.Uncolonizable;
        }

        /// <summary>
        ///     Generate all other planetary features
        /// </summary>
        /// <param name="planet"></param>
        /// <param name="generator"></param>
        /// <param name="closeRange"></param>
        /// <param name="rnd"></param>
        public void CompletePlanetGeneration(Planet planet, OrbitGenerator generator, DoubleRange closeRange,
            Random rnd)
        {
            planet.Orbit = generator.Generate(rnd);
            planet.AtmospherePresent = IsAtmospherePresent(planet.Orbit.DistanceR, rnd);
            planet.RadiationLevel = CalculateRadiationLevel(planet.AtmospherePresent, _star.RadiationLevel,
                planet.Orbit.DistanceR, _conditions.ForceLiving, BasicConstants.EarthDistance);
            planet.Mass = CalculateMass(planet.Orbit.DistanceR, closeRange, _conditions.ForceLiving,
                BasicConstants.PlanetMinCloseScale, BasicConstants.PlanetMedCloseScale,
                BasicConstants.PlanetMaxCloseScale, rnd);
            planet.SurfaceTemp = AssignSurfaceTemperature(planet.Orbit.DistanceR, planet.AtmospherePresent,
                _star.SurfaceTemp, rnd);
            _mediumDensity = SetMediumDensity(planet.Mass, rnd);

            planet.Radius = CalculateRadius(planet.Mass, _mediumDensity);
            planet.Orbit.PeriodOfRotation = generator.CalculatePeriodOfRotation(planet.Orbit.DistanceR,
                planet.Orbit.PeriodOfRevolution, _mediumDensity, rnd);

            var isGass = IsGasseous(_mediumDensity, false, rnd, planet.AtmospherePresent);
            var totalSpaces = AssignTotalSpaces(planet.Mass, _mediumDensity, isGass);

            planet.Spaces = PlanetProperties.CalculateSpaces(
                totalSpaces,
                planet.RadiationLevel,
                _conditions,
                IsWaterPresent(planet.AtmospherePresent, _conditions.ForceWater, false, rnd),
                planet.AtmospherePresent,
                rnd);

            if (!_conditions.MineralPoor && !_conditions.MineralRich)
            {
                _conditions.MineralRich = (RandomNumbers.RandomInt(0, 100, rnd) == 0);
                _conditions.MineralPoor = (!_conditions.MineralRich && RandomNumbers.RandomInt(0, 100, rnd) <= 20);
            }
            if (!_conditions.FoodRich && !_conditions.FoodPoor)
            {
                _conditions.FoodRich = (RandomNumbers.RandomInt(0, 100, rnd) == 0);
                _conditions.FoodPoor = (!_conditions.FoodRich && RandomNumbers.RandomInt(0, 100, rnd) <= 20);
            }
            planet.SatelliteProduction = PlanetProperties.CalculateProduction(planet.Spaces, _mediumDensity,
                BasicConstants.EarthDensity, _conditions);
            planet.SatelliteStatus = (isGass || totalSpaces == 0)
                ? SatelliteStatus.Uncolonizable
                : SatelliteStatus.Uncolonized;
            var satellites = NumberOfSatellite(planet.Mass, rnd);

            GenerateSatellites(planet, generator, satellites, rnd);
        }

        #region public wrapper  for test purpouse

        public int CalculateRadiationLevelTest(bool atmospherePresent, int starRadiation, double distance)
        {
            return CalculateRadiationLevel(atmospherePresent, starRadiation, distance, _conditions.ForceLiving,
                BasicConstants.EarthDistance);
        }

        public int AssignTotalSpacesTest(double mass, double density, bool isgass)
        {
            return AssignTotalSpaces(mass, density, isgass);
        }

        public int AssignSurfaceTemperatureTest(double distance, bool atmpspherePresent, int starTemperature)
        {
            return AssignSurfaceTemperature(distance, atmpspherePresent, starTemperature, new Random());
        }

        #endregion
    }
}