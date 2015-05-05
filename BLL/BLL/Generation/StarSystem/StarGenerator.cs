using Models.Universe;
using System;
using System.Collections.Generic;

namespace BLL.Generation.StarSystem
{
    public sealed class StarGenerator : IDisposable
    {
        private static Random _rnd;
        private bool _disposed;

        public StarGenerator()
        {
            _rnd = new Random();
        }
        
        #region public exposed methods
        /// <summary>
        /// It creates a bran new star without placing it and without any satellites
        /// </summary>
        /// <returns></returns>
        public Star CreateBrandNewStar()
        {
            var result = new Star
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Name = "NS-" + DateTime.Now.ToFileTimeUtc(),
                StarColor = StarProperties.DetermineStarColor(_rnd.Next(StarProperties.MinBaseRange, 100))
            };
            result.StarType = StarProperties.DetermineStarType(result.StarColor, _rnd.Next(StarProperties.MinBaseRange, 100));
            result.SurfaceTemp = StarProperties.DetermineSurfaceTemp(result.StarColor, result.StarType, _rnd.Next(StarProperties.MinBaseRange, StarProperties.MaxBaseRange));
            result.Mass = StarProperties.DetermineStarMass(result.StarType, result.StarColor, _rnd.Next(StarProperties.MinBaseRange, StarProperties.MaxBaseRange));
            result.RadiationLevel = StarProperties.DetermineStarRadiation(result.StarColor, _rnd.Next(StarProperties.MinBaseRange, StarProperties.MaxBaseRange));
            result.Radius = StarProperties.DetermineStarRadius(result.StarColor, result.StarType, _rnd.Next(StarProperties.MinBaseRange, StarProperties.MaxBaseRange));
            result.Planets = new List<Planet>();
            return result;
        }
        #endregion

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
            }
           // GC.SuppressFinalize(this);
        }
    }
}
