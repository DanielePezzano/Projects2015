using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Information.Struct;
using BLL.Utilities.Structs;
using Models.Universe;
using UnitOfWork.Implementations.Uows;

namespace BLL.Information
{
    public sealed class RetrieveInformations : IDisposable
    {
        private readonly MainUow _mainUow;
        private bool _disposed;
        private IntRange _rangeX;
        private IntRange _rangeY;

        /// <summary>
        ///     Costruttore
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="rangeX"></param>
        /// <param name="rangeY"></param>
        public RetrieveInformations(MainUow uow, IntRange rangeX, IntRange rangeY)
        {
            if (uow != null) _mainUow = uow;
            else throw new ArgumentNullException(nameof(uow));
            _rangeX = rangeX;
            _rangeY = rangeY;
        }

        /// <summary>
        ///     Costruttore generico
        /// </summary>
        public RetrieveInformations(MainUow uow)
        {
            if (uow != null) _mainUow = uow;
            else throw new ArgumentNullException(nameof(uow));
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _mainUow?.Dispose();
            //GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Recupera la porzione di universo richiesta
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public List<Star> StarsInRange(string cacheKey)
        {
            List<Star> result;
            try
            {
                result =
                    _mainUow.StarRepository.FindBy(
                        c =>
                            c.CoordinateX >= _rangeX.Min && c.CoordinateY <= _rangeX.Max && c.CoordinateY >= _rangeY.Min &&
                            c.CoordinateY <= _rangeY.Max, cacheKey).OrderBy(c => c.Id).ToList();
            }
            catch (InvalidCastException)
            {
                //Accade soltanto in fase di test, perché l'unità di lavoro del repository di test non implementa FindBy
                result =
                    _mainUow.StarRepository.Get(string.Empty,
                        c =>
                            c.CoordinateX >= _rangeX.Min && c.CoordinateY <= _rangeX.Max && c.CoordinateY >= _rangeY.Min &&
                            c.CoordinateY <= _rangeY.Max).ToList();
            }
            return result;
        }

        /// <summary>
        ///     Ritorna una lista di DTO fruibili per riempire un menu a tendina
        /// </summary>
        /// <returns></returns>
        public List<DropDownInfo> GetUniverseList()
        {
            var result = new List<DropDownInfo>();
            foreach (
                var item in _mainUow.GalaxyRepository.GetAll("RetrieveUniverseListForm").Select(c => new {c.Name, c.Id})
                )
            {
                var toAdd = new DropDownInfo(item.Id, item.Name);
                result.Add(toAdd);
            }
            return result;
        }
    }
}