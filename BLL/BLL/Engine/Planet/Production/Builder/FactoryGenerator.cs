using System;
using System.Collections.Generic;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using SharedDto.Universe.Technology;

namespace BLL.Engine.Planet.Production.Builder
{
    public static class FactoryGenerator
    {
        public static OreUpdater RetrieveBuilderOreUpdate(PlanetDto planetDto, RaceDto raceDto,List<TechnologyDto> technologiesDto,DateTime nowTime)
        {
            return new OreUpdater(planetDto, raceDto, technologiesDto, nowTime);
        }

        public static FoodUpdater RetrieveBuilderFoodUpdate(PlanetDto planetDto, RaceDto raceDto, List<TechnologyDto> technologyDtos,DateTime nowTime)
        {
            return new FoodUpdater(planetDto, raceDto, technologyDtos, nowTime);
        }
    }
}