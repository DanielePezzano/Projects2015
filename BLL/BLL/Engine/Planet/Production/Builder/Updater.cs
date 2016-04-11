using System;
using Models.Universe.Enum;
using SharedDto.Universe.Planets;

namespace BLL.Engine.Planet.Production.Builder
{
    public abstract class Updater
    {
        protected double Product;
        protected PlanetDto ReferredPlanetDto;

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