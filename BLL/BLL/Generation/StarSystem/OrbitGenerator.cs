using BLL.Utilities;
using Models.Universe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Utilities.Structs;

namespace BLL.Generation.StarSystem
{
    public sealed class OrbitGenerator : IDisposable
    {
        private Star _Star;
        private double _PlanetMass;
        private double _PlanetRadius;
        private bool _ForceLiving;
        private bool _disposed = false;
        private DoubleRange _CloseRange;
        private DoubleRange _SatelliteRange;        


        public OrbitGenerator(Star star, double planetMass, double planetRadius, DoubleRange closeRange, bool forceLiving = false)
        {
            this._Star = star;
            this._PlanetMass = planetMass;
            this._PlanetRadius = planetRadius;
            this._ForceLiving = forceLiving;
            this._CloseRange = closeRange;
            this._SatelliteRange = new DoubleRange(PlanetProperties._MinSatelliteDistance, PlanetProperties._MaxSatelliteDistance);
        }
        /// <summary>
        /// Calculate the distance expressed in UA
        /// </summary>
        /// <returns></returns>
        private double CalculateDistance(DoubleRange range, bool forceLiving, Random _Rnd)
        {
            double result = range.Max;
            if (forceLiving) return (result + RandomNumbers.RandomDouble(range.Min, range.Max, _Rnd));
            result = RandomNumbers.RandomDouble(range.Min, 40, _Rnd);
            return result;
        }
        /// <summary>
        /// Calculate the period of revolution in days
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        private double CalculatePeriodOfRevolution(double distance, double radius, double rate)
        {
            double totalDistance = distance + (radius * rate);
            return Math.Truncate((Math.Sqrt(Math.Pow(totalDistance, 3)) * 0.78) * 100) / 100;
        }
        /// <summary>
        /// Calculate the period of rotation
        /// es 0.45, 0.2, 4.9 50 - 100
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="revolution"></param>
        /// <returns></returns>
        public double CalculatePeriodOfRotation(double distance, double revolution, double density, Random _Rnd)
        {
            double result = 1;
            bool isRetro = (RandomNumbers.RandomInt(0, 10, _Rnd) == 0) ? true : false;
            if (distance <= _CloseRange.Max)
            {
                result = Math.Truncate(((20 * revolution / distance) * density) * 100) / 100;
            }
            else
            {
                if (density <= 3)
                {
                    // High velocity
                    result = RandomNumbers.RandomDouble(0.4, 0.7, _Rnd);
                }
                else
                {
                    result = RandomNumbers.RandomDouble(1, 7, _Rnd);
                }
            }
            return (isRetro) ? (result * -1) : result;
        }

        #region Wrapper for private methods test
        public double CalculateDistanceTest()
        {
            return this.CalculateDistance(this._CloseRange, this._ForceLiving,new Random());
        }

        public double CalculatePeriodOfRevolutionTest(double distance)
        {
            return this.CalculatePeriodOfRevolution(distance, this._Star.Radius, BasicConstants._StarRadToUaRate);
        }
        #endregion

        /// <summary>
        /// Planet whit masses between 0.05mt and 0.1mt will be placed
        /// as nearer as possible to the star
        /// 
        /// </summary>
        /// <returns></returns>
        public OrbitDetail Generate(Random _Rnd)
        {
            OrbitDetail orbit = new OrbitDetail();
            orbit.DistanceR = this.CalculateDistance(this._CloseRange, this._ForceLiving,_Rnd);
            orbit.Eccentricity = RandomNumbers.RandomDouble(BasicConstants._MinEcc, BasicConstants._MaxEccc, _Rnd);
            orbit.PeriodOfRevolution = this.CalculatePeriodOfRevolution(orbit.DistanceR, this._Star.Radius, BasicConstants._StarRadToUaRate);
            orbit.TetaZero = RandomNumbers.RandomDouble(0.1, 3, _Rnd);
            return orbit;
        }
        /// <summary>
        /// Calculate satellite main orbit
        /// </summary>
        /// <returns></returns>
        public OrbitDetail GenerateSatellite(Random _Rnd)
        {
            OrbitDetail orbit = new OrbitDetail();
            orbit.DistanceR = this.CalculateDistance(this._SatelliteRange, false,_Rnd);
            orbit.Eccentricity = RandomNumbers.RandomDouble(BasicConstants._MinEcc, BasicConstants._MaxEccc, _Rnd);
            orbit.PeriodOfRevolution = this.CalculatePeriodOfRevolution(orbit.DistanceR, this._PlanetRadius, 0);
            orbit.TetaZero = RandomNumbers.RandomDouble(0.1, 3, _Rnd);
            return orbit;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                _CloseRange = new DoubleRange();
                _SatelliteRange = new DoubleRange();
            }
            GC.SuppressFinalize(this);
        }
    }
}
