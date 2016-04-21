using System;
using System.Collections.Generic;
using BLL.Engine.Planet;
using BLL.Engine.Planet.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class ResearchProductionTest
    {
        RaceDto _raceDto = new RaceDto()
        {
            RaceBonuses = new List<RaceBonusDto>(){new RaceBonusDto()
             {
                 Bonus = RaceTraitsBonuses.Research,
                 Value = 5
             }},
            RaceName = "TestResProductiveRace"
        };

        TechnologyDto _technologyPhisycsDto = new TechnologyDto()
        {
            Name = "techTest",
            Field = "Phisycs",
            TechnologyBonuses = new List<TechnologyBonusDto>()
            {
                new TechnologyBonusDto()
                {
                    Bonus = BonusType.Researchbonus,
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
                    Bonus = BonusType.Researchbonus,
                    Value = 5
                }
            }
        };

        [TestMethod]
        public void ShouldAlwayBeFalse()
        {
            var planet = new PlanetDto()
            {
                ActivePopOnFoodProduction = 0,
                ActivePopOnOreProduction = 0,
                ActivePopOnResProduction = 10,
                ResearchPointProduction = 4,
                StoredOre = 0,
                Buildings = new List<BuildingDto>(),
                LastUpdateOreProduction = DateTime.Now,
                Status = SatelliteStatus.Colonized
            };


            var productionPerformer =
                BLL.Engine.Planet.IstanceFactory.FactoryGenerator.RetrieveProductinPerformer(
                    planet,
                    new RaceDto()
                    {
                        RaceBonuses = new List<RaceBonusDto>(),
                        RaceName = "TestResProductiveRace"
                    },
                    PlanetUpdateSelector.ResearchProduction,
                    new List<TechnologyDto>(),
                    DateTime.Now
                    );

            Assert.IsFalse(productionPerformer.Perform());
            Assert.IsTrue(planet.ResearchPoints == 0);

        }

        [TestMethod]
        public void ShouldAlwayBeFalseBecauseUncolonized()
        {
            var planet = new PlanetDto()
            {
                ActivePopOnFoodProduction = 0,
                ActivePopOnOreProduction = 0,
                ActivePopOnResProduction = 10,
                ResearchPointProduction = 4,
                StoredOre = 0,
                Buildings = new List<BuildingDto>(),
                LastUpdateOreProduction = DateTime.Now,
                Status = SatelliteStatus.Uncolonized
            };


            var productionPerformer =
                BLL.Engine.Planet.IstanceFactory.FactoryGenerator.RetrieveProductinPerformer(
                    planet,
                    new RaceDto()
                    {
                        RaceBonuses = new List<RaceBonusDto>(),
                        RaceName = "TestResProductiveRace"
                    },
                    PlanetUpdateSelector.ResearchProduction,
                    new List<TechnologyDto>(),
                    DateTime.Now
                    );

            Assert.IsFalse(productionPerformer.Perform());
            Assert.IsTrue(planet.ResearchPoints == 0);

        }


        [TestMethod]
        public void ShouldAlwayBeFalseBecauseAbandond()
        {
            var planet = new PlanetDto()
            {
                ActivePopOnFoodProduction = 0,
                ActivePopOnOreProduction = 0,
                ActivePopOnResProduction = 10,
                ResearchPointProduction = 4,
                StoredOre = 0,
                Buildings = new List<BuildingDto>(),
                LastUpdateOreProduction = DateTime.Now,
                Status = SatelliteStatus.Abandoned
            };


            var productionPerformer =
                BLL.Engine.Planet.IstanceFactory.FactoryGenerator.RetrieveProductinPerformer(
                    planet,
                    new RaceDto()
                    {
                        RaceBonuses = new List<RaceBonusDto>(),
                        RaceName = "TestResProductiveRace"
                    },
                    PlanetUpdateSelector.ResearchProduction,
                    new List<TechnologyDto>(),
                    DateTime.Now
                    );

            Assert.IsFalse(productionPerformer.Perform());
            Assert.IsTrue(planet.ResearchPoints == 0);

        }


        [TestMethod]
        public void ShouldAlwayBeFalseBecauseUncolonizble()
        {
            var planet = new PlanetDto()
            {
                ActivePopOnFoodProduction = 0,
                ActivePopOnOreProduction = 0,
                ActivePopOnResProduction = 10,
                ResearchPointProduction = 4,
                StoredOre = 0,
                Buildings = new List<BuildingDto>(),
                LastUpdateOreProduction = DateTime.Now,
                Status = SatelliteStatus.Uncolonizable
            };


            var productionPerformer =
                BLL.Engine.Planet.IstanceFactory.FactoryGenerator.RetrieveProductinPerformer(
                    planet,
                    new RaceDto()
                    {
                        RaceBonuses = new List<RaceBonusDto>(),
                        RaceName = "TestResProductiveRace"
                    },
                    PlanetUpdateSelector.ResearchProduction,
                    new List<TechnologyDto>(),
                    DateTime.Now
                    );

            Assert.IsFalse(productionPerformer.Perform());
            Assert.IsTrue(planet.ResearchPoints == 0);

        }

        [TestMethod]
        public void ShouldBe116WithoutTechAndRace()
        {
            var planet = new PlanetDto()
            {
                ActivePopOnFoodProduction = 0,
                ActivePopOnOreProduction = 0,
                ActivePopOnResProduction = 10,
                ResearchPointProduction = 4,
                StoredOre = 0,
                Buildings = new List<BuildingDto>(),
                LastUpdateOreProduction = DateTime.Now.AddHours(-1),
                Status = SatelliteStatus.Colonized
            };


            var productionPerformer = new ProductionPerformer(
                planet,
                PlanetUpdateSelector.ResearchProduction,
                new RaceDto()
                {
                    RaceBonuses = new List<RaceBonusDto>(),
                    RaceName = "TestResProductiveRace"
                },
                new List<TechnologyDto>(),
                DateTime.Now.AddHours(1)
                );

            Assert.IsTrue(productionPerformer.Perform());
            Assert.IsTrue(planet.ResearchPoints == 56);
        }

        [TestMethod]
        public void ShouldBeHighWithoutTech()
        {
            var planet = new PlanetDto()
            {
                ActivePopOnFoodProduction = 0,
                ActivePopOnOreProduction = 0,
                ActivePopOnResProduction = 10,
                ResearchPointProduction = 4,
                StoredOre = 0,
                Buildings = new List<BuildingDto>(),
                LastUpdateOreProduction = DateTime.Now.AddHours(-1),
                Status = SatelliteStatus.Colonized
            };


            var productionPerformer = new ProductionPerformer(
                planet,
                PlanetUpdateSelector.ResearchProduction,
                _raceDto,
                new List<TechnologyDto>(),
                DateTime.Now.AddHours(1)
                );

            Assert.IsTrue(productionPerformer.Perform());
            Assert.IsTrue(planet.ResearchPoints == 59);
        }

        [TestMethod]
        public void ShouldBeHighWithTechToo()
        {
            var planet = new PlanetDto()
            {
                ActivePopOnFoodProduction = 0,
                ActivePopOnOreProduction = 0,
                ActivePopOnResProduction = 10,
                ResearchPointProduction = 4,
                StoredOre = 0,
                Buildings = new List<BuildingDto>(),
                LastUpdateOreProduction = DateTime.Now.AddHours(-1),
                Status = SatelliteStatus.Colonized
            };


            var productionPerformer = new ProductionPerformer(
                planet,
                PlanetUpdateSelector.ResearchProduction,
                _raceDto,
                new List<TechnologyDto>()
                {
                    _technologyBuildingDto,
                    _technologyPhisycsDto
                },
                DateTime.Now.AddHours(1)
                );

            Assert.IsTrue(productionPerformer.Perform());
            Assert.IsTrue(planet.ResearchPoints == 62);
        }
    }
}
