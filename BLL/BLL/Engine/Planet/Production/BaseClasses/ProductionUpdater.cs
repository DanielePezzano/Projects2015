using System;
using System.Collections.Generic;
using BLL.Engine.Planet.BaseClasses;
using Models.Universe.Enum;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using SharedDto.Universe.Technology;

namespace BLL.Engine.Planet.Production.BaseClasses
{
    public abstract class ProductionUpdater : BaseUpdater
    {
        
        protected ProductionUpdater(PlanetDto referredPlanetDto, RaceDto raceDto, List<TechnologyDto> technologyDto, DateTime nowTime)
            :base(referredPlanetDto,raceDto,technologyDto,nowTime)
        {
            
        }

        protected override void AdjustByActivePopulation()
        {
            Product += Product * CalculatePercentageOfPopulationUsedInProduction();
        }

        protected override double AdjustByStatus(double quantityToAdjust,bool increaseOnOptimum=true)
        {
            var result = quantityToAdjust;
            switch (ReferredPlanetDto.Status)
            {
                case SatelliteStatus.Uncolonizable:
                case SatelliteStatus.Uncolonized:
                case SatelliteStatus.Abandoned:
                    result= 0;
                    break;
                case SatelliteStatus.Colonized:
                    break;
                case SatelliteStatus.Blocked:
                    if (increaseOnOptimum) result -= result * 0.8;
                    else result += result * 0.8;
                    break;
                case SatelliteStatus.Starvation:
                    if (increaseOnOptimum) result -= result * 0.5;
                    else result += result * 0.5;
                    break;
                case SatelliteStatus.Revolt:
                    if (increaseOnOptimum) result -= result * 0.4;
                    else result += result * 0.4;
                    break;
                case SatelliteStatus.Optimum:
                    if (increaseOnOptimum) result += result * 0.15;
                    else result -= result * 0.15;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return result;
        }

        protected abstract void CalculateRateOfProduction();
        protected abstract double CalculatePercentageOfPopulationUsedInProduction();
        protected abstract void AdjustByBuildings();
        protected abstract void AdjustByTechnology();
        protected abstract void AdjustBySocial();
    }
}