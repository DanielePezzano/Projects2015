using System;
using BLL.Generation.StarSystem.IstanceFactory;
using SharedDto.UtilityDto;
using UnitOfWork.Implementations.Context;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Implementations.Uows.UowDto;

namespace _2015ProjectsBackEndWs.ServiceLogic
{
    public sealed class SetOnly : IDisposable
    {
        private readonly ContextFactory _factory;
        private readonly MainUow _mainUow;
        private readonly UowRepositoryFactories _repoFactory;
        private readonly UowRepositories _repositories;
        private bool _disposed;

        public SetOnly()
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
            // GC.SuppressFinalize(this);
        }

        public bool GenerateStarSystem(SystemGenerationDto generationData, Random rnd)
        {
            var rangeX = FactoryGenerator.RetrieveIntRange(generationData.MinX, generationData.MaxX);
            var rangeY = FactoryGenerator.RetrieveIntRange(generationData.MinY, generationData.MaxY); 
            var customConditions = FactoryGenerator.RetrieveConditions(generationData.ForceWater, generationData.FoodRich,
                generationData.FoodPoor, generationData.MineralPoor, generationData.MineralRich,
                generationData.MostlyWater, generationData.ForceLiving);
            //var starSystemGenerator = FactoryGenerator.RetrieveStarSystemGenerator(customConditions, rnd, _mainUow,
            //    rangeX, rangeY);
            //var star = starSystemGenerator.Generate(rnd, _mainUow, "");
            //return star != null && starSystemGenerator.WriteToRepository(_mainUow, star, generationData.GalaxyId);
            return false;
        }
    }
}