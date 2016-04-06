using System;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;

namespace BLL.Engine.Planet.Production.Builder
{
    public static class FactoryGenerator
    {
        public static OreUpdate RetrieveBuilderOreUpdate(PlanetDto planetDto, RaceDto raceDto,DateTime nowTime)
        {
            return new OreUpdate(planetDto,raceDto,nowTime);
        }
    }
}