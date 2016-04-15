using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Engine.Planet.Production.BaseClasses;
using BLL.Engine.Planet.Production.Interfaces;
using BLL.Utilities.Structs;
using Models.Races.Enums;
using Models.Tech.Enum;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using SharedDto.Universe.Technology;

namespace BLL.Engine.Planet.Production
{
    public class CostsUpdater : Updater, IUpdater
    {
        private Costs _calculatedCosts;
        public bool UpdateToDo { get; set; }

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
            CalculateBaseBuildingCosts();
            CalculateBaseFleetCosts();
            AdjustByBuildings();
            AdjustBySocial();
            AdjustByTechnology();
            _calculatedCosts.OreCost = AdjustByStatus(_calculatedCosts.OreCost, false);
            _calculatedCosts.MoneyCost = AdjustByStatus(_calculatedCosts.MoneyCost, false);
        }

        protected override void AdjustByBuildings()
        {
            foreach (var bonus in ReferredPlanetDto.Buildings.SelectMany(building => building.Details.Where(c => c.Bonus == BonusType.MaintBonus)))
            {
                _calculatedCosts.MoneyCost += _calculatedCosts.MoneyCost * bonus.Value / 100;
                _calculatedCosts.OreCost += _calculatedCosts.OreCost * bonus.Value / 100;
            }
        }

        protected override void AdjustBySocial()
        {
            foreach (var value in _raceDto.RaceBonuses.Where(c => c.Bonus == RaceTraitsBonuses.MaintenanceOre).Select(c => c.Value))
            {
                _calculatedCosts.OreCost += _calculatedCosts.OreCost * value / 100;
            }
            foreach (var value in _raceDto.RaceBonuses.Where(c => c.Bonus == RaceTraitsBonuses.MaintenanceMoney).Select(c => c.Value))
            {
                _calculatedCosts.MoneyCost += _calculatedCosts.MoneyCost * value / 100;
            }
        }

        protected override double CalculatePercentageOfPopulationUsedInProduction()
        {
            return 1;
        }

        protected override void AdjustByTechnology()
        {
            foreach (var bonus in _technologyDto.Where(c => c.SubField == "Buildings" && c.SubField != "ShipComponent" && c.SubField != "ShipFrame" && c.SubField != "Weapons").SelectMany(technologyDto => technologyDto.TechnologyBonuses.Where(c => c.Bonus == BonusType.MaintBonus)))
            {
                _calculatedCosts.MoneyCost += _calculatedCosts.MoneyCost * bonus.Value / 100;
                _calculatedCosts.OreCost += _calculatedCosts.OreCost * bonus.Value / 100;
            }
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
            UpdateToDo = true;
            ReferredPlanetDto.LastMaintenanceDateTime = _nowTime;
            ReferredPlanetDto.StoredOre -= (_calculatedCosts.OreCost > 0) ? (int) _calculatedCosts.OreCost : 0;
            ReferredPlanetDto.PlanetIncomeBalance -= (_calculatedCosts.MoneyCost > 0) ? (int)_calculatedCosts.MoneyCost : 0;
        }
    }
}