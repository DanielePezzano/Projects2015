using System;
using System.Collections.Generic;
using BLL.Utilities.Structs;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using System.Linq;
using Models.Races.Enums;
using Models.Tech.Enum;
using SharedDto.Universe.Technology;

namespace BLL.Engine.Planet.Production.Builder
{
    public class OreUpdate : Updater, IUpdater
    {
        private TimeDiff _diff;
        private readonly RaceDto _raceDto;
        private readonly List<TechnologyDto> _technologyDto;
        private readonly DateTime _nowTime;

        public OreUpdate(PlanetDto referredPlanetDto, RaceDto raceDto, List<TechnologyDto> technologyDto, DateTime nowTime)
        {
            if (referredPlanetDto==null) throw  new ArgumentNullException(nameof(referredPlanetDto));
            if (raceDto == null) throw new ArgumentNullException(nameof(raceDto));
            if (technologyDto == null) throw new ArgumentNullException(nameof(technologyDto));

            ReferredPlanetDto = referredPlanetDto;
            _technologyDto = technologyDto;
            _nowTime = nowTime;
            _raceDto = raceDto;
            _diff = new TimeDiff(referredPlanetDto.LastUpdateOreProduction, nowTime);
        }

        #region Private Methods

        protected override void CalculateRateOfProduction()
        {
            AdjustByActivePopulation();
            AdjustByBuildings();
            AdjustByTechnology();
            AdjustByStatus();
            AdjustBySocial();
        }

        protected override double CalculatePercentageOfPopulationUsedInProduction()
        {
            return ReferredPlanetDto.ActivePopOnOreProduction/
                   (ReferredPlanetDto.ActivePopOnOreProduction + ReferredPlanetDto.ActivePopOnFoodProduction +
                    ReferredPlanetDto.ActivePopOnResProduction);
        }
        
        protected override void AdjustByBuildings()
        {
            foreach (var detail in ReferredPlanetDto.Buildings.SelectMany(building => building.Details.Where(c => c.Bonus == BonusType.OreBonus)))
            {
                Product += Product * detail.Value / 100;
            }
        }

        protected override void AdjustByTechnology()
        {
            foreach (var bonus in _technologyDto.SelectMany(technology => technology.TechnologyBonuses.Where(c => c.Bonus == BonusType.OreBonus)))
            {
                Product += Product * bonus.Value / 100;
            }
        }
        

        protected override void AdjustBySocial()
        {
            foreach (var value in _raceDto.RaceBonuses.Where(c=>c.Bonus==RaceTraitsBonuses.OreProduction).Select(c=>c.Value))
            {
                Product += Product*value/100;
            }
        }

        #endregion

        public void CheckTimeDifference()
        {
            if (_diff.Hours > 0)
            {
                Product = ReferredPlanetDto.OreProduction * _diff.Hours;
                CalculateRateOfProduction();
            }
        }

        public void Update()
        {
            if (Product <= 0) return;

            ReferredPlanetDto.StoredOre += (int) Product;
            ReferredPlanetDto.LastUpdateOreProduction = _nowTime;

        }
    }
}