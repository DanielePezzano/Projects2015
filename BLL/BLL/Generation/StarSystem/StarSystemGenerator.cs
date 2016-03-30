using System;
using BLL.Generation.StarSystem.Builders;
using BLL.Generation.StarSystem.Factories;
using BLL.Utilities.Structs;
using Models.Universe;
using UnitOfWork.Implementations.Uows;

namespace BLL.Generation.StarSystem
{
    public sealed class StarSystemGenerator
    {
        private readonly IntRange _rangeX;
        private readonly IntRange _rangeY;
        private readonly StarBuilder _starGenerator;
        private readonly StarPlacer _starPlacer;
        private readonly SolarSystemFactory _solarSystemFactory;

        public StarSystemGenerator(SolarSystemFactory factory,StarBuilder starGenerator,
            StarPlacer starPlacer,
            IntRange rangeX,
            IntRange rangeY)
        {
            _solarSystemFactory = factory;
            _starGenerator = starGenerator;
            _starPlacer = starPlacer;
            _rangeX = rangeX;
            _rangeY = rangeY;
        }
        
        public Star Generate(Random rnd, MainUow uow, Galaxy galaxy, string cacheKey)
        {
           return _solarSystemFactory.Constuct(_starGenerator, _starPlacer, _rangeX, _rangeY, cacheKey);
        }

        public void WriteToRepository(MainUow uow, Star generatedStarSystem, Galaxy galaxy)
        {
            generatedStarSystem.Galaxy = galaxy;
            uow.StarRepository.Add(generatedStarSystem);
             foreach (var planet in generatedStarSystem.Planets)
             {
                 uow.PlanetRepository.Add(planet);
                 foreach (var satellite in planet.Satellites)
                 {
                     uow.SatelliteRepository.Add(satellite);
                 }
             }
             uow.Save();
        }
    }
}