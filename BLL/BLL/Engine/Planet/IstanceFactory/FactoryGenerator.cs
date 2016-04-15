using System;
using System.Collections.Generic;
using BLL.Engine.Planet.Enums;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using SharedDto.Universe.Technology;

namespace BLL.Engine.Planet.IstanceFactory
{
    public static class FactoryGenerator
    {
        public static ProductionPerformer RetrieveProductinPerformer(PlanetDto planetDto, RaceDto raceDto, PlanetUpdateSelector chosenUpdate,
            List<TechnologyDto> technologyDtos,DateTime nowTime)
        {
            return new ProductionPerformer(planetDto, chosenUpdate, raceDto, technologyDtos, nowTime);
        }

    }
}