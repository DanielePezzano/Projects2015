using System;
using BLL.Generation.StarSystem.Builders;
using BLL.Utilities.Structs;
using UnitOfWork.Interfaces.UnitOfWork;

namespace BLL.Generation.StarSystem.Factories
{
    public static class FactoryGenerator
    {
        public static SolarSystemFactory RetrieveNewFactory(PlanetCustomConditions  conditions,Random rnd)
        {
            return new SolarSystemFactory(conditions,rnd);
        }

        public static StarBuilder RetrieveNewStarBuilder()
        {
            return new StarBuilder();
        }

        public static StarPlacer RetrieveStarPlacer(IUnitOfWork uow)
        {
            return new StarPlacer(uow);
        }

        public static PlanetCustomConditions RetrieveConditions(bool water, bool foodRich, bool foodPoor, bool mineralPoor, bool mineralRich, bool mostlyWater, bool forceLiving)
        {
            return new PlanetCustomConditions(water, foodRich, foodPoor, mineralPoor, mineralRich, mostlyWater,
                forceLiving);
        }

        public static StarSystemGenerator RetrieveStarSystemGenerator(PlanetCustomConditions  conditions,Random rnd,IUnitOfWork uow,IntRange rangeX,
            IntRange rangeY)
        {
            return new StarSystemGenerator(
                FactoryGenerator.RetrieveNewFactory(conditions, rnd),
                FactoryGenerator.RetrieveNewStarBuilder(),
                FactoryGenerator.RetrieveStarPlacer(uow),
                rangeX,
                rangeY);
        }
    }
}
