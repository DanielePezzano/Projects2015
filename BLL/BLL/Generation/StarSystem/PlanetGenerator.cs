using BLL.Utilities;
using BLL.Utilities.Structs;
using Models.Buildings;
using Models.Queues;
using Models.Universe;
using Models.Universe.Enum;
using System;
using System.Collections.Generic;

namespace BLL.Generation.StarSystem
{
    public sealed class PlanetGenerator : IDisposable
    {
        private Star _Star;
        private bool _ForceLiving = false;
        private bool _ForceWater = false;
        private bool _MostlyWater = false;
        private bool _MineralRich = false;
        private bool _MineralPoor = false;
        private bool _FoodRich = false;
        private bool _FoodPoor = false;
        private bool disposed = false;
        private double _MediumDensity = 5.5; //densità media terrestre --> se densità calcolata <=3 probabilmente è gassoso        
        private DoubleRange _SatelliteCloseRange = new DoubleRange(PlanetProperties._MinSatelliteDistance, PlanetProperties._MaxSatelliteDistance);
        private static Random _Rnd;

        public PlanetGenerator(Star star, bool forceLiving = false,
            bool forceWater = false,
            bool mostlyWater = false,
            bool mineralProductionRich = false,
            bool foodProductionRich = false,
            bool mineralProductioPoor = false,
            bool foodProductionPoor = false)
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
        private int CalculateRadiationLevel(bool atmospherePresent, int starRadiation, double distance, bool forceLiving, double fixedDistance)
        {
            if (forceLiving) return 1;
            int result = 1;
            if (distance <= fixedDistance)
            {
                result += starRadiation - (int)(starRadiation * (fixedDistance - distance));
            }
            else
            {
                int temp = starRadiation - (int)(starRadiation * (distance - fixedDistance));
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
            if (distance >= BasicConstants._MinAtmosphereDistance)
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
        private bool IsWaterPresent(bool hasAtmosphere, bool forcewater, bool isSatellite)
        {
            if (forcewater) return true;
            if (hasAtmosphere)
            {
                if (!isSatellite) return (RandomNumbers.RandomInt(0, 10, _Rnd) <= 1) ? true : false;
                else return (RandomNumbers.RandomInt(0, 100, _Rnd) <= 1) ? true : false;
            }
            return false;
        }
        /// <summary>
        /// It Calculate the Planet Mass: the closer to sun, the higher prob of lesser mass
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        private double CalculateMass(double distance, DoubleRange closeRange, bool forceliving, double scaleMaxClose, double scaleMaxMed, double scaleMaxGreatest)
        {
            if (forceliving) return 0.8 + RandomNumbers.RandomDouble(0.1, 1, _Rnd);
            double result = 0.8;
            double convertScaleMax = scaleMaxClose;
            if (distance > closeRange.Max)
            {
                if (distance > closeRange.Max && distance <= (BasicConstants._EarthDistance + 2))
                    convertScaleMax = scaleMaxMed;
                else
                    convertScaleMax = scaleMaxGreatest;
            }

            using (ScaleConversion scale = new ScaleConversion(10, convertScaleMax))
            {
                result = scale.Convert(RandomNumbers.RandomInt(1, 10, _Rnd));
            }
            return Math.Truncate(result * 100) / 100;
        }
        /// <summary>
        /// Calculate total Spaces for the planet
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private int AssignTotalSpaces(double mass, double density, bool isGasseous)
        {
            if (isGasseous) return 0;
            double result = 50;
            if (density >= BasicConstants._MinDensityForGas)
                result = 100 * (mass * density) / BasicConstants._EarthDensity;
            else
            {
                result = (density >= 1) ? 10 * mass / (Math.Pow(BasicConstants._EarthDensity * density, 2)) : 10 * mass * Math.Pow(density, 10) / BasicConstants._EarthDensity;
            }
            if ((int)result > 250) return 250;
            return (int)result;
        }
        /// <summary>
        /// if density is lesser than a minimum there's a high probability of gas planet
        /// </summary>
        /// <param name="density"></param>
        /// <returns></returns>
        private bool IsGasseous(double density, double mass, bool isSatellite)
        {
            if (isSatellite) return false;
            if (density <= 1) return true;
            bool result = false;
            int perc = 0;
            if (density > 2 && density <= BasicConstants._MinDensityForGas) perc = 20;
            if (density > 1 && density <= 2) perc = 70;
            if (RandomNumbers.RandomInt(0, 100, _Rnd) <= perc) result = true;
            return result;
        }
        /// <summary>
        /// set the medium density
        /// </summary>
        /// <param name="mass"></param>
        /// <returns></returns>
        private double SetMediumDensity(double mass)
        {
            double res = BasicConstants._EarthDensity;
            if (mass >= BasicConstants._EarthMass) res = RandomNumbers.RandomDouble(BasicConstants._MinDensity, BasicConstants._MaxDensity, _Rnd);
            else res = RandomNumbers.RandomDouble(BasicConstants._MinDensityForGas, (BasicConstants._EarthDensity + 1), _Rnd);
            return res;
        }
        /// <summary>
        /// DEtermine number of satellites
        /// </summary>
        /// <param name="mass"></param>
        /// <returns></returns>
        private int NumberOfSatellite(double mass, double satelliteFactor = 0.1)
        {
            int maxNumb = 0;
            if (mass < BasicConstants._MinMassForSatellite) return maxNumb;

            if (mass >= BasicConstants._MinMassForSatellite && mass <= (2 * BasicConstants._EarthMass)) maxNumb = 2;
            if (mass > (2 * BasicConstants._EarthMass)) maxNumb = (int)(satelliteFactor * mass);
            return RandomNumbers.RandomInt(0, maxNumb, _Rnd);
        }
        /// <summary>
        /// This will calculate the radius based on mass
        /// Radius is Moon-compared
        /// </summary>
        /// <param name="mass"></param>
        /// <returns></returns>
        private double CalculateRadius(double mass,double density, bool isSatellite = false)
        {
           if (!isSatellite) return Math.Truncate((mass / density) * 100) / 100;

           return Math.Truncate((density / (mass * BasicConstants._LunarVolumeFactor)) * 100) / 100;
        }

        /// <summary>
        /// Generate N satellites for this planet
        /// </summary>
        /// <param name="planet"></param>
        /// <param name="generator"></param>
        /// <param name="satellites"></param>
        private void GenerateSatellites(Planet planet, OrbitGenerator generator, int satellites)
        {
            for (int x = 0; x < satellites; x++)
            {
                Satellite toAdd = this.CreateSatellite(planet);

                this.CompleteSatelliteGeneration(toAdd, generator, planet.Orbit.DistanceR);
                planet.Satellites.Add(toAdd);
            }
        }
        /// <summary>
        /// Determine the surface temperature
        /// </summary>
        /// <returns></returns>
        private int AssignSurfaceTemperature(double distance, bool AtmpspherePresent, int starTemperature)
        {
            int result = -1;
            double temp = (starTemperature - (starTemperature * distance))/7.095;
            if (AtmpspherePresent) result = (int)(temp - (temp * 0.5));
            else result = (int)temp;
            if (result <= -260) result = -260;
            return result;
        }
        #region public wrapper  for test purpouse
        public int CalculateRadiationLevelTest(bool atmospherePresent, int starRadiation, double distance)
        {
            return this.CalculateRadiationLevel(atmospherePresent, starRadiation, distance, this._ForceLiving, BasicConstants._EarthDistance);
        }

        public int AssignTotalSpacesTest(double mass, double density, bool isgass)
        {
            return this.AssignTotalSpaces(mass, density, isgass);
        }
        public int AssignSurfaceTemperatureTest(double distance, bool AtmpspherePresent, int starTemperature)
        {
            return this.AssignSurfaceTemperature(distance,AtmpspherePresent,starTemperature);
        }
               
        #endregion

        /// <summary>
        /// This generate a completly new planet
        /// </summary>
        /// <returns></returns>
        public Planet CreateBrandNewPlanet()
        {
            Planet planet = new Planet();
            planet.CreatedAt = DateTime.Now;
            planet.UpdatedAt = DateTime.Now;
            planet.Star = _Star;
            planet.Name = "PL - " + DateTime.Now.ToFileTimeUtc();
            this._MediumDensity = (_ForceLiving) ? BasicConstants._EarthDensity + RandomNumbers.RandomDouble(-0.5, 0.5, _Rnd) : RandomNumbers.RandomDouble(BasicConstants._MinDensity, BasicConstants._MaxDensity, _Rnd);
            planet.Buildings = new List<Building>();
            planet.SatelliteSocial = new Models.Base.SatelliteSocials() { Population = 0, TaxLevel = Models.Base.Enum.TaxLevel.Normal };
            planet.User = null;
            planet.RingsPresent = (RandomNumbers.RandomInt(0, 100, _Rnd) == 0) ? true : false;
            planet.Satellites = new List<Satellite>();
            return planet;
        }
        /// <summary>
        /// this will generate a satellite for this planet
        /// </summary>
        /// <param name="planet"></param>
        /// <returns></returns>
        public Satellite CreateSatellite(Planet planet)
        {
            Satellite satellite = new Satellite();
            satellite.CreatedAt = DateTime.Now;
            satellite.UpdatedAt = DateTime.Now;
            satellite.Planet = planet;
            satellite.Name = "STL - " + DateTime.Now.ToFileTimeUtc();

            satellite.Buildings = new List<Building>();
            satellite.SatelliteSocial = new Models.Base.SatelliteSocials() { Population = 0, TaxLevel = Models.Base.Enum.TaxLevel.Normal };
            satellite.User = null;
            satellite.RingsPresent = false;
            return satellite;
        }
        /// <summary>
        /// Complete Satellite's generation process
        /// </summary>
        /// <param name="satellite"></param>
        /// <param name="generator"></param>
        /// <param name="planetDistance"></param>
        public void CompleteSatelliteGeneration(Satellite satellite, OrbitGenerator generator, double planetDistance)
        {            
            satellite.Orbit = generator.GenerateSatellite();
            satellite.AtmospherePresent = this.IsAtmospherePresent(satellite.Orbit.DistanceR);
            satellite.RadiationLevel = this.CalculateRadiationLevel(satellite.AtmospherePresent, _Star.RadiationLevel, planetDistance, false, BasicConstants._EarthDistance);
            satellite.Mass = this.CalculateMass(satellite.Orbit.DistanceR, _SatelliteCloseRange, _ForceLiving, BasicConstants._SatelliteMinCloseScale, BasicConstants._SatelliteMedCloseScale, BasicConstants._SatelliteMaxCloseScale);
            this._MediumDensity = RandomNumbers.RandomDouble(BasicConstants._MinDensityForGas, (BasicConstants._EarthDensity + 1), _Rnd);
            satellite.SurfaceTemp = this.AssignSurfaceTemperature(planetDistance, satellite.AtmospherePresent, _Star.SurfaceTemp);
            satellite.Radius = this.CalculateRadius(satellite.Mass, _MediumDensity, true);
            satellite.Orbit.PeriodOfRotation = generator.CalculatePeriodOfRotation(satellite.Orbit.DistanceR, satellite.Orbit.PeriodOfRevolution, _MediumDensity);

            int totalSpaces = this.AssignTotalSpaces(satellite.Mass, _MediumDensity,false);
            satellite.Spaces = PlanetProperties.CalculateSpaces(
                totalSpaces,
                satellite.RadiationLevel,
                false,
                false,
                false,
                this.IsWaterPresent(satellite.AtmospherePresent, false, true),
                satellite.AtmospherePresent,
                _Rnd);

            satellite.SatelliteProduction = PlanetProperties.CalculateProduction(satellite.Spaces, _MediumDensity, BasicConstants._EarthDensity);
            satellite.SatelliteStatus = (totalSpaces > 0) ? SatelliteStatus.Uncolonized : SatelliteStatus.Uncolonizable;
        }
        /// <summary>
        /// Generate all other planetary features
        /// </summary>
        /// <param name="planet"></param>
        /// <param name="generator"></param>
        /// <param name="closeRange"></param>
        public void CompletePlanetGeneration(Planet planet, OrbitGenerator generator, DoubleRange closeRange)
        {
            planet.Orbit = generator.Generate();
            planet.AtmospherePresent = this.IsAtmospherePresent(planet.Orbit.DistanceR);
            planet.RadiationLevel = this.CalculateRadiationLevel(planet.AtmospherePresent, _Star.RadiationLevel, planet.Orbit.DistanceR, _ForceLiving, BasicConstants._EarthDistance);
            planet.Mass = this.CalculateMass(planet.Orbit.DistanceR, closeRange, _ForceLiving, BasicConstants._PlanetMinCloseScale, BasicConstants._PlanetMedCloseScale, BasicConstants._PlanetMaxCloseScale);
            planet.SurfaceTemp = this.AssignSurfaceTemperature(planet.Orbit.DistanceR, planet.AtmospherePresent, _Star.SurfaceTemp);
            this._MediumDensity = this.SetMediumDensity(planet.Mass);

            planet.Radius = this.CalculateRadius(planet.Mass,this._MediumDensity);
            planet.Orbit.PeriodOfRotation = generator.CalculatePeriodOfRotation(planet.Orbit.DistanceR, planet.Orbit.PeriodOfRevolution, _MediumDensity);

            bool isGass = this.IsGasseous(_MediumDensity, planet.Mass, false);
            int totalSpaces = this.AssignTotalSpaces(planet.Mass, _MediumDensity, isGass);
            
            planet.Spaces = PlanetProperties.CalculateSpaces(
                totalSpaces,
                 planet.RadiationLevel,
                _ForceWater,
                _MostlyWater,
                _ForceLiving,
                this.IsWaterPresent(planet.AtmospherePresent, _ForceWater, false),
                planet.AtmospherePresent,
                _Rnd);

            planet.SatelliteProduction = PlanetProperties.CalculateProduction(planet.Spaces, _MediumDensity, BasicConstants._EarthDensity);
            planet.SatelliteStatus = (isGass || totalSpaces==0) ? SatelliteStatus.Uncolonizable : SatelliteStatus.Uncolonized;
            int satellites = this.NumberOfSatellite(planet.Mass);

            GenerateSatellites(planet, generator, satellites);                        
        }

        public void Dispose()
        {
            if (!disposed)
            {
                disposed = true;
                _SatelliteCloseRange = new DoubleRange();
                _Star = null;
            }
            GC.SuppressFinalize(this);
        }
    }
}
