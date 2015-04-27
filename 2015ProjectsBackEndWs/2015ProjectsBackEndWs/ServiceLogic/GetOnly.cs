﻿using _2015ProjectsBackEndWs.DataMapper;
using _2015ProjectsBackEndWs.DTO.Universe;
using _2015ProjectsBackEndWs.DTO.UtilityDto;
using _2015ProjectsBackEndWs.Utility;
using BLL.Information;
using BLL.Utilities.Structs;
using Models.Universe;
using System;
using System.Collections.Generic;
using UnitOfWork.Cache;
using UnitOfWork.Implementations.Context;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Implementations.Uows.UowDto;
using UnitOfWork.Interfaces.Context;

namespace _2015ProjectsBackEndWs.ServiceLogic
{
    public sealed class GetOnly : IDisposable
    {
        private bool _Disposed = false;
        private ContextFactory _Factory = null;
        private IContext _Context = null;
        private UowRepositories _Repositories = null;
        private MainUow _MainUow = null;
        private DalCache _Cache = null;
        private UowRepositoryFactories _RepoFactory = null;

        public GetOnly()
        {
            _Factory = new ContextFactory();
            _Context = _Factory.Retrieve();
            _Repositories = _Factory.CreateRepositories();
            _Cache = _Factory.CreateCache();
            _RepoFactory = new UowRepositoryFactories(_Context, _Cache, _Repositories);
            _MainUow = new MainUow(_Context, _Cache, _RepoFactory);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="universeRage"></param>
        /// <returns></returns>
        public List<StarDto> ProcessRetrieveMethod(UniverseRangeDto universeRage)
        {
            IntRange rangeX = new IntRange(universeRage.MinX, universeRage.MaxX);
            IntRange rangeY = new IntRange(universeRage.MinY, universeRage.MaxY);
            List<StarDto> stars = new List<StarDto>();
            StarEntityMapper mapper = new StarEntityMapper();
            List<Star> starEntities = RetrieveInformation(ref rangeX, ref rangeY);
            if (starEntities != null)
                stars = mapper.EntityListToModel(starEntities);
            return stars;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public PlanetDto RetrieveSinglePlanet(int id)
        {
            string cacheKey = "SelectPlanetWhereId=" + id;
            RetrievePlanetInformation retrieve = new RetrievePlanetInformation(_MainUow, id);
            PlanetEntityMapper mapper = new PlanetEntityMapper();
            return mapper.EntityToModel(retrieve.Retrieve(cacheKey));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rangeX"></param>
        /// <param name="rangeY"></param>
        /// <returns></returns>
        private List<Star> RetrieveInformation(ref IntRange rangeX, ref IntRange rangeY)
        {
            List<Star> starEntities = new List<Star>();
            RetrieveInformations Retrieve = new RetrieveInformations(_MainUow, rangeX, rangeY);
            string cacheKey = rangeX.Min + CallSeparators.coordX + rangeX.Max + CallSeparators.otherSeparator + rangeY.Min + CallSeparators.coordY + rangeY.Max;
            starEntities = Retrieve.StarsInRange(cacheKey);
            return starEntities;
        }

        public void Dispose()
        {
            if (!_Disposed)
            {
                if (_Factory != null) _Factory.Dispose();
                if (_Repositories != null) _Repositories.Dispose();
                if (_MainUow != null) _MainUow.Dispose();
                if (_RepoFactory != null) _RepoFactory.Dispose();
                _Disposed = true;
            }
            GC.SuppressFinalize(this);
        }
    }
}