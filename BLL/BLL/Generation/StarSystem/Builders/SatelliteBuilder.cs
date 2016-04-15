
using System;
using System.Collections.Generic;
using BLL.Generation.StarSystem.Interfaces;
using BLL.Utilities;
using BLL.Utilities.Structs;
using Models.Base;
using Models.Base.Enum;
using Models.Buildings;
using Models.Universe;
using Models.Universe.Enum;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Stars;
using SharedDto.UtilityDto;

namespace BLL.Generation.StarSystem.Builders
{
    [Obsolete]
    public class SatelliteBuilder : IBuilder
    {
        private SystemGenerationDto _conditions;
        private double _mediumDensity; //densità media terrestre --> se densità calcolata <=3 probabilmente è gassoso   
        private Star _star;
        private Random _rnd;
        private Satellite _resultSat;
        private bool _isGasseous;
        private readonly DoubleRange _satelliteCloseRange = new DoubleRange(PlanetProperties.MinSatelliteDistance,
            PlanetProperties.MaxSatelliteDistance);

        #region Private Methods
        private bool IsAtmospherePresent(double distance, Random rnd)
        {
            if (_conditions.ForceLiving) return true;
            if (distance >= BasicConstants.MinAtmosphereDistance)
            {
                return RandomNumbers.RandomInt(0, 100, rnd) <= 30;
            }
            return false;
        }

        private void IsGasseous(double density, Random rnd, bool atmospherePresent)
        {
            if (!atmospherePresent) _isGasseous = false;
            if (density <= 1) _isGasseous = true;
            var result = false;
            var perc = 0;
            if (density > 2 && density <= BasicConstants.MinDensityForGas) perc = 20;
            if (density > 1 && density <= 2) perc = 70;
            if (RandomNumbers.RandomInt(1, 100, rnd) <= perc) result = true;
            _isGasseous = result;
        }

        private static bool IsWaterPresent(bool hasAtmosphere, bool forcewater, Random rnd)
        {
            if (forcewater) return true;
            if (!hasAtmosphere) return false;
            return RandomNumbers.RandomInt(0, 100, rnd) <= 1;
        }

        private void CreateSatellite()
        {
            _resultSat = new Satellite
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Planet = null,
                Name = "STL - " +RandomNumbers.RandomString(7),
                Buildings = new List<Building>(),
                SatelliteSocial = new SatelliteSocials { Population = 0, TaxLevel = TaxLevel.Normal },
                User = null,
                RingsPresent = false
            };
        }

        private int CalculateTotalSpaces(double mass, bool isGasseous)
        {
            if (isGasseous) return 0;
            double result;
            if (_mediumDensity >= BasicConstants.MinDensityForGas)
                result = 100 * (mass * _mediumDensity) / BasicConstants.EarthDensity;
            else
            {
                result = _mediumDensity >= 1
                    ? 10 * mass / Math.Pow(BasicConstants.EarthDensity * _mediumDensity, 2)
                    : 10 * mass * Math.Pow(_mediumDensity, 10) / BasicConstants.EarthDensity;
            }
            if ((int)result > 250) return 250;
            return (int)result;
        }

        private static int CalculateRadiationLevel(bool atmospherePresent, int starRadiation, double distance, bool forceLiving, double fixedDistance)
        {
            if (forceLiving) return 1;
            var result = 1;
            if (distance <= fixedDistance)
            {
                result += starRadiation - (int)(starRadiation * (fixedDistance - distance));
            }
            else
            {
                var temp = starRadiation - (int)(starRadiation * (distance - fixedDistance));
                result += temp > 0 ? temp : 3;
            }
            return atmospherePresent ? result / 2 : result;
        }

        private static double ConvertScale(double distance, DoubleRange closeRange, double scaleMaxMed, double scaleMaxGreatest, double scaleMaxClose)
        {
            if (!(distance > closeRange.Max)) return scaleMaxClose;
            if (distance > closeRange.Max && distance <= BasicConstants.EarthDistance + 2) return scaleMaxMed;
            return scaleMaxGreatest;
        }

        private static double CalculateMass(double distance, DoubleRange closeRange, bool forceliving, double scaleMaxClose,
            double scaleMaxMed, double scaleMaxGreatest, Random rnd)
        {
            if (forceliving) return 0.8 + RandomNumbers.RandomDouble(0.1, 1, rnd);
            double result;
            using (var scale = new ScaleConversion(10, ConvertScale(distance, closeRange, scaleMaxMed, scaleMaxGreatest, scaleMaxClose)))
            {
                result = Math.Truncate(scale.Convert(RandomNumbers.RandomInt(1, 10, rnd)) * 100) / 100;
            }
            if (Math.Abs(result) < 0.001) result = scaleMaxClose;
            return result;
        }

