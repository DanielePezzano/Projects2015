using System;
using System.Collections.Generic;
using BLL.Utilities;
using BLL.Utilities.Structs;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Stars;
using SharedDto.UtilityDto;

namespace BLL.Generation.StarSystem.Builders
{
    public sealed class StarBuilder : IDisposable
    {
        private static Random _rnd;
        private bool _disposed;

        public StarBuilder(Random rnd)
        {
            _rnd = rnd;
        }

        #region Private Methods

        private static void AssignRadius(StarDto result)
        {
            result.Radius = StarProperties.DetermineStarRadius(result.StarColor, result.StarType,
                _rnd.Next(StarProperties.MinBaseRange, StarProperties.MaxBaseRange));
        }

        private static void AssignRadiation(StarDto result)
        {
            result.RadiationLevel = StarProperties.DetermineStarRadiation(result.StarColor,
                _rnd.Next(StarProperties.MinBaseRange, StarProperties.MaxBaseRange));
        }

        private static void AssignMass(StarDto result)
        {
            result.Mass = StarProperties.DetermineStarMass(result.StarType, result.StarColor,
                _rnd.Next(StarProperties.MinBaseRange, StarProperties.MaxBaseRange));
        }

        private static void AssignSurfaceTemp(StarDto result)
        {
            result.SurfaceTemp = StarProperties.DetermineSurfaceTemp(result.StarColor, result.StarType,
                _rnd.Next(StarProperties.MinBaseRange, StarProperties.MaxBaseRange));
        }

        private static void AssignType(StarDto result)
        {
            result.StarType = StarProperties.DetermineStarType(result.StarColor,
                _rnd.Next(StarProperties.MinBaseRange, 100));
        }

        #endregion



        #region public exposed methods
        /// <summary>
        /// It creates a bran new star without placing it and without any satellites
        /// </summary>
        /// <returns></returns>
        public StarDto CreateBrandNewStar(int galaxyId=-1)
        {
            var result = new StarDto
            {
                Name = "NS-" + RandomNumbers.RandomString(7,_rnd),
                StarColor = StarProperties.DetermineStarColor(_rnd.Next(StarProperties.MinBaseRange, 100)),
                Planets = new List<PlanetDto>(),
                Id = -1,
                GalaxyId = galaxyId,
                CreatedAt = DateTime.Now
            };
            AssignType(result);
            AssignSurfaceTemp(result);
            AssignMass(result);
            AssignRadiation(result);
            AssignRadius(result);
            return result;
        }



        /// <summary>
        ///     Determine the probability range of planet existence, based on the star color
        /// </summary>
        /// <param name="star"></param>
        /// <returns></returns>
        public IntRange CalculatePlanetProbability(StarDto star)
        {
            var myRange = new IntRange { Min = 0 };
            var determiner = StarProperties.SwitchColorDeterminer(star.StarColor);
            switch (determiner)
            {
                case 0:
                    myRange.Max = 5;
                    break;
                case 3:
                    myRange.Max = 60;
                    break;
                case 1:
                    myRange.Max = 50;
                    break;
                case 2:
                    myRange.Max = 20;
                    break;
                case 4:
                    myRange.Max = 70;
                    break;
                default:
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
            return seed <= planetProb.Max;
        }

        /// <summary>
        ///     Determine the number of present planets
        /// </summary>
        /// <param name="planetRage"></param>
        /// <param name="rnd"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int CalculateNumberOfPlanets(IntRange planetRage, Random rnd, SystemGenerationDto conditions)
        {
            int result;
            using (var conversion = new ScaleConversion(100, planetRage.Max - planetRage.Min))
            {
                result = (int)conversion.Convert(RandomNumbers.RandomInt(0, 100, rnd));
            }
            if (result == 0 && (conditions.ForceLiving || conditions.MostlyWater || conditions.ForceWater))
                result = 1;
            return result;
        }

        /// <summary>
        ///     Determine the max number of possible plantes, based on the star color
        /// </summary>
        /// <param name="star"></param>
        /// <returns></returns>
        public IntRange CalculateMaxNumberOfPlanet(StarDto star)
        {
            var myRange = new IntRange { Min = 0 };
            var determiner = StarProperties.SwitchColorDeterminer(star.StarColor);
            switch (determiner)
            {
                case 0:
                case 2:
                    myRange.Max = 2;
                    break;
                case 3:
                case 4:
                    myRange.Max = 9;
                    break;
                case 1:
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
        }
    }
}
