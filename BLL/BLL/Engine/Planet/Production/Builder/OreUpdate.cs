using System;
using System.Collections.Generic;
using BLL.Utilities.Structs;
using Models.Universe.Enum;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using System.Linq;
using Models.Races.Enums;
using Models.Tech.Enum;
using SharedDto.Universe.Technology;

namespace BLL.Engine.Planet.Production.Builder
{
    public class OreUpdate : IUpdater
    {
        private TimeDiff _diff;
        private PlanetDto _planetDto;
        private readonly RaceDto _raceDto;
        private readonly List<TechnologyDto> _technologyDto;
        private double _product;
        private readonly DateTime _nowTime;

        public OreUpdate(PlanetDto planetDto, RaceDto raceDto, List<TechnologyDto> technologyDto, DateTime nowTime)
        {
            if (planetDto==null) throw  new ArgumentNullException(nameof(planetDto));
            if (raceDto == null) throw new ArgumentNullException(nameof(raceDto));
            if (technologyDto == null) throw new ArgumentNullException(nameof(technologyDto));

            _planetDto = planetDto;
            _technologyDto = technologyDto;
            _nowTime = nowTime;
            _raceDto = raceDto;
            _diff = new TimeDiff(planetDto.LastUpdateOreProduction, nowTime);
        }

        #region Private Methods

        private void CalculateRateOfProduction()
        {
            _product = _planetDto.OreProduction * _diff.Hours;

            AdjustByActivePopulation();
            AdjustByBuildings();
            AdjustByTechnology();
            AdjustByStatus();
            AdjustBySocial();
        }

        private double CalculatePercentageOfPopulationUsedInProduction()
        {
            return _planetDto.ActivePopOnOreProduction/
                   (_planetDto.ActivePopOnOreProduction + _planetDto.ActivePopOnFoodProduction +
                    _planetDto.ActivePopOnResProduction);
        }

        private void AdjustByActivePopulation()
        {
            _product += _product*CalculatePercentageOfPopulationUsedInProduction();
        }

        private void AdjustByBuildings()
        {
            foreach (var detail in _planetDto.Buildings.SelectMany(building => building.Details.Where(c => c.Bonus == BonusType.OreBonus)))
            {
                _product += _product * detail.Value / 100;
            }
        }

        private void AdjustByTechnology()
        {
            foreach (var bonus in _technologyDto.SelectMany(technology => technology.TechnologyBonuses.Where(c => c.Bonus == BonusType.OreBonus)))
            {
                _product += _product * bonus.Value / 100;
            }
        }

        private void AdjustByStatus()
        {
            switch (_planetDto.Status)
            {
                case SatelliteStatus.Uncolonizable:
                case SatelliteStatus.Uncolonized:
                case SatelliteStatus.Abandoned:
                    _product = 0;
                    break;
                case SatelliteStatus.Colonized:
                    break;
                case SatelliteStatus.Blocked:
                    _product -= _product * 0.7;
                    break;
                case SatelliteStatus.Starvation:
                    _product -= _product * 0.5;
                    break;
                case SatelliteStatus.Revolt:
                    _product -= _product*0.4;
                    break;
                case SatelliteStatus.Optimum:
                    _product += _product*0.15;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void AdjustBySocial()
        {
            foreach (var value in _raceDto.RaceBonuses.Where(c=>c.Bonus==RaceTraitsBonuses.OreProduction).Select(c=>c.Value))
            {
                _product += _product*value/100;
            }
        }

        #endregion

        public void CheckTimeDifference()
        {
            if (_diff.Hours > 0)
            {
                CalculateRateOfProduction();
            }
        }

        public void Update()
        {
            if (_product <= 0) return;

            _planetDto.StoredOre += (int) _product;
            _planetDto.LastUpdateOreProduction = _nowTime;

        }
    }
}