        private static int CalculateSurfaceTemperature(double distance, bool atmpspherePresent, int starTemperature, Random rnd)
        {
            int result;
            var delta = (-14 - 73.15) * (starTemperature / 5700.00);
            var t = 14 + 1.25 * (delta * (1 - distance) / -0.5);

            if (atmpspherePresent) result = (int)Math.Truncate(t + t * 0.55);
            else result = (int)t;

            if (result < -270) result = -270;
            return result;
        }

        private double CalculateRadius(double mass)
        {
            return Math.Truncate(_mediumDensity / (mass * BasicConstants.LunarVolumeFactor) * 100) / 100;
        }

        private void AssignOrbit(OrbitGenerator generator)
        {
            _resultSat.Orbit = generator.GenerateSatellite(_rnd);
        }

        private void AssignRadiationLevel()
        {
            _resultSat.RadiationLevel = CalculateRadiationLevel(_resultSat.AtmospherePresent, _star.RadiationLevel,
                _resultSat.Orbit.DistanceR, false, BasicConstants.EarthDistance);
        }

        private void AssignAtmosphere()
        {
            _resultSat.AtmospherePresent = IsAtmospherePresent(_resultSat.Orbit.DistanceR, _rnd);
        }

        private void AssignMass()
        {
            _resultSat.Mass = CalculateMass(_resultSat.Orbit.DistanceR, _satelliteCloseRange,
                _conditions.ForceLiving, BasicConstants.SatelliteMinCloseScale,
                BasicConstants.SatelliteMedCloseScale, BasicConstants.SatelliteMaxCloseScale, _rnd);
        }

        private void AssignSurfaceTemperature(Star star)
        {
            _resultSat.SurfaceTemp = CalculateSurfaceTemperature(_resultSat.Orbit.DistanceR,
                _resultSat.AtmospherePresent, star.SurfaceTemp, _rnd);
        }

        private void AssignRadius()
        {
            _resultSat.Radius = CalculateRadius(_resultSat.Mass);
        }

        private void AssignPeriodOfRotation(OrbitGenerator generator)
        {
            _resultSat.Orbit.PeriodOfRotation = generator.CalculatePeriodOfRotation(_resultSat.Orbit.DistanceR,
                _resultSat.Orbit.PeriodOfRevolution, _mediumDensity, _rnd);
        }

        private void AssignTotalSpaces()
        {
            IsGasseous(_mediumDensity, _rnd, _resultSat.AtmospherePresent);

            _resultSat.Spaces = PlanetProperties.CalculateSpaces(
                CalculateTotalSpaces(_resultSat.Mass, _isGasseous),
                _resultSat.RadiationLevel,
                _conditions,
                IsWaterPresent(_resultSat.AtmospherePresent, _conditions.ForceWater, _rnd),
                _resultSat.AtmospherePresent,
                _rnd);
        }

        private void AssignProduction()
        {
            //_resultSat.SatelliteProduction = PlanetProperties.CalculateProduction(_resultSat.Spaces, _mediumDensity,
            //    BasicConstants.EarthDensity, _conditions);
        }

        private void CheckConditionRichness()
        {
            _conditions.FoodRich = RandomNumbers.RandomInt(0, 100, _rnd) == 0;
            _conditions.FoodPoor = !_conditions.FoodRich && RandomNumbers.RandomInt(0, 100, _rnd) <= 20;

            _conditions.MineralRich = RandomNumbers.RandomInt(0, 100, _rnd) == 0;
            _conditions.MineralPoor = !_conditions.MineralRich && RandomNumbers.RandomInt(0, 100, _rnd) <= 20;
        }

        private void AssignStatus()
        {
            _resultSat.SatelliteStatus = _resultSat.Spaces.Totalspaces > 0 ? SatelliteStatus.Uncolonized : SatelliteStatus.Uncolonizable;
        }

        private void CompleteGeneration()
        {
            _resultSat.IsGaseous = _isGasseous;
            _resultSat.IsHabitable = _resultSat.AtmospherePresent && !_isGasseous;
        }
        #endregion

        //public BaseEntity Build(StarDto star, SystemGenerationDto conditions, Random rnd, OrbitGenerator generator, DoubleRange closeRange)
        //{
        //    _star = star;
        //    _conditions = conditions;
        //    _rnd = rnd;
        //    _mediumDensity = 5.5;

        //    CreateSatellite();
        //    AssignOrbit(generator);
        //    AssignAtmosphere();
        //    AssignRadiationLevel();
        //    AssignMass();
        //    AssignSurfaceTemperature(star);
        //    AssignRadius();
        //    AssignPeriodOfRotation(generator);
        //    AssignTotalSpaces();

        //    CheckConditionRichness();
        //    AssignProduction();
        //    AssignStatus();
        //    CompleteGeneration();

        //    return _resultSat;
        //}

        public PlanetDto Build(StarDto star, SystemGenerationDto conditions, Random rnd, OrbitGenerator generator, DoubleRange closeRange)
        {
            throw new NotImplementedException();
        }
    }
}
