using System;
using System.Collections.Generic;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using SharedDto.Universe.Technology;

namespace BLL.Engine.Planet.Production.IstanceFactory
{
    public static class FactoryGenerator
    {
        public static OreUpdater RetrieveBuilderOreUpdater(PlanetDto planetDto, RaceDto raceDto,List<TechnologyDto> technologiesDto,DateTime nowTime)
        {
            return new OreUpdater(planetDto, raceDto, technologiesDto, nowTime);
        }

        public static FoodUpdater RetrieveBuilderFoodUpdater(PlanetDto planetDto, RaceDto raceDto, List<TechnologyDto> technologyDtos,DateTime nowTime)
        {
            return new FoodUpdater(planetDto, raceDto, technologyDtos, nowTime);
        }

        public static ResearchUpdater RetrieveBuilderResearchUpdater(PlanetDto planetDto, RaceDto raceDto, List<TechnologyDto> technologyDtos, DateTime nowTime)
        {
            return new ResearchUpdater(planetDto, raceDto, technologyDtos, nowTime);
        }
    }
}