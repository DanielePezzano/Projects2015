using Models.Base;
using System;
using System.Collections.Generic;
using BLL.Utilities;
using BLL.Utilities.Structs;
using Models.Base.Enum;
using Models.Buildings;
using Models.Universe;
using Models.Universe.Enum;

namespace BLL.Generation.StarSystem.Builders
{
    public class PlanetBuilder : IBuilder
    {
        private PlanetCustomConditions _conditions;
        private double _mediumDensity = BasicConstants.EarthDensity; //densità media terrestre --> se densità calcolata <=3 probabilmente è gassoso   
        private Star _star;
        private Random _rnd;
        private Planet _resultPlanet;
        private bool _isGasseous;

        #region Private Method
        private void BasePlanet()
        {
            //_mediumDensity = _conditions.ForceLiving || _conditions.ForceWater || _conditions.MostlyWater
            //    ? BasicConstants.EarthDensity + RandomNumbers.RandomDouble(-0.5, 0.5, _rnd)
            //    : RandomNumbers.RandomDouble(BasicConstants.MinDensity, BasicConstants.MaxDensity, _rnd);

            _resultPlanet = new Planet
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Star = _star,
                Name = "PL - " + RandomNumbers.RandomString(7),
                Buildings = new List<Building>(),
                Satellites = new List<Satellite>(),
                User = null,
                RingsPresent = RandomNumbers.RandomInt(0, 100, _rnd) == 0,
                SatelliteSocial = new SatelliteSocials { Population = 0, TaxLevel = TaxLevel.Normal }
            };
        }

