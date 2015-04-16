using BLL.Utilities;
using BLL.Utilities.Structs;
using Models.Buildings;
using Models.Queues;
using Models.Universe;
using System;
using System.Collections.Generic;

namespace BLL.Generation.StarSystem
{
    public sealed class PlanetGenerator
    {
        private Star _Star;
        private bool _ForceLiving = false;
        private bool _ForceWater = false;
        private bool _MostlyWater = false;
        private bool _MineralRich = false;
        private bool _MineralPoor = false;
        private bool _FoodRich = false;
        private bool _FoodPoor = false;
        private bool _Gas = false;
        private double _MediumDensity = 5.5; //densità media terrestre --> se densità calcolata <=3 probabilmente è gassoso
        private const double _MinAtmosphereDistance = 0.7;
        private const double _EarthMass = 1.0;
        private const double _EarthRadius = 1.0;
        private const double _EarthDistance = 1.0;
        private const double _EarthDensity = 5.553;
        private static Random _Rnd;

        public PlanetGenerator(Star star, bool forceLiving = false,
            bool forceWater = false,
            bool mostlyWater =false,
            bool mineralProductionRich=false, 
            bool foodProductionRich=false,
            bool mineralProductioPoor = false,
            bool foodProductionPoor =false)
        {
            this._Star = star;
            this._ForceLiving = forceLiving;
            this._FoodRich = foodProductionRich;
            this._MineralRich = mineralProductionRich;
            this._MostlyWater = mostlyWater;
            this._ForceWater = forceWater;
            this._MineralPoor = mineralProductioPoor;
            this._FoodPoor = foodProductionPoor;
            _Rnd = new Random();
        }
        /// <summary>
        /// Calculate Radiation Level
        /// </summary>
        /// <param name="atmospherePresent"></param>
        /// <param name="starRadiation"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        private int CalculateRadiationLevel(bool atmospherePresent, int starRadiation, double distance)
        {
            if (_ForceLiving) return 1;
            int result = 1;
            if (distance <= _EarthDistance)
            {
                result += starRadiation - (int)(starRadiation * (_EarthDistance - distance));
            }
            else
            {
                int temp = starRadiation - (int)(starRadiation * (distance - _EarthDistance));
                result += (temp > 0) ? temp : 3;
            }
            return (atmospherePresent) ? result / 2 : result;
        }
        /// <summary>
        /// Determine if an atmosphere is present
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        private bool IsAtmospherePresent(double distance)
        {
            if (_ForceLiving) return true;
            if (distance >= _MinAtmosphereDistance)
            {
                return (RandomNumbers.RandomInt(0, 100, _Rnd) <= 30) ? true : false;
            }
            else return false;
        }
        /// <summary>
        /// Determine Water Presence
        /// </summary>
        /// <param name="hasAtmosphere"></param>
        /// <returns></returns>
        private bool IsWaterPresent(bool hasAtmosphere)
        {
            if (_ForceWater) return true;
            if (hasAtmosphere)
            {
                return (RandomNumbers.RandomInt(0, 10, _Rnd) <= 1) ? true : false;
            }
            return false;
        }
        /// <summary>
        /// It Calculate the Planet Mass: the closer to sun, the higher prob of lesser mass
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        private double CalculateMass(double distance, DoubleRange closeRange)
        {
            if (_ForceLiving) return 0.8 + RandomNumbers.RandomDouble(0.1, 1, _Rnd);
            double result = 0.8;
            double convertScaleMax = 20.55;
            if (distance >= closeRange.Min && distance <= closeRange.Max)
            {
                // you're in close range, high prob of lesser hearth mass and anyway no prob of much greater
                convertScaleMax = 1.8;
            }
            else
            {
                if (distance > closeRange.Max && distance <= (_EarthDistance + 2))
                {
                    convertScaleMax = 23.7;
                }
                else
                {
                    convertScaleMax = 180;
                }
            }

            using (ScaleConversion scale = new ScaleConversion(10, convertScaleMax))
            {
                result = scale.Convert(RandomNumbers.RandomInt(1, 10, _Rnd));
            }
            return result;
        }
        /// <summary>
        /// Calculate total Spaces for the planet
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private int AssignTotalSpaces(double mass, double density)
        {
            if (_Gas) return 0;
            double result = 50;
            if (density >= 3)
                result = 100 * (mass * density) / _EarthDensity;
            else
            {
                result = (density >= 1) ? 10 * mass / (Math.Pow(_EarthDensity * density, 2)) : 10 * mass * Math.Pow(density, 10) / _EarthDensity;
            }
            if ((int)result > 250) return 250;
            return (int)result;
        }
        /// <summary>
        /// This will calculate the radius based on mass
        /// </summary>
        /// <param name="mass"></param>
        /// <returns></returns>
        private double CalculateRadius(double mass)
        {
            return Math.Truncate(((15 * mass) / (250 * _EarthDensity)) * 100) / 100;
        }

