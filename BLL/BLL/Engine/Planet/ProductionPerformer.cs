using System;
using System.Collections.Generic;
using BLL.Engine.Exceptions;
using BLL.Engine.Interfaces;
using BLL.Engine.Planet.Enums;
using BLL.Engine.Planet.Production.Interfaces;
using BLL.Engine.Planet.Production.IstanceFactory;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using SharedDto.Universe.Technology;

namespace BLL.Engine.Planet
{
    public class ProductionPerformer : IPerformer
    {
        private PlanetDto _planetDto;
        private readonly RaceDto _raceDto;
        private readonly List<TechnologyDto> _technologyDtos;
        private readonly PlanetUpdateSelector _selector;
        private IUpdater _updater;
        private readonly DateTime _timeNow;

        public ProductionPerformer(PlanetDto planetDto, PlanetUpdateSelector chosenUpdate, RaceDto raceDto,
            List<TechnologyDto>  technologyDtos,
            DateTime timenow)
        {
            _technologyDtos = technologyDtos;
            _planetDto = planetDto;
            _selector = chosenUpdate;
            _raceDto = raceDto;
            _timeNow = timenow;
        }

        public void Perform()
        {
            RetrieveUpdater();
            _updater?.CheckTimeDifference();
            if(_updater!=null && _updater.UpdateToDo) _updater?.Update();
        }
        
        private void RetrieveUpdater()
        {
            switch (_selector)
            {
                case PlanetUpdateSelector.OreProduction:
                    _updater = FactoryGenerator.RetrieveBuilderOreUpdater(_planetDto, _raceDto, _technologyDtos, _timeNow);
                    break;
                case PlanetUpdateSelector.FoodProduction:
                    _updater = FactoryGenerator.RetrieveBuilderFoodUpdater(_planetDto, _raceDto, _technologyDtos,
                        _timeNow);
                    break;
                case PlanetUpdateSelector.ResearchProduction:
                    _updater = FactoryGenerator.RetrieveBuilderResearchUpdater(_planetDto, _raceDto, _technologyDtos,
                        _timeNow);
                    break;
                case PlanetUpdateSelector.SocialStatus:
                case PlanetUpdateSelector.CostsUpdate:
                    throw new Exception(EngineExceptions.WrongPerformerCall.ToString());
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}