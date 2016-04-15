using System;
using BLL.Generation.StarSystem.Builders;
using BLL.Utilities.Structs;
using SharedDto.Universe.Stars;

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

        //private static void AssociateSystemToGalaxy(MainUow uow, int galaxyId, Star generatedStar)
        //{
        //    var toAssociate = uow.GalaxyRepository.GetByKey(galaxyId, "");
        //    if (toAssociate != null) generatedStar.Galaxy = toAssociate;
        //}

        public StarDto Generate(Random rnd, string cacheKey)
        {
           return _solarSystemFactory.Constuct(_starGenerator, _starPlacer, _rangeX, _rangeY);
        }

        //public bool WriteToRepository(MainUow uow, Star generatedStarSystem, int galaxyId)
        //{
        //    var result = false;
        //    try
        //    {
        //        AssociateSystemToGalaxy(uow, galaxyId, generatedStarSystem);
        //        uow.StarRepository.Add(generatedStarSystem);
        //        foreach (var planet in generatedStarSystem.Planets)
        //        {
        //            uow.PlanetRepository.Add(planet);
        //            foreach (var satellite in planet.Satellites)
        //            {
        //                uow.SatelliteRepository.Add(satellite);
        //            }
        //        }
        //        uow.Save();
        //        result = true;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //    return result;
        //}
    }
}