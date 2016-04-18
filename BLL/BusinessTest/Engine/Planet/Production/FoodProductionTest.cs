using System;
using System.Collections.Generic;
using BLL.Engine.Planet;
using BLL.Engine.Planet.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Buildings.Enums;
using Models.Races.Enums;
using Models.Tech.Enum;
using Models.Universe.Enum;
using SharedDto.Universe.Building;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using SharedDto.Universe.Technology;

namespace BusinessTest.Engine.Planet.Production
{
    [TestClass]
    public class FoodProductionTest
    {
        RaceDto _raceDto = new RaceDto()
        {
            RaceBonuses = new List<RaceBonusDto>(){new RaceBonusDto()
             {
                 Bonus = RaceTraitsBonuses.FoodProduction,
                 Value = 5
             }},
            RaceName = "TestOreProductiveRace"
        };

        TechnologyDto _technologySocialDto = new TechnologyDto()
        {
            Name = "techTest",
            Field = "Social",
            TechnologyBonuses = new List<TechnologyBonusDto>()
            {
                new TechnologyBonusDto()
                {
                    Bonus = BonusType.Foodbonus,
                    Value = 5
                }
            }
        };

        BuildingDto _building = new BuildingDto()
        {
            BuildingType = BuildingType.Civil,
            Details = new List<BuildingSpecsDto>()
            {
                new BuildingSpecsDto()
                {
                    Bonus = BonusType.Foodbonus,
                    Value = 5
                }
            }
        };

        TechnologyDto _technologyBuildingDto = new TechnologyDto()
        {
            Name = "techTest",
            Field = "Buildings",
            TechnologyBonuses = new List<TechnologyBonusDto>()
            {
                new TechnologyBonusDto()
                {
                    Bonus = BonusType.Foodbonus,
                    Value = 5
                }
            }
        };
        /*
         * Nota sulla popolazione attiva: di norma è il 30% della popolazione totale,
         * ma potremmo pensare di legare questa percentuale all'eventuale presenza
         * di bonus demografici.
         * quindi se pensiamo a pop=100 => p.A = 30 da dividere nei vari settori.
         */
        PlanetDto planet = new PlanetDto()
            {
                ActivePopOnFoodProduction = 20,
                ActivePopOnOreProduction = 8,
                ActivePopOnResProduction = 2,
                FoodProduction = 4,
                StoredFood = 0,
                Population = 100
                
            };

        [TestMethod]
        public void ShouldAlwayBeFalse()
        {
            planet.Buildings = new List<BuildingDto>();
            planet.LastUpdateOreProduction = DateTime.Now;
            planet.Status = SatelliteStatus.Colonized;


            var productionPerformer =
                BLL.Engine.Planet.IstanceFactory.FactoryGenerator.RetrieveProductinPerformer(
                    planet,
                    new RaceDto()
                    {
                        RaceBonuses = new List<RaceBonusDto>(),
                        RaceName = "TestOreProductiveRace"
                    },
                    PlanetUpdateSelector.FoodProduction,
                    new List<TechnologyDto>(),
                    DateTime.Now
                    );

            Assert.IsFalse(productionPerformer.Perform());
            Assert.IsTrue(planet.StoredFood == 0);

        }

        [TestMethod]
        public void ShouldAlwayBeFalseBecauseUncolonized()
        {
            planet.Buildings = new List<BuildingDto>();
            planet.LastUpdateOreProduction = DateTime.Now.AddHours(-1);
            planet.Status = SatelliteStatus.Uncolonized;


            var productionPerformer =
                BLL.Engine.Planet.IstanceFactory.FactoryGenerator.RetrieveProductinPerformer(
                    planet,
                    new RaceDto()
                    {
                        RaceBonuses = new List<RaceBonusDto>(),
                        RaceName = "foodProductive"
                    },
                    PlanetUpdateSelector.FoodProduction,
                    new List<TechnologyDto>(),
                    DateTime.Now
                    );

            Assert.IsFalse(productionPerformer.Perform());
            Assert.IsTrue(planet.StoredOre == 0);

        }


