using System;
using BLL.Generation.StarSystem;
using BLL.Utilities.Structs;
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
            var rangeX = new IntRange(generationData.MinX, generationData.MaxX);
            var rangeY = new IntRange(generationData.MinY, generationData.MaxY);
            var customConditions = new PlanetCustomConditions
            {
                ForceLiving = generationData.ForceLiving,
                ForceWater = generationData.ForceWater,
                MostlyWater = generationData.MostlyWater,
                MineralPoor = generationData.MineralPoor,
                MineralRich = generationData.MineralRich,
                FoodPoor = generationData.FoodPoor,
                FoodRich = generationData.FoodRich
            };

            //var generator = new GeneratePortion(
            //    rangeX.Min,
            //    rangeX.Max,
            //    rangeY.Min,
            //    rangeY.Max,
            //    _mainUow,
            //    customConditions,
            //    generationData.GalaxyId
            //    );
            //return generator.Generate(rnd);
            return false;
        }
    }
}