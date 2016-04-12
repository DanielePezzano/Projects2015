using System;
using System.Collections.Generic;
using BLL.Utilities.Structs;
using Models.Universe.Enum;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using SharedDto.Universe.Technology;

namespace BLL.Engine.Planet.Production.Builder
{
    public abstract class Updater
    {
        protected double Product;
        protected PlanetDto ReferredPlanetDto;
        protected TimeDiff _diff;
        protected readonly RaceDto _raceDto;
        protected readonly List<TechnologyDto> _technologyDto;
        protected readonly DateTime _nowTime;

        protected Updater(PlanetDto referredPlanetDto, RaceDto raceDto, List<TechnologyDto> technologyDto, DateTime nowTime)
        {
            if (referredPlanetDto == null) throw new ArgumentNullException(nameof(referredPlanetDto));
            if (raceDto == null) throw new ArgumentNullException(nameof(raceDto));
            if (technologyDto == null) throw new ArgumentNullException(nameof(technologyDto));

            ReferredPlanetDto = referredPlanetDto;
            _technologyDto = technologyDto;
            _nowTime = nowTime;
            _raceDto = raceDto;
            _diff = new TimeDiff(referredPlanetDto.LastUpdateOreProduction, nowTime);
        }

        protected void AdjustByActivePopulation()
        {
            Product += Product * CalculatePercentageOfPopulationUsedInProduction();
        }

        protected void AdjustByStatus()
        {
            switch (ReferredPlanetDto.Status)
            {
                case SatelliteStatus.Uncolonizable:
                case SatelliteStatus.Uncolonized:
                case SatelliteStatus.Abandoned:
                    Product = 0;
                    break;
                case SatelliteStatus.Colonized:
                    break;
                case SatelliteStatus.Blocked:
                    Product -= Product * 0.8;
                    break;
                case SatelliteStatus.Starvation:
                    Product -= Product * 0.5;
                    break;
                case SatelliteStatus.Revolt:
                    Product -= Product * 0.4;
                    break;
                case SatelliteStatus.Optimum:
                    Product += Product * 0.15;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected abstract void CalculateRateOfProduction();
        protected abstract double CalculatePercentageOfPopulationUsedInProduction();
        protected abstract void AdjustByBuildings();
        protected abstract void AdjustByTechnology();
        
        protected abstract void AdjustBySocial();
    }
}