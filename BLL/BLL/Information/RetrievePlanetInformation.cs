using System;
using Models.Universe;
using UnitOfWork.Implementations.Uows;

namespace BLL.Information
{
    public sealed class RetrievePlanetInformation : IDisposable
    {
        private readonly int _itemId;
        private readonly MainUow _mainUow;
        private bool _disposed;

        public RetrievePlanetInformation(MainUow uow, int itemId)
        {
            if (uow == null) throw new ArgumentNullException("uow");
            if (itemId < 0) throw new ArgumentException("itemId");
            _mainUow = uow;
            _itemId = itemId;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                if (_mainUow != null) _mainUow.Dispose();
            }
           // GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Retrieve PlanetInfo
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public Planet Retrieve(string cacheKey)
        {
            return _mainUow.PlanetRepository.GetByKey(_itemId, cacheKey);
        }
    }
}