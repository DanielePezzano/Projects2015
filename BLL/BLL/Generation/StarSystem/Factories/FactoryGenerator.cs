using System;
using BLL.Generation.StarSystem.Builders;
using BLL.Utilities.Structs;
using Models.Universe;
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

        public static PlanetCustomConditions RetrieveConditions()
        {
            return new PlanetCustomConditions(false, false, false, false, false, false, false);
        }

        public static StarSystemGenerator RetrieveStarSystemGenerator(PlanetCustomConditions  conditions,Random rnd,IUnitOfWork uow,IntRange rangeX,
            IntRange rangeY)
        {
            return new StarSystemGenerator(
                RetrieveNewFactory(conditions, rnd),
                RetrieveNewStarBuilder(),
                RetrieveStarPlacer(uow),
                rangeX,
                rangeY);
        }

        public static OrbitGenerator RetrieOrbitGenerator(Star star, DoubleRange closeRange,PlanetCustomConditions conditions)
        {
            return new OrbitGenerator(star, closeRange, conditions);
        }

        public static IntRange RetrieveIntRange(int minX,int maxX)
        {
            return new IntRange(minX, maxX);
        }
    }
}
