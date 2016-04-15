using System;
using System.Collections.Generic;
using BLL.Engine.BaseClasses;
using BLL.Engine.Exceptions;
using BLL.Engine.Interfaces;
using BLL.Engine.Planet.Enums;
using BLL.Engine.Planet.Production.IstanceFactory;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using SharedDto.Universe.Technology;

namespace BLL.Engine.Planet
{
    public class ProductionPerformer :BasePerformer, IPerformer
    {
        

        public ProductionPerformer(PlanetDto planetDto, PlanetUpdateSelector chosenUpdate, RaceDto raceDto,
            List<TechnologyDto>  technologyDtos,
            DateTime timenow):base(planetDto,chosenUpdate,raceDto,technologyDtos,timenow)
        {
            
        }

        public bool Perform()
        {
            RetrieveUpdater();
            Updater?.CheckTimeDifference();
            Updater?.Update();
            return Updater?.UpdateToDo ?? false;
        }
        
        protected  override  void RetrieveUpdater()
        {
            switch (Selector)
            {
                case PlanetUpdateSelector.OreProduction:
                    Updater = FactoryGenerator.RetrieveBuilderOreUpdater(ReferredPlanetDto, ReferredRaceDto, TechnologyDtos, TimeNow);
                    break;
                case PlanetUpdateSelector.FoodProduction:
                    Updater = FactoryGenerator.RetrieveBuilderFoodUpdater(ReferredPlanetDto, ReferredRaceDto, TechnologyDtos,
                        TimeNow);
                    break;
                case PlanetUpdateSelector.ResearchProduction:
                    Updater = FactoryGenerator.RetrieveBuilderResearchUpdater(ReferredPlanetDto, ReferredRaceDto, TechnologyDtos,
                        TimeNow);
                    break;
                case PlanetUpdateSelector.CostsUpdate:
                    Updater = FactoryGenerator.RetrieveBuilderCostUpdater(ReferredPlanetDto, ReferredRaceDto, TechnologyDtos,
                        TimeNow);
                    break;
                case PlanetUpdateSelector.SocialStatus:
                    throw new Exception(EngineExceptions.WrongPerformerCall.ToString());
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}