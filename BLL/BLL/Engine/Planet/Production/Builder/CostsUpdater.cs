using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Utilities.Structs;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using SharedDto.Universe.Technology;

namespace BLL.Engine.Planet.Production.Builder
{
    public class CostsUpdater : Updater, IUpdater
    {
        private Costs _calculatedCosts;

        public CostsUpdater(PlanetDto referredPlanetDto, RaceDto raceDto, List<TechnologyDto> technologyDto, DateTime nowTime):
            base(referredPlanetDto,raceDto,technologyDto,nowTime)
        {

            _calculatedCosts = new Costs(0,0,0,0);
        }

       #region Private Methods

        private void CalculateBaseBuildingCosts()
        {
            foreach (var buildingDto in ReferredPlanetDto.Buildings)
            {
                _calculatedCosts.OreCost += buildingDto.OreMaintenanceCost;
                _calculatedCosts.MoneyCost += buildingDto.MoneyMaintenanceCost;
            }
        }

        private void CalculateBaseFleetCosts()
        {
            foreach (var orbitingFleetDto in ReferredPlanetDto.OrbitingFleetDtos.Where(c=>c.UserId==ReferredPlanetDto.UserId))
            {
                _calculatedCosts.OreCost += orbitingFleetDto.OreMaintenanceCost;
                _calculatedCosts.MoneyCost += orbitingFleetDto.MoneyMaintenanceCost;
            }
        }

        protected override void CalculateRateOfProduction()
        {
            throw new NotImplementedException();
        }

        protected override void AdjustByBuildings()
        {
            throw new NotImplementedException();
        }

        protected override void AdjustBySocial()
        {
            throw new NotImplementedException();
        }

        protected override double CalculatePercentageOfPopulationUsedInProduction()
        {
            throw new NotImplementedException();
        }

        protected override void AdjustByTechnology()
        {
            throw new NotImplementedException();
        }

        #endregion

        public void CheckTimeDifference()
        {
            if (_diff.Hours <= 0) return;
            
            CalculateRateOfProduction();
        }

        public void Update()
        {
            if (Product <= 0) return;

            ReferredPlanetDto.LastMaintenanceDateTime = _nowTime;
        }
    }
}