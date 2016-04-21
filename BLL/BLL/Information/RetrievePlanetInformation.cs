using System;
using Models.Universe;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Interfaces.UnitOfWork;

namespace BLL.Information
{
    public sealed class RetrievePlanetInformation : IDisposable
    {
        private readonly int _itemId;
        private readonly IUnitOfWork _mainUow;
        private bool _disposed;
        private bool _isTest;

        public RetrievePlanetInformation(IUnitOfWork uow, int itemId,bool isTest=false)
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (itemId < 0) throw new ArgumentException(nameof(itemId));
            _mainUow = uow;
            _itemId = itemId;
            _isTest = isTest;
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            if (_isTest) ((TestUow) _mainUow)?.Dispose();
            else ((ProductionUow) _mainUow)?.Dispose();
        }

        /// <summary>
        ///     Retrieve PlanetInfo
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public Planet Retrieve(string cacheKey)
        {
            return (_isTest) ? ((TestUow)_mainUow)?.PlanetRepository.GetByKey(_itemId, cacheKey):
                ((ProductionUow)_mainUow)?.PlanetRepository.GetByKey(_itemId, cacheKey);
        }
    }
}