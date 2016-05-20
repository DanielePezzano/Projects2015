using System;
using BLL.Generation.StarSystem.Builders;
using BLL.Utilities.Structs;
using DAL.Operations.IstanceFactory;
using SharedDto.Universe.Stars;
using SharedDto.UtilityDto;

namespace BLL.Generation.StarSystem.IstanceFactory
{
    public static class FactoryGenerator
    {
        public static SolarSystemFactory RetrieveNewFactory(SystemGenerationDto systemGenerationDto, Random rnd)
        {
            return new SolarSystemFactory(systemGenerationDto, rnd);
        }

        public static StarBuilder RetrieveStarBuilder(Random rnd)
        {
            return new StarBuilder(rnd);
        }

        public static PlanetBuilder RetrievePlanetBuilder()
        {
            return new PlanetBuilder();
        }
        [Obsolete]
        public static SatelliteBuilder RetrieveSatelliteBuilder()
        {
            return new SatelliteBuilder();
        }

        public static StarPlacer RetrieveStarPlacer(OpFactory opFactory)
        {
            return new StarPlacer(opFactory);
        }
        [Obsolete]
        public static PlanetCustomConditions RetrieveConditions(bool water, bool foodRich, bool foodPoor, bool mineralPoor, bool mineralRich, bool mostlyWater, bool forceLiving)
        {
            return new PlanetCustomConditions(water, foodRich, foodPoor, mineralPoor, mineralRich, mostlyWater,
                forceLiving);
        }
        [Obsolete]
        public static PlanetCustomConditions RetrieveConditions()
        {
            return new PlanetCustomConditions(false, false, false, false, false, false, false);
        }

        public static StarSystemGenerator RetrieveStarSystemGenerator(Random rnd, SystemGenerationDto systemGenerationDto, OpFactory opFactory)
        {
            return new StarSystemGenerator(
                RetrieveNewFactory(systemGenerationDto, rnd),
                RetrieveStarBuilder(rnd),
                RetrieveStarPlacer(opFactory),
                UtilitiesFactory.RetrieveRange(systemGenerationDto.MinX,systemGenerationDto.MaxX),
                UtilitiesFactory.RetrieveRange(systemGenerationDto.MinY,systemGenerationDto.MaxY));
        }

        public static OrbitGenerator RetrieveOrbitGenerator(StarDto star, DoubleRange closeRange, SystemGenerationDto conditions)
        {
            return new OrbitGenerator(star, closeRange, conditions);
        }

        public static IntRange RetrieveIntRange(int min,int max)
        {
            return new IntRange(min, max);
        }

        public static SystemGenerationDto RetrieveSystemGenerationDto(bool foodPoor, bool foodRich, bool forceWater,bool forceLiving,
            bool mostlyWater, bool mineralRich, bool mineralPoor, int minX, int maxX, int minY, int maxY)
        {
            return new SystemGenerationDto()
            {
                FoodPoor = foodPoor,
                FoodRich = foodRich,
                ForceWater = forceWater,
                ForceLiving = forceLiving,
                MostlyWater = mostlyWater,
                MineralRich = mineralRich,
                MineralPoor = mineralPoor,
                MinX = minX,
                MaxX = maxX,
                MinY = minY,
                MaxY = maxY

            };
        }
    }
}
