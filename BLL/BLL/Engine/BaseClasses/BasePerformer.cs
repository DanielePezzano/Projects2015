using System;
using System.Collections.Generic;
using BLL.Engine.Interfaces;
using BLL.Engine.Planet.Enums;
using BLL.Engine.Planet.Production.Interfaces;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using SharedDto.Universe.Technology;

namespace BLL.Engine.BaseClasses
{
    public abstract class BasePerformer
    {
        protected PlanetDto ReferredPlanetDto;
        protected readonly RaceDto ReferredRaceDto;
        protected readonly List<TechnologyDto> TechnologyDtos;
        protected readonly PlanetUpdateSelector Selector;
        protected readonly DateTime TimeNow;
        protected IUpdater Updater;

        protected BasePerformer(PlanetDto planetDto, PlanetUpdateSelector chosenUpdate, RaceDto raceDto,
            List<TechnologyDto> technologyDtos,
            DateTime timenow)
        {
            TechnologyDtos = technologyDtos;
            ReferredPlanetDto = planetDto;
            Selector = chosenUpdate;
            ReferredRaceDto = raceDto;
            TimeNow = timenow;
        }

        protected abstract void RetrieveUpdater();
    }
}