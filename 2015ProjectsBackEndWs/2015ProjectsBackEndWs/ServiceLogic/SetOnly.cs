using BLL.Generation;
using BLL.Generation.StarSystem;
using BLL.Utilities.Structs;
using SharedDto;
using System;
using UnitOfWork.Cache;
using UnitOfWork.Implementations.Context;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Implementations.Uows.UowDto;
using UnitOfWork.Interfaces.Context;

namespace _2015ProjectsBackEndWs.ServiceLogic
{
    public sealed class SetOnly : IDisposable
    {
        private bool _Disposed = false;
        private ContextFactory _Factory = null;
        private IContext _Context = null;
        private UowRepositories _Repositories = null;
        private MainUow _MainUow = null;
        private DalCache _Cache = null;
        private UowRepositoryFactories _RepoFactory = null;

        public SetOnly()
        {
            _Factory = new ContextFactory();
            _Context = _Factory.Retrieve();
            _Repositories = _Factory.CreateRepositories();
            _Cache = _Factory.CreateCache();
            _RepoFactory = new UowRepositoryFactories(_Context, _Cache, _Repositories);
            _MainUow = new MainUow(_Context, _Cache, _RepoFactory);
        }

        public bool GenerateStarSystem(PlanetGenerationDto generationData, Random Rnd)
        {
            IntRange rangeX = new IntRange(generationData.MinX, generationData.MaxX);
            IntRange rangeY = new IntRange(generationData.MinY, generationData.MaxY);
            PlanetCustomConditions customConditions = new PlanetCustomConditions()
            {
                ForceLiving = generationData.ForceLiving,
                ForceWater = generationData.ForceWater,
                MostlyWater = generationData.MostlyWater,
                MineralPoor = generationData.MineralPoor,
                MineralRich = generationData.MineralRich,
                FoodPoor = generationData.FoodPoor,
                FoodRich = generationData.FoodRich
            };

            GeneratePortion generator = new GeneratePortion(
                            rangeX.Min,
                            rangeX.Max,
                            rangeY.Min,
                            rangeY.Max,
                            _MainUow,
                            customConditions,
                            generationData.Galaxy_Id
                            );
            return generator.Generate(Rnd);
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