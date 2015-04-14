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
        private IGalaxy _Galaxy;
        private const int _MinDistance = 25;
        private static Random _Rnd;

        public StarPlacer(IUnitOfWork uow, IGalaxy galaxy)
        {
            this._Galaxy = galaxy;
            this._Uow = (MainUow)uow;
            _Rnd = new Random();
        }

        private Coordinates GenerateRandomCoordinates(int min, int max)
        {
            return new Coordinates(_Rnd.Next(min, max), _Rnd.Next(min, max));
        }

        private bool ValidPlace(Coordinates coord)
        {
            //find the stars within minimum distance
            bool result = false;
            int count = this._Uow.StarRepository.Count(x => this._Galaxy.Stars.Any(s => s.Id == x.Id) && x.Coordinate.X >= (coord.X - 50) && x.Coordinate.X <= (coord.X + 50) && x.Coordinate.Y >= (coord.Y - 50) && x.Coordinate.Y <= (coord.Y + 50));
            result = (count > 0) ? false : true;
            return result;
        }

        public bool ValidPlaceTest(Coordinates coordinate)
        {
            return this.ValidPlace(coordinate);
        }

        public Coordinates GenerateRandomCoordinatesTest(int min, int max)
        {
            return this.GenerateRandomCoordinates(min, max);
        }

        public void Place(Star star)
        {
            throw new NotImplementedException();
        }
    }
}