        [TestMethod]
        public void ShouldAlwayBeFalseBecauseAbandond()
        {

            planet.Buildings = new List<BuildingDto>();
            planet.LastUpdateOreProduction = DateTime.Now.AddHours(-1);
            planet.Status = SatelliteStatus.Abandoned;

            var productionPerformer =
                BLL.Engine.Planet.IstanceFactory.FactoryGenerator.RetrieveProductinPerformer(
                    planet,
                    new RaceDto()
                    {
                        RaceBonuses = new List<RaceBonusDto>(),
                        RaceName = "foodProductive"
                    },
                    PlanetUpdateSelector.FoodProduction,
                    new List<TechnologyDto>(),
                    DateTime.Now
                    );

            Assert.IsFalse(productionPerformer.Perform());
            Assert.IsTrue(planet.StoredOre == 0);

        }


        [TestMethod]
        public void ShouldAlwayBeFalseBecauseUncolonizble()
        {
            planet.Buildings = new List<BuildingDto>();
            planet.LastUpdateOreProduction = DateTime.Now.AddHours(-1);
            planet.Status = SatelliteStatus.Uncolonizable;


            var productionPerformer =
                BLL.Engine.Planet.IstanceFactory.FactoryGenerator.RetrieveProductinPerformer(
                    planet,
                    new RaceDto()
                    {
                        RaceBonuses = new List<RaceBonusDto>(),
                        RaceName = "foodProductive"
                    },
                    PlanetUpdateSelector.FoodProduction,
                    new List<TechnologyDto>(),
                    DateTime.Now
                    );

            Assert.IsFalse(productionPerformer.Perform());
            Assert.IsTrue(planet.StoredOre == 0);

        }

        [TestMethod]
        public void ShouldBe116WithoutTechAndRace()
        {
            planet.Buildings = new List<BuildingDto>();
            planet.LastUpdateOreProduction = DateTime.Now.AddHours(-2);
            planet.Status = SatelliteStatus.Colonized;


            var productionPerformer = new ProductionPerformer(
                planet,
                PlanetUpdateSelector.FoodProduction,
                new RaceDto()
                {
                    RaceBonuses = new List<RaceBonusDto>(),
                    RaceName = "TestFoodProductiveRace"
                },
                new List<TechnologyDto>(),
                DateTime.Now.AddHours(1)
                );

            Assert.IsTrue(productionPerformer.Perform());
            Assert.IsTrue(planet.StoredFood == 0);
        }

        [TestMethod]
        public void ShouldBeHighWithoutTech()
        {
            planet.Buildings = new List<BuildingDto>();
            planet.LastUpdateOreProduction = DateTime.Now.AddHours(-2);
            planet.Status = SatelliteStatus.Colonized;


            var productionPerformer = new ProductionPerformer(
                planet,
                PlanetUpdateSelector.FoodProduction,
                _raceDto,
                new List<TechnologyDto>(),
                DateTime.Now.AddHours(1)
                );

            Assert.IsTrue(productionPerformer.Perform());
            Assert.IsTrue(planet.StoredFood ==12);
        }

        [TestMethod]
        public void ShouldBeHighWithTechToo()
        {
            planet.Buildings = new List<BuildingDto>();
            planet.LastUpdateOreProduction = DateTime.Now.AddHours(-2);
            planet.Status = SatelliteStatus.Colonized;


            var productionPerformer = new ProductionPerformer(
                planet,
                PlanetUpdateSelector.FoodProduction,
                _raceDto,
                new List<TechnologyDto>()
                {
                    _technologyBuildingDto,
                    _technologySocialDto
                },
                DateTime.Now.AddHours(1)
                );

            Assert.IsTrue(productionPerformer.Perform());
            Assert.IsTrue(planet.StoredFood == 25);
        }

        [TestMethod]
        public void ShouldBeHigerhWithBuildings()
        {
            planet.Buildings = new List<BuildingDto>() {_building};
            planet.LastUpdateOreProduction = DateTime.Now.AddHours(-2);
            planet.Status = SatelliteStatus.Colonized;


            var productionPerformer = new ProductionPerformer(
                planet,
                PlanetUpdateSelector.FoodProduction,
                _raceDto,
                new List<TechnologyDto>()
                {
                    _technologyBuildingDto,
                    _technologySocialDto
                },
                DateTime.Now.AddHours(1)
                );

            Assert.IsTrue(productionPerformer.Perform());
            Assert.IsTrue(planet.StoredFood == 38);
        }
    }
}
