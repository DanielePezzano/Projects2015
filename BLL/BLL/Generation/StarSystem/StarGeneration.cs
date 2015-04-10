using Models.Universe;
using Models.Universe.Enum;
using System;
using System.Collections.Generic;
using UnitOfWork.Interfaces.UnitOfWork;

namespace BLL.Generation.StarSystem
{
    public sealed class StarGeneration
    {
        private IUnitOfWork _Uow = null;
        private Galaxy _Galaxy;
        private static Random _Rnd;

        public StarGeneration(IUnitOfWork uow,Galaxy galaxy)
        {
            this._Uow = uow;
            this._Galaxy = galaxy;
            _Rnd = new Random();
        }


        #region Star Color
        
        #endregion

        #region Star Type
        
        #endregion

        #region other
        private int DetermineSurfaceTemp(StarColor starColor, StarType starType)
        {
            throw new NotImplementedException();
        }
        private int DetermineStarRadiation(StarType starType, StarColor starColor)
        {
            throw new NotImplementedException();
        }

        private float DetermineStarMass(StarType starType, StarColor starColor)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region position
        #endregion

        #region public exposed methods
        public Star CreateNewStar()
        {
            Star result = new Star();
            result.CreatedAt = DateTime.Now;
            result.UpdatedAt = DateTime.Now;
            result.Name = "Str-" + DateTime.Now.GetHashCode();
            result.StarColor = StarProperties.DetermineStarColor(_Rnd.Next(0, 100));
            result.StarType = StarProperties.DetermineStarType(result.StarColor);
            result.SurfaceTemp = this.DetermineSurfaceTemp(result.StarColor, result.StarType);
            result.Mass = this.DetermineStarMass(result.StarType, result.StarColor);
            result.RadiationLevel = this.DetermineStarRadiation(result.StarType, result.StarColor);
            result.Universe = this._Galaxy;
            result.Satellites = new List<Satellite>();
            return result;
        }
        #endregion
    }
}
