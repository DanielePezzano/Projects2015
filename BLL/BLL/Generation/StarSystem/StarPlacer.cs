using System;
using BLL.Utilities.Structs;
using Models.Universe;
using Models.Universe.Strcut;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Interfaces.UnitOfWork;

namespace BLL.Generation.StarSystem
{
    public sealed class StarPlacer
    {
        private const int MinDistance = 25;
        private readonly MainUow _uow;

        public StarPlacer(IUnitOfWork uow)
        {
            _uow = (MainUow) uow;
        }

        private static Coordinates GenerateRandomCoordinates(int minX, int maxX, int minY, int maxY, Random rnd)
        {
            return new Coordinates(rnd.Next(minX, maxX), rnd.Next(minY, maxY));
        }

        /// <summary>
        ///     Check If coordinates are valid:
        ///     are there any stars whitin the minimum distance?
        /// </summary>
        /// <param name="coord"></param>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        private bool ValidPlace(Coordinates coord, string cacheKey)
        {
            //find the stars within minimum distance
            var count =
                _uow.StarRepository.Count(
                    s =>
                        s.CoordinateX >= coord.X - MinDistance && s.CoordinateX <= coord.X + MinDistance &&
                        s.CoordinateX >= coord.Y - MinDistance && s.CoordinateX <= coord.Y + MinDistance, cacheKey);
            var result = count <= 0;
            return result;
        }

        /// <summary>
        ///     it Place a newly created star in the first available point
        ///     of the universe, inside the given square.
        ///     Note: if no place is found, the given square will be "enlarged" until ok
        /// </summary>
        /// <param name="star"></param>
        /// <param name="rangeY"></param>
        /// <param name="rnd">Random Seeder</param>
        /// <param name="cacheKey">Valid Place Cache Key</param>
        /// <param name="rangeX"></param>
        public void Place(Star star, IntRange rangeX, IntRange rangeY, Random rnd, string cacheKey)
        {
            var invalidPlaceCounter = 0;
            var coord = GenerateRandomCoordinates(rangeX.Min, rangeX.Max, rangeY.Min, rangeY.Max, rnd);
            var validCoordinates = ValidPlace(coord, cacheKey);

            while (!validCoordinates)
            {
                invalidPlaceCounter++;
                if (invalidPlaceCounter >= 10)
                {
                    rangeX.Min -= MinDistance;
                    rangeX.Max += MinDistance;
                    rangeY.Min -= MinDistance;
                    rangeY.Max += MinDistance;
                }
                coord = GenerateRandomCoordinates(rangeX.Min, rangeX.Max, rangeY.Min, rangeY.Max, rnd);
                validCoordinates = ValidPlace(coord, string.Empty);
            }
            star.CoordinateX = coord.X;
            star.CoordinateY = coord.Y;
        }

        #region Wrapper for testing private methods

        public bool ValidPlaceTest(Coordinates coordinate)
        {
            return ValidPlace(coordinate, string.Empty);
        }

        public Coordinates GenerateRandomCoordinatesTest(int minX, int maxX, int minY, int maxY, Random rnd)
        {
            return GenerateRandomCoordinates(minX, maxX, minY, maxY, rnd);
        }

        #endregion
    }
}