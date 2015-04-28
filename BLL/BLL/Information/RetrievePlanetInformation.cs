using BLL.Utilities.Structs;
using Models.Universe;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork.Implementations.Uows;

namespace BLL.Information
{
    public sealed class RetrievePlanetInformation : IDisposable
    {
        private MainUow _MainUow = null;
        private bool _Disposed = false;
        private int _ItemId = -1;

        public RetrievePlanetInformation(MainUow uow, int itemId)
        {
            if (uow == null) throw new ArgumentNullException("uow");
            if (itemId < 0) throw new ArgumentException("itemId");
            this._MainUow = uow;
            this._ItemId = itemId;
        }
        /// <summary>
        /// Retrieve PlanetInfo
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public Planet Retrieve(string cacheKey)
        {
            return this._MainUow.PlanetRepository.GetByKey(this._ItemId, cacheKey);
        }        

        public void Dispose()
        {
            if (!_Disposed)
            {
                this._Disposed = true;
                if (this._MainUow!=null) this._MainUow.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
