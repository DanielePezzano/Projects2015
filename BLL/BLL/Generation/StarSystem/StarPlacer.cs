using System;
using BLL.Utilities.Structs;
using Models.Universe.Strcut;
using SharedDto.Universe.Stars;
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
        /// <returns></returns>
        private bool ValidPlace(Coordinates coord)
        {
            //find the stars within minimum distance
            var count =
                _uow.StarRepository.Count(
                    s =>
                        s.CoordinateX >= coord.X - MinDistance && s.CoordinateX <= coord.X + MinDistance &&
                        s.CoordinateX >= coord.Y - MinDistance && s.CoordinateX <= coord.Y + MinDistance, "");
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
        /// <param name="rangeX"></param>
        public void Place(StarDto star, IntRange rangeX, IntRange rangeY, Random rnd)
        {
            var invalidPlaceCounter = 0;
            var coord = GenerateRandomCoordinates(rangeX.Min, rangeX.Max, rangeY.Min, rangeY.Max, rnd);
            var validCoordinates = ValidPlace(coord);

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
                validCoordinates = ValidPlace(coord);
            }
            star.PositionX = coord.X;
            star.PositgionY = coord.Y;
        }

        #region Wrapper for testing private methods

        public bool ValidPlaceTest(Coordinates coordinate)
        {
            return ValidPlace(coordinate);
        }

        public Coordinates GenerateRandomCoordinatesTest(int minX, int maxX, int minY, int maxY, Random rnd)
        {
            return GenerateRandomCoordinates(minX, maxX, minY, maxY, rnd);
        }

        #endregion
    }
}