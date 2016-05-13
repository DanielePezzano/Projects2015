using System;
using BLL.Utilities.Structs;
using DAL.Operations;
using DAL.Operations.Enums;
using DAL.Operations.IstanceFactory;
using Models.Universe;
using Models.Universe.Strcut;
using SharedDto.Universe.Stars;
using UnitOfWork.Interfaces.UnitOfWork;

namespace BLL.Generation.StarSystem
{
    public sealed class StarPlacer
    {
        private const int MinDistance = 25;
        private OpFactory _opFactory;

        public StarPlacer(OpFactory opFactory)
        {
            _opFactory = opFactory;
        }

        private static Coordinates GenerateRandomCoordinates(int minX, int maxX, int minY, int maxY, Random rnd)
        {
            return new Coordinates(rnd.Next(minX, maxX), rnd.Next(minY, maxY));
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
        /// <param name="uow"></param>
        public void Place(StarDto star, IntRange rangeX, IntRange rangeY, Random rnd,IUnitOfWork uow=null)
        {
            var invalidPlaceCounter = 0;
            var coord = GenerateRandomCoordinates(rangeX.Min, rangeX.Max, rangeY.Min, rangeY.Max, rnd);

            while (!ValidPlace(coord,uow))
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
            }
            star.PositionX = coord.X;
            star.PositionY = coord.Y;
        }

        /// <summary>
        ///     Check If coordinates are valid:
        ///     are there any stars whitin the minimum distance?
        /// </summary>
        /// <param name="coord"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        public bool ValidPlace(Coordinates coord,IUnitOfWork uow=null)
        {
            var cacheKey = $"ValidPlace=>{coord.X}_{coord.Y}";
            return _opFactory.SetOperation<Star>(MappedRepositories.StarRepository, MappedOperations.ValidStarPlace, cacheKey,s => s.CoordinateX >= coord.X - MinDistance && s.CoordinateX <= coord.X + MinDistance &&
                        s.CoordinateX >= coord.Y - MinDistance && s.CoordinateX <= coord.Y + MinDistance,uow).CheckResult;
        }

        #region Wrapper for testing private methods

        public Coordinates GenerateRandomCoordinatesTest(int minX, int maxX, int minY, int maxY, Random rnd)
        {
            return GenerateRandomCoordinates(minX, maxX, minY, maxY, rnd);
        }

        #endregion
    }
}