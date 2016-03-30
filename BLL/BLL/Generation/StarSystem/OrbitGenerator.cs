﻿using System;
using BLL.Utilities;
using BLL.Utilities.Structs;
using Models.Universe;

namespace BLL.Generation.StarSystem
{
    public sealed class OrbitGenerator : IDisposable
    {
        private readonly bool _forceLiving;
        private double _planetRadius;
        private readonly Star _star;
        private DoubleRange _closeRange;
        private bool _disposed;
        private DoubleRange _satelliteRange;

        public OrbitGenerator(Star star, DoubleRange closeRange,
            bool forceLiving = false)
        {
            _star = star;
            _forceLiving = forceLiving;
            _closeRange = closeRange;
            _satelliteRange = new DoubleRange(PlanetProperties.MinSatelliteDistance,
                PlanetProperties.MaxSatelliteDistance);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _closeRange = new DoubleRange();
            _satelliteRange = new DoubleRange();
        }

        /// <summary>
        ///     Calculate the distance expressed in UA
        /// </summary>
        /// <returns></returns>
        private double CalculateDistance(DoubleRange range, bool forceLiving, Random rnd)
        {
            var result = range.Max;
            if (forceLiving) return (result + RandomNumbers.RandomDouble(range.Min, range.Max, rnd));
            return RandomNumbers.RandomDouble(range.Min, 40, rnd);
        }

        /// <summary>
        ///     Calculate the period of revolution in days
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="radius"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        private double CalculatePeriodOfRevolution(double distance, double radius, double rate)
        {
            var totalDistance = distance + (radius*rate);
            return Math.Truncate((Math.Sqrt(Math.Pow(totalDistance, 3))*0.78)*100)/100;
        }

        /// <summary>
        ///     Calculate the period of rotation
        ///     es 0.45, 0.2, 4.9 50 - 100
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="revolution"></param>
        /// <param name="density"></param>
        /// <param name="rnd"></param>
        /// <returns></returns>
        public double CalculatePeriodOfRotation(double distance, double revolution, double density, Random rnd)
        {
            double result;
            var isRetro = (RandomNumbers.RandomInt(0, 10, rnd) == 0);
            if (distance <= _closeRange.Max)
            {
                result = Math.Truncate(((20*revolution/distance)*density)*100)/100;
            }
            else
            {
                result = density <= 3 ? RandomNumbers.RandomDouble(0.4, 0.7, rnd) : RandomNumbers.RandomDouble(1, 7, rnd);
            }
            if (result < 0) return 1;
            return (isRetro) ? (result*-1) : result;
        }

        /// <summary>
        ///     Planet whit masses between 0.05mt and 0.1mt will be placed
        ///     as nearer as possible to the star
        /// </summary>
        /// <returns></returns>
        public OrbitDetail Generate(Random rnd)
        {
            var distance = CalculateDistance(_closeRange, _forceLiving, rnd);
            return new OrbitDetail
            {
                DistanceR = distance,
                Eccentricity = RandomNumbers.RandomDouble(BasicConstants.MinEcc, BasicConstants.MaxEccc, rnd),
                PeriodOfRevolution = CalculatePeriodOfRevolution(distance, _star.Radius,BasicConstants.StarRadToUaRate),
                TetaZero = RandomNumbers.RandomDouble(0.1, 3, rnd)
            };
        }

        /// <summary>
        ///     Calculate satellite main orbit
        /// </summary>
        /// <returns></returns>
        public OrbitDetail GenerateSatellite(Random rnd)
        {
            var distance = CalculateDistance(_satelliteRange, false, rnd);
            return new OrbitDetail
            {
                DistanceR = distance,
                Eccentricity = RandomNumbers.RandomDouble(BasicConstants.MinEcc, BasicConstants.MaxEccc, rnd),
                PeriodOfRevolution = CalculatePeriodOfRevolution(distance, _planetRadius, 0),
                TetaZero = RandomNumbers.RandomDouble(0.1, 3, rnd)
            };
        }

        public void AssignPlanetRadius(double radius)
        {
            _planetRadius = radius;
        }

        #region Wrapper for private methods test

        public double CalculateDistanceTest()
        {
            return CalculateDistance(_closeRange, _forceLiving, new Random());
        }

        public double CalculatePeriodOfRevolutionTest(double distance)
        {
            return CalculatePeriodOfRevolution(distance, _star.Radius, BasicConstants.StarRadToUaRate);
        }

        #endregion
    }
}