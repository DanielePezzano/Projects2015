using System;
using System.Collections.Generic;
using BLL.Utilities;
using BLL.Utilities.Structs;
using Models.Universe;
using Models.Universe.Enum;

namespace BLL.Generation.StarSystem.Builders
{
    public sealed class StarBuilder : IDisposable
    {
        private static Random _rnd;
        private bool _disposed;

        public StarBuilder()
        {
            _rnd = new Random();
        }
        
        #region public exposed methods
        /// <summary>
        /// It creates a bran new star without placing it and without any satellites
        /// </summary>
        /// <returns></returns>
        public Star CreateBrandNewStar()
        {
            var result = new Star
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Name = "NS-" + DateTime.Now.ToFileTimeUtc(),
                StarColor = StarProperties.DetermineStarColor(_rnd.Next(StarProperties.MinBaseRange, 100))
            };
            result.StarType = StarProperties.DetermineStarType(result.StarColor, _rnd.Next(StarProperties.MinBaseRange, 100));
            result.SurfaceTemp = StarProperties.DetermineSurfaceTemp(result.StarColor, result.StarType, _rnd.Next(StarProperties.MinBaseRange, StarProperties.MaxBaseRange));
            result.Mass = StarProperties.DetermineStarMass(result.StarType, result.StarColor, _rnd.Next(StarProperties.MinBaseRange, StarProperties.MaxBaseRange));
            result.RadiationLevel = StarProperties.DetermineStarRadiation(result.StarColor, _rnd.Next(StarProperties.MinBaseRange, StarProperties.MaxBaseRange));
            result.Radius = StarProperties.DetermineStarRadius(result.StarColor, result.StarType, _rnd.Next(StarProperties.MinBaseRange, StarProperties.MaxBaseRange));
            result.Planets = new List<Planet>();
            return result;
        }

        /// <summary>
        ///     Determine the probability range of planet existence, based on the star color
        /// </summary>
        /// <param name="star"></param>
        /// <returns></returns>
        public IntRange CalculatePlanetProbability(Star star)
        {
            var myRange = new IntRange { Min = 0 };
            switch (star.StarColor)
            {
                case StarColor.Blue:
                    myRange.Max = 5;
                    break;
                case StarColor.Orange:
                    myRange.Max = 60;
                    break;
                case StarColor.Red:
                    myRange.Max = 50;
                    break;
                case StarColor.White:
                    myRange.Max = 20;
                    break;
                case StarColor.Yellow:
                    myRange.Max = 70;
                    break;
            }
            return myRange;
        }


        /// <summary>
        ///     Determine if there are planets
        /// </summary>
        /// <param name="planetProb"></param>
        /// <param name="rnd"></param>
        /// <returns></returns>
        public bool HasPlanets(IntRange planetProb, Random rnd)
        {
            var seed = RandomNumbers.RandomInt(0, 100, rnd);
            return (seed <= planetProb.Max);
        }

        /// <summary>
        ///     Determine the number of present planets
        /// </summary>
        /// <param name="planetRage"></param>
        /// <param name="rnd"></param>
        /// <returns></returns>
        public int CalculateNumberOfPlanets(IntRange planetRage, Random rnd)
        {
            int result;
            using (var conversion = new ScaleConversion(100, (planetRage.Max - planetRage.Min)))
            {
                result = (int)conversion.Convert(RandomNumbers.RandomInt(0, 100, rnd));
            }
            return result;
        }

        /// <summary>
        ///     Determine the max number of possible plantes, based on the star color
        /// </summary>
        /// <param name="star"></param>
        /// <returns></returns>
        public IntRange CalculateMaxNumberOfPlanet(Star star)
        {
            var myRange = new IntRange { Min = 0 };

            switch (star.StarColor)
            {
                case StarColor.Blue:
                case StarColor.White:
                    myRange.Max = 2;
                    break;
                case StarColor.Orange:
                case StarColor.Yellow:
                    myRange.Max = 9;
                    break;
                case StarColor.Red:
                    myRange.Max = 5;
                    break;
                default:
                    myRange.Max = 1;
                    break;
            }
            return myRange;
        }
        #endregion

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
            }
           // GC.SuppressFinalize(this);
        }
    }
}