        private bool IsAtmospherePresent(double distance, Random rnd)
        {
            if (_conditions.ForceLiving || _conditions.ForceWater || _conditions.MostlyWater) return true;
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

        private bool IsWaterPresent(Random rnd)
        {
            if (_conditions.ForceWater || _conditions.MostlyWater) return true;
            if (!_resultPlanet.AtmospherePresent) return false;
            return RandomNumbers.RandomInt(1, 10, rnd) == 1;
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

        private static double ConvertScale(double distance, DoubleRange closeRange, double scaleMaxMed, double scaleMaxGreatest, double scaleMaxClose)
        {
            if (!(distance > closeRange.Max)) return scaleMaxClose;
            if (distance > closeRange.Max && distance <= BasicConstants.EarthDistance + 2) return scaleMaxMed;
            return scaleMaxGreatest;
        }

        private double CalculateMass(double distance, DoubleRange closeRange, double scaleMaxClose,
            double scaleMaxMed, double scaleMaxGreatest, Random rnd)
        {
            if (_conditions.ForceWater || _conditions.MostlyWater || _conditions.ForceLiving) return 0.8 + RandomNumbers.RandomDouble(0.1, 1, rnd);
            double result;
            using (var scale = new ScaleConversion(10, ConvertScale(distance, closeRange, scaleMaxMed, scaleMaxGreatest, scaleMaxClose)))
            {
                result = Math.Truncate(scale.Convert(RandomNumbers.RandomInt(1, 10, rnd)) * 100) / 100;
            }
            if (Math.Abs(result) < 0.001) result = scaleMaxClose;
            return result;
        }

        private int CalculateRadiationLevel(bool atmospherePresent, int starRadiation, double distance, double fixedDistance)
        {
            if (_conditions.ForceWater || _conditions.MostlyWater || _conditions.ForceLiving) return 1;
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

        private double CalculateMediumDensity(double mass, Random rnd)
        {
            if (_conditions.ForceWater || _conditions.MostlyWater || _conditions.ForceLiving)
                return RandomNumbers.RandomDouble(BasicConstants.EarthDensity - 0.5, BasicConstants.EarthDensity + 0.5,
                    rnd);
            double res;
            if (GenericUtils.IsInRangeDoubleCompared(mass, BasicConstants.EarthMass,5.0))
                res = RandomNumbers.RandomDouble(BasicConstants.MinDensity, BasicConstants.MaxDensity, rnd);
            else
                res = RandomNumbers.RandomDouble(BasicConstants.MinDensityForGas, BasicConstants.EarthDensity + 1,
                    rnd);
            return res;
        }

        private static int CalculateSurfaceTemperature(double distance, bool atmpspherePresent, int starTemperature, Random rnd)
        {
            if (rnd == null) throw new ArgumentNullException(nameof(rnd));
            int result;
            var delta = (-14 - 73.15)*(starTemperature/5700.00);
            var t = 14 + 1.25 * (delta*(1-distance)/-0.5);

            if (atmpspherePresent) result = (int) Math.Truncate(t + t * 0.55);
            else result = (int)t;

            if (result < -270) result = -270;
            return result;
        }

        private double CalculateRadius(double mass)
        {
            return Math.Truncate(mass / _mediumDensity * 100) / 100; 
        }
        

        private void AssignAtmoshpere(double distance, Random rnd)
        {
            _resultPlanet.AtmospherePresent = IsAtmospherePresent(distance, rnd);
        }

        private void AssignOrbit(OrbitGenerator generator)
        {
            _resultPlanet.Orbit = generator.Generate(_rnd);
        }

        private void AssignRadiation(int radiationLevelStar)
        {
            _resultPlanet.RadiationLevel = CalculateRadiationLevel(_resultPlanet.AtmospherePresent, radiationLevelStar,
                _resultPlanet.Orbit.DistanceR,  BasicConstants.EarthDistance);
        }

        private void AssignMass(DoubleRange closeRange)
        {
            _resultPlanet.Mass = CalculateMass(_resultPlanet.Orbit.DistanceR, closeRange, 
                BasicConstants.PlanetMinCloseScale, BasicConstants.PlanetMedCloseScale,
                BasicConstants.PlanetMaxCloseScale, _rnd);
        }

        private void AssignSurfaceTemperature(Star star)
        {
            _resultPlanet.SurfaceTemp = CalculateSurfaceTemperature(_resultPlanet.Orbit.DistanceR,
                _resultPlanet.AtmospherePresent, star.SurfaceTemp, _rnd);
        }

        private void AssignRadius()
        {
            _mediumDensity = CalculateMediumDensity(_resultPlanet.Mass, _rnd);
            _resultPlanet.Radius = CalculateRadius(_resultPlanet.Mass);
        }

        private void AssignPeriodOfRotation(OrbitGenerator generator)
        {
            _resultPlanet.Orbit.PeriodOfRotation = generator.CalculatePeriodOfRotation(_resultPlanet.Orbit.DistanceR,
                _resultPlanet.Orbit.PeriodOfRevolution, _mediumDensity, _rnd);
        }

        private void AssignTotalSpaces()
        {
            IsGasseous(_mediumDensity, _rnd, _resultPlanet.AtmospherePresent);

            _resultPlanet.Spaces = PlanetProperties.CalculateSpaces(
                CalculateTotalSpaces(_resultPlanet.Mass,_isGasseous),
                _resultPlanet.RadiationLevel,
                _conditions,
                IsWaterPresent(_rnd),
                _resultPlanet.AtmospherePresent,
                _rnd);
        }

        private void CheckConditionRichness()
        {
            if (!_conditions.MineralPoor && !_conditions.MineralRich)
            {
                _conditions.MineralRich = RandomNumbers.RandomInt(0, 100, _rnd) == 0;
                _conditions.MineralPoor = !_conditions.MineralRich && RandomNumbers.RandomInt(0, 100, _rnd) <= 20;
            }
            if (_conditions.FoodRich || _conditions.FoodPoor) return;
            _conditions.FoodRich = RandomNumbers.RandomInt(0, 100, _rnd) == 0;
            _conditions.FoodPoor = !_conditions.FoodRich && RandomNumbers.RandomInt(0, 100, _rnd) <= 20;
        }

        private void AssignProduction()
        {
            _resultPlanet.SatelliteProduction = PlanetProperties.CalculateProduction(_resultPlanet.Spaces, _mediumDensity,
                BasicConstants.EarthDensity, _conditions);
        }

        private void AssignStatus()
        {
            _resultPlanet.SatelliteStatus = _isGasseous || _resultPlanet.Spaces.Totalspaces == 0
                ? SatelliteStatus.Uncolonizable
                : SatelliteStatus.Uncolonized;
        }

        private void CompleteGeneration()
        {
            _resultPlanet.IsGaseous = _isGasseous;
            _resultPlanet.IsHabitable = _resultPlanet.AtmospherePresent && !_isGasseous;
        }

        #endregion

        public BaseEntity Build(Star star, PlanetCustomConditions conditions, Random rnd, OrbitGenerator generator, DoubleRange closeRange)
        {
            _star = star;
            _conditions = conditions;
            _rnd = rnd;

            BasePlanet();
            AssignOrbit(generator);
            AssignAtmoshpere(_resultPlanet.Orbit.DistanceR, rnd);
            AssignRadiation(_star.RadiationLevel);
            AssignMass(closeRange);
            AssignSurfaceTemperature(star);
            AssignRadius();
            AssignPeriodOfRotation(generator);
            AssignTotalSpaces();
            
            CheckConditionRichness();

            AssignProduction();
            AssignStatus();
            CompleteGeneration();
            
            return _resultPlanet;
        }

        
    }
}
