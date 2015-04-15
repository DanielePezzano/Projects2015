using Models.Universe;
using Models.Universe.Strcut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Interfaces.UnitOfWork;

namespace BLL.Generation.StarSystem
{
    public sealed class StarPlacer
    {
        private MainUow _Uow = null;
        private Galaxy _Galaxy;
        private const int _MinDistance = 25;
        private static Random _Rnd;

        public StarPlacer(IUnitOfWork uow, Galaxy galaxy)
        {
            this._Galaxy = galaxy;
            this._Uow = (MainUow)uow;
            _Rnd = new Random();
        }

        private Coordinates GenerateRandomCoordinates(int minX, int maxX, int minY, int maxY)
        {
            return new Coordinates(_Rnd.Next(minX, maxX), _Rnd.Next(minY, maxY));
        }
        /// <summary>
        /// Check If coordinates are valid:
        /// are there any stars whitin the minimum distance?
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        private bool ValidPlace(Coordinates coord)
        {
            //find the stars within minimum distance
            bool result = false;
            int count = this._Uow.StarRepository.Count(x =>x.Coordinate.X >= (coord.X - _MinDistance) && x.Coordinate.X <= (coord.X + _MinDistance) && x.Coordinate.Y >= (coord.Y - _MinDistance) && x.Coordinate.Y <= (coord.Y + _MinDistance));
            result = (count > 0) ? false : true;
            return result;
        }

        #region Wrapper for testing private methods
        public bool ValidPlaceTest(Coordinates coordinate)
        {
            return this.ValidPlace(coordinate);
        }

        public Coordinates GenerateRandomCoordinatesTest(int minX, int maxX,int minY, int maxY)
        {
            return this.GenerateRandomCoordinates(minX, maxX, minY, maxY);
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
        public void Place(Star star, int minX, int maxX, int minY, int maxY)
        {
            int invalidPlaceCounter = 0;           
            Coordinates coord = this.GenerateRandomCoordinates(minX, maxX, minY, maxY);
            bool validCoordinates = this.ValidPlace(coord);

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
                coord = this.GenerateRandomCoordinates(minX, maxX, minY, maxY);
                validCoordinates = this.ValidPlace(coord);
            }
            star.Coordinate = coord;
        }
    }
}
