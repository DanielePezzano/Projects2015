using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Engine.Interfaces;
using BLL.Engine.Planet.Social.BaseClasses;
using BLL.Engine.Planet.Social.Interfaces;
using BLL.Engine.Planet.Structs;
using Models.Buildings.Enums;
using Models.Races.Enums;
using Models.Tech.Enum;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using SharedDto.Universe.Technology;

namespace BLL.Engine.Planet.Social
{
    public class PopulationUpdater : SocialUpdater, ISocialUpdater, IUpdater
    {

        public bool UpdateToDo { get; set; }
        public int CalculatedPopulation { get; set; }
        public StatusCheckResult ConsistencyCheckPop { get; set; }

        public PopulationUpdater(PlanetDto referredPlanetDto, RaceDto raceDto, List<TechnologyDto> technologyDtos, DateTime nowTime) :
            base(referredPlanetDto, raceDto, technologyDtos, nowTime)
        {

        }

        #region private methods

        private void CalculateVariationInTime()
        {
            for (var i = 0; i < Diff.Hours; i++)
            {
                DetermineBaseIncrement();
                AdjustByBuildings();
                AdjustByTechnology();
                AdjustBySocial();

                ConsistencyCheckPop = AdjustByStatus(Product);
                CalculatedPopulation += (int)Math.Round(CalculatedPopulation * ConsistencyCheckPop.Value / 100);
            }
        }

        private void DetermineBaseIncrement()
        {
            var oneThird = (double)ReferredPlanetDto.MaxPopulation/3;
            if (CalculatedPopulation <= oneThird) Product = 3;
            if (CalculatedPopulation > oneThird && CalculatedPopulation <= 2 * oneThird) Product = 1.5;
            if (CalculatedPopulation > 2 * oneThird && CalculatedPopulation <= ReferredPlanetDto.MaxPopulation) Product = 0.5;
            if (CalculatedPopulation > ReferredPlanetDto.MaxPopulation) Product = -5;
        }
        
        protected override void AdjustByBuildings()
        {
            foreach (var bonus in ReferredPlanetDto.Buildings.Where(c=>c.BuildingType==BuildingType.Civil).SelectMany(building => building.Details.Where(c=>c.Bonus==BonusType.Populationbonus)))
            {
                Product += Product * bonus.Value / 100;
            }
        }

        protected override void AdjustBySocial()
        {
            foreach (var bonus in ReferredRaceDto.RaceBonuses.Where(c=>c.Bonus==RaceTraitsBonuses.Social))
            {
                Product += Product * bonus.Value / 100;
            }
        }

        protected override void AdjustByTechnology()
        {
            foreach (
                var bonus in
                    TechnologyDtos.Where(
                        c =>
                            c.Field == "Buildings" && c.Field != "ShipComponent" && c.Field != "ShipFrame" &&
                            c.Field != "Weapons")
                        .SelectMany(
                            technologyDto => technologyDto.TechnologyBonuses.Where(c => c.Bonus == BonusType.Populationbonus)))
            {
                Product += Product * bonus.Value / 100;
            }
        }
        #endregion
        


        public void Update()
        {
            if (ConsistencyCheckPop.ConsistencyCheck == false) return;
            if (CalculatedPopulation < 1 && ReferredPlanetDto.IsHomePlanet) CalculatedPopulation = 1;
            if (CalculatedPopulation < 1 && !ReferredPlanetDto.IsHomePlanet) CalculatedPopulation = 0;
            if (CalculatedPopulation != ReferredPlanetDto.Population)
            {
                ReferredPlanetDto.Population = CalculatedPopulation;
                ReferredPlanetDto.LastUpdatePopDateTime = TimeNow;

            }
            UpdateToDo = true;
            
        }

        public void CheckTimeDifference()
        {
            if (Diff.Hours <= 0) return;
            CalculatedPopulation = ReferredPlanetDto.Population;
            CalculateVariationInTime();
        }
    }
}