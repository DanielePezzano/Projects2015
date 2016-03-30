
using System;
using System.Collections.Generic;
using BLL.Utilities;
using BLL.Utilities.Structs;
using Models.Base;
using Models.Base.Enum;
using Models.Buildings;
using Models.Universe;

namespace BLL.Generation.StarSystem.Builders
{
    public class SatelliteBuilder : IBuilder
    {
        private PlanetCustomConditions _conditions;
        private double _planetDistance = 0;
        private double _mediumDensity = 5.5; //densità media terrestre --> se densità calcolata <=3 probabilmente è gassoso   
        private Star _star;
        private Random _rnd;
        private Satellite _resultSat;
        private bool _isGasseous;
        private DoubleRange _satelliteCloseRange = new DoubleRange(PlanetProperties.MinSatelliteDistance,
            PlanetProperties.MaxSatelliteDistance);

        #region Private Methods
        private bool IsAtmospherePresent(double distance, Random rnd)
        {
            if (_conditions.ForceLiving) return true;
            if (distance >= BasicConstants.MinAtmosphereDistance)
            {
                return (RandomNumbers.RandomInt(0, 100, rnd) <= 30);
            }
            return false;
        }

        private void CreateSatellite()
        {
            _resultSat = new Satellite
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Planet = null,
                Name = "STL - " + DateTime.Now.ToFileTimeUtc(),
                Buildings = new List<Building>(),
                SatelliteSocial = new SatelliteSocials { Population = 0, TaxLevel = TaxLevel.Normal },
                User = null,
                RingsPresent = false
            };
        }

        private int CalculateRadiationLevel(bool atmospherePresent, int starRadiation, double distance, bool forceLiving, double fixedDistance)
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
                result += (temp > 0) ? temp : 3;
            }
            return (atmospherePresent) ? result / 2 : result;
        }

        private double ConvertScale(double distance, DoubleRange closeRange, double scaleMaxMed, double scaleMaxGreatest, double scaleMaxClose)
        {
            if (!(distance > closeRange.Max)) return scaleMaxClose;
            if (distance > closeRange.Max && distance <= (BasicConstants.EarthDistance + 2)) return scaleMaxMed;
            return scaleMaxGreatest;
        }

        private double CalculateMass(double distance, DoubleRange closeRange, bool forceliving, double scaleMaxClose,
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

        private int CalculateSurfaceTemperature(double distance, bool atmpspherePresent, int starTemperature, Random rnd)
        {
            int result;
            var temp = (starTemperature - (starTemperature * distance)) / 7.095;
            if (atmpspherePresent) result = (int)(temp - (temp * 0.5));
            else result = (int)temp;

            if (result >= 100) return result;
            result = RandomNumbers.RandomInt(100, !atmpspherePresent ? 273 : 500, rnd);
            return result;
        }

        private double CalculateRadius(double mass)
        {
            return Math.Truncate((_mediumDensity / (mass * BasicConstants.LunarVolumeFactor)) * 100) / 100;
        }

        private void AssignOrbit(OrbitGenerator generator)
        {
            _resultSat.Orbit = generator.GenerateSatellite(_rnd);
        }

        private void AssignRadiationLevel()
        {
            _resultSat.RadiationLevel = CalculateRadiationLevel(_resultSat.AtmospherePresent, _star.RadiationLevel,
                _planetDistance, false, BasicConstants.EarthDistance);
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
            _resultSat.SurfaceTemp = CalculateSurfaceTemperature(_planetDistance,
                _resultSat.AtmospherePresent, star.SurfaceTemp, _rnd);
        }

        private void AssignRadius()
        {
            _resultSat.Radius = CalculateRadius(_resultSat.Mass);
        }

        #endregion

        public BaseEntity Build(Star star, PlanetCustomConditions conditions, Random rnd, OrbitGenerator generator, DoubleRange closeRange,double planetDistance = 0)
        {
            _star = star;
            _conditions = conditions;
            _rnd = rnd;
            _planetDistance = planetDistance;

            CreateSatellite();
            AssignOrbit(generator);
            AssignAtmosphere();
            AssignRadiationLevel();
            AssignMass();
            AssignSurfaceTemperature(star);
            AssignRadius();
            

            return _resultSat;
        }
    }
}
