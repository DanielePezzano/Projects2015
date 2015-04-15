using Models.Universe;
using System;
using System.Collections.Generic;

namespace BLL.Generation.StarSystem
{
    public sealed class StarGenerator
    {
        private static Random _Rnd;

        public StarGenerator()
        {
            _Rnd = new Random();
        }
        
        #region public exposed methods
        /// <summary>
        /// It creates a bran new star without placing it and without any satellites
        /// </summary>
        /// <returns></returns>
        public Star CreateBrandNewStar()
        {
            Star result = new Star();
            result.CreatedAt = DateTime.Now;
            result.UpdatedAt = DateTime.Now;
            result.Name = "NS-" + DateTime.Now.ToFileTimeUtc();
            result.StarColor = StarProperties.DetermineStarColor(_Rnd.Next(StarProperties.MinBaseRange, 100));
            result.StarType = StarProperties.DetermineStarType(result.StarColor, _Rnd.Next(StarProperties.MinBaseRange, 100));
            result.SurfaceTemp = StarProperties.DetermineSurfaceTemp(result.StarColor, result.StarType, _Rnd.Next(StarProperties.MinBaseRange, StarProperties.MaxBaseRange));
            result.Mass = StarProperties.DetermineStarMass(result.StarType, result.StarColor, _Rnd.Next(StarProperties.MinBaseRange, StarProperties.MaxBaseRange));
            result.RadiationLevel = StarProperties.DetermineStarRadiation(result.StarColor, _Rnd.Next(StarProperties.MinBaseRange, StarProperties.MaxBaseRange));
            result.Radius = StarProperties.DetermineStarRadius(result.StarColor, result.StarType, _Rnd.Next(StarProperties.MinBaseRange, StarProperties.MaxBaseRange));
            result.Planets = new List<Planet>();
            return result;
        }
        #endregion
    }
}
