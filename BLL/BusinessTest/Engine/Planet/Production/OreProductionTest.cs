using System;
using System.Collections.Generic;
using BLL.Engine.Planet;
using BLL.Engine.Planet.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Races.Enums;
using Models.Universe.Enum;
using SharedDto.Universe.Building;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using SharedDto.Universe.Technology;

namespace BusinessTest.Engine.Planet.Production
{
    [TestClass]
    public class OreProductionTest
    {
        RaceDto _raceDto = new RaceDto()
        {
            RaceBonuses = new List<RaceBonusDto>(){new RaceBonusDto()
             {
                 Bonus = RaceTraitsBonuses.OreProduction,
                 Value = 5
             }},
            RaceName = "TestOreProductiveRace"
        };

        [TestMethod]
        public void ShouldAlwayBeFalse()
        {
            var planet = new PlanetDto()
            {
                ActivePopOnFoodProduction = 0,
                ActivePopOnOreProduction = 10,
                ActivePopOnResProduction = 0,
                OreProduction = 4,
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
                        RaceName = "TestOreProductiveRace"
                    },
                    PlanetUpdateSelector.OreProduction,
                    new List<TechnologyDto>(),
                    DateTime.Now
                    );

            Assert.IsFalse(productionPerformer.Perform());
            Assert.IsTrue(planet.StoredOre == 0);

        }

        [TestMethod]
        public void ShouldAlwayBeFalseBecauseUncolonized()
        {
            var planet = new PlanetDto()
            {
                ActivePopOnFoodProduction = 0,
                ActivePopOnOreProduction = 10,
                ActivePopOnResProduction = 0,
                OreProduction = 4,
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
                        RaceName = "TestOreProductiveRace"
                    },
                    PlanetUpdateSelector.OreProduction,
                    new List<TechnologyDto>(),
                    DateTime.Now
                    );

            Assert.IsFalse(productionPerformer.Perform());
            Assert.IsTrue(planet.StoredOre == 0);

        }


        [TestMethod]
        public void ShouldAlwayBeFalseBecauseAbandond()
        {
            var planet = new PlanetDto()
            {
                ActivePopOnFoodProduction = 0,
                ActivePopOnOreProduction = 10,
                ActivePopOnResProduction = 0,
                OreProduction = 4,
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
                        RaceName = "TestOreProductiveRace"
                    },
                    PlanetUpdateSelector.OreProduction,
                    new List<TechnologyDto>(),
                    DateTime.Now
                    );

            Assert.IsFalse(productionPerformer.Perform());
            Assert.IsTrue(planet.StoredOre == 0);

        }


        [TestMethod]
        public void ShouldAlwayBeFalseBecauseUncolonizble()
        {
            var planet = new PlanetDto()
            {
                ActivePopOnFoodProduction = 0,
                ActivePopOnOreProduction = 10,
                ActivePopOnResProduction = 0,
                OreProduction = 4,
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
                        RaceName = "TestOreProductiveRace"
                    },
                    PlanetUpdateSelector.OreProduction,
                    new List<TechnologyDto>(),
                    DateTime.Now
                    );

            Assert.IsFalse(productionPerformer.Perform());
            Assert.IsTrue(planet.StoredOre == 0);

        }

        [TestMethod]
        public void ShouldBe116WithoutTechAndRace()
        {
            var planet = new PlanetDto()
            {
                ActivePopOnFoodProduction = 0,
                ActivePopOnOreProduction = 10,
                ActivePopOnResProduction = 0,
                OreProduction = 4,
                StoredOre = 0,
                Buildings = new List<BuildingDto>(),
                LastUpdateOreProduction = DateTime.Now.AddHours(-1),
                Status = SatelliteStatus.Colonized
            };

            
            var productionPerformer = new ProductionPerformer(
                planet,
                PlanetUpdateSelector.OreProduction,
                new RaceDto()
                {
                    RaceBonuses = new List<RaceBonusDto>(),
                    RaceName = "TestOreProductiveRace"
                }, 
                new List<TechnologyDto>(),
                DateTime.Now.AddHours(1)
                );

            Assert.IsTrue(productionPerformer.Perform());
            Assert.IsTrue(planet.StoredOre==16);
        }

        [TestMethod]
        public void ShouldBeHighWithoutTech()
        {
            var planet = new PlanetDto()
            {
                ActivePopOnFoodProduction = 0,
                ActivePopOnOreProduction = 10,
                ActivePopOnResProduction = 0,
                OreProduction = 4,
                StoredOre = 0,
                Buildings = new List<BuildingDto>(),
                LastUpdateOreProduction = DateTime.Now.AddHours(-1),
                Status = SatelliteStatus.Colonized
            };


            var productionPerformer = new ProductionPerformer(
                planet,
                PlanetUpdateSelector.OreProduction,
                _raceDto,
                new List<TechnologyDto>(),
                DateTime.Now.AddHours(1)
                );

            Assert.IsTrue(productionPerformer.Perform());
            Assert.IsTrue(planet.StoredOre == 17);
        }
    }
}
