using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Information.Struct;
using BLL.Utilities.Structs;
using Models.Universe;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Interfaces.UnitOfWork;

namespace BLL.Information
{
    public sealed class RetrieveInformations : IDisposable
    {
        private readonly IUnitOfWork _mainUow;
        private bool _disposed;
        private bool _isTest;
        private IntRange _rangeX;
        private IntRange _rangeY;

        /// <summary>
        ///     Costruttore
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="rangeX"></param>
        /// <param name="rangeY"></param>
        /// <param name="isTest"></param>
        public RetrieveInformations(IUnitOfWork uow, IntRange rangeX, IntRange rangeY, bool isTest=false)
        {
            if (uow != null) _mainUow = uow;
            else throw new ArgumentNullException(nameof(uow));
            _rangeX = rangeX;
            _rangeY = rangeY;
            _isTest = isTest;
        }

        /// <summary>
        ///     Costruttore generico
        /// </summary>
        public RetrieveInformations(ProductionUow uow)
        {
            if (uow != null) _mainUow = uow;
            else throw new ArgumentNullException(nameof(uow));
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            if (_isTest) ((TestUow)_mainUow)?.Dispose();
            else ((ProductionUow)_mainUow)?.Dispose();
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
                result = (_isTest) ? ((TestUow)_mainUow)?.StarRepository.FindBy(
                        c =>
                            c.CoordinateX >= _rangeX.Min && c.CoordinateY <= _rangeX.Max && c.CoordinateY >= _rangeY.Min &&
                            c.CoordinateY <= _rangeY.Max, cacheKey).OrderBy(c => c.Id).ToList():
                    ((ProductionUow)_mainUow)?.StarRepository.FindBy(
                        c =>
                            c.CoordinateX >= _rangeX.Min && c.CoordinateY <= _rangeX.Max && c.CoordinateY >= _rangeY.Min &&
                            c.CoordinateY <= _rangeY.Max, cacheKey).OrderBy(c => c.Id).ToList();
            }
            catch (InvalidCastException)
            {
                //Accade soltanto in fase di test, perché l'unità di lavoro del repository di test non implementa FindBy
                result = (_isTest) ?((TestUow)_mainUow)?.StarRepository.Get(string.Empty,
                        c =>
                            c.CoordinateX >= _rangeX.Min && c.CoordinateY <= _rangeX.Max && c.CoordinateY >= _rangeY.Min &&
                            c.CoordinateY <= _rangeY.Max).ToList():
                            ((ProductionUow)_mainUow)?.StarRepository.Get(string.Empty,
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
            var collection = (_isTest)
                ? ((TestUow) _mainUow)?.GalaxyRepository.GetAll("RetrieveUniverseListForm")
                    .Select(c => new {c.Name, c.Id})
                : ((ProductionUow) _mainUow)?.GalaxyRepository.GetAll("RetrieveUniverseListForm")
                    .Select(c => new {c.Name, c.Id});

            if (collection == null) return result;

            result.AddRange(collection.Select(item => new DropDownInfo(item.Id, item.Name)));
            return result;
        }
    }
}