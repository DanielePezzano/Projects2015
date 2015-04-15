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
    public sealed class OrbitGenerator
    {
        private Star _Star;
        private double _PlanetMass;
        private double _PlanetRadius;
        private double _MediumDensity;
        private bool _ForceLiving;
        private DoubleRange _CloseRange;
        private const double _StarRadToUaRate = 0.04;
        private const double _MinEcc = 0.8;
        private const double _MaxEccc = 1.1;

        private static Random _Rnd;

        public OrbitGenerator(Star star, double planetMass, double planetRadius, DoubleRange closeRange,double density, bool forceLiving = false)
        {
            this._Star = star;
            this._PlanetMass = planetMass;
            this._PlanetRadius = planetRadius;
            this._ForceLiving = forceLiving;
            this._CloseRange = closeRange;
            this._MediumDensity = density;
            _Rnd = new Random();
        }
        /// <summary>
        /// Calculate the distance expressed in UA
        /// </summary>
        /// <returns></returns>
        private double CalculateDistance()
        {
            double result = 0.8;
            if (_ForceLiving) return (result + RandomNumbers.RandomDouble(_CloseRange.Min, _CloseRange.Max, _Rnd));
            if (this._PlanetMass <= 0.05)
            {
                //place it very close!
                result = Math.Truncate(RandomNumbers.RandomDouble(_CloseRange.Min, _CloseRange.Max, _Rnd) * 100) / 100;
            }
            else
            {
                result = Math.Truncate(RandomNumbers.RandomDouble(_CloseRange.Max, 40, _Rnd) * 100) / 100;
            }
            return result;
        }
        /// <summary>
        /// Calculate the period of revolution in days
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        private double CalculatePeriodOfRevolution(double distance)
        {
            double totalSunDistance = distance + (_Star.Radius * _StarRadToUaRate);
            return (Math.Truncate(Math.Sqrt(Math.Pow(totalSunDistance, 3)) * 0.78)*100)/100;
        }
        /// <summary>
        /// Calculate the period of rotation
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="revolution"></param>
        /// <returns></returns>
        private double CalculatePeriodOfRotation(double distance, double revolution)
        {
            double result = 1;
            bool isRetro = (RandomNumbers.RandomInt(0, 10, _Rnd) == 0) ? true : false;
            if (distance <= _CloseRange.Max)
            {
                result = Math.Truncate(revolution * 670) / 100;
            }
            else
            {
                if (_MediumDensity <= 3)
                {
                    // High velocity
                    result = Math.Truncate(RandomNumbers.RandomDouble(0.4, 0.7, _Rnd) * 100) / 100;
                }
                else
                {
                    result = Math.Truncate(RandomNumbers.RandomDouble(1, 7, _Rnd) * 100) / 100;
                }
            }
            return (isRetro) ? (result * -1) : result;
        }

        #region Wrapper for private methods test
        public double CalculateDistanceTest()
        {
            return this.CalculateDistance();
        }

        public double CalculatePeriodOfRevolutionTest(double distance)
        {
            return this.CalculatePeriodOfRevolution(distance);
        }
        #endregion

        /// <summary>
        /// Planet whit masses between 0.05mt and 0.1mt will be placed
        /// as nearer as possible to the star
        /// 
        /// </summary>
        /// <returns></returns>
        public OrbitDetail Generate()
        {
            OrbitDetail orbit = new OrbitDetail();
            orbit.DistanceR = this.CalculateDistance();
            orbit.Eccentricity = Math.Truncate(RandomNumbers.RandomDouble(_MinEcc, _MaxEccc, _Rnd) * 100) / 100;
            orbit.PeriodOfRevolution = this.CalculatePeriodOfRevolution(orbit.DistanceR);
            orbit.PeriodOfRotation = this.CalculatePeriodOfRotation(orbit.DistanceR, orbit.PeriodOfRevolution);
            orbit.TetaZero = Math.Truncate(RandomNumbers.RandomDouble(0.1, 3, _Rnd));
            return orbit;
        }
    }
}
