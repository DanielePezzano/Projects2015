using System;
using System.Collections.Generic;
using BLL.Engine.Planet.Exceptions;
using BLL.Utilities.Structs;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using SharedDto.Universe.Technology;

namespace BLL.Engine.Planet.BaseClasses
{
    public abstract class BaseUpdater
    {
        public double Product;
        public PlanetDto ReferredPlanetDto;
        public TimeDiff Diff;
        public RaceDto ReferredRaceDto;
        public List<TechnologyDto> TechnologyDtos;
        public DateTime TimeNow;

        protected BaseUpdater(PlanetDto referredPlanetDto, RaceDto raceDto, List<TechnologyDto> technologyDto, DateTime nowTime)
        {
            PreliminaryChecks(referredPlanetDto, raceDto, technologyDto);

            ReferredPlanetDto = referredPlanetDto;
            TechnologyDtos = technologyDto;
            TimeNow = nowTime;
            ReferredRaceDto = raceDto;
            Diff = new TimeDiff(referredPlanetDto.LastUpdateOreProduction, nowTime);
        }

        private void PreliminaryChecks(PlanetDto referredPlanetDto, RaceDto raceDto, List<TechnologyDto> technologyDto)
        {
            if (referredPlanetDto == null) throw new ArgumentNullException(nameof(referredPlanetDto));
            if (raceDto == null) throw new ArgumentNullException(nameof(raceDto));
            if (technologyDto == null) throw new ArgumentNullException(nameof(technologyDto));
            if (raceDto.RaceBonuses == null)
                throw new Exception(PlanetExceptions.RaceDtoNullBonusesException.ToString());
            if (technologyDto == null)
                throw new Exception(PlanetExceptions.TechnologyListNull.ToString());
        }

        protected abstract double AdjustByStatus(double quantityToAdjust, bool increaseOnOptimum = true);
        protected abstract void AdjustByActivePopulation();
    }
}
