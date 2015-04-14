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
        
        #region public exposed methods
        public Star CreateNewStar()
        {
            Star result = new Star();
            result.CreatedAt = DateTime.Now;
            result.UpdatedAt = DateTime.Now;
            result.Name = "Str-" + DateTime.Now.ToFileTimeUtc();
            result.StarColor = StarProperties.DetermineStarColor(_Rnd.Next(StarProperties.MinBaseRange, 100));
            result.StarType = StarProperties.DetermineStarType(result.StarColor, _Rnd.Next(StarProperties.MinBaseRange, 100));
            result.SurfaceTemp = StarProperties.DetermineSurfaceTemp(result.StarColor, result.StarType, _Rnd.Next(StarProperties.MinBaseRange, StarProperties.MaxBaseRange));
            result.Mass = StarProperties.DetermineStarMass(result.StarType, result.StarColor, _Rnd.Next(StarProperties.MinBaseRange, StarProperties.MaxBaseRange));
            result.RadiationLevel = StarProperties.DetermineStarRadiation(result.StarColor, _Rnd.Next(StarProperties.MinBaseRange, StarProperties.MaxBaseRange));
            
            result.Universe = this._Galaxy;
            result.Satellites = new List<Satellite>();
            return result;
        }
        #endregion
    }
}
