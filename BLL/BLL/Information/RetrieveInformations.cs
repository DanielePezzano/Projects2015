using BLL.Utilities.Structs;
using Models.Universe;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork.Implementations.Uows;

namespace BLL.Information
{
    public sealed class RetrieveInformations : IDisposable
    {
        private MainUow _MainUow;
        private IntRange _RangeX;
        private IntRange _RangeY;
        private bool _Disposed = false;

        /// <summary>
        /// Costruttore
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="rangeX"></param>
        /// <param name="rangeY"></param>
        public RetrieveInformations(MainUow uow, IntRange rangeX, IntRange rangeY)
        {
            if (uow != null) this._MainUow = uow; else throw new ArgumentNullException("uow");
            this._RangeX = rangeX;
            this._RangeY = rangeY;
        }
        /// <summary>
        /// Costruttore generico
        /// </summary>
        public RetrieveInformations(MainUow uow)
        {
            if (uow != null) this._MainUow = uow; else throw new ArgumentNullException("uow");
        }
        /// <summary>
        /// Recupera la porzione di universo richiesta
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public List<Star> StarsInRange(string cacheKey)
        {
            List<Star> result = new List<Star>();
            try
            {
                result = this._MainUow.StarRepository.FindBy(c => c.CoordinateX >= _RangeX.Min && c.CoordinateY <= _RangeX.Max && c.CoordinateY >= _RangeY.Min && c.CoordinateY <= _RangeY.Max, cacheKey).OrderBy(c => c.Id).ToList();
            }
            catch (InvalidCastException ex)
            {
                //Accade soltanto in fase di test, perché l'unità di lavoro del repository di test non implementa FindBy
                result = this._MainUow.StarRepository.Get(string.Empty, c => c.CoordinateX >= _RangeX.Min && c.CoordinateY <= _RangeX.Max && c.CoordinateY >= _RangeY.Min && c.CoordinateY <= _RangeY.Max).ToList();
            }
            catch (Exception ex)
            {
                var e = ex.Message;
                throw;
            }
            return result;
        }
        /// <summary>
        /// Ritorna una lista di DTO fruibili per riempire un menu a tendina
        /// </summary>
        /// <returns></returns>
        public List<DropDownInfo> GetUniverseList()
        {
            List<DropDownInfo> result = new List<DropDownInfo>();
            foreach (var item in this._MainUow.GalaxyRepository.GetAll("RetrieveUniverseListForm").Select(c => new { Name = c.Name, Id = c.Id }))
            {
                DropDownInfo toAdd = new DropDownInfo(item.Id, item.Name);
                result.Add(toAdd);
            }
            return result;
        }

        public void Dispose()
        {
            if (!_Disposed)
            {
                this._Disposed = true;
                if (this._MainUow != null) this._MainUow.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
