﻿using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Engine.Interfaces;
using BLL.Engine.Planet.Production.BaseClasses;
using BLL.Engine.Planet.Production.Interfaces;
using BLL.Engine.Planet.Structs;
using Models.Buildings.Enums;
using Models.Races.Enums;
using Models.Tech.Enum;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using SharedDto.Universe.Technology;

namespace BLL.Engine.Planet.Production
{
    public class FoodUpdater : ProductionUpdater, IProcutionUpdater, IUpdater
    {
        private double _foodConsumption;
        public bool UpdateToDo { get; set; }
        public StatusCheckResult ConsistencyCheckFood { get; set; }

        public FoodUpdater(PlanetDto referredPlanetDto, RaceDto raceDto, List<TechnologyDto> technologyDto, DateTime nowTime):
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
            AdjustByFoodConsumption();
            ConsistencyCheckFood = AdjustByStatus(Product);

            Product = ConsistencyCheckFood.Value;
        }

        protected void CalculateFoodConsumption()
        {
            _foodConsumption = ReferredPlanetDto.Population * Diff.Hours*0.8;

            foreach (var foodConsumptionBonus in ReferredRaceDto.RaceBonuses.Where(c => c.Bonus == RaceTraitsBonuses.FoodConsumption).Select(c => c.Value))
            {
                _foodConsumption += _foodConsumption*foodConsumptionBonus/100;
            }
        }

        protected override double CalculatePercentageOfPopulationUsedInProduction()
        {
            return ReferredPlanetDto.ActivePopOnFoodProduction;
        }

        private void AdjustByFoodConsumption()
        {
            CalculateFoodConsumption();
            Product -= _foodConsumption;
        }
        

        protected override void AdjustByBuildings()
        {
            foreach (var detail in ReferredPlanetDto.Buildings.Where(c=>c.BuildingType==BuildingType.Civil).SelectMany(building => building.Details.Where(c=>c.Bonus==BonusType.Foodbonus)))
            {
                Product += Product * detail.Value / 100;
            }
        }

        protected override void AdjustBySocial()
        {
            if (ReferredRaceDto.RaceBonuses.Count <= 0) return;
            foreach (
                var value in
                    ReferredRaceDto.RaceBonuses.Where(c => c.Bonus == RaceTraitsBonuses.FoodProduction)
                        .Select(c => c.Value))
            {
                Product += Product*value/100;
            }
        }

        protected override void AdjustByTechnology()
        {
            foreach (var bonus in TechnologyDtos.Where(c => c.Field == "Buildings" && c.Field != "ShipComponent" && c.Field != "ShipFrame" && c.Field != "Weapons").SelectMany(technologyDto => technologyDto.TechnologyBonuses.Where(c => c.Bonus == BonusType.Foodbonus)))
            {
                Product += Product * bonus.Value / 100;
            }
        }

        #endregion

        public void CheckTimeDifference()
        {
            if (Diff.Hours <= 0) return;
            Product = ReferredPlanetDto.FoodProduction * Diff.Hours;
            CalculateRateOfProduction();
        }

        

        public void Update()
        {
            if (!ConsistencyCheckFood.ConsistencyCheck) return;
            UpdateToDo = true;
            ReferredPlanetDto.StoredFood += (int)Math.Round(Product);
            ReferredPlanetDto.LastUpdateFoodProduction = TimeNow;

            if (ReferredPlanetDto.StoredFood <= 0) ReferredPlanetDto.StoredFood = 0;
        }
    }
}