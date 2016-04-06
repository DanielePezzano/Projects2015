using System;
using BLL.Engine.Planet.Enum;
using BLL.Engine.Planet.Production.Builder;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Interfaces.UnitOfWork;

namespace BLL.Engine.Planet
{
    public class Builder : IBuilder
    {
        private PlanetDto _planetDto;
        private readonly RaceDto _raceDto;
        private MainUow _uow;
        private readonly bool _isTest;
        private readonly PlanetUpdateSelector _selector;
        private IUpdater _updater;
        private readonly DateTime _timeNow;

        public Builder(IUnitOfWork uow, PlanetDto planetDto, PlanetUpdateSelector chosenUpdate, RaceDto raceDto, bool isTest,DateTime timenow)
        {
            _planetDto = planetDto;
            _uow = (MainUow) uow;
            _isTest = isTest;
            _selector = chosenUpdate;
            _raceDto = raceDto;
            _timeNow = timenow;
        }

        public void Build()
        {
            RetrieveUpdater();
            _updater?.CheckTimeDifference();
            _updater?.Update();
            if (!_isTest) WriteUpdate();
        }

        public PlanetDto RetrievePlanetDto()
        {
            return _planetDto;
        }

        
        private void WriteUpdate()
        {
           var toUpdate = _uow?.PlanetRepository.GetByKey(_planetDto.Id, "");
            if (toUpdate != null)
            {
                toUpdate.SatelliteProduction.StoredFood = _planetDto.StoredFood;
                toUpdate.SatelliteProduction.StoredOre = _planetDto.StoredOre;
            }
            _uow?.Save();
        }

        private void RetrieveUpdater()
        {
            switch (_selector)
            {
                case PlanetUpdateSelector.OreProduction:
                    _updater = FactoryGenerator.RetrieveBuilderOreUpdate(_planetDto, _raceDto, _timeNow);
                    break;
                case PlanetUpdateSelector.FoodProduction:
                    break;
                case PlanetUpdateSelector.ResearchProduction:
                    break;
                case PlanetUpdateSelector.SocialStatus:
                    break;
                case PlanetUpdateSelector.Buildings:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}