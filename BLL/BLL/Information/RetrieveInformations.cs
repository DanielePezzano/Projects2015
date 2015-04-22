using BLL.Utilities.Structs;
using Models.Universe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Implementations.Uows;

namespace BLL.Information
{
    public class RetrieveInformations
    {
        private MainUow _MainUow;
        private IntRange _RangeX;
        private IntRange _RangeY;

        public RetrieveInformations(MainUow uow, IntRange rangeX, IntRange rangeY)
        {
            if (uow != null) this._MainUow = uow; else throw new ArgumentNullException("uow");
            this._RangeX = rangeX;
            this._RangeY = rangeY;
        }

        public List<Star> StarsInRange(string cacheKey)
        {
            return this._MainUow.StarRepository.Get(cacheKey, c => c.CoordinateX >= _RangeX.Min && c.CoordinateY <= _RangeX.Max && c.CoordinateY >= _RangeY.Min && c.CoordinateY <= _RangeY.Max, c => c.OrderBy(x => x.Id), "Planet").ToList();
        }
    }
}
