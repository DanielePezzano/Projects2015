using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Engine.Interfaces;
using BLL.Engine.Planet.Production.BaseClasses;
using BLL.Engine.Planet.Production.Interfaces;
using Models.Races.Enums;
using Models.Tech.Enum;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using SharedDto.Universe.Technology;

namespace BLL.Engine.Planet.Production
{
    public class OreUpdater : ProductionUpdater, IProcutionUpdater, IUpdater
    {
        public bool UpdateToDo { get; set; }

        public OreUpdater(PlanetDto referredPlanetDto, RaceDto raceDto, List<TechnologyDto> technologyDto, DateTime nowTime):
            base(referredPlanetDto, raceDto, technologyDto, nowTime)
        {
            
        }

        #region Private Methods

        protected override void CalculateRateOfProduction()
        {
            AdjustByActivePopulation();
            AdjustByBuildings();
            AdjustByTechnology();
            AdjustBySocial();

            Product = AdjustByStatus(Product);
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
            foreach (var bonus in TechnologyDtos.Where(c => c.SubField == "Buildings" && c.SubField != "ShipComponent" && c.SubField != "ShipFrame" && c.SubField != "Weapons").SelectMany(technologyDto => technologyDto.TechnologyBonuses.Where(c => c.Bonus == BonusType.OreBonus)))
            {
                Product += Product * bonus.Value / 100;
            }
        }


        protected override void AdjustBySocial()
        {
            if (ReferredRaceDto.RaceBonuses.Count <= 0) return;
            foreach (
                var value in
                    ReferredRaceDto.RaceBonuses.Where(c => c.Bonus == RaceTraitsBonuses.OreProduction)
                        .Select(c => c.Value))
            {
                Product += Product*value/100;
            }
        }

        #endregion

        public void CheckTimeDifference()
        {
            if (Diff.Hours <= 0) return;
            Product = ReferredPlanetDto.OreProduction * Diff.Hours;
            CalculateRateOfProduction();
        }

        public void Update()
        {
            if (Product <= 0) return;
            UpdateToDo = true;
            ReferredPlanetDto.StoredOre += (int)Math.Round(Product);
            ReferredPlanetDto.LastUpdateOreProduction = TimeNow;

        }
    }
}