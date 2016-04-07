using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Information;
using BLL.Utilities.Structs;
using Models.Universe;
using SharedDto;
using SharedDto.DataMapper;
using SharedDto.Form.Universe;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Stars;
using SharedDto.UtilityDto;
using UnitOfWork.Implementations.Context;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Implementations.Uows.UowDto;

namespace _2015ProjectsBackEndWs.ServiceLogic
{
    public sealed class GetOnly : IDisposable
    {
        private readonly ContextFactory _factory;
        private readonly MainUow _mainUow;
        private readonly UowRepositoryFactories _repoFactory;
        private readonly UowRepositories _repositories;
        private bool _disposed;

        public GetOnly()
        {
            _factory = new ContextFactory();
            var context = _factory.Retrieve();
            _repositories = _factory.CreateRepositories();
            var cache = _factory.CreateCache();
            _repoFactory = new UowRepositoryFactories(context, cache, _repositories);
            _mainUow = new MainUow(context, _repoFactory);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _factory?.Dispose();
            _repositories?.Dispose();
            _mainUow?.Dispose();
            _repoFactory?.Dispose();
            _disposed = true;
            //GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Restituisce la lista degli universi presenti nel db
        /// </summary>
        /// <returns></returns>
        public GalaxyList RetrieveGalaxyList()
        {
            var result = new GalaxyList {Galaxies = new List<GalaxyForm>()};
            using (var retriever = new RetrieveInformations(_mainUow))
            {
                foreach (var toAdd in retriever.GetUniverseList().Select(item => new GalaxyForm
                {
                    GalaxyId = item.ItemId,
                    Name = item.ItemName
                }))
                {
                    result.Galaxies.Add(toAdd);
                }
            }
            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="universeRage"></param>
        /// <returns></returns>
        public List<StarDto> ProcessRetrieveMethod(UniverseRangeDto universeRage)
        {
            var rangeX = new IntRange(universeRage.MinX, universeRage.MaxX);
            var rangeY = new IntRange(universeRage.MinY, universeRage.MaxY);
            var starEntities = RetrieveInformation(ref rangeX, ref rangeY);
            return starEntities != null ? StarEntityMapper.EntityListToModel(starEntities) : new List<StarDto>();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public PlanetDto RetrieveSinglePlanet(int id)
        {
            var cacheKey = "SelectPlanetWhereId=" + id;
            PlanetDto result;
            using (var retrieve = new RetrievePlanetInformation(_mainUow, id))
            {
                result = PlanetEntityMapper.EntityToModel(retrieve.Retrieve(cacheKey));
            }
            return result;
        }

        /// <summary>
        ///     Controlla esistenza di un email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsEmailFree(string email)
        {
            bool result;
            using (var retrieve = new RetrieveUserInformation(_mainUow, email))
            {
                result = !retrieve.ExistsEmail();
            }
            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="rangeX"></param>
        /// <param name="rangeY"></param>
        /// <returns></returns>
        private List<Star> RetrieveInformation(ref IntRange rangeX, ref IntRange rangeY)
        {
            List<Star> starEntities;
            using (var retrieve = new RetrieveInformations(_mainUow, rangeX, rangeY))
            {
                var cacheKey = rangeX.Min + CallSeparators.CoordX + rangeX.Max + CallSeparators.OtherSeparator +
                                  rangeY.Min + CallSeparators.CoordY + rangeY.Max;
                starEntities = retrieve.StarsInRange(cacheKey);
            }
            return starEntities;
        }
    }
}