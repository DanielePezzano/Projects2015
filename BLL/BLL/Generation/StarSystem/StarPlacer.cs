using Models.Universe;
using Models.Universe.Strcut;
using System;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Interfaces.UnitOfWork;

namespace BLL.Generation.StarSystem
{
    public sealed class StarPlacer
    {
        private MainUow _Uow = null;
        private Galaxy _Galaxy;
        private const int _MinDistance = 25;

        public StarPlacer(IUnitOfWork uow, Galaxy galaxy)
        {
            this._Galaxy = galaxy;
            this._Uow = (MainUow)uow;
        }

        private Coordinates GenerateRandomCoordinates(int minX, int maxX, int minY, int maxY,Random _Rnd)
        {
            return new Coordinates(_Rnd.Next(minX, maxX), _Rnd.Next(minY, maxY));
        }
        /// <summary>
        /// Check If coordinates are valid:
        /// are there any stars whitin the minimum distance?
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        private bool ValidPlace(Coordinates coord,string cacheKey)
        {
            //find the stars within minimum distance
            bool result = false;
            int count = this._Uow.StarRepository.Count(s => s.CoordinateX >= (coord.X - _MinDistance) && s.CoordinateX <= (coord.X + _MinDistance) && s.CoordinateX >= (coord.Y - _MinDistance) && s.CoordinateX <= (coord.Y + _MinDistance), cacheKey);
            result = (count > 0) ? false : true;
            return result;
        }

        #region Wrapper for testing private methods
        public bool ValidPlaceTest(Coordinates coordinate)
        {
            return this.ValidPlace(coordinate, string.Empty);
        }

        public Coordinates GenerateRandomCoordinatesTest(int minX, int maxX,int minY, int maxY,Random _Rnd)
        {
            return this.GenerateRandomCoordinates(minX, maxX, minY, maxY,_Rnd);
        } 
        #endregion
        /// <summary>
        /// it Place a newly created star in the first available point
        /// of the universe, inside the given square.
        /// Note: if no place is found, the given square will be "enlarged" until ok
        /// </summary>
        /// <param name="star"></param>
        /// <param name="minX"></param>
        /// <param name="maxX"></param>
        /// <param name="minY"></param>
        /// <param name="maxY"></param>
        /// <param name="_Rnd">Random Seeder</param>
        /// <param name="cacheKey">Valid Place Cache Key</param>
        public void Place(Star star, int minX, int maxX, int minY, int maxY,Random _Rnd,string cacheKey)
        {
            int invalidPlaceCounter = 0;           
            Coordinates coord = this.GenerateRandomCoordinates(minX, maxX, minY, maxY,_Rnd);
            bool validCoordinates = this.ValidPlace(coord, cacheKey);

            while (!validCoordinates)
            {
                invalidPlaceCounter++;
                if (invalidPlaceCounter >= 10)
                {
                    minX -= _MinDistance;
                    maxX += _MinDistance;
                    minY -= _MinDistance;
                    maxX += _MinDistance;
                }
                coord = this.GenerateRandomCoordinates(minX, maxX, minY, maxY,_Rnd);
                validCoordinates = this.ValidPlace(coord, string.Empty);
            }
            star.CoordinateX = coord.X;
            star.CoordinateY = coord.Y;
        }
    }
}