        #region public wrapper  for test purpouse
        public int CalculateRadiationLevelTest(bool atmospherePresent, int starRadiation, double distance)
        {
            return this.CalculateRadiationLevel(atmospherePresent, starRadiation, distance);
        }

        public int AssignTotalSpacesTest(double mass, double density)
        {
            return this.AssignTotalSpaces(mass, density);
        }
        #endregion

        public Planet CreateBrandNewPlanet()
        {
            Planet planet = new Planet();
            planet.CreatedAt = DateTime.Now;
            planet.UpdatedAt = DateTime.Now;
            planet.Star = _Star;
            planet.Name = "PL" + DateTime.Now.ToFileTimeUtc();
            this._MediumDensity = (_ForceLiving) ? _EarthDensity + RandomNumbers.RandomDouble(-0.5, 0.5, _Rnd) : RandomNumbers.RandomDouble(0.2, 15, _Rnd);
            planet.BuildingQueues = new List<BuildingQueue>();
            planet.Buildings = new List<Building>();
            planet.FleetQueues = new List<FleetQueue>();
            planet.Researches = new List<ResearchQueue>();
            planet.SatelliteSocial = new Models.Base.SatelliteSocials() { Population = 0, TaxLevel = Models.Base.Enum.TaxLevel.Normal };
            planet.User = null;
            planet.RingsPresent = (RandomNumbers.RandomInt(0, 100, _Rnd) == 0) ? true : false;
            return planet;
        }
        /// <summary>
        /// Generate all other planetary features
        /// </summary>
        /// <param name="planet"></param>
        /// <param name="generator"></param>
        /// <param name="closeRange"></param>
        public void AddOrbitGenerationAndCollateral(Planet planet, OrbitGenerator generator, DoubleRange closeRange)
        {
            planet.Orbit = generator.Generate();
            planet.AtmospherePresent = this.IsAtmospherePresent(planet.Orbit.DistanceR);
            planet.RadiationLevel = this.CalculateRadiationLevel(planet.AtmospherePresent, _Star.RadiationLevel, planet.Orbit.DistanceR);
            planet.Mass = this.CalculateMass(planet.Orbit.DistanceR, closeRange);
            planet.Radius = this.CalculateRadius(planet.Mass);
            planet.Orbit.PeriodOfRotation = generator.CalculatePeriodOfRotation(planet.Orbit.DistanceR, planet.Orbit.PeriodOfRevolution, _MediumDensity);
            planet.Spaces = PlanetProperties.CalculateSpaces(
                this.AssignTotalSpaces(planet.Mass, _MediumDensity), 
                planet.RadiationLevel,
                _ForceWater,
                _MostlyWater, 
                _ForceLiving, 
                this.IsWaterPresent(planet.AtmospherePresent),
                planet.AtmospherePresent,
                _Rnd);
            

        }
      

    }
}
