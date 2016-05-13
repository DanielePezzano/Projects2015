using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Information.Struct;
using BLL.Utilities.Structs;
using DAL.Operations.Enums;
using DAL.Operations.IstanceFactory;
using Models.Universe;
using UnitOfWork.Interfaces.UnitOfWork;

namespace BLL.Information
{
    public sealed class RetrieveInformations : IDisposable
    {
        private bool _disposed;
        private OpFactory _opFactory;
        private IntRange _rangeX;
        private IntRange _rangeY;

        /// <summary>
        ///     Costruttore
        /// </summary>
        /// <param name="rangeX"></param>
        /// <param name="rangeY"></param>
        /// <param name="opFactory"></param>
        public RetrieveInformations(IntRange rangeX, IntRange rangeY,OpFactory opFactory)
        {
            if (opFactory==null) throw new ArgumentNullException(nameof(opFactory));
            _rangeX = rangeX;
            _rangeY = rangeY;
            _opFactory = opFactory;
        }

        /// <summary>
        ///     Costruttore generico
        /// </summary>
        public RetrieveInformations(OpFactory opFactory)
        {
            if (opFactory == null) throw new ArgumentNullException(nameof(opFactory));
            _opFactory = opFactory;
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _opFactory = null;
        }

        /// <summary>
        ///     Recupera la porzione di universo richiesta
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        public List<Star> StarsInRange(string cacheKey,IUnitOfWork uow=null)
        {
            return _opFactory.SetOperation<Star>(MappedRepositories.StarRepository, MappedOperations.FindBy, cacheKey,
                c =>
                    c.CoordinateX >= _rangeX.Min && c.CoordinateY <= _rangeX.Max && c.CoordinateY >= _rangeY.Min &&
                    c.CoordinateY <= _rangeY.Max, uow).RawResult as List<Star>;
        }

        /// <summary>
        ///     Ritorna una lista di DTO fruibili per riempire un menu a tendina
        /// </summary>
        /// <returns></returns>
        public List<DropDownInfo> GetUniverseList(string cacheKey,IUnitOfWork uow=null)
        {
            var result = new List<DropDownInfo>();
            var collection = _opFactory.SetOperation<Galaxy>(MappedRepositories.GalaxyRepository,
                MappedOperations.GetNameAndId, cacheKey, null, uow);

            if (collection.RawResult == null) return result;

            result.AddRange(((List<Galaxy>)collection.RawResult).Select(item => new DropDownInfo(item.Id, item.Name)));
            return result;
        }
    }
}