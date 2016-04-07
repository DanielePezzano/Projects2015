using System;
using System.Collections.Generic;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using SharedDto.Universe.Technology;

namespace BLL.Engine.Planet.Production.Builder
{
    public static class FactoryGenerator
    {
        public static OreUpdate RetrieveBuilderOreUpdate(PlanetDto planetDto, RaceDto raceDto,List<TechnologyDto> technologiesDto,DateTime nowTime)
        {
            return new OreUpdate(planetDto, raceDto, technologiesDto, nowTime);
        }
    }
}